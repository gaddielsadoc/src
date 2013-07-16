using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public partial class pages_BoletinPublicado : BasePage
{
    private string _XMLRootFolder = ConfigurationSettings.AppSettings["XMLFolder"];
    private string PathDocsPDF = ConfigurationSettings.AppSettings["PDFFolder"];

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
                //cargaDatos();
                this.cargaddlPlantas();
                if (ddlPlanta.SelectedIndex == 0)
                    bntEnviar.Visible = false;
                else
                    bntEnviar.Visible = true;
                    
            }

            else if (Session["usernameCalidad"] == null)
            {
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
        
        try
        {
            parameters.Add("@idPlanta",ddlPlanta.SelectedValue);
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_BoletinChildPublicado", parameters, this.Session["connection"].ToString());
            gdvBoletin.DataSource = dt;
            gdvBoletin.DataBind();

            GridViewMail.DataSource = dt;
            GridViewMail.DataBind();

            ViewState["GridBoletin"] = dt;

            //campos de cabecera
            txtNombre.Text = dt.Rows.Count > 0 ? dt.Rows[0]["vNombreBoletinHeader"].ToString() : "";
            //txtDesde.Text = dt.Rows.Count > 0 ? dt.Rows[0]["dateFechaInicioBoletinHeader"].ToString() : "";txtHasta.Text = dt.Rows.Count > 0 ? dt.Rows[0]["dateFechaFinBoletinHeader"].ToString() : "";           

            //si no se cargo nada en el boletín, no mostrar el botón de enviar correo:
            if (gdvBoletin.Rows.Count < 1)
                bntEnviar.Visible = false;
            else
                bntEnviar.Visible = true;
        }
        catch (Exception EX)
        {
            Log.Error(EX);
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
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorPlanta").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }

    protected void gdvBoletin_PreRender(object sender, EventArgs e)
    {
        //if (gdvBoletin.HeaderRow != null)
        //    gdvBoletin.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvBoletin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:

                //AGREGA EL MOUSE OVER A INDICACIONES
                
                ((HyperLink)e.Row.FindControl("textoIndicaciones")).Attributes.Add("onmouseover", "return overlib('" + ((HyperLink)e.Row.FindControl("textoIndicaciones")).Text.ToString() + "', ABOVE)");
                ((HyperLink)e.Row.FindControl("textoIndicaciones")).Attributes.Add("onmouseout", "return nd();");
                if (((HyperLink)e.Row.FindControl("textoIndicaciones")).Text.Length > 20)
                {
                    ((HyperLink)e.Row.FindControl("textoIndicaciones")).Text = ((HyperLink)e.Row.FindControl("textoIndicaciones")).Text.Substring(0, 20) + "...";
                }
                

                e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("@", "<br />");


                if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[0].Text).Trim()))
                    {


                        e.Row.Cells[0].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[0].Text)).ToString();
                    }
                if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[12].Text).Trim()))
                    {


                        e.Row.Cells[12].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[12].Text)).ToString();
                    }


        //        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvBoletin, ("Select$" + e.Row.RowIndex.ToString()));   
             
                //--ficha tecnica
                var link = e.Row.FindControl("HyperLink1");
                if (null != link) {
                    HyperLink linkReal = link as HyperLink;
                    string path = PathDocsPDF;
                    string fisico = MapPath(path);
                    var aux = fisico + (string)((DataRowView)e.Row.DataItem)["d6_FICHTEC"];
                    aux = Security.Encrypt(aux);
                    linkReal.NavigateUrl = "~/frmFilePreview.aspx?fp=" + Server.UrlEncode(aux);
                    linkReal.Text = ((string)((DataRowView)e.Row.DataItem)["d6_FICHTEC"]) == "" ? "": GetLocalResourceObject("FichaTecnica").ToString();//"Ficha Técnica";                    
                    //string fichaTecnicaName = (string)((DataRowView)e.Row.DataItem)["d6_FICHTEC"];
                }

                //--hoja seguridad
                link = e.Row.FindControl("HyperLink2");
                if (null != link)
                {
                    HyperLink linkReal = link as HyperLink;
                    string path = PathDocsPDF;
                    string fisico = MapPath(path);
                    var aux = fisico + (string)((DataRowView)e.Row.DataItem)["d6_HOJSEG"];
                    aux = Security.Encrypt(aux);
                    linkReal.NavigateUrl = "~/frmFilePreview.aspx?fp=" + Server.UrlEncode(aux);
                    linkReal.Text = ((string)((DataRowView)e.Row.DataItem)["d6_HOJSEG"]) == "" ? "" : GetLocalResourceObject("H.Seguridad").ToString(); //"H. Seguridad";
                    //string fichaTecnicaName = (string)((DataRowView)e.Row.DataItem)["d6_HOJSEG"];
                }

                //--registro cofepris
                link = e.Row.FindControl("HyperLink3");
                if (null != link)
                {
                    HyperLink linkReal = link as HyperLink;
                    string path = PathDocsPDF;
                    string fisico = MapPath(path);
                    var aux = fisico + (string)((DataRowView)e.Row.DataItem)["d6_REGCOFE"];
                    aux = Security.Encrypt(aux);
                    linkReal.NavigateUrl = "~/frmFilePreview.aspx?fp=" + Server.UrlEncode(aux);
                    linkReal.Text = ((string)((DataRowView)e.Row.DataItem)["d6_REGCOFE"]) == "" ? "" : "Reg. COFEPRIS";
                    //string fichaTecnicaName = (string)((DataRowView)e.Row.DataItem)["d6_REGCOFE"];
                }
                break;
        }
    }

    protected void gdvBoletin_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btbExport_Click(object sender, EventArgs e)
    {
        try
        {
            string path = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}",
                                                                 _XMLRootFolder, GetLocalResourceObject("Boletin.pdf")));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var htmlTw = new HtmlTextWriter(sw);
            gdvBoletin.RenderControl(htmlTw);
            var html = sb.ToString();
            //var html = "";
            //Create PDF document
            //var ms = new MemoryStream();
            Rectangle rect = PageSize.LEGAL.Rotate();
            var pdfDocument = new Document(rect, 5, 5, 5, 5);
            var writer = PdfWriter.GetInstance(pdfDocument, new FileStream(path, FileMode.Create));
            pdfDocument.Open();



            pdfDocument.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDocument, new StringReader(html));

            pdfDocument.Close();

            Response.Redirect("~/frmFilePreview.aspx?fp=" + Server.UrlEncode(path), false);

            //Envío de correo
            /*var ms = new FileStream("d:\\Test\\calidad\\calidad.pdf", FileMode.Open);

            var pv = new Dictionary<string, string>
                                                            {
                                                                {"@nombre", "Testing"}
                                                            };

            var files = new Dictionary<string, Stream>
                                                            {
                                                                {"Boletin.pdf", ms}
                                                            };

            Common.SendMailByDictionary(pv, files,"alejandro.mejia@dominio6.com","BoletinPublicado");
            ms.Close();

            if(File.Exists("d:\\Test\\calidad\\calidad.pdf"))
                File.Delete("d:\\Test\\calidad\\calidad.pdf");*/
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    
    protected void bntEnviar_Click(object sender, EventArgs e)
    {
        try
        {

            string path = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}",
                                                                 _XMLRootFolder, GetLocalResourceObject("Boletin.pdf")));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var htmlTw = new HtmlTextWriter(sw);
            //gdvBoletin.RenderControl(htmlTw);
            GridViewMail.Visible = true;
            GridViewMail.RenderControl(htmlTw);
            GridViewMail.Visible = false;
            var html = sb.ToString();
            //var html = "";
            //Create PDF document
            //var ms = new MemoryStream();
            Rectangle rect = PageSize.A4.Rotate();
            var pdfDocument = new Document(rect);
            var writer = PdfWriter.GetInstance(pdfDocument, new FileStream(path, FileMode.Create));
            pdfDocument.Open();
            
            pdfDocument.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDocument, new StringReader(html));

            pdfDocument.Close();


            #region Correo
            var parameters = new Dictionary<string, object>();
            parameters.Add("@ACTIVO", true);
            parameters.Add("@planta", ddlPlanta.SelectedValue);
            var dt = DataAccess.executeStoreProcedureDataTable("spr_ObtieneCorreos", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    var ms = new FileStream(path, FileMode.Open);
                   
                    try{
                    
                    
                    if (DBNull.Value != row["correo"] && !string.IsNullOrEmpty(row["correo"].ToString()))
                    {
                        //Envío de correo


                        var pv = new Dictionary<string, string>();

                        var files = new Dictionary<string, Stream>
                                                                        {
                                                                            {"Boletin.pdf", ms}
                                                                        };

                        Common.SendMailByDictionary(pv, files, row["correo"].ToString(), "BoletinPublicado");
                        //popUpMessageControl1.setAndShowInfoMessage("Correo enviado exitosamente", Common.MESSAGE_TYPE.Success);
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("CorreoExito").ToString(), Common.MESSAGE_TYPE.Success);

                    }
                    ms.Close();
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                        //popUpMessageControl1.setAndShowInfoMessage("No fue posible enviar el correo: " + ex.Message, Common.MESSAGE_TYPE.Warning);
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("CorreoError").ToString(), Common.MESSAGE_TYPE.Warning);
                    }
                    finally 
                    { ms.Close();
                    }
                }




                if (File.Exists(path))
                    File.Delete(path);
            }
            #endregion

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorCorreo").ToString(), Common.MESSAGE_TYPE.Warning);
        }
        //finally {

        //    //ms = null;
        //    //ms.Close();
        //}

    }
        


    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
        // No code required here.
    }


    protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPlanta.SelectedIndex == 0)
        {
            bntEnviar.Visible = false;
            gdvBoletin.DataSource = null;
            gdvBoletin.DataBind();
        }
        else
        {
            bntEnviar.Visible = true;
            cargaDatos();
        }
        
        
            
    }
}