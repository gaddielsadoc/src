using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public partial class catalog_frmTratamientos : BasePage
{
    private string _XMLRootFolder = ConfigurationSettings.AppSettings["XMLFolder"];    

    #region Eventos Pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneTratamientos();
                cargaddlQuimicos();
                obtienePlantas();
            }
            else
            {
                //para volver a poner los quimicos y no se pierdan al recargal la pagina
                QuimTmp.Value =  ViewState["QuimTmp"].ToString();             
            }
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }

    private void obtieneTratamientos()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_Tratamiento", parameters, this.Session["connection"].ToString());   
        gvTratamiento.DataSource = ds;
        gvTratamiento.DataBind();
    }

    private void obtienePlantas()
    {
        ddlPlanta.Items.Clear();
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllSites", parameters, this.Session["connection"].ToString());
            ddlPlanta.DataSource = ds;
            ddlPlanta.DataValueField = "Farm";
            ddlPlanta.DataTextField = "Name";
            ddlPlanta.DataBind();
            //ddlPlanta.Items.Insert(0, new ListItem("-Seleccione-", "-1"));
            ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
            ddlPlanta.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaPlantasError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlQuimicos()
    {
        var parameters = new Dictionary<string, object>();
        ddlFiltro.Items.Clear();
       ddlFiltro.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlQuimicos", parameters, this.Session["connection"].ToString());
            ddlFiltro.DataValueField = "ITEMNMBR";
            ddlFiltro.DataTextField = "ITEMDESC";
            ddlFiltro.DataSource = dt2;
            ddlFiltro.DataBind();

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Error").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void VolverAlPanelInicial()
    {
        hdnIdTratamiento.Value = "0";
        ViewState["QuimTmp"] = QuimTmp.Value = string.Empty;        
        txtNombre.Text = string.Empty;
        chkActivo.Checked = true;
        gvTratamiento.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
        ddlFiltro.SelectedValue = "-1";
        ddlPlanta.SelectedIndex = 0;
    }
    

    protected void btnSave_Click(object sender, EventArgs e)
    {        
        if (txtNombre.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("requerido").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }
        if (ddlPlanta.SelectedIndex == 0 )
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("requeridoPlanta").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }
        
        else
        {
            string qumicos = string.Empty;
            qumicos = Request.Form[ddlFiltro.UniqueID];
            qumicos = qumicos.Replace(',', '|');
            //se eliminan los campos vacios
            qumicos = qumicos.Replace("-1|", "");
            qumicos = qumicos.Replace("-1", "");

            if (String.IsNullOrEmpty(qumicos.Trim()))
            { 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SeleccioneUno").ToString(), Common.MESSAGE_TYPE.Error);
                return;
            }
          
            string[] split = qumicos.Split('|');



            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
            xmlString.AppendFormat("<{0}>", "Quimicos");
            foreach (string s in split)
            {
                xmlString.AppendFormat("<c><id>{0}</id></c>", s);             
            }
            xmlString.AppendFormat("</{0}>", "Quimicos");
           
            var parameters = new Dictionary<string, object>();           
            parameters.Add("@idTratamiento", hdnIdTratamiento.Value);
            parameters.Add("@nombre", txtNombre.Text);
            parameters.Add("@planta", ddlPlanta.SelectedValue);
            parameters.Add("@Quimicos", xmlString.ToString());
            if (chkActivo.Checked)
                parameters.Add("@activo", 1);
            else
                parameters.Add("@activo", 0);
            try
            {
                // 0 = nombre repetido
                float permiso = DataAccess.executeStoreProcedureFloat("spr_INSERT_Tratamientos", parameters, this.Session["connection"].ToString());
                if (permiso == -1)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Exist").ToString(), Common.MESSAGE_TYPE.Warning);
                    txtNombre.Text = String.Empty;
                }
                else if (permiso == 0)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SaveIt").ToString(), Common.MESSAGE_TYPE.Success);
                    obtieneTratamientos();
                    VolverAlPanelInicial();
                }
                else //cualquier otro valor, nos indica el id del tratamiento que tiene los quimicos enviados
                {
                    parameters.Clear();
                    parameters.Add("@idTratamiento", permiso);
                    var dt2 = DataAccess.executeStoreProcedureDataTable("Spr_get_InfoTratamientoRepetido", parameters, this.Session["connection"].ToString());
                    string error = string.Format((GetLocalResourceObject("ExistQuimicos").ToString()), dt2.Rows[0]["nomPlanta"].ToString(), dt2.Rows[0]["vNombre"].ToString());
                    popUpMessageControl1.setAndShowInfoMessage(error, Common.MESSAGE_TYPE.Warning);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Error2").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
            }
            

        }
       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            VolverAlPanelInicial();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }

    }

    protected void gvTratamiento_PreRender(object sender, EventArgs e)
    {
        if (gvTratamiento.HeaderRow != null)
            gvTratamiento.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvTratamiento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTratamiento, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    protected void gvTratamiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            hdnIdTratamiento.Value = gvTratamiento.DataKeys[gvTratamiento.SelectedIndex].Value.ToString();
            parameters.Add("@idTratamiento", hdnIdTratamiento.Value.ToString() );
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_GET_Tratamiento", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtNombre.Text = dt.Rows[0]["vNombre"].ToString().Trim();
                ddlPlanta.SelectedValue = dt.Rows[0]["idPlanta"].ToString().Trim();
                if (dt.Rows[0]["bActivo"].ToString().Equals("True"))
                    chkActivo.Checked = true;
                else
                    chkActivo.Checked = false;               
                btnActualizar.Visible = true;
                btnCancel.Visible = true;
                btnLimpiar.Visible = false;
                btnSave.Visible = false;



                 //obtiene las id de plagas del quimico seleccionado y las guardo en plagasTmp para que el jquery las acomode
                DataTable dtQuim = DataAccess.executeStoreProcedureDataTable("spr_SelectQuimicoByTratamientoId", parameters, this.Session["connection"].ToString());
                QuimTmp.Value = string.Empty;
                if (dtQuim.Rows.Count > 0)
                {
                    foreach (DataRow item in dtQuim.Rows)
                    {
                        if (dtQuim.Rows.IndexOf(item) == dtQuim.Rows.Count - 1)
                        {
                            QuimTmp.Value += item[0].ToString();
                        }
                        else
                        {
                            QuimTmp.Value += item[0].ToString() + "|";
                        }

                    }

                    ViewState["QuimTmp"] = QuimTmp.Value;
                }
                else
                    ddlFiltro.SelectedValue = "-1";

            }
            else
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NoEncontrado").ToString(), Common.MESSAGE_TYPE.Error);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvTratamiento.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new System.Web.UI.PostBackOptions(gvTratamiento, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    #endregion

    

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
        // No code required here.
    }
}