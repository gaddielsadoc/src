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
                popUpMessageControl1.setAndShowInfoMessage("Su sesión ha expirado. Por favor, refresque la página", Common.MESSAGE_TYPE.Warning);
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

    protected void gdvProgramaManager_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvProgramaManager, ("Select$" + e.Row.RowIndex.ToString()));
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
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NombreProgramaHeader", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlNombre.Items.Add("--Todos--");
                ddlNombre.DataSource = dt;
                ddlNombre.DataBind();
            }

            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderosTodos", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlInvernadero.Items.Add("--Todos--");
                ddlInvernadero.DataSource = dt;
                ddlInvernadero.DataBind();
            }
            parameters.Add("@idUser", String.IsNullOrEmpty (Session["userIDCalidad"].ToString()) ? "0" :  Session["userIDCalidad"].ToString() );
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlPlantas", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlPlanta.Items.Add("--Todas--");
                ddlPlanta.DataSource = dt;
                ddlPlanta.DataBind();
            }

            
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + ex.Message, Common.MESSAGE_TYPE.Error);
        }
        ddlEstatus.Items.Add("--Todos--");
        ddlEstatus.Items.Add("Abierto");
        ddlEstatus.Items.Add("Cancelado");
        ddlEstatus.Items.Add("Ejecutado");
        ddlEstatus.Items.Add("Entregado");
        ddlEstatus.Items.Add("Pendiente");
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
            parameters.Add("@estatus",ddlEstatus.SelectedItem.ToString());
        if (!String.IsNullOrEmpty(txtDesde.Text.Trim()))
            parameters.Add("@desde", txtDesde.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage("Seleccione ambas fechas ", Common.MESSAGE_TYPE.Error);
            return;
        }
        if (!String.IsNullOrEmpty(txtHasta.Text.Trim()))
            parameters.Add("@hasta", txtHasta.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage("Seleccione ambas fechas ", Common.MESSAGE_TYPE.Error);
            return;
        }
        if (DateTime.Parse(txtDesde.Text.Trim()).CompareTo(DateTime.Parse(txtHasta.Text.Trim())) == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage("La fecha 'desde' no puede ser mayor que 'hasta'", Common.MESSAGE_TYPE.Error);
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
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + ex.Message, Common.MESSAGE_TYPE.Error);
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

        mdlPopupMessageGralControl.Show();  

    }


    protected void save_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        //ImageButton imgButton = (ImageButton)sender;
        //GridViewRow row = (GridViewRow)imgButton.NamingContainer;

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
                    popUpMessageControl1.setAndShowInfoMessage("Programa cancelado exitosamente.", Common.MESSAGE_TYPE.Success);
                else if (guardo == 2)
                    popUpMessageControl1.setAndShowInfoMessage("El programa está siendo utilizado por alguien más. Asegúrese de que nadie lo está usando antes de cancelarlo.", Common.MESSAGE_TYPE.Warning);
                else if (guardo == 3)
                    popUpMessageControl1.setAndShowInfoMessage("El programa ya fue guardado con datos de pesaje. Imposible cancelarlo.", Common.MESSAGE_TYPE.Warning);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            cargaDatos();

        }   

        
        
    }
    
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        cargaDatos();
    }
}