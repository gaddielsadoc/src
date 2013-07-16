using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;


public partial class frmLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != Session["Locale"])
        {
            ddlLocale.SelectedValue = (string)Session["Locale"];
        }
    }
    protected override void InitializeCulture()
    {


        if (this.Session["uiCulture"] != null)
        {

            //Session["Locale"] = Request.Form["ctl00$ddlLocale"];
            //for UI elements
            UICulture = this.Session["uiCulture"].ToString();

            //for region specific formatting
            Culture = this.Session["uiCulture"].ToString();
        }
        else
        {
            if (null != Session["uiCulture"])
            {
                UICulture = (string)Session["uiCulture"];
                Culture = (string)Session["uiCulture"];
            }
            else
            {
                Session["uiCulture"] = CultureInfo.CurrentCulture.Name;
                UICulture = (string)Session["uiCulture"];
                Culture = (string)Session["uiCulture"];
            }
        }
        //if (Request.Form["ctl00$ddlLocale"] != null)
        //{
        //    Session["Locale"] = Request.Form["ctl00$ddlLocale"];
        //    //for UI elements
        //    UICulture = Request.Form["ctl00$ddlLocale"];

        //    //for region specific formatting
        //    Culture = Request.Form["ctl00$ddlLocale"];
        //}
        //else
        //{
        //    if (null != Session["Locale"])
        //    {
        //        UICulture = (string)Session["Locale"];
        //        Culture = (string)Session["Locale"];
        //    }
        //    else
        //    {
        //        Session["Locale"] = CultureInfo.CurrentCulture.Name;
        //        UICulture = (string)Session["Locale"];
        //        Culture = (string)Session["Locale"];
        //    }
        //}        
        base.InitializeCulture();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        this.Session["uiCulture"] = "es-MX";
        Response.Redirect(this.Request.Url.AbsolutePath, true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        this.Session["uiCulture"] = "en-US";
        Response.Redirect(this.Request.Url.AbsolutePath, true);
    }
}
