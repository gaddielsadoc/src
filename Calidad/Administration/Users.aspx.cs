using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Net.Mail;
using System.Web.UI.WebControls;


public partial class Administration_Users : BasePage
{
    #region Atributos
    #endregion

    #region Eventos de pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack) return;
            obtieneUsuarios(true);
            obtienePlantas();
            obtieneRoles();
            obtieneMateriales();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }        
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            limpiaCampos();
        }   
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }        
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            string mensaje = "";

            //Valida correo
            try
            {
                if (!string.IsNullOrEmpty(ltEmail.Text))
                {
                    new MailAddress(ltEmail.Text);
                }                
            }
            catch (FormatException)
            {
               
                mensaje += (string)GetLocalResourceObject("CorreoMal");
            }  

            //Valida campos obligatorios...
            if ("" == hdIdItem.Value)
            {
                if ((null == txtCuenta.Text) || ("" == txtCuenta.Text))
                {
                    mensaje += (string)GetLocalResourceObject("CuentaRequerido");
                }

                /*if ((null == txtPassword.Text) || ("" == txtPassword.Text))
                {
                    mensaje += "El campo Contraseña, es requerido <br/>";
                }
            
                if ((null == txtNombre.Text) || ("" == txtNombre.Text))
                {
                    mensaje += "El campo Nombre, es requerido <br/>";
                }
                 * */
            }

            if (ddlTipo.SelectedIndex <= 0)
            {
                mensaje += (string)GetLocalResourceObject("TipoRequerido");
            }

            string plantaList = string.Empty;
            foreach (ListItem item in ddlPlanta.Items)
            {
                if (item.Selected)
                {
                    plantaList += item.Value + "|";
                }
            }

            //if ((string.IsNullOrEmpty(plantaList)) && ("RECEPCION" != ddlTipo.SelectedItem.Text))
            //{
            //    mensaje += "El campo Planta, es requerido para usuarios de PLANTA o APOYO <br/>";
            //}

            if ("" == hdIdItem.Value)
            {
                //Valida no existencia de otro usuario igual...
                if (this.existeUsuario(true, txtCuenta.Text))
                {
                    mensaje += (string)GetLocalResourceObject("existeUsuario");
                }
                bool autenticated = false;
                //Valida que exista en el LDAP
#if DEBUG
                if (txtCuenta.Text.Trim().CompareTo("recepcion") == 0)
                {
                    autenticated = true;
                }
                else if (txtCuenta.Text.Trim().CompareTo("planta") == 0)
                {
                    autenticated = true;
                }
                else if (txtCuenta.Text.Trim().CompareTo("planta2") == 0)
                {
                    autenticated = true;
                }
                else if (txtCuenta.Text.Trim().CompareTo("apoyo") == 0)
                {
                    autenticated = true;
                }
                else if (txtCuenta.Text.Trim().CompareTo("apoyo2") == 0)
                {
                    autenticated = true;
                }
                else if (txtCuenta.Text.Trim().CompareTo("apoyo3") == 0)
                {
                    autenticated = true;
                }else if (txtCuenta.Text.Trim().CompareTo("apoyo4") == 0)
                {
                    autenticated = true;
                }
#endif

                if (!autenticated)
                    autenticated = DGActiveDirectory.userExistOnActiveDirectory(txtCuenta.Text.Trim());
                //autenticated = true;

                if (!autenticated)
                {
                    mensaje += (string)GetLocalResourceObject("NoexisteUsuario") ;
                }

            }


            //Muestra PopUp con mensaje(s), si hubo...
            if (mensaje.Trim().Length > 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(mensaje, Common.MESSAGE_TYPE.Error);
                mensaje = "";
                return;
            }
            else
            {
                if ("" == hdIdItem.Value)
                {
                    // Es INSERT
                    this.guardaUsuario(ltNombreUsuario.Text, txtCuenta.Text, ltEmail.Text, ddlTipo.SelectedValue, plantaList, checkActivo.Checked);
                }
                else
                {
                    // Es UPDATE
                    int id = 0;
                    Int32.TryParse(hdIdItem.Value.ToString(), out id);
                    this.actualizaUsuario(id, ltEmail.Text, ddlTipo.SelectedValue, plantaList, checkActivo.Checked);
                }
            }

            this.limpiaCampos();
            Response.Redirect(this.Page.Request.Path);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    protected void grViewPendings_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (null != ViewState["dsUsers"])
            {
                DataSet ds = ViewState["dsUsers"] as DataSet;

                if (ds != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(ds.CreateDataReader());
                    DataView dataView = new DataView(dataTable);
                    dataView.Sort = e.SortExpression + " " + Common.ConvertSortDirectionToSql(e.SortDirection);

                    grViewPendings.DataSource = dataView;
                    grViewPendings.DataBind();
                }
            }            
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    protected void grViewPendings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(grViewPendings, ("Select$" + e.Row.RowIndex.ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    protected void grViewPendings_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem item in ddlPlanta.Items)
            {
                item.Selected = false;
            }
            int id = -1;
            if (null != this.grViewPendings.SelectedPersistedDataKey)
            {
                Int32.TryParse(this.grViewPendings.SelectedPersistedDataKey["idUsuario"].ToString(), out id);
            }
            else
            {
                Int32.TryParse(this.grViewPendings.SelectedDataKey["idUsuario"].ToString(), out id);
            }

            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            //parameters.Add("@isAdmin", true);
            parameters.Add("@id", id);

            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_ObtieneUsuarios", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                hdIdItem.Value = dt.Rows[0]["idUsuario"].ToString().Trim();

                ltNombreUsuario.Text = dt.Rows[0]["nombre"].ToString().Trim();

                ltEmail.Text = dt.Rows[0]["email"].ToString().Trim();
                /*txtNombre.Enabled = false;*/

                txtCuenta.Text = dt.Rows[0]["usuario"].ToString().Trim();
                txtCuenta.Enabled = false;

                //txtPassword.Visible = false;

                //ddlTipo.SelectedValue = dt.Rows[0]["Tipo_Usr"].ToString().Trim();
                string rol = dt.Rows[0]["Tipo_Usr"].ToString().Trim();
                foreach (ListItem item in ddlTipo.Items)
                {
                    if (item.Text == rol)
                    {
                        ddlTipo.SelectedValue = item.Value;
                    }
                }

                //ddlPlanta.SelectedValue = dt.Rows[0]["ID_PLANTA"].ToString().Trim();
                string[] plantas = dt.Rows[0]["plantaList"].ToString().Trim().Split(',');
                foreach (string item in plantas)
                {
                    ListItem listItem = ddlPlanta.Items.FindByValue(item);
                    if (null != listItem)
                    {
                        listItem.Selected = true;
                    }
                }
                grViewPendings.Enabled = false;

                checkActivo.Checked = (bool)dt.Rows[0]["activo"];
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
        finally
        {
            Page_Load(null, null);
        }
    }
    protected void grViewPendings_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            int id = -1;
            if (null != e.Keys["idUsuario"])
            {
                Int32.TryParse(e.Keys["idUsuario"].ToString(), out id);
            }
            else
            {
                Int32.TryParse(this.grViewPendings.SelectedDataKey["idUsuario"].ToString(), out id);
            }

            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@id", id);

            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_BorraUsuarioAdmin", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("borrado"),dt.Rows[0]["idUsuario"].ToString()), Common.MESSAGE_TYPE.Error);
            }
        }

        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
        finally
        {
            this.limpiaCampos();
            Response.Redirect(this.Page.Request.Path);
            //Page_Load(null, null);
        }

    }
    protected void txtCuenta_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ltNombreUsuario.Text = string.Empty;
