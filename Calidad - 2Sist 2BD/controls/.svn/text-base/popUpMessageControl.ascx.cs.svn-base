using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class controls_popUpControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void setAndShowInfoMessage(string message, Common.MESSAGE_TYPE type)
    {
        string imageScr = "error";
        switch (type)
        {
            case Common.MESSAGE_TYPE.Error:
                imageScr = "error";
                break;
            case Common.MESSAGE_TYPE.Info:
                break;
            case Common.MESSAGE_TYPE.Warning:
                break;
            case Common.MESSAGE_TYPE.Success:
                imageScr = "ok";
                break;
            default:
                imageScr = "error";
                break;
        }

        imgMessageGralControl.Src = string.Format("../comun/img/{0}.png",imageScr);
        lblMessageGralControl.Text = message;
        mdlPopupMessageGralControl.Show();
    }
}
