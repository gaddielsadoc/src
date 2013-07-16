using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class error_UploadTooLarge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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

}