using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class pages_Reporte : BasePage
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

                cargaFiltros();
                btnExcel1.Visible = false;
                btnExcel2.Visible = false; 
            }

            else if (Session["usernameCalidad"] == null)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Sesion").ToString(), Common.MESSAGE_TYPE.Warning);
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void cargaFiltros()
    {
        DateTime thisDay = DateTime.Today;
        txtDesde.Text = thisDay.AddDays(-7).ToString("yyyy-MM-dd");
        txtHasta.Text = thisDay.ToString("yyyy-MM-dd");
        DataTable dt = null;
       
        DataTable dtUserInfo = (DataTable)Session["dtUserInfoCalidad"];
        int roleId = dtUserInfo.Rows[0]["roleIds"] != DBNull.Value ? (int)dtUserInfo.Rows[0]["roleIds"] : -1;

        var parameters = new Dictionary<string, object>();
        try
        {
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlPlantas", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (roleId == 1)
                    ddlPlanta.Items.Add(GetLocalResourceObject("Todas").ToString());
                ddlPlanta.DataSource = dt;
                ddlPlanta.DataBind();
            }

            parameters.Clear();
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderosTodos", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                ddlInvernadero.Items.Add(GetLocalResourceObject("Todos").ToString());
                ddlInvernadero.DataSource = dt;
                ddlInvernadero.DataBind();
            }

            parameters.Clear();
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString()); 
            parameters.Add("@planta", 0);
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlGrowers", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (roleId == 1)
                    ddlGrower.Items.Add(GetLocalResourceObject("Todos").ToString());
                ddlGrower.DataSource = dt;
                ddlGrower.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }

        ddlEstatus.Items.Clear();
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Todos").ToString(), "-- Todos --"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Abierto").ToString(), "Abierto"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Cancelado").ToString(), "Cancelado"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Ejecutado").ToString(), "Ejecutado"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Entregado").ToString(), "Entregado"));
        ddlEstatus.Items.Add(new System.Web.UI.WebControls.ListItem(GetLocalResourceObject("Pendiente").ToString(), "Pendiente"));
    }

    private int numeroSemana()
    {
        DateTime fecha = DateTime.Parse(txtDesde.Text.Trim());
        int w = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, fecha.DayOfWeek);
        return w;
    }

    private int numeroYear()
    {
        DateTime fecha = DateTime.Parse(txtDesde.Text.Trim());
        return fecha.Year;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
        if (ddlPlanta.SelectedIndex != 0)
            parameters.Add("@planta", ddlPlanta.SelectedValue);
        if (ddlInvernadero.SelectedIndex != 0)
            parameters.Add("@invernadero", ddlInvernadero.SelectedValue);
        if (ddlGrower.SelectedIndex != 0)
            parameters.Add("@grower", ddlGrower.SelectedValue);
        if (ddlEstatus.SelectedIndex != 0)
            parameters.Add("@estatus", ddlEstatus.SelectedValue.ToString());
        if (!String.IsNullOrEmpty(txtDesde.Text.Trim()))
            parameters.Add("@desde", txtDesde.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("AmbasFechas").ToString() , Common.MESSAGE_TYPE.Error); 
            return;
        }
        if (!String.IsNullOrEmpty(txtHasta.Text.Trim()))
            parameters.Add("@hasta", txtHasta.Text.Trim());
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("AmbasFechas").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }

        if (DateTime.Parse(txtDesde.Text.Trim()).CompareTo(DateTime.Parse(txtHasta.Text.Trim())) == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("FechaIncoherente").ToString(), Common.MESSAGE_TYPE.Error);
        }

        parameters.Add("@year", numeroYear());
        parameters.Add("@semana", numeroSemana()); 

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_RepiterProgramaReporte", parameters, this.Session["connection"].ToString());


            if (dt.Rows.Count > 0)
            {
                ViewState["rpt"] = null;
                ViewState["rpt"] = dt;
                rpt.DataSource = dt;
                rpt.DataBind();
                btnExcel1.Visible = true;

                if (dt.Rows.Count > 4)
                {
                    btnExcel2.Visible = true;   
                }
            }

            else
            {
                ViewState["rpt"] = null;
                rpt.DataSource = null;
                rpt.DataBind();
                btnExcel1.Visible = false;
                btnExcel2.Visible = false; 
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void rpt_RowDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
           
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView GridView1 = (GridView)e.Item.FindControl("GridView1");
            var parameters = new Dictionary<string, object>();
            if (ddlPlanta.SelectedIndex != 0)
                parameters.Add("@planta", ddlPlanta.SelectedValue);
            
            parameters.Add("@invernadero", ((DataTable)ViewState["rpt"]).Rows[e.Item.ItemIndex]["idInvernadero"].ToString() == null ? "0" : ((DataTable)ViewState["rpt"]).Rows[e.Item.ItemIndex]["idInvernadero"].ToString());
            parameters.Add("@programa", ((DataTable)ViewState["rpt"]).Rows[e.Item.ItemIndex]["idProgramacionHeader"].ToString() == null ? "0" : ((DataTable)ViewState["rpt"]).Rows[e.Item.ItemIndex]["idProgramacionHeader"].ToString());

            if (!String.IsNullOrEmpty(txtDesde.Text.Trim()))
                parameters.Add("@desde", txtDesde.Text.Trim());
            if (!String.IsNullOrEmpty(txtHasta.Text.Trim()))
                parameters.Add("@hasta", txtHasta.Text.Trim());

            try
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ProgramaReporte", parameters, this.Session["connection"].ToString());
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
           
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = e.Row.Cells[4].Text.Replace("@", "<br />");
            e.Row.Cells[5].Text = e.Row.Cells[5].Text.Replace("@", "<br />");
        }//if
       
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        if (ViewState["rpt"] == null)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SinDatos").ToString(), Common.MESSAGE_TYPE.Error);
            return;
        }
        try
        {
            rpt.DataSource = ViewState["rpt"];
            rpt.DataBind();

            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //Page page = new Page();
            //HtmlForm form = new HtmlForm();
            //rpt.EnableViewState = false;

            //// Deshabilitar la validación de eventos, sólo asp.net 2
            //page.EnableEventValidation = false;
            //// Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            //page.DesignerInitialize();
            //page.Controls.Add(form);
            //form.Controls.Add(rpt);
            //page.RenderControl(htw);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel"; //vnd.
            //Response.AddHeader("Content-Disposition", "attachment; filename=Reporte.xls");           

            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = Encoding.Default;                     
            ////Response.Write("<table>");
            //Response.Write(sb.ToString());
            ////Response.Write("</table>");

            ////Response.Flush();
            ////Response.End();
            //Context.ApplicationInstance.CompleteRequest();
            //////////////////////////////////////////////////////////////////////////////////////////////////////



            // Now image in the pdf file
                string imageFilePath = Server.MapPath(".") + "\\imageReport\\naturesweet.png"; 
	            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
	 
	            //Resize image depend upon your need
                jpg.ScaleToFit(580f, 560f);
	 
	            //Give space before image
	            //jpg.SpacingBefore = 30f;
	 
	            //Give some space after the image
                //jpg.SpacingAfter = 1f;
	            jpg.Alignment = Element.ALIGN_LEFT;

                Paragraph paragraph = new Paragraph(GetLocalResourceObject("ltPlanta.Text").ToString() +": "+ ddlPlanta.SelectedItem );
                Paragraph paragraph1 = new Paragraph(GetLocalResourceObject("ltInvernadero.Text").ToString() +": " + ddlInvernadero.SelectedValue);
                Paragraph paragraph2 = new Paragraph(GetLocalResourceObject("ltProductor.Text").ToString()+ ": " + ddlGrower.SelectedValue);
                Paragraph paragraph3 = new Paragraph(GetLocalResourceObject("ltEstatus.Text").ToString()+ ": " + ddlEstatus.SelectedItem);
                Paragraph paragraph4 = new Paragraph(GetLocalResourceObject("ltCreadoDesde.Text").ToString() +": " + txtDesde.Text );
                Paragraph paragraph5 = new Paragraph(GetLocalResourceObject("ltHasta.Text").ToString() +": " + txtHasta.Text);
	 
	          	             
	        
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + GetLocalResourceObject("Reporte").ToString() + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //GridView1.RenderControl(hw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();
            rpt.EnableViewState = false;

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;
            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(rpt);
            page.RenderControl(hw);


            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            pdfDoc.Add(jpg);
            pdfDoc.Add(paragraph);
            pdfDoc.Add(paragraph1);
            pdfDoc.Add(paragraph2);
            pdfDoc.Add(paragraph3);
            pdfDoc.Add(paragraph4);
            pdfDoc.Add(paragraph5);

            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Context.ApplicationInstance.CompleteRequest();


            //////////////////////////////////////////////////////////////////////////////////////////////////////



            //System.Web.UI.Control ctl = this.rpt;
            //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=Excel.xls");
            //HttpContext.Current.Response.Charset = "UTF-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //ctl.EnableViewState = false;
            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            //// Deshabilitar la validación de eventos, sólo asp.net 2
            //page.EnableEventValidation = false;
            //// Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            //page.DesignerInitialize();
            //page.Controls.Add(form);
            //form.Controls.Add(ctl);
            //page.RenderControl(hw);

            //HttpContext.Current.Response.Write(tw.ToString());
            ////HttpContext.Current.Response.End();
            //Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

           
}