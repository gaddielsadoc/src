using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class catalog_frmQuimico :BasePage// System.Web.UI.Page
{
    private string PathDocs = ConfigurationSettings.AppSettings["PDFFolder"];

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneQuimicos();
                this.obtienePlagas();
                this.obtieneTipoAplicacion();

                txtTiempoAplicacionHrs.Text = "0";
                txtTiempoAplicacionMin.Text = "0";

                Button1.Value = (string)GetLocalResourceObject("Button1");
            }
            /*
            if (Page.IsPostBack)
            {
                string[] values = Request.Form.GetValues("txtDosisInf");
            }*/
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    //hace el select y lo mete al ds
    private void obtieneQuimicos()
    {
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllQuimicos", parameters, this.Session["connection"].ToString());
            gvQuimico.DataSource = ds;
            gvQuimico.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }
    //select de plagas
    private void obtienePlagas()
    {
        ddlPlagas.Items.Clear();
        ddlPlagas.Items.Add(new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlPlaga", parameters, this.Session["connection"].ToString());
            ddlPlagas.DataSource = ds;
            ddlPlagas.DataValueField = "campoId";
            ddlPlagas.DataTextField = "campoNombre";
            ddlPlagas.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    //select de tipo de aplicacion
    private void obtieneTipoAplicacion()
    {
        ddlTipoAplicacion.Items.Clear();
        ddlTipoAplicacion.Items.Add(new ListItem(GetLocalResourceObject("Unica").ToString(), "-1"));
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlTipoAplicacion", parameters, this.Session["connection"].ToString());
            ddlTipoAplicacion.DataSource = ds;
            ddlTipoAplicacion.DataValueField = "idTipoAplicacion";
            ddlTipoAplicacion.DataTextField = "nombre";
            ddlTipoAplicacion.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void gvQuimico_SelectedIndexChanged(object sender, EventArgs e)
    {
        dosisTmp.Value = "";
        VolverAlPanelInicial();
        var dataKey = gvQuimico.DataKeys[gvQuimico.SelectedIndex];
        try
        {
            if (dataKey != null)
                Session["IdQuimicoCookie"] = dataKey.Value.ToString();
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@quimicoId", Session["IdQuimicoCookie"]);
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromQuimicoId", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                lbquimico.Text = dt.Rows[0]["ITMSHNAM"].ToString().Trim();
                txtIngrediente.Text = dt.Rows[0]["d6_INGACT"].ToString().Trim();
                txtTolerancia.Text = dt.Rows[0]["d6_EPATOL"].ToString().Trim();
                txtEpa.Text = dt.Rows[0]["d6_EPA180"].ToString().Trim();
                txtIntervalo.Text = dt.Rows[0]["d6_SEGCOS"].ToString().Trim();
                txtTiempo.Text = dt.Rows[0]["d6_REENT"].ToString().Trim();
                txtIndicaciones.Text = dt.Rows[0]["d6_INDIC"].ToString().Trim();
                /*txtDosisSup.Text = dt.Rows[0]["d6_DOSISSUP"].ToString().Trim();
                txtDosisInf.Text = dt.Rows[0]["d6_DOSISINF"].ToString().Trim();
                ddlDosisUni1.SelectedValue = dt.Rows[0]["d6_DOSISUNI1"].ToString().Trim();
                ddlDosisUni2.SelectedValue = dt.Rows[0]["d6_DOSISUNI2"].ToString().Trim();
                txtDosisCantidad.Text = dt.Rows[0]["d6_DSSCANTUNI2"].ToString().Trim();*/
                txtGrupo.Text = dt.Rows[0]["d6_GPOQUIM"].ToString().Trim();
                txtPersist.Text = dt.Rows[0]["d6_PERSIS"].ToString().Trim();
                txtMaxAplicEtiqu.Text = dt.Rows[0]["d6_MAXAPLETIQ"].ToString().Trim();
                txtMaxAplCiclo.Text = dt.Rows[0]["d6_MAXAPLCICLO"].ToString().Trim();
                if (!String.IsNullOrEmpty(dt.Rows[0]["d6_TIEMPOAPLIMIN"].ToString().Trim()))
                    txtTiempoAplicacionMin.Text = dt.Rows[0]["d6_TIEMPOAPLIMIN"].ToString().Trim();
                else
                    txtTiempoAplicacionMin.Text = "0";
                if (!String.IsNullOrEmpty(dt.Rows[0]["d6_TIEMPOAPLIHRS"].ToString().Trim()))
                    txtTiempoAplicacionHrs.Text = dt.Rows[0]["d6_TIEMPOAPLIHRS"].ToString().Trim();
                else
                    txtTiempoAplicacionHrs.Text = "0";
                /*
                if (dt.Rows[0]["d6_Abgrr"].ToString().Trim() == "&#923;")
                    ddlAbgrr.SelectedValue = "A";
                else if (dt.Rows[0]["d6_Abgrr"].ToString().Trim() == "&#8592;")
                    ddlAbgrr.SelectedValue = "izq";
                else
                 * */

                if (dt.Rows[0]["d6_bCOMODIN"].ToString().Equals("False") || dt.Rows[0]["d6_bCOMODIN"].ToString().Equals("0") || String.IsNullOrEmpty(dt.Rows[0]["d6_bCOMODIN"].ToString()))
                     chkComodin.Checked = false;
                else
                    chkComodin.Checked = true;

                ddlTipoQuimico.SelectedValue = dt.Rows[0]["d6_TYPEQUIM"].ToString().Trim();
                ddlAbgrr.SelectedValue = dt.Rows[0]["d6_Abgrr"].ToString().Trim();


                if (dt.Rows[0]["d6_MAXAPLUNI"].ToString().Equals("False"))
                {
                    rbMaxNumero.Checked = true;
                    rbMaxCantidad.Checked = false;
                    ltMaxAplicacionTipo.Text = dt.Rows[0]["d6_DOSISUNI1"].ToString().Trim() + " / " + dt.Rows[0]["d6_DOSISUNI2"].ToString().Trim();
                }
                else
                {
                    rbMaxCantidad.Checked = true;
                    rbMaxNumero.Checked = false;
                    ltMaxAplicacionTipo.Text = GetLocalResourceObject("Aplicaciones").ToString();
                }

                //ddlPlagas
                //obtiene las id de plagas del quimico seleccionado y las guardo en plagasTmp para que el jquery las acomode
                DataTable dtplagas = DataAccess.executeStoreProcedureDataTable("spr_SelectPlagasByQuimicoId", parameters, this.Session["connection"].ToString());
                plagasTmp.Value = string.Empty;
                if (dtplagas.Rows.Count > 0)
                {
                    foreach (DataRow item in dtplagas.Rows)
                    {
                        if (dtplagas.Rows.IndexOf(item) == dtplagas.Rows.Count - 1)
                        {
                            plagasTmp.Value += item[0].ToString();
                        }
                        else
                        {
                            plagasTmp.Value += item[0].ToString() + "|";
                        }

                    }
                }

                //RELLENA EL CAMPO dosisTmp 
                DataTable dtDosisQuimico = DataAccess.executeStoreProcedureDataTable("spr_SelectDosisByQuimicoId", parameters, this.Session["connection"].ToString());
                dosisTmp.Value = string.Empty;
                if (dtDosisQuimico.Rows.Count > 0)
                {
                    foreach (DataRow item in dtDosisQuimico.Rows)
                    {
                        if (dtDosisQuimico.Rows.IndexOf(item) == dtDosisQuimico.Rows.Count - 1)
                        {
                            dosisTmp.Value += item["tipoAplicacionId"].ToString() + "|";
                            dosisTmp.Value += item["minima"].ToString() + "|";
                            dosisTmp.Value += item["maxima"].ToString() + "|";
                            dosisTmp.Value += item["unidad1"].ToString() + "|";
                            dosisTmp.Value += item["unidad2"].ToString() + "|";
                            dosisTmp.Value += item["cantidad"].ToString();
                        }
                        else
                        {
                            dosisTmp.Value += item["tipoAplicacionId"].ToString() + "|";
                            dosisTmp.Value += item["minima"].ToString() + "|";
                            dosisTmp.Value += item["maxima"].ToString() + "|";
                            dosisTmp.Value += item["unidad1"].ToString() + "|";
                            dosisTmp.Value += item["unidad2"].ToString() + "|";
                            dosisTmp.Value += item["cantidad"].ToString() + "@";
                        }

                    }
                }

                //FICHA TECNICA
                if (DBNull.Value == dt.Rows[0]["d6_FICHTEC"] || string.IsNullOrEmpty((string)dt.Rows[0]["d6_FICHTEC"]))
                {
                    ltFichaTecnica.Text = ltFichaTecnica.NavigateUrl = string.Empty;
                    hiddenFichaTecnica.Value = "false";
                }
                else
                {
                    string fisico = MapPath(PathDocs);
                    var aux = fisico + (string)dt.Rows[0]["d6_FICHTEC"];
                    aux = Security.Encrypt(aux);
                    ltFichaTecnica.NavigateUrl = "~/frmFilePreview.aspx?fp=" + Server.UrlEncode(aux);
                    ltFichaTecnica.Text = (string)dt.Rows[0]["d6_FICHTEC"];
                    hiddenFichaTecnica.Value = "true";
                }
                //HOJA DE SEGURIDAD
                if (DBNull.Value == dt.Rows[0]["d6_HOJSEG"] || string.IsNullOrEmpty((string)dt.Rows[0]["d6_HOJSEG"]))
                {
                    ltHojaSeguridad.Text = ltHojaSeguridad.NavigateUrl = string.Empty;
                    hiddenHojaSeguridad.Value = "false";
                }
                else
                {
                    string fisico = MapPath(PathDocs);
                    var aux = fisico + (string)dt.Rows[0]["d6_HOJSEG"];
                    aux = Security.Encrypt(aux);
                    ltHojaSeguridad.NavigateUrl = "~/frmFilePreview.aspx?fp=" + Server.UrlEncode(aux);
                    ltHojaSeguridad.Text = (string)dt.Rows[0]["d6_HOJSEG"];
                    hiddenHojaSeguridad.Value = "true";
                }
                //REGISTRO COFEPRIS
                if (DBNull.Value == dt.Rows[0]["d6_REGCOFE"] || string.IsNullOrEmpty((string)dt.Rows[0]["d6_REGCOFE"]))
                {
                    ltRegistroC.Text = ltRegistroC.NavigateUrl = string.Empty;
                    hiddenRegistroC.Value = "false";
                }
                else
                {
                    string fisico = MapPath(PathDocs);
                    var aux = fisico + (string)dt.Rows[0]["d6_REGCOFE"];
                    aux = Security.Encrypt(aux);
                    ltRegistroC.NavigateUrl = "~/frmFilePreview.aspx?fp=" + Server.UrlEncode(aux);
                    ltRegistroC.Text = (string)dt.Rows[0]["d6_REGCOFE"];
                    hiddenRegistroC.Value = "true";
                }

                gvQuimico.Enabled = true;
                Button1.Visible = true;
                btnCancel.Visible = false;
                btnLimpiar.Visible = true;

                gvQuimico.Enabled = true;
                Button1.Visible = true;
                btnCancel.Visible = false;
                btnLimpiar.Visible = true;


            }
            else
            {
                //No se encontró el registro
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }
    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        VolverAlPanelInicial();
    }
    protected void gvQuimico_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsQuimico"])
            {
                DataSet ds = ViewState["dsQuimico"] as DataSet;

                if (ds != null)
                {
                    gvQuimico.DataSource = ds;
                    gvQuimico.DataBind();
                }
            }
            ((GridView)sender).PageIndex = e.NewPageIndex;
            ((GridView)sender).DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void gvQuimico_PreRender(object sender, EventArgs e)
    {
        
        if (gvQuimico.HeaderRow != null)
            gvQuimico.HeaderRow.TableSection = TableRowSection.TableHeader;
        
    }
    protected void gvQuimico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        switch (e.Row.RowType)
        {

            case DataControlRowType.DataRow:
                try
                {
                    Label lbl = (Label)e.Row.FindControl("Label0");

                    if (!string.IsNullOrEmpty(lbl.Text))
                        lbl.Text = GetLocalResourceObject(lbl.Text.Replace(" ", "")).ToString();

                    if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[12].Text)))
                    {


                        e.Row.Cells[12].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[12].Text).Replace(" ", "").Replace("á", "a")).ToString();
                    }
                }
                catch(Exception ex)
                { 
                   
                }
                ((HyperLink)e.Row.FindControl("texto")).Attributes.Add("onmouseover", "return overlib('" + ((HyperLink)e.Row.FindControl("texto")).Text.ToString() + "', ABOVE)");
                ((HyperLink)e.Row.FindControl("texto")).Attributes.Add("onmouseout", "return nd();");
        
                if (((HyperLink)e.Row.FindControl("texto")).Text.Length > 20)
                {
                    ((HyperLink)e.Row.FindControl("texto")).Text = ((HyperLink)e.Row.FindControl("texto")).Text.Substring(0, 20)+"...";
                }
                
                e.Row.Cells[9].Text = e.Row.Cells[9].Text.Replace("@", "<br />");
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvQuimico, ("Select$" + e.Row.RowIndex.ToString()));
            break;
        }
        
    }

    //boton guardar es el que hace update si todo sale bien
    protected void Actualizar(object sender, EventArgs e)
    {

        string value = Request.Form["ddlTipoApli"];


        try
        {   
            if (Session["IdQuimicoCookie"] == null || Session["IdQuimicoCookie"].ToString() == "")
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("RGRL02").ToString(), Common.MESSAGE_TYPE.Error);
            }
            else
            {
                //VALIDACIONES SERVER SIDE
                float varPersist = 0, varIntervalo, varTiempo, varDosisSup, varDosisInf, varDosisCantidad, varMaxAplCiclo;
                int toleranciaApliMin, toleranciaApliHrs;
                bool error = false ;
                string mensajeError = "";

                if (txtPersist.Text != "" && !float.TryParse(txtPersist.Text, out varPersist))
                {
                    error = true;
                    mensajeError += GetLocalResourceObject("CampoPersistenciaN").ToString();
                }
                if (!float.TryParse(txtIntervalo.Text, out varIntervalo))
                {
                    error = true;
                    mensajeError += GetLocalResourceObject("CampoIntervaloN").ToString();
                }
                if (!float.TryParse(txtTiempo.Text, out varTiempo))
                {
                    error = true;
                    mensajeError += GetLocalResourceObject("CampoTiempoN").ToString();
                }
                /*
                if (!float.TryParse(txtDosisSup.Text, out varDosisSup))
                {
                    error = true;
                    mensajeError += " El campo Dosis superior debe ser número<br>";
                }
                if (!float.TryParse(txtDosisInf.Text, out varDosisInf))
                {
                    error = true;
                    mensajeError += " El campo Dosis Inferior debe ser número<br>";
                }
                if (!float.TryParse(txtDosisCantidad.Text, out varDosisCantidad))
                {
                    error = true;
                    mensajeError += " El campo Dosis cantidad debe ser número<br>";
                }
                */
                if (!float.TryParse(txtMaxAplCiclo.Text, out varMaxAplCiclo))
                {
                    error = true;
                    mensajeError += GetLocalResourceObject("CampoMaximaN").ToString(); ;
                }
                if (!int.TryParse(txtTiempoAplicacionMin.Text, out toleranciaApliMin))
                {
                    error = true;
                    mensajeError += GetLocalResourceObject("CampoMinutosTN").ToString(); ;
                }
                if (!int.TryParse(txtTiempoAplicacionHrs.Text, out toleranciaApliHrs))
                {
                    error = true;
                    mensajeError += GetLocalResourceObject("CampoHorasTN").ToString(); 
                }

                              

                //var fisico trae la dirección fisica para archivos
                string fisico = MapPath(PathDocs);
                //var parameters trae los parametros a agregar
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();

                //FICHA TÉCNICA
                string FileExtension = String.Empty;
                if (fupFichaTecnica.HasFile)
                {
                    FileExtension = fupFichaTecnica.PostedFile.FileName.Substring(fupFichaTecnica.PostedFile.FileName.LastIndexOf('.') + 1).ToLower();
                    if (FileExtension != "pdf")
                    {
                        //si el quimico no es pdf
                        mensajeError += GetLocalResourceObject("fichaPDFBR").ToString(); 
                        error = true;
                    }
                    else
                    {
                        //PDF Ficha Técnica
                        //Eliminar Anterior en caso de que exista
                        if (hiddenFichaTecnica.Value == "true" && File.Exists(fisico + ltFichaTecnica.Text))
                            File.Delete(fisico + ltFichaTecnica.Text);
                        //guarda el registro como IdQuimico_fichaTecnica.pdf
                        parameters.Add("@fichaTecnica", Session["IdQuimicoCookie"].ToString().Trim() + "_fichaTecnica.pdf");
                        //Guardar Archivo
                        fupFichaTecnica.SaveAs(fisico + Session["IdQuimicoCookie"].ToString().Trim() + "_fichaTecnica.pdf");
                    }
                }
                else
                {
                    parameters.Add("@fichaTecnica", System.DBNull.Value);
                }
                //HOJA DE SEGURIDAD
                if (fupHojaSeguridad.HasFile) 
                {
                    FileExtension = fupHojaSeguridad.PostedFile.FileName.Substring(fupHojaSeguridad.PostedFile.FileName.LastIndexOf('.') + 1).ToLower();
                    if (FileExtension != "pdf")
                    {
                        mensajeError += GetLocalResourceObject("hojaPDFBR").ToString();
                        error = true;
                    }
                    else
                    {
                        //PDF hoja de seguridad
                        //Eliminar Anterior en caso de que exista
                        if (hiddenHojaSeguridad.Value == "true" && File.Exists(fisico + ltHojaSeguridad.Text))
                            File.Delete(fisico + ltHojaSeguridad.Text);
                        //guarda el registro como IdQuimico_hojaSeguridad.pdf
                        parameters.Add("@hojaSeguridad", Session["IdQuimicoCookie"].ToString().Trim() + "_hojaSeguridad.pdf");
                        //Guardar Archivo
                        fupHojaSeguridad.SaveAs(fisico + Session["IdQuimicoCookie"].ToString().Trim() + "_hojaSeguridad.pdf");
                    }
                }
                else
                {
                    parameters.Add("@hojaSeguridad", System.DBNull.Value);
                }
                //REGISTRO COFEPRIS
                if (fupRegistroC.HasFile)
                {
                    FileExtension = fupRegistroC.PostedFile.FileName.Substring(fupRegistroC.PostedFile.FileName.LastIndexOf('.') + 1).ToLower();
                    if (FileExtension != "pdf")
                    {
                        mensajeError +=  GetLocalResourceObject("registroPDFBR").ToString();
                        error = true;
                    }
                    else
                    {
                        //PDF registro cofepris
                        //Eliminar Anterior en caso de que exista
                        if (hiddenRegistroC.Value == "true" && File.Exists(fisico + ltRegistroC.Text))
                            File.Delete(fisico + ltRegistroC.Text);
                        //guarda el registro como IdQuimico_registroC.pdf
                        parameters.Add("@registroC", Session["IdQuimicoCookie"].ToString().Trim() + "_registroC.pdf");
                        //Guardar Archivo
                        fupRegistroC.SaveAs(fisico + Session["IdQuimicoCookie"].ToString().Trim() + "_registroC.pdf");
                    }
                }
                else
                {
                    parameters.Add("@registroC", System.DBNull.Value);
                }
                //Plagas
                string plagas = string.Empty;
                plagas = Request.Form[ddlPlagas.UniqueID];
                plagas = plagas.Replace(',', '|');
                //se eliminan los campos vacios
                plagas = plagas.Replace("-1|", "");
                plagas = plagas.Replace("-1", "");

                //si se dice que es comodin, no debe guardar plagas
                if (chkComodin.Checked == true)
                {
                    if (!String.IsNullOrEmpty(plagas)) 
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("borrarPlagas").ToString(), Common.MESSAGE_TYPE.Info);
                        return;

                    }
                }
                else
                if (plagas == null || plagas == "")
                {
                    mensajeError += GetLocalResourceObject("aPlaga").ToString();
                    error = true;
                }


                if (!error)
                {
                    
                    parameters.Add("@IdQuimico", Session["IdQuimicoCookie"].ToString());
                    parameters.Add("@txtIngrediente", txtIngrediente.Text);
                    parameters.Add("@txtTolerancia", txtTolerancia.Text);
                    parameters.Add("@txtEpa", txtEpa.Text);
                    parameters.Add("@txtIndicaciones", txtIndicaciones.Text);
                    //parameters.Add("@ddlDosisUni1", ddlDosisUni1.SelectedValue);
                    //parameters.Add("@ddlDosisUni2", ddlDosisUni2.SelectedValue);
                    parameters.Add("@txtGrupo", txtGrupo.Text);
                    parameters.Add("@txtMaxAplicEtiqu", txtMaxAplicEtiqu.Text);
                    parameters.Add("@ddlTipoQuimico", ddlTipoQuimico.SelectedValue);
                    parameters.Add("@txtMaxAplCiclo", txtMaxAplCiclo.Text);
                    if (ddlAbgrr.SelectedValue == "A")
                        parameters.Add("@ddlAbgrr", "&#923;");
                    else if (ddlAbgrr.SelectedValue == "izq")
                        parameters.Add("@ddlAbgrr", "&#8592;");
                    else
                        parameters.Add("@ddlAbgrr", ddlAbgrr.SelectedValue);
                    //decimales que deben ser convertidos
                    parameters.Add("@txtPersist", varPersist);
                    parameters.Add("@txtIntervalo", varIntervalo);
                    parameters.Add("@txtTiempo", varTiempo);
                    /*
                    parameters.Add("@txtDosisSup", varDosisSup);
                    parameters.Add("@txtDosisInf", varDosisInf);
                    parameters.Add("@txtDosisCantidad", varDosisCantidad);
                     */

                    //DOSIS
                    bool end = false;
                    int i = 0;
                    //string tipoAplicacion, dosisMinimas, dosisMaximas, unidad1, dosisCantidad, unidad2;
                    //DataTable dt = initDataTable();
                    string[] tipoApli = Request.Form["ddlTipoApli[]"].Split(',');

                    //borramos todos las dosis quimico
                    Dictionary<string, object> param = new System.Collections.Generic.Dictionary<string, object>();
                    param.Add("@quimicoId", Session["IdQuimicoCookie"].ToString());
                    String Res = DataAccess.executeStoreProcedureString("spr_DeleteAllDosis", param, this.Session["connection"].ToString());

                    //insertamos todas las nuevas dosis quimico
                    do
                    {
                        if (Request.Form["txtDosisInf" + i] != null)
                        {
                            Dictionary<string, object> paramDosis = new System.Collections.Generic.Dictionary<string, object>();

                            paramDosis.Add("@quimicoId", Session["IdQuimicoCookie"].ToString());            //id quimico
                            paramDosis.Add("@tipoApli", tipoApli[i] );                                      //tipo aplicacion
                            paramDosis.Add("@dosisMinima", Double.Parse(Request.Form["txtDosisInf" + i]) ); //minima
                            paramDosis.Add("@dosisMaxima", Double.Parse(Request.Form["txtDosisSup" + i]) ); //maxima
                            paramDosis.Add("@dosisUnidad1", Request.Form["ddlDosisUni1" + i]);              //unidad 1
                            paramDosis.Add("@dosisUnidad2", Request.Form["ddlDosisUni2" + i]);              //unidad 2
                            paramDosis.Add("@cantidad", Request.Form["txtDosisCantidad" + i]);              //cantidad

                            Res = DataAccess.executeStoreProcedureString("spr_InsertDosis", paramDosis, this.Session["connection"].ToString());
                            i++;
                        }
                        else { end = true; }
                    }while (!end);


                    parameters.Add("@txtToleranciaApliHrs", toleranciaApliHrs);
                    parameters.Add("@txtToleranciaApliMin", toleranciaApliMin);

                    //booleano d6_MAXAPLUNI $
                    if (rbMaxCantidad.Checked == false)
                    {
                        parameters.Add("@d6_MAXAPLUNI", "0");
                    }
                    else
                    {
                        parameters.Add("@d6_MAXAPLUNI", "1");
                    }

                    //guarda la lista de plagas.
                    parameters.Add("@plagasList", plagas);

                    //guardar si es comodin o no
                    if(chkComodin.Checked == true)
                        parameters.Add("@comodin", true);
                    else
                        parameters.Add("@comodin", false);

                    //ejecuta el stored procedure
                    String Rs = DataAccess.executeStoreProcedureString("spr_UpdateQuimico", parameters, this.Session["connection"].ToString());
                    if (Rs.Equals("error"))
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("exist").ToString(), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("saveIt").ToString(), Common.MESSAGE_TYPE.Success);
                        obtieneQuimicos();
                        VolverAlPanelInicial();
                    }

                }
                else
                {
                    popUpMessageControl1.setAndShowInfoMessage(mensajeError, Common.MESSAGE_TYPE.Error);
                }


            }
        }
        catch (Exception exception){
            Log.Error(exception);
            //popUpMessageControl1.setAndShowInfoMessage(exception.Message, Common.MESSAGE_TYPE.Error);
        }
        //obtieneQuimicos();
        //VolverAlPanelInicial();
    }

    protected void VolverAlPanelInicial()
    {
        lbquimico.Text = "";
		txtIngrediente.Text = "";
		txtTolerancia.Text = "";
		txtEpa.Text = "";
		txtIntervalo.Text = "";		
		txtTiempo.Text = "";
		txtIndicaciones.Text = "";
        /*
		txtDosisSup.Text = "";
		txtDosisInf.Text = "";
		txtDosisCantidad.Text = "";
        */
		txtGrupo.Text = "";
		txtPersist.Text = "";
		txtMaxAplicEtiqu.Text = "";
        txtTolerancia.Text = "";
		rbMaxNumero.Checked = false;
        rbMaxCantidad.Checked = true;
		gvQuimico.Enabled = true;
        Button1.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        plagasTmp.Value = string.Empty;
        fupFichaTecnica.Attributes.Clear();
        fupHojaSeguridad.Attributes.Clear();
        fupRegistroC.Attributes.Clear();
        ltFichaTecnica.Text = "";
        ltHojaSeguridad.Text = "";
        ltRegistroC.Text = "";
        hiddenFichaTecnica.Value = "false";
        hiddenHojaSeguridad.Value = "false";
        hiddenRegistroC.Value = "false";
        ddlPlagas.SelectedValue = "-1";
        txtMaxAplCiclo.Text = string.Empty;
        txtTiempoAplicacionHrs.Text = "0";
        txtTiempoAplicacionMin.Text = "0";
        ddlTipoAplicacion.SelectedValue = "-1";
        dosisTmp.Value = string.Empty;
        chkComodin.Checked = false;
    }

}

