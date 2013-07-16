using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;


public partial class catalog_frmTipoAplicacion : BasePage// System.Web.UI.Page
{
        
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneTiposAplicacion();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void obtieneTiposAplicacion()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllTiposAplicaciones", parameters,this.Session["connection"].ToString());
        gvTipoAplicacion.DataSource = ds;
        gvTipoAplicacion.DataBind();
    }


    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        VolverAlPanelInicial();
    }


    protected void Guardar_Actualizar(object sender, EventArgs e)
    {

        if (txtNombre.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("nameRequired").ToString(), Common.MESSAGE_TYPE.Error);
        }
        else
        {
            try
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@nombre", txtNombre.Text);
                parameters.Add("@descripcion", txtDescripcion.Text);
                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);

                if (Accion.Value == "Añadir")
                {
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@nombre", txtNombre.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteTipoAplicacion", find,this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("exist").ToString(), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        String Rs = DataAccess.executeStoreProcedureString("spr_InsertTipoAplicacion", parameters,this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                        {
                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("exist").ToString(), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage(string.Format(GetLocalResourceObject("saveIt").ToString(),txtNombre.Text), Common.MESSAGE_TYPE.Success);
                        }
                    }
                }
                else
                {
                    if (Session["idTipoAplicacionCookie"] == null || Session["idTipoAplicacionCookie"].ToString() == "")
                    {
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("RGRL02").ToString(), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        parameters.Add("@IdTipoAplicacion", Session["idTipoAplicacionCookie"].ToString());
                        String Rs = DataAccess.executeStoreProcedureString("spr_UpdateTipoAplicacion", parameters,this.Session["connection"].ToString());
                        if (Rs.Equals("error"))
                        {
                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("nochanges").ToString(), Common.MESSAGE_TYPE.Info);
                        }
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("saved").ToString(), Common.MESSAGE_TYPE.Success);
                        }

                    }
                }
                obtieneTiposAplicacion();
                VolverAlPanelInicial();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            }
        }

    }

    protected void gvTipoAplicacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdTipoAplicacionCookie"] = gvTipoAplicacion.DataKeys[gvTipoAplicacion.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IdTipoAplicacion", Session["IdTipoAplicacionCookie"]);
        try
        {
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromTipoAplicacionId", parameters,this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtNombre.Text = dt.Rows[0]["nombre"].ToString().Trim();
                txtDescripcion.Text = dt.Rows[0]["descripcion"].ToString().Trim();
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
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }


    protected void gvTipoAplicacion_PreRender(object sender, EventArgs e)
    {
        if (gvTipoAplicacion.HeaderRow != null)
            gvTipoAplicacion.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    protected void gvTipoAplicacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsTipoAplicacion"])
            {
                DataSet ds = ViewState["dsTipoAplicacion"] as DataSet;

                if (ds != null)
                {
                    gvTipoAplicacion.DataSource = ds;
                    gvTipoAplicacion.DataBind();
                }
            }
            ((GridView)sender).PageIndex = e.NewPageIndex;
            ((GridView)sender).DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }

    }

    protected void gvTipoAplicacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTipoAplicacion, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvTipoAplicacion.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new PostBackOptions(gvTipoAplicacion, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }


    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtNombre.Text = "";
        txtDescripcion.Text = "";
        chkActivo.Checked = true;
        gvTipoAplicacion.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }

}