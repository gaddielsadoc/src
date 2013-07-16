using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using log4net;

public partial class frmFilePreview : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(frmFilePreview));

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if authenticated
            string filePath = Request.QueryString["fp"];
            if (null != filePath)
            {
                filePath = Security.Decrypt(filePath);
                //Response.WriteFile(filePath);
                //Response.End();
                string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);

                FileStream MyFileStream = new FileStream(filePath, FileMode.Open);
                byte[] Buffer = new byte[(int)MyFileStream.Length]; 
                MyFileStream.Read(Buffer, 0, (int)MyFileStream.Length); 
                MyFileStream.Close(); 
                Response.ContentType = "application/octet-stream"; 
                Response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + "\""); 
                Response.BinaryWrite(Buffer);
                Context.ApplicationInstance.CompleteRequest();
                //Response.End();    
            }
        }
        catch (Exception ex)
        {
            log.Error(ex);
            Response.Redirect("~/error/GenericError.aspx");
        }
    }
}