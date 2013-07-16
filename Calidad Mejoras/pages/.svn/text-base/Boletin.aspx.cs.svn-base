using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

public partial class pages_Boletin : BasePage
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

            ViewState["idBoletin"] = Convert.ToString(Request.QueryString["idBoletin"]) != null ? Convert.ToString(Request.QueryString["idBoletin"]) : "";
            ViewState["idPlanta"] = Convert.ToString(Request.QueryString["idPlanta"]) != null ? Convert.ToString(Request.QueryString["idPlanta"]) : "";
            limpiarCampos();
            this.cargaddlPlantas();
            cargaPlagas();

            if (!ViewState["idPlanta"].ToString().Equals("") )
            {
                ddlPlanta.SelectedValue = ViewState["idPlanta"].ToString();
                ddlPlanta.Enabled = false;
            }

            if (ViewState["idPlanta"].ToString().Equals("0"))
            {
                ddlPlanta.SelectedIndex = 0;
                ddlPlanta.Enabled = true;
            }


            cargaDatos();

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

    // -- Calendario --
    protected void RegistraDatePicker_Load(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (sender as TextBox);
            txt.Attributes.Add("readOnly", "true");
            StringBuilder builder = new StringBuilder();
            string clientId = txt.ClientID;
            builder.Append("$(function() {");
            builder.Append("$('#" + clientId + "').datepicker({dateFormat: 'mm/dd/yy'");
            builder.Append("});");
            builder.Append("});");
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), clientId, builder.ToString(), true);
        }catch(Exception ex)
        {
            Log.Error(ex);
        }
    }
    
    private void limpiarCampos()
    {
        txtNombre.Text = String.Empty;
        //txtDesde.Text = String.Empty;
        //txtHasta.Text = String.Empty;
        //txtEstatus.Text = String.Empty;
    }

    private void limpiarBuscador()
    {
        gdvResultados.DataSource = null;
        gdvResultados.DataBind();

        gdvSeleccionados.DataSource = null;
        gdvSeleccionados.DataBind();

        limpiarCosasDeSession();
    }

    private void limpiarCosasDeSession() 
    {
        ViewState["GridResultados"] = null;
        ViewState["GridResultadosFijos"] = null;

        ViewState["GridSeleccionados"] = null;
    }

    private void cargaDatos() 
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        if (ddlPlanta.SelectedIndex == 0) //no hay planta seleccionada
        {
            return;
        }
        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idBoletin", ViewState["idBoletin"].ToString() );
        if (ViewState["idPlanta"].ToString().Equals(""))
            parameters.Add("@idPlanta", ddlPlanta.SelectedValue);
        else
        {
            parameters.Add("@idPlanta", ViewState["idPlanta"].ToString());
            ddlPlanta.SelectedValue = ViewState["idPlanta"].ToString();
        }
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_BoletinChild", parameters, this.Session["connection"].ToString());
            gdvBoletin.DataSource = dt;
            gdvBoletin.DataBind();          
            ViewState["GridBoletin"] = dt;

            //campos de cabecera
            txtNombre.Text = dt.Rows.Count > 0 ? dt.Rows[0]["vNombreBoletinHeader"].ToString() : "";
           
        }catch(Exception EX)
        {
            Log.Error(EX); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + EX.Message, Common.MESSAGE_TYPE.Error);
        }
        
    }

    private void cargaPlagas() 
    {
        //filtro de busqueda
        var parameters = new Dictionary<string, object>(); 
        parameters.Clear();
        var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlPlaga", parameters, this.Session["connection"].ToString());
        ddlFiltro.DataValueField = "campoId";
        ddlFiltro.DataTextField = "campoNombre";
        ddlFiltro.DataSource = dt2;
        ddlFiltro.DataBind();
    
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
            ddlPlanta.Items.Insert(0, GetLocalResourceObject("Seleccione").ToString() );
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorPlanta").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }

    private void updateBoletin() 
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idBoletin", ViewState["idBoletin"].ToString());
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        //
        try
        {
            string status = DataAccess.executeStoreProcedureString("spr_UPDATE_BoletinEstado", parameters, this.Session["connection"].ToString());
            cargaDatos();
            //popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinStatus").ToString() + status + "\"", Common.MESSAGE_TYPE.Success);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinStatus").ToString() + "\""  + GetLocalResourceObject(status).ToString() + "\"", Common.MESSAGE_TYPE.Success);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }
    
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        } 
        try
        {

        limpiarBuscador();
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPlaga", ddlFiltro.SelectedValue);
        var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_QuimicosConPlaga", parameters, this.Session["connection"].ToString());
        gdvResultados.DataSource = dt;
        gdvResultados.DataBind();
        //Session["GridResultados"] = dt;
        ViewState["GridResultados"] = dt;
        ViewState["GridResultadosFijos"] = dt;

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void btnAddToBoletin_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        var parameters1 = new Dictionary<string, object>();
        parameters1.Add("@idBoletin", ViewState["idBoletin"].ToString());
        parameters1.Add("@idPlanta", ViewState["idPlanta"].ToString());

        int existe = DataAccess.executeStoreProcedureGetInt("spr_GET_EstaBoletinPublicado", parameters1, this.Session["connection"].ToString());
        if (existe == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinCambio").ToString(),Common.MESSAGE_TYPE.Warning);
            return;
        }

        if (gdvSeleccionados.Rows.Count <= 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectQuimico").ToString(), Common.MESSAGE_TYPE.Error); 
            return;
        }
        try
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idBoletin", ViewState["idBoletin"].ToString());
            //obtener los ids de los quimicos:
            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
            xmlString.AppendFormat("<{0}>", "Quimicos");
            for (int i = 0; i < gdvSeleccionados.Rows.Count; i++)
            {
                //no guardar repetidos:

                bool igual = false;
                //ver que no se guarde un quimico que ya este en el grid del programa
                if (gdvBoletin.DataSource != null || gdvBoletin.Rows.Count > 0)
                {
                    int x = 0;
                    igual = false;
                    foreach (GridViewRow rowX in gdvBoletin.Rows)
                    {
                        if (gdvBoletin.DataKeys[x].Value.ToString() == gdvSeleccionados.DataKeys[i].Value.ToString())
                        {
                            igual = true;
                            break;
                        }
                        x++;
                    }
                }
                //
                if (!igual)
                {
                    xmlString.AppendFormat("<cont>{0}", i);
                    xmlString.AppendFormat("<id>{0}</id></cont>", gdvSeleccionados.DataKeys[i].Value.ToString());
                }
            }

            //--ver si ya habia datos en el grid de programación, y si hay, guardarlos tambien, para conservar todo
            if (gdvBoletin.DataSource != null || gdvBoletin.Rows.Count > 0)
            {
                int i = 0;
                foreach (GridViewRow row in gdvBoletin.Rows)
                {
                    xmlString.AppendFormat("<cont>{0}", i);
                    xmlString.AppendFormat("<id>{0}</id></cont>", gdvBoletin.DataKeys[i].Value.ToString());
                    i++;
                }

            }


            xmlString.AppendFormat("</{0}>", "Quimicos");
            parameters.Add("@Quimicos", xmlString.ToString());
            try
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToBoletin", parameters, this.Session["connection"].ToString());
                gdvBoletin.DataSource = dt;
                gdvBoletin.DataBind();
                ViewState["GridBoletin"] = dt;
            }
            catch (Exception ex)
            {
                Log.Error(ex); 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message,
                                                           Common.MESSAGE_TYPE.Error);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
        //si no hay palnta, no guardar
        if (ddlPlanta.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectPlanta").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }

        //si no hay quimicos, no guardar
        if (gdvBoletin.Rows.Count < 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardarBoletin").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }
        //checar que no se guarde repetido


        //revisar que el boletin no este publicado, si lo está, no hacer cambios. Un boletin publicado no puede tener cambios.
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idBoletin", ViewState["idBoletin"].ToString());
        parameters.Add("@idPlanta", ViewState["idPlanta"].ToString());
        int existe = DataAccess.executeStoreProcedureGetInt("spr_GET_EstaBoletinPublicado", parameters, this.Session["connection"].ToString());
        if (existe == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardarBoletinPublicado").ToString(), Common.MESSAGE_TYPE.Warning);
            //popUpMessageControl1.setAndShowInfoMessage("Este Boletín se encuentra publicado. Un Boletín publicado no guarda cambios. Desactive el Boletín antes de realizar los cambios.", Common.MESSAGE_TYPE.Warning);
            return;
        }

        if (String.IsNullOrEmpty((txtNombre.Text).Trim() ))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NombreVersion").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        } 
        //checar que no se guarde repetido
        parameters.Clear();
        parameters.Add("@idBoletin", ViewState["idBoletin"].ToString());
        parameters.Add("@nomBoletin", (txtNombre.Text).Trim());
        parameters.Add("@planta", ddlPlanta.SelectedValue);
        existe = DataAccess.executeStoreProcedureGetInt("spr_GET_NombreBoletinPublicado", parameters, this.Session["connection"].ToString());
        if (existe == 1)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NombreVersionRepetido").ToString(), Common.MESSAGE_TYPE.Warning);
            //popUpMessageControl1.setAndShowInfoMessage("El nombre / versión para este Boletín ya existe. Ingrese uno diferente.", Common.MESSAGE_TYPE.Warning);
            return;
        }
        else
        {
            try
            {
            parameters.Clear();
            parameters.Add("@idPlanta", ddlPlanta.SelectedValue);
            parameters.Add("@idBoletin", ViewState["idBoletin"].ToString());
            parameters.Add("@nomBoletin", (txtNombre.Text).Trim());
            parameters.Add("@dInicio", "");// (txtDesde.Text).Trim());
            parameters.Add("@dHasta", "");// (txtDesde.Text).Trim()); (txtHasta.Text).Trim());
            parameters.Add("@user", Session["usernameCalidad"].ToString());

            //obtener los ids de los quimicos:
            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
            xmlString.AppendFormat("<{0}>", "Quimicos");
            for (int i = 0; i < gdvBoletin.Rows.Count; i++)
            {
                xmlString.AppendFormat("<cont>{0}", i);
                xmlString.AppendFormat("<id>{0}</id></cont>", gdvBoletin.DataKeys[i].Value.ToString());
            }
            xmlString.AppendFormat("</{0}>", "Quimicos");
            parameters.Add("@Quimicos", xmlString.ToString());


            ViewState["idBoletin"] = DataAccess.executeStoreProcedureGetInt("spr_INSERT_Boletin", parameters, this.Session["connection"].ToString());
            ViewState["idPlanta"] = ddlPlanta.SelectedValue;
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinGuardado").ToString(), Common.MESSAGE_TYPE.Success);
            }
            catch (Exception ex)
            {
                Log.Error(ex); 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
            }

            limpiarBuscador();
            cargaDatos();
        }
    }

    protected void btnActivar_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
        if (ddlPlanta.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectPlanta").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }
        else
            ViewState["idPlanta"] = ddlPlanta.SelectedValue;

        //primero guardar boletin
        if (ViewState["idBoletin"].ToString() == "-1" || ViewState["idBoletin"].ToString() == "" || ViewState["idBoletin"].ToString() == "0")
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinActivar").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }
        
        //revisar estado actual del boletin        
        var dt = (DataTable)ViewState["GridBoletin"];
        if (dt == null)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinActivar").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }
        if (dt.Rows[0]["iEstatusBoletinHeader"].ToString() == "True") //--esta activado, desactivar
        {
            updateBoletin();
        }
        else //esta desactivo, activar
        { 
            //revisar que no existe otro boletin activo
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idBoletin", ViewState["idBoletin"].ToString());
            parameters.Add("@idPlanta", ViewState["idPlanta"].ToString());

            int existe = DataAccess.executeStoreProcedureGetInt("spr_GET_BoletinPublicado", parameters, this.Session["connection"].ToString());
            if (existe == 1)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoletinActivarExistente").ToString(), Common.MESSAGE_TYPE.Warning);
                return;
            }
            else 
            {
                updateBoletin();
            }
        
        }
        
    }

    protected void imgDeleteNominee_Click(object sender, EventArgs e)
    {
        //ImageButton imgButton = (ImageButton)sender;
        //GridViewRow row = (GridViewRow)imgButton.NamingContainer;

        //if (row.RowIndex >= 0)
        //{
        //    //lblIdNominee.Text = row.Cells[0].Text;
        //    lblIdNominee.Text = ((Label)row.FindControl("lblIDRowToDelete")).Text;
        //    mdlPopupConfirmationNomineeAdmin.Show();

        //}

    }

    #region seleccionar del grid de los buscadores
    protected void gdvSeleccionados_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["GridSeleccionados"] != null)
        {
            DataTable dt = (DataTable)ViewState["GridResultados"];
            DataRow row = dt.NewRow();
            row["ITEMNMBR"] = gdvSeleccionados.DataKeys[gdvSeleccionados.SelectedIndex].Value.ToString();
            row["ITEMDESC"] = gdvSeleccionados.SelectedRow.Cells[0].Text;
            dt.Rows.Add(row);
            gdvResultados.DataSource = dt;
            gdvResultados.DataBind();
            ViewState["GridResultados"] = dt;
            ViewState["GridResultadosFijos"] = dt;
        }       

        ////borrar el seleccionado 
        ((DataTable)ViewState["GridSeleccionados"]).Rows.Remove(
                                                                    (   (DataTable)ViewState["GridSeleccionados"]   ).Rows[gdvSeleccionados.SelectedIndex]
                                                                );
        DataTable dt2 = (DataTable)ViewState["GridSeleccionados"];
        gdvSeleccionados.DataSource = dt2;
        gdvSeleccionados.DataBind();
        
    }

    protected void gdvResultados_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["GridSeleccionados"] != null)
        {
            DataTable dt = (DataTable)ViewState["GridSeleccionados"];
            DataRow row = dt.NewRow();
            row["ITEMNMBR"] = gdvResultados.DataKeys[gdvResultados.SelectedIndex].Value.ToString();
            row["ITEMDESC"] = gdvResultados.SelectedRow.Cells[0].Text;
            dt.Rows.Add(row);
            gdvSeleccionados.DataSource = dt;
            gdvSeleccionados.DataBind();
            ViewState["GridSeleccionados"] = dt;
        }
        else
        {
            DataTable dt = (DataTable)ViewState["GridResultados"];           
            dt.Clear();            
            DataRow row = dt.NewRow();
            row["ITEMNMBR"] = gdvResultados.DataKeys[gdvResultados.SelectedIndex].Value.ToString();
            row["ITEMDESC"] = gdvResultados.SelectedRow.Cells[0].Text;
            dt.Rows.Add(row);
            gdvSeleccionados.DataSource = dt;
            gdvSeleccionados.DataBind();
            ViewState["GridSeleccionados"] = dt;
        }

        //borrar el seleccionado 
        ((DataTable)ViewState["GridResultadosFijos"]).Rows.Remove(((DataTable)ViewState["GridResultadosFijos"]).Rows[gdvResultados.SelectedIndex]);      
        DataTable dt2 = (DataTable)ViewState["GridResultadosFijos"];  
        gdvResultados.DataSource = dt2;
        gdvResultados.DataBind();       
        ViewState["GridResultados"] = dt2;        
    }
    #endregion

    #region formatos del grid
    protected void gdvResultados_PreRender(object sender, EventArgs e)
    {
        if (gdvResultados.HeaderRow != null)
            gdvResultados.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvResultados, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    protected void gdvSeleccionados_PreRender(object sender, EventArgs e)
    {
        if (gdvSeleccionados.HeaderRow != null)
            gdvSeleccionados.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvSeleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvSeleccionados, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }

    protected void gdvBoletin_PreRender(object sender, EventArgs e)
    {
        if (gdvBoletin.HeaderRow != null)
            gdvBoletin.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvBoletin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                
                try
                {
                    if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[0].Text).Trim()))
                    {


                        e.Row.Cells[0].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[0].Text)).ToString();
                    }
                    if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[12].Text).Trim()))
                    {


                        e.Row.Cells[12].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[12].Text)).ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                 
                //AGREGA EL MOUSE OVER A INDICACIONES
                ((HyperLink)e.Row.FindControl("textoIndicaciones")).Attributes.Add("onmouseover", "return overlib('" + ((HyperLink)e.Row.FindControl("textoIndicaciones")).Text.ToString() + "', ABOVE)");
                ((HyperLink)e.Row.FindControl("textoIndicaciones")).Attributes.Add("onmouseout", "return nd();");
                if (((HyperLink)e.Row.FindControl("textoIndicaciones")).Text.Length > 20)
                {
                    ((HyperLink)e.Row.FindControl("textoIndicaciones")).Text = ((HyperLink)e.Row.FindControl("textoIndicaciones")).Text.Substring(0, 20) + "...";
                }

                //e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvBoletin, ("Select$" + e.Row.RowIndex.ToString()));
                e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("@", "<br />");
                break;
        }

        
    }

    protected void gdvBoletin_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void gdvBoletin_RowDeleting(object sender, GridViewDeleteEventArgs e) 
    {
        
        ((DataTable)ViewState["GridBoletin"]).Rows.Remove(
                                                                   ((DataTable)ViewState["GridBoletin"]).Rows[e.RowIndex]
                                                               );
        DataTable dt = (DataTable)ViewState["GridBoletin"];
        gdvBoletin.DataSource = dt;
        gdvBoletin.DataBind();
    }
    #endregion

    #region poner los encabezados en el grid que no sirve
    //protected void gdvBoletin_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView oGridView = (GridView)sender;
    //        GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //        TableCell oTableCell = new TableCell();
    //        oTableCell.Text = "Nombre comercial";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Ingrediente activo";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Plagas que controla";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "EPA Ref #180";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);


    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Tolerancia EPA. ppm";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Intervalo de seguridad a cosecha";
    //        oTableCell.ColumnSpan = 2;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Tiempo de reentrada";
    //        oTableCell.ColumnSpan = 2;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Dosis";
    //        oTableCell.ColumnSpan = 2;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Unidad";
    //        oTableCell.ColumnSpan = 3;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Indicaciones";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Grupo químico";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Máxima aplicación";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "5";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Compatibilidad con abejorros.";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Persistencia dias.";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);
    //    }
    //}

    //protected void gdvBoletin_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView oGridView = (GridView)sender;
    //        GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //        TableCell oTableCell = new TableCell();
    //        oTableCell.Text = "Nombre comercial";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Ingrediente activo";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Plagas que controla";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "EPA Ref #180";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);


    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Tolerancia EPA. ppm";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Intervalo de seguridad a cosecha";
    //        oTableCell.ColumnSpan = 2;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Tiempo de reentrada";
    //        oTableCell.ColumnSpan = 2;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Dosis";
    //        oTableCell.ColumnSpan = 2;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Unidad";
    //        oTableCell.ColumnSpan = 3;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Indicaciones";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Grupo químico";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Máxima aplicación";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "5";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Compatibilidad con abejorros.";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);

    //        oTableCell = new TableCell();
    //        oTableCell.Text = "Persistencia dias.";
    //        oTableCell.ColumnSpan = 1;
    //        oGridViewRow.Cells.Add(oTableCell);
    //    }
    //}
    #endregion


   

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        // specified ASP.NET server control at run time.
        // No code required here.
    }
}