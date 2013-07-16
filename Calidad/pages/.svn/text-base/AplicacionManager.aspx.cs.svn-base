using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class pages_AplicacionManager : BasePage //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["usernameCalidad"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx", false);
                }

                cargaFiltros();
                cargaDatos();
            }

            else if (Session["usernameCalidad"] == null)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Sesion").ToString(), Common.MESSAGE_TYPE.Warning);
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    #region seleccionar del grid
    protected void gdvProgramaManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Aplicacion.aspx?idPrograma=" + gdvProgramaManager.DataKeys[gdvProgramaManager.SelectedIndex].Value.ToString());
    }
    #endregion

    #region formatos del grid
    protected void gdvProgramaManager_PreRender(object sender, EventArgs e)
    {
        if (gdvProgramaManager.HeaderRow != null)
            gdvProgramaManager.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    public string GetValor(string valor)
    {
        return valor;
    }
    protected void gdvProgramaManager_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvProgramaManager, ("Select$" + e.Row.RowIndex.ToString()));

                if (!string.IsNullOrEmpty(e.Row.Cells[1].Text))
                {
                    e.Row.Cells[1].Text = GetLocalResourceObject(e.Row.Cells[1].Text).ToString();
                }

                break;
        }


        

    }
    #endregion

    private void cargaFiltros()
    {
        DateTime thisDay = DateTime.Today;
        txtDesde.Text = thisDay.AddDays(-7).ToString("yyyy-MM-dd");
        txtHasta.Text = thisDay.ToString("yyyy-MM-dd");
        DataTable dt = null;
        var parameters = new Dictionary<string, object>();
        try
        {

            parameters.Clear(); 
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NombreProgramaHeader", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlNombre.Items.Add(GetLocalResourceObject("Todos").ToString());
                ddlNombre.DataSource = dt;
                ddlNombre.DataBind();
            }
            else
            {
                ddlNombre.Items.Clear(); 
                ddlNombre.Items.Add(GetLocalResourceObject("Todos").ToString());                
            }

            parameters.Clear();
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderosTodos", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlInvernadero.Items.Add(GetLocalResourceObject("Todos").ToString());
                ddlInvernadero.DataSource = dt;
                ddlInvernadero.DataBind();
            }
            parameters.Clear();
            parameters.Add("@idUser", String.IsNullOrEmpty (Session["userIDCalidad"].ToString()) ? "0" :  Session["userIDCalidad"].ToString() );
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlPlantas", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlPlanta.Items.Add(GetLocalResourceObject("Todas").ToString());
                ddlPlanta.DataSource = dt;
                ddlPlanta.DataBind();
            }

            
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + ex.Message, Common.MESSAGE_TYPE.Error);
        }


        ListItem L = new ListItem(GetLocalResourceObject("Todos").ToString(), "-- Todos --");
        ddlEstatus.Items.Add(L);
        L = new ListItem(GetLocalResourceObject("Abierto_").ToString(), "Abierto");
        ddlEstatus.Items.Add(L);//("Abierto");
        L = new ListItem(GetLocalResourceObject("Cancelado_").ToString(), "Cancelado");
        ddlEstatus.Items.Add(L);
        L = new ListItem(GetLocalResourceObject("Cancelo Almacen_").ToString(), "Cancelo Almacen");
        ddlEstatus.Items.Add(L);
        L = new ListItem(GetLocalResourceObject("Ejecutado_").ToString(), "Ejecutado");
        ddlEstatus.Items.Add(L);
        L = new ListItem(GetLocalResourceObject("Entregado_").ToString(), "Entregado");
        ddlEstatus.Items.Add(L);
        L = new ListItem(GetLocalResourceObject("Entrega Parcial_").ToString(), "Entrega Parcial");
        ddlEstatus.Items.Add(L);
        L = new ListItem(GetLocalResourceObject("Pendiente_").ToString(), "Pendiente");
        ddlEstatus.Items.Add(L);

        ddlEstatus.DataBind();
    }

    private void cargaDatos()
    {
        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());

        if(ddlNombre.SelectedIndex != 0)
            parameters.Add("@nombre", ddlNombre.SelectedItem.ToString());
        if (ddlPlanta.SelectedIndex != 0) 
            parameters.Add("@planta", ddlPlanta.SelectedValue);
        if (ddlInvernadero.SelectedIndex != 0) 
            parameters.Add("@invernadero", ddlInvernadero.SelectedValue);
        if (ddlEstatus.SelectedIndex != 0)
            parameters.Add("@estatus", ddlEstatus.SelectedValue.ToString());
        if (!String.IsNullOrEmpty(txtDesde.Text.Trim()))
            parameters.Add("@desde", txtDesde.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectFechas").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }
        if (!String.IsNullOrEmpty(txtHasta.Text.Trim()))
            parameters.Add("@hasta", txtHasta.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectFechas").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }
        if (DateTime.Parse(txtDesde.Text.Trim()).CompareTo(DateTime.Parse(txtHasta.Text.Trim())) == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("FechaMayor").ToString(), Common.MESSAGE_TYPE.Error);
        }

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ProgramaHeader", parameters, this.Session["connection"].ToString());
            gdvProgramaManager.DataSource = dt;
            gdvProgramaManager.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorData").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Aplicacion.aspx?idPrograma=0");
    }

    protected void imgCancelarNominee_Click(object sender, EventArgs e)
    {
        Session["row"] = null;
        ImageButton imgButton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)imgButton.NamingContainer;
        Session["row"] = row;

        //mdlPopupMessageGralControl.Show();  
        seleccionarRazonCancelar();

    }
    
    protected void save_Click(object sender, EventArgs e)
    {
              
    }

    private void seleccionarRazonCancelar() 
    {
        //sacar las razones que pertenecen a la planta de ese programa
        GridViewRow row = (GridViewRow)Session["row"];
        if (row.RowIndex >= 0)
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@idPrograma", gdvProgramaManager.DataKeys[row.RowIndex].Value.ToString());
            
            try
            {
                var ds = DataAccess.executeStoreProcedureDataSet("dbo.spr_GET_RazonesXPlantaXPrograma", parameters, this.Session["connection"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlRazones.DataSource = ds;
                    ddlRazones.DataBind();
                    popUpRazones.Show();
                }
                else
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Norasones").ToString(), Common.MESSAGE_TYPE.Warning);
                }
            }
            catch (Exception ex)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("error").ToString(), Common.MESSAGE_TYPE.Warning);
                Log.Error(ex);
            }            
        }
        
    }

    protected void save2_OnClick(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        GridViewRow row = (GridViewRow)Session["row"];
        if (row.RowIndex >= 0)
        {
           

            var parameters = new Dictionary<string, object>();
            parameters.Add("@idPrograma", gdvProgramaManager.DataKeys[row.RowIndex].Value.ToString());
            parameters.Add("@user", Session["usernameCalidad"].ToString());

            try
            {
                int guardo = DataAccess.executeStoreProcedureGetInt("spr_CANCEL_ProgramaHeader", parameters, this.Session["connection"].ToString());
                if (guardo == 1)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaCancelado").ToString(), Common.MESSAGE_TYPE.Success);
                    
                    //guardar el log de cancelaciones
                    parameters.Add("@idRazon", ddlRazones.SelectedValue);
                    parameters.Add("@bcancel", 0); //0 = cancelado
                    DataAccess.executeStoreProcedureNonQuery("spr_INSERT_LogCancelacionesModificaciones", parameters, this.Session["connection"].ToString());

                    cargaDatos();
                }
                else if (guardo == 2)
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaEnUso").ToString(), Common.MESSAGE_TYPE.Warning);
                else if (guardo == 3)
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaGuadrado").ToString(), Common.MESSAGE_TYPE.Warning);
            }
            catch (Exception ex)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("error").ToString(), Common.MESSAGE_TYPE.Warning);
                Log.Error(ex);
            }

        }
        popUpRazones.Hide();
    }
    
    protected void copyAplication(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)imgButton.NamingContainer;
        
        if(row.RowIndex >= 0){
            Response.Redirect("frmCopiarPrograma.aspx?idCopy=" + gdvProgramaManager.DataKeys[row.RowIndex].Value.ToString() );
        }
    }

    
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        cargaDatos();
    }
}