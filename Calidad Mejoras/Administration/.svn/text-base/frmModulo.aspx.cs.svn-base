using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;

public partial class Administration_frmModulo : BasePage
{    
    #region Eventos de Pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneModulo();
            }
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }

    protected void Guardar_Actualizar(object sender, EventArgs e)
    {
        try
        {
            int order;
            if (txtModulo.Text.Trim().Equals(""))
            {
                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("ModuloRequerido"), Common.MESSAGE_TYPE.Error);
            }
            else if (!int.TryParse(txtOrden.Text, out order)) {
                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("errorOrden"), Common.MESSAGE_TYPE.Error);
            }
            else
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@modulo", txtModulo.Text);
                parameters.Add("@ruta", txtRuta.Text);
                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);

                if (Accion.Value == "Añadir")
                {
                    //Verificar que el valor "Modulo" a insertar no estan anteriormente agregados
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@modulo", txtModulo.Text);
                    find.Add("@ruta", txtRuta.Text);
                    find.Add("@orden", txtOrden.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteModulo", find, this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("NotChangesExist"), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_InsertModulo", parameters, this.Session["connection"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("ElModulo") + "\" " + txtModulo.Text + "\" " + (string)GetLocalResourceObject("saveIt"), Common.MESSAGE_TYPE.Success);
                        }
                    }
                }
                else
                {
                    if (Session["IdModuloCookie"] == null || Session["IdModuloCookie"].ToString() == "")
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("lostID"), Common.MESSAGE_TYPE.Error);
                    else
                    {
                        parameters.Add("@IdModulo", Session["IdModuloCookie"].ToString());
                        parameters.Add("@orden", txtOrden.Text);
                        String Rs = DataAccess.executeStoreProcedureString("spr_UpdateModulo", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("NotChangesExist"), Common.MESSAGE_TYPE.Info);
                        else
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("Cambiosrealizados"), Common.MESSAGE_TYPE.Success);

                    }
                }
                obtieneModulo();
                VolverAlPanelInicial();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
   
    protected void Cancelar_Limpiar(object sender, EventArgs e)
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

    protected void gvModulo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {                
            case DataControlRowType.DataRow:
                //e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(_select, string.Empty);
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvModulo, "Select$" + e.Row.RowIndex); 
                break;
        }
    }

    protected void gvModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {


            Session["IdModuloCookie"] = gvModulo.DataKeys[gvModulo.SelectedIndex].Value.ToString();
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@IdModulo", Session["IdModuloCookie"]);
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromModuloId", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtModulo.Text = dt.Rows[0]["modulo"].ToString().Trim();
                txtRuta.Text = dt.Rows[0]["ruta"].ToString().Trim();
                txtOrden.Text = dt.Rows[0]["orden"].ToString().Trim();
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
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected void gvModulo_PreRender(object sender, EventArgs e)
    {
        if (gvModulo.HeaderRow != null)
            gvModulo.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvModulo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsModulo"])
            {
                DataSet ds = ViewState["dsModulo"] as DataSet;

                if (ds != null)
                {
                    gvModulo.DataSource = ds;
                    gvModulo.DataBind();
                }
            }
            ((GridView)sender).PageIndex = e.NewPageIndex;
            ((GridView)sender).DataBind();
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
            for (int i = 0; i < gvModulo.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new System.Web.UI.PostBackOptions(gvModulo, "Select$" + i.ToString()));
            }
            base.Render(writer);

        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    #endregion

    #region Aux Methods
    private void obtieneModulo()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllModulo", parameters, this.Session["connection"].ToString());
        ViewState["dsModulo"] = ds;
        gvModulo.DataSource = ds;
        gvModulo.DataBind();
    }
   
    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtModulo.Text = "";
        txtRuta.Text = "";
        txtOrden.Text = "";
        chkActivo.Checked = true;
        gvModulo.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }
    #endregion
}