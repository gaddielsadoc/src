using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class pages_ReporteCancelaciones : BasePage
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

    private void cargaFiltros()
    {
        DateTime thisDay = DateTime.Today;
        txtDesde.Text = thisDay.AddDays(-7).ToString("yyyy-MM-dd");
        txtHasta.Text = thisDay.ToString("yyyy-MM-dd");
        DataTable dt = null;

        DataTable dtUserInfo = (DataTable)Session["dtUserInfoCalidad"];
        int roleId = dtUserInfo.Rows[0]["roleIds"] != DBNull.Value ? (int)dtUserInfo.Rows[0]["roleIds"] : -1;

        var parameters = new Dictionary<string, object>();
        try
        {
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlPlantas", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (roleId == 1)
                    ddlPlanta.Items.Add(GetLocalResourceObject("Todas").ToString());
                ddlPlanta.DataSource = dt;
                ddlPlanta.DataBind();
            }

            parameters.Clear();
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderosTodos", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlInvernadero.Items.Add(GetLocalResourceObject("Todos").ToString());
                ddlInvernadero.DataSource = dt;
                ddlInvernadero.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }

        ddlEstatus.Items.Clear();
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Todos").ToString(), "-- Todos --"));
        //ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Abierto").ToString(), "Abierto"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Cancelado").ToString(), "Cancelado"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Modificado").ToString(), "Modificado"));
        //ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Ejecutado").ToString(), "Ejecutado"));
        //ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Entregado").ToString(), "Entregado"));
        //ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Pendiente").ToString(), "Pendiente"));
    }

     [WebMethod]
    public static void lnkViejoP_Click2(string idE, string session)
    {
        //if (((LinkButton)sender).Attributes["este"] != null)
        //{
        //    LinkButton lnk1 = (LinkButton)sender;
        //    return ((LinkButton)sender).Attributes["este"].ToString();
        //}
        DataTable dt = null;
        if (!String.IsNullOrEmpty(idE))
        {
            //traer datos
            //cargar grid
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idPrograma", idE);
            
            int x = 0;
            try
            {
                dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ProgramaChild", parameters, session );
                if (dt.Rows.Count > 0)
                {

                }
            }
            catch (Exception e) { }

        }//if (!String.IsNullOrEmpty(idE))

        //return dt;
    }

   
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        var parameters = new Dictionary<string, object>();
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
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("AmbasFechas").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }
        if (!String.IsNullOrEmpty(txtHasta.Text.Trim()))
            parameters.Add("@hasta", txtHasta.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("AmbasFechas").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }

        if (DateTime.Parse(txtDesde.Text.Trim()).CompareTo(DateTime.Parse(txtHasta.Text.Trim())) == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("FechaIncoherente").ToString(), Common.MESSAGE_TYPE.Error);
        }

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ReporteCancelaciones", parameters, this.Session["connection"].ToString());


            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    #region gridview

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("@", "<br />");
            //e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("@", "<br />");
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    #endregion
    
}