using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;

public partial class Administration_frmSubModulo : BasePage
{
    #region Eventos Pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                this.obtieneSubModulosGv();
                this.obtieneModulos();               
            }
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSubM.Text.Trim().Equals("") || txtRuta.Text.Trim().Equals(""))
            {
                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("allFieldsRequired") , Common.MESSAGE_TYPE.Error);
            }
            else
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@idModulo", ddlModulo.SelectedItem.Value);
                if (!string.IsNullOrEmpty(ddlSunMP.SelectedValue))
                    parameters.Add("@IdSubModuloParent", ddlSunMP.SelectedItem.Value);
                parameters.Add("@subModulo", txtSubM.Text);
                parameters.Add("@ruta", txtRuta.Text);
                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);


                if (Accion.Value == "Añadir")
                {
                    //Verificar que el valor "Modulo" a insertar no estan anteriormente agregados
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@idModulo", ddlModulo.SelectedItem.Value);
                    find.Add("@subModulo", txtSubM.Text);
                    find.Add("@ruta", txtRuta.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteSubModulo", find, this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("alreadyExist"), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_InsertSubModulo", parameters, this.Session["connection"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("exito"), txtSubM.Text), Common.MESSAGE_TYPE.Success);
                        }
                    }
                }
                else
                {
                    if (Session["IdSubMCookie"] == null || Session["IdSubMCookie"].ToString() == "")
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGRL02"), Common.MESSAGE_TYPE.Error);
                    else
                    {
                        parameters.Add("@IdSubModulo", Session["IdSubMCookie"].ToString());
                        String Rs = DataAccess.executeStoreProcedureString("spr_updateSubModulo", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("alreadyExist"), Common.MESSAGE_TYPE.Info);
                        else
                            if (Rs.Equals("Success"))
                                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("Cambiosrealizados"), Common.MESSAGE_TYPE.Success);
                            else
                                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGUPSM01"), Common.MESSAGE_TYPE.Success);

                    }
                }
                obtieneSubModulosGv();
                VolverAlPanelInicial();

            }
        }
        catch (Exception ex) {
            Log.Error(ex.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
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
    protected void gvSubM_PreRender(object sender, EventArgs e)
    {
        if (gvSubM.HeaderRow != null)
            gvSubM.HeaderRow.TableSection = TableRowSection.TableHeader;
    }    
    protected void gvSubM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSubM, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    protected void gvSubM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["IdSubMCookie"] = gvSubM.DataKeys[gvSubM.SelectedIndex].Value.ToString();
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@IdSubModulo", Session["IdSubMCookie"]);
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromSubModuloId", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {

                txtSubM.Text = dt.Rows[0]["subModulo"].ToString().Trim();
                txtRuta.Text = dt.Rows[0]["ruta"].ToString().Trim();
                ddlModulo.SelectedValue = dt.Rows[0]["idModulo"].ToString().Trim();
                inicializaDDLS(ddlModulo.SelectedValue);
                ListItem item = ddlSunMP.Items.FindByValue(Session["IdSubMCookie"].ToString());
                if (null != item)
                {
                    ddlSunMP.Items.Remove(item);
                }
                try
                {
                    ddlSunMP.SelectedValue = dt.Rows[0]["idSubModuloParent"].ToString().Trim();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }


                if (dt.Rows[0]["activo"].ToString().Equals("True"))
                    chkActivo.Checked = true;
                else
                    chkActivo.Checked = false;
                Accion.Value = "Guardar Cambios";
                btnActualizar.Visible = true;
                btnCancel.Visible = true;
                btnLimpiar.Visible = false;
                btnSave.Visible = false;
            }
            else
            {
                //No se encontró el registro
            }
        }
        catch (Exception ex) {
            Log.Error(ex.ToString());
        }
    }
    protected void ddlSunMP_DataBound(object sender, EventArgs e)
    {
        try
        {
            ListItem none = new ListItem(GetLocalResourceObject("Ninguno").ToString(), string.Empty);
            ddlSunMP.Items.Insert(0, none);
            ddlSunMP.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
        
    }
    protected void ddlModulo_DataBound(object sender, EventArgs e)
    {
        try
        {
            ListItem none = new ListItem(GetLocalResourceObject("Seleccione").ToString(), string.Empty);
            ddlModulo.Items.Insert(0, none);
            ddlModulo.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            inicializaDDLS(ddlModulo.SelectedValue);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    protected override void Render(HtmlTextWriter writer)
    {
        for (int i = 0; i < gvSubM.Rows.Count; i++)
        {
            Page.ClientScript.RegisterForEventValidation(new System.Web.UI.PostBackOptions(gvSubM, "Select$" + i.ToString()));
        }
        base.Render(writer);
    }
    #endregion

    private void obtieneModulos()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@activo", 1);
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllModulo", parameters, this.Session["connection"].ToString());
        ddlModulo.DataSource = ds;
        ddlModulo.DataTextField = "modulo";
        ddlModulo.DataValueField = "idModulo";
        ddlModulo.DataBind();
    }
    private void obtieneSubModulosGv()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllSubModulos", parameters, this.Session["connection"].ToString());
        ViewState["dsSubMes"] = ds;
        gvSubM.DataSource = ds;
        gvSubM.DataBind();
    }
    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtSubM.Text = "";
        txtRuta.Text = "";
        ddlModulo.SelectedIndex = 0;
        ddlSunMP.SelectedIndex = 0;
        chkActivo.Checked = true;
        gvSubM.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }
    protected void inicializaDDLS(String id)
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();        
        parameters.Add("@idModulo", id);
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectSubModuloByIdModulo", parameters, this.Session["connection"].ToString());
        DataTable dt = ds.Tables[0].Clone();
        string indent = string.Empty;
        foreach (DataRow item in ds.Tables[0].Select("idSubModuloParent is null"))
        {
            item["subModulo"] = indent + item["subModulo"];
            dt.ImportRow(item);
            addChilds(item, dt, ds, 1);
        }

        ddlSunMP.DataSource = dt;
        ddlSunMP.DataBind();
    }
    private void addChilds(DataRow item, DataTable dtOut, DataSet dsIn, int level)
    {        
        int idSubModulo;
        idSubModulo = (int)item["idSubModulo"];

        foreach (DataRow childItem in dsIn.Tables[0].Select("idSubModuloParent = " + idSubModulo))
        {
            string indent = string.Empty;
            for (int i = 0; i < level; i++)
            {
                indent = indent + Server.HtmlDecode("&nbsp;&#8226;");
            }
            indent = indent + Server.HtmlDecode("&nbsp;");
            childItem["subModulo"] = indent + childItem["subModulo"];
            dtOut.ImportRow(childItem);
            addChilds(childItem, dtOut, dsIn, level + 1);
        }
    }
    
}