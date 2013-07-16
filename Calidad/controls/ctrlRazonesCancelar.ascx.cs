using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;



public partial class controls_ctrlRazonesCancelar : System.Web.UI.UserControl
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BasePage));

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

   
    public void showPopup(int idPrograma)
    {
        //sacar las razones que pertenecen a la planta de ese programa
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@idPrograma", idPrograma);
        ViewState["_idPrograma"] = idPrograma;
        try
        {
            var ds = DataAccess.executeStoreProcedureDataSet("dbo.spr_GET_RazonesXPlantaXPrograma", parameters, this.Session["connection"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRazones.DataSource = ds;
                ddlRazones.DataBind();
                mdlPopupMessageGralControl.Show();
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

    protected void save_OnClick(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", ViewState["_idPrograma"].ToString() );
        parameters.Add("@user", Session["usernameCalidad"].ToString());

        try
        {
            int guardo = DataAccess.executeStoreProcedureGetInt("spr_CANCEL_ProgramaHeader", parameters, this.Session["connection"].ToString());
            if (guardo == 1)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaCancelado").ToString(), Common.MESSAGE_TYPE.Success);

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

    protected void cancelar2_OnClick(object sender, EventArgs e)
    {
        ViewState["_idPrograma"] = 0;
    }
}