#if DEBUG
            if (txtCuenta.Text.Trim().CompareTo("recepcion") == 0)
            {
                ltNombreUsuario.Text = "Usuario recepcion";
                return;
            }else if(txtCuenta.Text.Trim().CompareTo("planta") == 0)
            {
                ltNombreUsuario.Text = "Usuario planta";
                return;
            }else if(txtCuenta.Text.Trim().CompareTo("planta2") == 0)
            {
                ltNombreUsuario.Text = "Usuario planta 2";
                return;
            }else if(txtCuenta.Text.Trim().CompareTo("apoyo") == 0)
            {
                ltNombreUsuario.Text = "Usuario apoyo";
                return;
            }else if(txtCuenta.Text.Trim().CompareTo("apoyo2") == 0)
            {
                ltNombreUsuario.Text = "Usuario apoyo 2";
                return;
            }else if(txtCuenta.Text.Trim().CompareTo("apoyo3") == 0)
            {
                ltNombreUsuario.Text = "Usuario apoyo 3";
                return;
            }else if(txtCuenta.Text.Trim().CompareTo("apoyo4") == 0)
            {
                ltNombreUsuario.Text = "Usuario apoyo 4";
                return;
            }             
#endif

            DataTable tabla = DGActiveDirectory.getGeneralInfoBothDomains(txtCuenta.Text.Trim());
                if (null != tabla && tabla.Rows.Count == 1)
                {
                    string nombre = string.Empty;
                    nombre += tabla.Rows[0]["FirstName"];
                    nombre += " ";
                    nombre += tabla.Rows[0]["LastName"];
                    ltNombreUsuario.Text = nombre;
                    ltEmail.Text = tabla.Rows[0]["Email"] as string;
                }
                else
                {
                    ltNombreUsuario.Text = string.Empty;
                    popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("NoexisteUsuario"),
                                                               Common.MESSAGE_TYPE.Error);
                }
            
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("ErrorAttempt"), Common.MESSAGE_TYPE.Warning);
        }
    }
    protected void grViewPendings_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsUsers"])
            {
                DataSet ds = ViewState["dsUsers"] as DataSet;

                if (ds != null)
                {
                    grViewPendings.DataSource = ds;
                    grViewPendings.DataBind();
                }
            }
            ((GridView)sender).PageIndex = e.NewPageIndex;
            ((GridView)sender).DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    #endregion
    
    #region Metodos Auxiliares
    public void obtieneUsuarios(Boolean isAdmin)
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        //parameters.Add("@isAdmin", isAdmin);

        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_ObtieneUsuarios", parameters, this.Session["connection"].ToString());

        ViewState["dsUsers"] = ds;
        grViewPendings.DataSource = ds;
        grViewPendings.DataBind();
    }
    public void limpiaCampos()
    {
        ltEmail.Text = string.Empty;
        ltNombreUsuario.Text = string.Empty;
        txtCuenta.Text = string.Empty;
        //txtPassword.Text = string.Empty;
        hdIdItem.Value = null;
        ddlTipo.SelectedIndex = -1;
        grViewPendings.SelectedIndex = -1;
        grViewPendings.Enabled = true;
        txtCuenta.Enabled = true;
        checkActivo.Checked = true;
        foreach (ListItem item in ddlPlanta.Items)
        {
            item.Selected = false;
        }
    }
    public Boolean existeUsuario(Boolean isAdmin, String cuenta)
    {
        Boolean exists = false;

        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            //parameters.Add("@isAdmin", isAdmin);
            parameters.Add("@cuenta", cuenta);

            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_ExisteUsuario", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                exists = true;
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("noValid"), Common.MESSAGE_TYPE.Error);
        }

        return exists;
    }
    protected void guardaUsuario(String nombre, String cuenta, String email, String tipo, String plantaList, bool activo)
    {
        try
        {
            //Error free, hace el insert...
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@nombre", nombre);
            parameters.Add("@cuenta", cuenta);
            parameters.Add("@rolesList", tipo);
            parameters.Add("@plantaList", plantaList);
            parameters.Add("@activo", activo);
            parameters.Add("@email", email);

            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_GuardaUsuarioAdmin", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("saveIt"),dt.Rows[0]["idUsuario"].ToString(),cuenta) , Common.MESSAGE_TYPE.Error);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("noValid"), Common.MESSAGE_TYPE.Error);
        }
    }
    protected void actualizaUsuario(int id, String email, String tipo, String plantaList, bool activo)
    {
        try
        {
            //Error free, hace el insert...
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@rolesList", tipo);
            parameters.Add("@plantaList", plantaList);
            parameters.Add("@activo", activo);
            parameters.Add("@email", email);

            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_ActualizaUsuarioAdmin", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("update"), dt.Rows[0]["idUsuario"].ToString()), Common.MESSAGE_TYPE.Error);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("noUpdate"), Common.MESSAGE_TYPE.Error);
        }

    }
