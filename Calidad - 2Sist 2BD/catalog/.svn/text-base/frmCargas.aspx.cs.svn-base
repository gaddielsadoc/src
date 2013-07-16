using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class catalog_frmCargas : BasePage //System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneCargas();
                this.obtienePlantas();
                this.obtieneEquipoAplicacion();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }


    private void obtieneCargas()
    {
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllCargas", parameters, this.Session["connection"].ToString());
            gvCarga.DataSource = ds;
            gvCarga.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage("Error al intentar cargar los datos", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaDatosError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

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
            //ddlEquipoAplicacion.Items.Insert(0, new ListItem("-Seleccione-", "-1"));
            ddlEquipoAplicacion.Items.Insert(0, new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage("Error al intentar cargar el Equipo de Aplicación", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaEquipoError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    private void obtienePlantas()
    {
        ddlPlanta.Items.Clear();
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllSites", parameters, this.Session["connection"].ToString());
            ddlPlanta.DataSource = ds;
            ddlPlanta.DataValueField = "Farm";
            ddlPlanta.DataTextField = "Name";
            ddlPlanta.DataBind();
            //ddlPlanta.Items.Insert(0, new ListItem("-Seleccione-", "-1"));
            ddlPlanta.Items.Insert(0, new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1" ));
            ddlPlanta.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaPlantasError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }



    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        VolverAlPanelInicial();
    }

    protected void Guardar_Actualizar(object sender, EventArgs e)
    {

        if (ddlPlanta.SelectedValue.Equals("-1"))
        {
            //popUpMessageControl1.setAndShowInfoMessage("La planta es requerida.", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("PlantaRequerida").ToString()), Common.MESSAGE_TYPE.Error);
        }
        else if (txtCapacidad.Text.Trim().Equals(""))
        {
            //popUpMessageControl1.setAndShowInfoMessage("El campo capacidad es requerido.", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CapacidadRequerida").ToString()), Common.MESSAGE_TYPE.Error);
        }
        else if (ddlEquipoAplicacion.Text.Trim().Equals("-1"))
        {
            //popUpMessageControl1.setAndShowInfoMessage("El campo equipo aplicacion es requerido.", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("EquipoRequerido").ToString()), Common.MESSAGE_TYPE.Error);
        }
        else
        {
            try
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                
                parameters.Add("@idPlanta", ddlPlanta.SelectedValue);
                if (!ddlEquipoAplicacion.Text.Trim().Equals("-1"))
                    parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
                parameters.Add("@capacidad", txtCapacidad.Text);
                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);

                if (Accion.Value == "Añadir")
                {
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@idPlanta", ddlPlanta.SelectedValue);
                    find.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteCarga", find, this.Session["connection"].ToString()) > 0)
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios debido a que el registro ya existe.", Common.MESSAGE_TYPE.Info);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("DatoExisteError").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        String Rs = DataAccess.executeStoreProcedureString("spr_InsertCarga", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("Ese registro ya había sido capturado.", Common.MESSAGE_TYPE.Error);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("DatoError").ToString()), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("El nuevo volumen de agua se guardó exitosamente.", Common.MESSAGE_TYPE.Success);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("GuardarExito").ToString()), Common.MESSAGE_TYPE.Success);
                            VolverAlPanelInicial();
                        }
                    }
                }
                else
                {
                    if (Session["idCargaCookie"] == null || Session["idCargaCookie"].ToString() == "")
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL02: Se perdió la información del ID actual", Common.MESSAGE_TYPE.Error);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("IDInfoError").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        parameters.Add("@IdCarga", Session["idCargaCookie"].ToString());
                        String Rs = DataAccess.executeStoreProcedureString("spr_UpdateCarga", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("error"))
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios, intentelo de nuevo.", Common.MESSAGE_TYPE.Info);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("NoCambios").ToString()), Common.MESSAGE_TYPE.Info);
                        }
                        else
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("Cambios realizados.", Common.MESSAGE_TYPE.Success);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CambiosExito").ToString()), Common.MESSAGE_TYPE.Success);
                        }

                    }
                }
                VolverAlPanelInicial();
                this.obtieneCargas();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                //popUpMessageControl1.setAndShowInfoMessage("Error al intentar Guardar", Common.MESSAGE_TYPE.Error);
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("gueadarError").ToString()), Common.MESSAGE_TYPE.Error);
            }

        }

    }


    protected void gvCarga_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdCargaCookie"] = gvCarga.DataKeys[gvCarga.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@idCarga", Session["IdCargaCookie"]);
        DataTable dt = null; ;

        try
        {
            dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromCargaId", parameters, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage("Error al seleccionar un registro", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("SelectError").ToString()), Common.MESSAGE_TYPE.Error);
        }

        if (dt.Rows.Count > 0)
        {

            ddlPlanta.SelectedValue = dt.Rows[0]["idPlanta"].ToString();
            if (dt.Rows[0]["idEquipoAplicacion"].ToString().Trim() != "")
            {
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString().Trim();
            }
            else
            {
                ddlEquipoAplicacion.SelectedValue = "-1";
            }
            txtCapacidad.Text = dt.Rows[0]["capacidad"].ToString().Trim();
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
    }

    protected void gvCarga_PreRender(object sender, EventArgs e)
    {
        if (gvCarga.HeaderRow != null)
            gvCarga.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvCarga_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvCarga, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }


    protected void VolverAlPanelInicial()
    {
        ddlPlanta.SelectedValue = "-1";
        ddlEquipoAplicacion.SelectedValue = "-1";
        txtCapacidad.Text = String.Empty;
        chkActivo.Checked = true;
        Accion.Value = "Añadir";
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }


}