using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    /*protected void Page_Load(object sender, EventArgs e)
    {
        //if (null != Session["Locale"])
        //{
        //    ddlLocale.SelectedValue = (string)Session["Locale"];
        //}

        if (Session["usernameCalidad"] != null)
        {
            ltUsername.Text = (string)Session["usernameCalidad"];
        }
        
        if (!IsPostBack)
        {
            this.Page.Title = System.Configuration.ConfigurationManager.AppSettings.Get("appTitle");
           
        }

       // cargaMenus();

    }*/

      
    protected void lnkSalir_Click(object sender, EventArgs e)
    {
        try
        {
            Session.RemoveAll();
            Response.Redirect("~/frmLogin.aspx");
        }
        catch (Exception ex)
        {
            //popUpMessageControl.setAndShowInfoMessage("ERROR: <br />" + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void lnkMisDatosMaster_Click(object sender, EventArgs e)
    {
        //PopUpCorreo2.ShowPopUpOnlyEdit(this.Page);
    }

    protected void cargaMenus() 
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        var parameters = new Dictionary<string, object>();

        //usurio admin
        //if (ConfigurationManager.AppSettings["bTesting"] == "True")
        DataTable dtUserInfo = (DataTable)Session["dtUserInfoCalidad"];
        
        string roleId = dtUserInfo.Rows[0]["roleIds"] != DBNull.Value ? (string)dtUserInfo.Rows[0]["roleIds"] : string.Empty;
        int idRol; 

        if (int.TryParse(roleId, out idRol))
        {
            parameters.Add("@idRol", idRol);
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_MenuModulos", parameters, this.Session["connection"].ToString());
            parameters.Clear();
            foreach (DataRow mod in dt.Rows)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                
                HtmlGenericControl a = new HtmlGenericControl("a");
                a.InnerText = mod["modulo"].ToString();

                li.Controls.Add(a);

                HtmlGenericControl ul = new HtmlGenericControl("ul");
                
                //submodulos:

                parameters.Clear();
                parameters.Add("@idRol", 1);
                parameters.Add("@idMod", Int32.Parse(mod["idModulo"].ToString()) );
                var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_MenuSubModulos", parameters, this.Session["connection"].ToString());
                foreach (DataRow sub in dt2.Rows)
                {

                    HtmlGenericControl li2 = new HtmlGenericControl("li");
                    LinkButton lnkB = new LinkButton();


                    lnkB.Text = sub["subName"].ToString();
                    lnkB.PostBackUrl = string.Format(sub["subRuta"].ToString() );
                  
                    li2.Controls.Add(lnkB);

                    ul.Controls.Add(li2);
                    li.Controls.Add(ul);
                    //menuConteiner.Controls.Add(li);
                }              
                
            }
        }

    }
    //protected void LinkButton2_Click(object sender, EventArgs e)
    //{
    //    this.Session["uiCulture"] = "es-MX";
    //    Response.Redirect(this.Request.Url.AbsolutePath, true);
        
    //}
    //protected void LinkButton3_Click(object sender, EventArgs e)
    //{
    //    this.Session["uiCulture"] = "en-US";
    //    Response.Redirect(this.Request.Url.AbsolutePath, true);
    //}
}

