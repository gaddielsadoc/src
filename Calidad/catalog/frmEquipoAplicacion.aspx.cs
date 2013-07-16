using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class catalog_frmEquipoAplicacion : BasePage //System.Web.UI.Page
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
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void obtieneTiposAplicacion()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllEquipoAplicaciones", parameters, this.Session["connection"].ToString());
        gvEquipoAplicacion.DataSource = ds;
        gvEquipoAplicacion.DataBind();
    }


    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        VolverAlPanelInicial();
    }


    protected void Guardar_Actualizar(object sender, EventArgs e)
    {

        if (txtNombre.Text.Trim().Equals(""))
        {
            //popUpMessageControl1.setAndShowInfoMessage("El campo nombre es requerido.", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("NombreRequerido").ToString()), Common.MESSAGE_TYPE.Error);
        }
        else
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

                if (DataAccess.executeStoreProcedureGetInt("spr_ExisteEquipoAplicacion", find, this.Session["connection"].ToString()) > 0)
                {
                    //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios debido a que el tipo de Aplicacion ya existe.", Common.MESSAGE_TYPE.Info);
                    popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("YaExiste").ToString()), Common.MESSAGE_TYPE.Error);
                }
                else
                {
                    String Rs = DataAccess.executeStoreProcedureString("spr_InsertEquipoAplicacion", parameters, this.Session["connection"].ToString());
                    if (Rs.Equals("Repetido"))
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("Ese registro ya había sido capturado.", Common.MESSAGE_TYPE.Error);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("yaExiste2").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("El Modulo \"" + txtNombre.Text + "\" se guardó exitosamente.", Common.MESSAGE_TYPE.Success);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("EquipoGuardado"), txtNombre.Text.ToString()), Common.MESSAGE_TYPE.Success);
                    }
                }
            }
            else
            {
                if (Session["idEquipoAplicacionCookie"] == null || Session["idEquipoAplicacionCookie"].ToString() == "")
                {
                    //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL02: Se perdió la información del ID actual", Common.MESSAGE_TYPE.Error);
                    popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("RGRL02").ToString()), Common.MESSAGE_TYPE.Error);
                }
                else
                {
                    parameters.Add("@IdEquipoAplicacion", Session["idEquipoAplicacionCookie"].ToString());
                    String Rs = DataAccess.executeStoreProcedureString("spr_UpdateEquipoAplicacion", parameters, this.Session["connection"].ToString());
                    if (Rs.Equals("error"))
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios intentelo de nuevo.", Common.MESSAGE_TYPE.Info);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("NoCambios").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("Cambios realizados.", Common.MESSAGE_TYPE.Success);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CambiosExito").ToString()), Common.MESSAGE_TYPE.Success);
                    }

                }
            }
            obtieneTiposAplicacion();
            VolverAlPanelInicial();

        }

    }

    protected void gvEquipoAplicacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdEquipoAplicacionCookie"] = gvEquipoAplicacion.DataKeys[gvEquipoAplicacion.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IdEquipoAplicacion", Session["IdEquipoAplicacionCookie"]);
        DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromEquipoAplicacionId", parameters, this.Session["connection"].ToString());
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


    protected void gvEquipoAplicacion_PreRender(object sender, EventArgs e)
    {
        if (gvEquipoAplicacion.HeaderRow != null)
            gvEquipoAplicacion.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    protected void gvEquipoAplicacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsEquipoAplicacion"])
            {
                DataSet ds = ViewState["dsEquipoAplicacion"] as DataSet;

                if (ds != null)
                {
                    gvEquipoAplicacion.DataSource = ds;
                    gvEquipoAplicacion.DataBind();
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

    protected void gvEquipoAplicacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvEquipoAplicacion, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }


    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvEquipoAplicacion.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new PostBackOptions(gvEquipoAplicacion, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }


    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtNombre.Text = "";
        txtDescripcion.Text = "";
        chkActivo.Checked = true;
        gvEquipoAplicacion.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }

}