/*
    private bool isOnActiveDirectory(string userName, string password)
    {
        string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];

        string errorSpeech = string.Empty;
        //DataTable dt = getUserLogInformation(userName, Security.Encrypt(password),"");
        //DataTable dt = DGActiveDirectory.getGeneralInfoAll(userName, GDLDomain);
        //IF EXITS ON USER LOG
        try
        {
            errorSpeech = this.IsAuthenticated("LDAP://" + GDLDomain, GDLDomain, userName, password);
            if (errorSpeech == "")
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            throw new Exception(ex.Message);
        }
    }
*/
/*
    private string IsAuthenticated(string _path, string domain, string username, string pwd)
    {
        string domainAndUsername = domain + @"\" + username;
        string errorSpeech = lablesXML.getNameSpanish("errorSpeech");
        DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain), domainAndUsername, pwd);

        try
        {
            //Bind to the native AdsObject to force authentication.
            //object obj = entry.NativeObject;

            DirectorySearcher search = new DirectorySearcher(entry);

            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();

            if (null == result)
            {
                return errorSpeech;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return errorSpeech;
        }

        return "";
    }
*/
    private void obtienePlantas()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlPlantasTodas", parameters, this.Session["connection"].ToString());
        ddlPlanta.DataSource = ds;
        ddlPlanta.DataTextField = "campoNombre";
        ddlPlanta.DataValueField = "campoId";
        ddlPlanta.DataBind();
    }
    private void obtieneRoles()
    {

        var parameters = new Dictionary<string, object>();
        var ds = DataAccess.executeStoreProcedureDataSet("spr_ObtieneRoles", parameters, this.Session["connection"].ToString());
        ddlTipo.Items.Add(new ListItem("--Seleccione--", string.Empty));
        ddlTipo.DataSource = ds;
        ddlTipo.DataTextField = "rol";
        ddlTipo.DataValueField = "idRol";
        ddlTipo.DataBind();
    }
    private void obtieneMateriales()
    {
        //var parameters = new Dictionary<string, object> {{"@activo", true}};
        //var ds = DataAccess.executeStoreProcedureDataSet("spr_ObtieneMaterials", parameters);
        //chlMateriales.DataSource = ds;
        //chlMateriales.DataTextField = "clave";
        //chlMateriales.DataValueField = "idMaterial";
        //chlMateriales.DataBind();
    }

    #endregion


    protected void grViewPendings_PreRender(object sender, EventArgs e)
    {
        if (grViewPendings.HeaderRow != null)
            grViewPendings.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}
