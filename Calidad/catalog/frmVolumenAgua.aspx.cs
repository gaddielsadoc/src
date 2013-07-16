using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class catalog_frmVolumenAgua : BasePage //System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddlTipoBoquilla.Enabled = false;
                ddlEquipoAplicacion.Enabled = false;
                txtEdad.Enabled = false;
                txtVolumen.Enabled = false;
                chkCapar.Enabled = false;
                chkFin.Enabled = false;

                this.obtieneVolumenDeAgua();
                this.obtieneSites();
                //this.obtieneTiposAplicacion();
                this.obtieneTiposBoquilla();
                this.obtieneEquipoAplicacion();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void obtieneVolumenDeAgua()
    {
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllVolumenesAgua", parameters, this.Session["connection"].ToString());
            gvVolumenAgua.DataSource = ds;
            gvVolumenAgua.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }
    //select sites
    private void obtieneSites()
    {
        ddlSite.Items.Clear();
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllSites", parameters, this.Session["connection"].ToString());
            ddlSite.DataSource = ds;
            ddlSite.DataValueField = "Farm";
            ddlSite.DataTextField = "Name";
            ddlSite.DataBind();
            ddlSite.Items.Insert(0, new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
            ddlSite.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    //select tipos de boquilla
    private void obtieneTiposBoquilla()
    {
        try
        {
            ddlTipoBoquilla.Items.Clear();
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectActivoTiposBoquillas", parameters, this.Session["connection"].ToString());
            ddlTipoBoquilla.DataSource = ds;
            ddlTipoBoquilla.DataValueField = "idTipoBoquilla";
            ddlTipoBoquilla.DataTextField = "nombre";
            ddlTipoBoquilla.DataBind();
            ddlTipoBoquilla.Items.Insert(0, new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }
    //select tipos de aplicacion
    //private void obtieneTiposAplicacion()
    //{
    //    ddlTipoAplicacion.Items.Clear();
    //    try
    //    {
    //        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
    //        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectActivoTiposAplicaciones", parameters);
    //        ddlTipoAplicacion.DataSource = ds;
    //        ddlTipoAplicacion.DataValueField = "idTipoAplicacion";
    //        ddlTipoAplicacion.DataTextField = "nombre";
    //        ddlTipoAplicacion.DataBind();
    //        ddlTipoAplicacion.Items.Insert(0, new ListItem("-Seleccione-", "-1"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error(ex);
    //        popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
    //    }
    //}

    //select equipos de aplicacion
    private void obtieneEquipoAplicacion()
    {
        ddlEquipoAplicacion.Items.Clear();
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectActivoEquipoAplicaciones", parameters, this.Session["connection"].ToString());
            ddlEquipoAplicacion.DataSource = ds;
            ddlEquipoAplicacion.DataValueField = "idEquipoAplicacion";
            ddlEquipoAplicacion.DataTextField = "nombre";
            ddlEquipoAplicacion.DataBind();
            ddlEquipoAplicacion.Items.Insert(0, new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
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

    protected void Guardar_Actualizar(object sender, EventArgs e)
    {

        if (ddlSite.SelectedValue.Equals("-1"))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("FarmRequired").ToString(), Common.MESSAGE_TYPE.Error);
        }
        if (txtVolumen.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("VAguaRequired").ToString(), Common.MESSAGE_TYPE.Error);
        }
        else
        {
            try
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                if (ddlSite.SelectedValue == "-1")
                    parameters.Add("@idSite", "");
                else
                    parameters.Add("@idSite", ddlSite.SelectedValue);
                if (ddlTipoBoquilla.SelectedValue == "-1")
                    parameters.Add("@idTipoBoquilla", "");
                else
                    parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
                    parameters.Add("@idTipoAplicacion", "");
                if (ddlEquipoAplicacion.SelectedValue == "-1")
                    parameters.Add("@idEquipoAplicacion", "");
                else
                    parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
                parameters.Add("@volumen", txtVolumen.Text);
                parameters.Add("@edad", txtEdad.Text);
                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);

                if (chkCapar.Checked)
                    parameters.Add("@capar", 1);
                else
                    parameters.Add("@capar", 0);
                if (chkFin.Checked)
                    parameters.Add("@fin", 1);
                else
                    parameters.Add("@fin", 0);

                if (Accion.Value == "Añadir")
                {
                    //ESTE IF ES EN CASO DE QUE SE AÑADA REGISTRO NUEVO
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    if (ddlSite.SelectedValue == "-1")
                        find.Add("@idSite", "");
                    else
                        find.Add("@idSite", ddlSite.SelectedValue);
                    if (ddlTipoBoquilla.SelectedValue == "-1")
                        find.Add("@idTipoBoquilla", "");
                    else
                        find.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
                        find.Add("@idTipoAplicacion", "");
                    if (ddlEquipoAplicacion.SelectedValue == "-1")
                        find.Add("@idEquipoAplicacion", "");
                    else
                        find.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

                    find.Add("@semana", txtEdad.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteVolumenAgua", find, this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("exist").ToString(), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        String Rs = DataAccess.executeStoreProcedureString("spr_InsertVolumenAgua", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                        {
                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("captured").ToString(), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SaveIt").ToString(), Common.MESSAGE_TYPE.Success);
                            VolverAlPanelInicial();
                        }
                    }
                }
                else
                {
                    if (Session["idVolumenAguaCookie"] == null || Session["idVolumenAguaCookie"].ToString() == "")
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("RGRL02").ToString(), Common.MESSAGE_TYPE.Error);
                        VolverAlPanelInicial();
                    }
                    else
                    {
                        //EN ACTUALIZAR CHECAMOS QUE EL REGISTRO NO EXISTA
                        Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                        find.Add("@idVolumenAgua", Session["idVolumenAguaCookie"].ToString());
                        if (ddlSite.SelectedValue == "-1")
                            find.Add("@idSite", "");
                        else
                            find.Add("@idSite", ddlSite.SelectedValue);
                        if (ddlTipoBoquilla.SelectedValue == "-1")
                            find.Add("@idTipoBoquilla", "");
                        else
                            find.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
                        find.Add("@idTipoAplicacion", "");

                        if (ddlEquipoAplicacion.SelectedValue == "-1")
                            find.Add("@idEquipoAplicacion", "");
                        else
                            find.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
                        find.Add("@semana", txtEdad.Text);

                        if (DataAccess.executeStoreProcedureGetInt("spr_ExisteVolumenAgua", find, this.Session["connection"].ToString()) > 0)
                        {
                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("exist").ToString(), Common.MESSAGE_TYPE.Info);
                        }
                        else
                        {
                            parameters.Add("@IdVolumenAgua", Session["idVolumenAguaCookie"].ToString());
                            String Rs = DataAccess.executeStoreProcedureString("spr_UpdateVolumenAgua", parameters, this.Session["connection"].ToString());
                            if (Rs.Equals("error"))
                            {
                                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("noChanges").ToString(), Common.MESSAGE_TYPE.Info);
                            }
                            else
                            {
                                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Saved").ToString(), Common.MESSAGE_TYPE.Success);
                                VolverAlPanelInicial();
                            }
                        }

                    }
                }
                obtieneVolumenDeAgua();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            }

        }

    }

    protected void gvVolumenAgua_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdVolumenAguaCookie"] = gvVolumenAgua.DataKeys[gvVolumenAgua.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IdVolumenAgua", Session["IdVolumenAguaCookie"]);
        DataTable dt = null; ;

        try
        {
            dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromVolumenAguaId", parameters, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }

        if (dt.Rows.Count > 0)
        {
            string idTipoBoquilla;
            //string idTipoAplicacion;
            string idEquipoAplicacion;

            if (dt.Rows[0]["idTipoBoquilla"].ToString() == "0")
            {
                idTipoBoquilla = "-1";
            }
            else
            {
                idTipoBoquilla = dt.Rows[0]["idTipoBoquilla"].ToString().Trim();
            }

            //if (dt.Rows[0]["idTipoAplicacion"].ToString() == "0")
            //{
            //    idTipoAplicacion = "-1";
            //}
            //else
            //{
            //    idTipoAplicacion = dt.Rows[0]["idTipoAplicacion"].ToString().Trim();
            //}

            if (dt.Rows[0]["idEquipoAplicacion"].ToString() == "0")
            {
                idEquipoAplicacion = "-1";
            }
            else
            {
                idEquipoAplicacion = dt.Rows[0]["idEquipoAplicacion"].ToString().Trim();
            }
            ddlSite.SelectedValue = dt.Rows[0]["IDSITE"].ToString();
            ddlTipoBoquilla.SelectedValue = idTipoBoquilla;
            //ddlTipoAplicacion.SelectedValue = idTipoAplicacion;
            ddlEquipoAplicacion.SelectedValue = idEquipoAplicacion;
            txtVolumen.Text = dt.Rows[0]["volumen"].ToString().Trim();
            txtEdad.Text = dt.Rows[0]["edad"].ToString().Trim();
            if (dt.Rows[0]["activo"].ToString().Equals("True"))
                chkActivo.Checked = true;
            else
                chkActivo.Checked = false;

            if (dt.Rows[0]["capar"].ToString().Equals("True"))
                chkCapar.Checked = true;
            else
                chkCapar.Checked = false;
            if (dt.Rows[0]["fin"].ToString().Equals("True"))
                chkFin.Checked = true;
            else
                chkFin.Checked = false;

            Accion.Value = "Guardar Cambios";
            btnActualizar.Visible = true;
            btnCancel.Visible = true;
            btnLimpiar.Visible = false;
            btnSave.Visible = false;

            abilitarCampos();

        }
    }


    protected void gvVolumenAgua_PreRender(object sender, EventArgs e)
    {
        if (gvVolumenAgua.HeaderRow != null)
            gvVolumenAgua.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    protected void gvVolumenAgua_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsVolumenAgua"])
            {
                DataSet ds = ViewState["dsVolumenAgua"] as DataSet;

                if (ds != null)
                {
                    gvVolumenAgua.DataSource = ds;
                    gvVolumenAgua.DataBind();
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

    protected void gvVolumenAgua_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvVolumenAgua, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvVolumenAgua.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new PostBackOptions(gvVolumenAgua, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }


    protected void abilitarCampos() 
    {
        ddlTipoBoquilla.Enabled = false;
        ddlEquipoAplicacion.Enabled = false;
        txtEdad.Enabled = false;
        txtVolumen.Enabled = false;
        chkCapar.Enabled = false;
        chkFin.Enabled = false;
        
        //colima
        if (ddlSite.SelectedValue == "5")
        {
            txtEdad.Enabled = true;
            txtVolumen.Enabled = true;
        }

        //tux
        else if (ddlSite.SelectedValue == "4")
        {
            ddlTipoBoquilla.Enabled = true;
            txtVolumen.Enabled = true;
        }

        //nay
        else if (ddlSite.SelectedValue == "8")
        {
            txtEdad.Enabled = true;
            ddlEquipoAplicacion.Enabled = true;
            chkCapar.Enabled = true;
            chkFin.Enabled = true;
            txtVolumen.Enabled = true;
        }

        //sis zap cdi 
        else if (ddlSite.SelectedValue == "2" || ddlSite.SelectedValue == "6" || ddlSite.SelectedValue == "7")
        {
            ddlTipoBoquilla.Enabled = true;
            ddlEquipoAplicacion.Enabled = true;
            txtVolumen.Enabled = true;
        }

        else //plantas de EU que no tengo configuradas
        {
            ddlTipoBoquilla.Enabled = true;
            ddlEquipoAplicacion.Enabled = true;
            txtEdad.Enabled = true;
            txtVolumen.Enabled = true;
            chkCapar.Enabled = true;
            chkFin.Enabled = true;
        }
    
    }
    protected void VolverAlPanelInicial()
    {
        ddlSite.SelectedValue = "-1";
        ddlTipoBoquilla.SelectedValue = "-1";
        //ddlTipoAplicacion.SelectedValue = "-1";
        ddlEquipoAplicacion.SelectedValue = "-1";
        txtVolumen.Text = String.Empty;
        txtEdad.Text = String.Empty;
        chkActivo.Checked = true;
        chkCapar.Checked = false;
        chkFin.Checked = false;
        Accion.Value = "Añadir";
        gvVolumenAgua.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }



    protected void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiar
        ddlTipoBoquilla.SelectedValue = "-1";       
        ddlEquipoAplicacion.SelectedValue = "-1";
        txtVolumen.Text = String.Empty;
        txtEdad.Text = String.Empty;
        chkActivo.Checked = true;
        chkCapar.Checked = false;
        chkFin.Checked = false;


        abilitarCampos();
    
    }

    public int resultado { get; set; }
    
    
}