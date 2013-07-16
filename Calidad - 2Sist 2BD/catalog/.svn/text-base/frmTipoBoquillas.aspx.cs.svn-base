using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class catalog_frmTipoBoquillas : BasePage// System.Web.UI.Page
{
       
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneTiposBoquilla();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void obtieneTiposBoquilla()
    {
        try
        {

            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllTiposBoquillas", parameters,this.Session["connection"].ToString());
            gvTipoBoquilla.DataSource = ds;
            gvTipoBoquilla.DataBind();
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

        if (txtNombre.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("required"), Common.MESSAGE_TYPE.Error);
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

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteTipoBoquilla", find, this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("exist"), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        String Rs = DataAccess.executeStoreProcedureString("spr_InsertTipoBoquilla", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                        {
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("exist"), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("saveIt"), txtNombre.Text), Common.MESSAGE_TYPE.Success);
                        }
                    }
                }
                else
                {
                    if (Session["idTipoBoquillaCookie"] == null || Session["idTipoBoquillaCookie"].ToString() == "")
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGRL02"), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        parameters.Add("@IdTipoBoquilla", Session["idTipoBoquillaCookie"].ToString());
                        String Rs = DataAccess.executeStoreProcedureString("spr_UpdateTipoBoquilla", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("error"))
                        {
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("nochanges"), Common.MESSAGE_TYPE.Info);
                        }
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("saved"), Common.MESSAGE_TYPE.Success);
                        }

                    }
                }
                obtieneTiposBoquilla();
                VolverAlPanelInicial();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            }
        }

    }

    protected void gvTipoBoquilla_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdTipoBoquillaCookie"] = gvTipoBoquilla.DataKeys[gvTipoBoquilla.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IdTipoBoquilla", Session["IdTipoBoquillaCookie"]);
        try
        {
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromTipoBoquillaId", parameters, this.Session["connection"].ToString());
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
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }


    protected void gvTipoBoquilla_PreRender(object sender, EventArgs e)
    {
        if (gvTipoBoquilla.HeaderRow != null)
            gvTipoBoquilla.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    protected void gvTipoBoquilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsTipoBoquilla"])
            {
                DataSet ds = ViewState["dsTipoBoquilla"] as DataSet;

                if (ds != null)
                {
                    gvTipoBoquilla.DataSource = ds;
                    gvTipoBoquilla.DataBind();
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

    protected void gvTipoBoquilla_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTipoBoquilla, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvTipoBoquilla.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new PostBackOptions(gvTipoBoquilla, "Select$" + i.ToString()));
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
        gvTipoBoquilla.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }

}