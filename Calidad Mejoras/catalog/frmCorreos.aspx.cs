using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public partial class catalog_frmCorreos : BasePage
{
    private string _XMLRootFolder = ConfigurationSettings.AppSettings["XMLFolder"];    

    #region Eventos Pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneCorreos();
                this.cargaddlPlantas();
            }
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
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
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorPlantas").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }

    protected void btnSaveCorreo_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlPlanta.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("PlantaRequerida").ToString()), Common.MESSAGE_TYPE.Error);
                return;
            }

            if (txtCorreo.Text.Trim().Equals(""))
            {
                //popUpMessageControl1.setAndShowInfoMessage("El campo Correo es requerido.", Common.MESSAGE_TYPE.Error);
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CorreoRequerido").ToString()), Common.MESSAGE_TYPE.Error);
            }
            else
            {
                var parameters = new Dictionary<string, object>();
                DataSet ds = null;

                parameters.Add("@correo", txtCorreo.Text);
                parameters.Add("@planta", ddlPlanta.SelectedValue);
                if (chkCorreoActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);


                if (Accion.Value == "Añadir")
                {
                    //Verificar que el valor "Correo" a insertar no estan anteriormente agregados
                    var find = new Dictionary<string, object> {{"@correo", txtCorreo.Text}};
                    find.Add("@planta", ddlPlanta.SelectedValue);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteCorreo", find, this.Session["connection"].ToString()) > 0)
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios debido a que el correo ya existe.", Common.MESSAGE_TYPE.Info);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CorreoExisteError").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        ds = DataAccess.executeStoreProcedureDataSet("spr_InsertCorreo", parameters, this.Session["connection"].ToString());
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                            //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL03: No se pudo guardar el registro", Common.MESSAGE_TYPE.Error);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CorreoNoSaveError").ToString()), Common.MESSAGE_TYPE.Error);
                        else
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("El Correo \"" + txtCorreo.Text + "\" se guardó exitosamente.", Common.MESSAGE_TYPE.Success);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CorreoGuardado"), txtCorreo.Text.ToString()), Common.MESSAGE_TYPE.Success);

                        }
                    }
                }
                else
                {
                    if (Session["IdCorreoCookie"] == null || Session["IdCorreoCookie"].ToString() == "")
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL02: Se perdió la información del ID actual", Common.MESSAGE_TYPE.Error);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("RGRL02").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        parameters.Add("@IdCorreo", Session["IdCorreoCookie"].ToString());
                        ds = DataAccess.executeStoreProcedureDataSet("spr_UpdateCorreo", parameters, this.Session["connection"].ToString());
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL01: No se encontró ID", Common.MESSAGE_TYPE.Error);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("RGRL02").ToString()), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            String Resultado = ds.Tables[0].Rows[0]["Resultado"].ToString();
                            if (Resultado.Equals("Existe"))
                            {
                                //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios debido a que este correo ya esta registrado.", Common.MESSAGE_TYPE.Info);
                                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CorreoExisteError").ToString()), Common.MESSAGE_TYPE.Error);
                            }
                            else
                                if (Resultado.Equals("Update"))
                                {
                                    //popUpMessageControl1.setAndShowInfoMessage("Cambios realizados.", Common.MESSAGE_TYPE.Success);
                                    popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CambiosBien").ToString()), Common.MESSAGE_TYPE.Success);
                                }
                        }
                    }
                }
                obtieneCorreos();
                VolverAlPanelInicial();

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected void btnCancelCorreo_Click(object sender, EventArgs e)
    {
        try
        {
            VolverAlPanelInicial();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }

    }

    protected void gvCorreo_PreRender(object sender, EventArgs e)
    {
        if (gvCorreo.HeaderRow != null)
            gvCorreo.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvCorreo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvCorreo, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    protected void gvCorreo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["IdCorreoCookie"] = gvCorreo.DataKeys[gvCorreo.SelectedIndex].Value.ToString();
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@IdCorreo", Session["IdCorreoCookie"]);
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromCorreoId", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtCorreo.Text = dt.Rows[0]["correo"].ToString().Trim();
                ddlPlanta.SelectedValue = dt.Rows[0]["idPlanta"].ToString();
                if (dt.Rows[0]["activo"].ToString().Equals("True"))
                    chkCorreoActivo.Checked = true;
                else
                    chkCorreoActivo.Checked = false;
                Accion.Value = "Guardar Cambios";
                btnActualizar.Visible = true;
                btnCancelCorreo.Visible = true;
                btnLimpiar.Visible = false;
                btnSaveCorreo.Visible = false;
            }
            else
            {
                //No se encontró el registro
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvCorreo.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new System.Web.UI.PostBackOptions(gvCorreo, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    #endregion

    private void obtieneCorreos()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_ObtieneCorreos", parameters, this.Session["connection"].ToString());
        ViewState["dsCorreos"] = ds;
        gvCorreo.DataSource = ds;
        gvCorreo.DataBind();     

    }

    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtCorreo.Text = "";
        chkCorreoActivo.Checked = true;
        gvCorreo.Enabled = true;
        btnActualizar.Visible = false;
        btnCancelCorreo.Visible = false;
        btnLimpiar.Visible = true;
        btnSaveCorreo.Visible = true;
        ddlPlanta.SelectedIndex = 0;
    }
    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        try
        {
            string path = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}",
                                                                 _XMLRootFolder, "Boletin.pdf"));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            var htmlTw = new HtmlTextWriter(sw);
            gvCorreo.RenderControl(htmlTw);
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
            var dt = DataAccess.executeStoreProcedureDataTable("spr_ObtieneCorreos", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                
                foreach (DataRow row in dt.Rows)
                {
                    if (DBNull.Value != row["correo"] && !string.IsNullOrEmpty(row["correo"].ToString()))
                    {
                        //Envío de correo
                        var ms = new FileStream(path, FileMode.Open);

                        var pv = new Dictionary<string, string>();

                        var files = new Dictionary<string, Stream>
                                                                        {
                                                                            {"Boletin.pdf", ms}
                                                                        };

                        Common.SendMailByDictionary(pv, files, row["correo"].ToString(), "BoletinPublicado");
                        ms.Close();

                        if (File.Exists(path))
                            File.Delete(path);
                    }
                }
            }
            #endregion
           
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
        // No code required here.
    }
}