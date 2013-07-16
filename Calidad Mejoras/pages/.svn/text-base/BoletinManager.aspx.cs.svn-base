using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class pages_BoletinManager : BasePage //System.Web.UI.Page
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

                this.cargaddlPlantas();
            }

            else if (Session["usernameCalidad"] == null)
            {
                //popUpMessageControl1.setAndShowInfoMessage("Su sesión ha expirado. Por favor, refresque la página", Common.MESSAGE_TYPE.Warning);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SesionExpirada").ToString(), Common.MESSAGE_TYPE.Warning);
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void cargaDatos() 
    {
        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue);
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_Boletin", parameters, this.Session["connection"].ToString());
            gdvBoletinManager.DataSource = dt;
            gdvBoletinManager.DataBind();
        }catch(Exception ex)
        {
            Log.Error(ex); 
            //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de carga datos ", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }
    private void cargaddlPlantas()
    {
        ddlPlanta.Items.Clear();
        var parameters = new Dictionary<string, object>();
        try
        {
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet dt = DataAccess.executeStoreProcedureDataSet("dbo.spr_GET_ddlPlantas", parameters, this.Session["connection"].ToString());
            ddlPlanta.DataSource = dt;
            ddlPlanta.DataValueField = "campoId";
            ddlPlanta.DataTextField = "campoNombre";
            ddlPlanta.DataBind();
            ddlPlanta.Items.Insert(0, GetLocalResourceObject("Seleccione").ToString());
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar las plantas", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaPlantasError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    #region seleccionar del grid    
    protected void gdvBoletinManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("Boletin.aspx?idBoletin=" + gdvBoletinManager.DataKeys[gdvBoletinManager.SelectedIndex].Value.ToString()+"&idPlanta="+ddlPlanta.SelectedValue);            
    }
    #endregion

    #region formatos del grid
    protected void gdvBoletinManager_PreRender(object sender, EventArgs e)
    {
        if (gdvBoletinManager.HeaderRow != null)
            gdvBoletinManager.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvBoletinManager_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvBoletinManager, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    #endregion
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
       Response.Redirect("Boletin.aspx?idBoletin=0&idPlanta=0");   
    }



    protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPlanta.SelectedIndex != 0)
            cargaDatos();
    }
}