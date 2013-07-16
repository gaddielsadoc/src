using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class catalog_TipoQuimico : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneTiposQuimico();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);            
        }
    }

    //hace el select y lo mete al ds
    private void obtieneTiposQuimico()
    {
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllTipoQuimicos", parameters, this.Session["connection"].ToString());
            gvTipoQuimico.DataSource = ds;
            gvTipoQuimico.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("ErrorInterno"), Common.MESSAGE_TYPE.Error);
        }
    }

    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        VolverAlPanelInicial();
    }

    protected void Guardar_Actualizar(object sender, EventArgs e)
    {

        if (txtTipoQuimico.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("NotEmptyField"), Common.MESSAGE_TYPE.Error);

        }
        else
        {
            try
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@nombre", txtTipoQuimico.Text);
                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);


                if (Accion.Value == "Añadir")
                {
                    //Verificar que el valor "Medida" a insertar no estan anteriormente agregados
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@nombre", txtTipoQuimico.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteTipoQuimico", find, this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("tipoQuimicoExist"), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        String Rs = DataAccess.executeStoreProcedureString("spr_InsertaTipoQuimico", parameters, this.Session["connection"].ToString());
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("saveIt"),txtTipoQuimico.Text), Common.MESSAGE_TYPE.Success);
                    }
                }
                else
                {
                    if (Session["IdTipoQuimicoCookie"] == null || Session["IdTipoQuimicoCookie"].ToString() == "")
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGRL02"), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        parameters.Add("@IdTipoQuimico", Session["IdTipoQuimicoCookie"].ToString());
                        String Rs = DataAccess.executeStoreProcedureString("spr_UpdateTipoQuimico", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("error"))
                        {
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("NoChanges"), Common.MESSAGE_TYPE.Info);
                        }
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("UpdateIt"), Common.MESSAGE_TYPE.Success);
                        }

                    }
                }
                obtieneTiposQuimico();
                VolverAlPanelInicial();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            }

        }

    }

    //grid view click
    protected void gvTipoQuimico_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdTipoQuimicoCookie"] = gvTipoQuimico.DataKeys[gvTipoQuimico.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IdTipoQuimico", Session["IdTipoQuimicoCookie"]);
        DataTable dt = null;

        try
        {
            dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromTipoQuimicoId", parameters, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
        if (dt.Rows.Count > 0)
        {

            txtTipoQuimico.Text = dt.Rows[0]["nombre"].ToString().Trim();
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
    protected void gvTipoQuimico_PreRender(object sender, EventArgs e)
    {
        if (gvTipoQuimico.HeaderRow != null)
            gvTipoQuimico.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
    protected void gvTipoQuimico_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsMedida"])
            {
                DataSet ds = ViewState["dsMedida"] as DataSet;

                if (ds != null)
                {
                    gvTipoQuimico.DataSource = ds;
                    gvTipoQuimico.DataBind();
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
    protected void gvTipoQuimico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTipoQuimico, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvTipoQuimico.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new PostBackOptions(gvTipoQuimico, "Select$" + i.ToString()));
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
        txtTipoQuimico.Text = "";
        chkActivo.Checked = true;
        gvTipoQuimico.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }


}