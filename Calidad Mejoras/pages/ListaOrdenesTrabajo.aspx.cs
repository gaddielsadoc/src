using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class pages_ListaOrdenesTrabajo : BasePage //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            popUpCapturaDosis.ResponseControlEventHandler += new RevisionEventHandler(ResponseControlEventHandler);
            popUpCapturaDosis.Visible = false;
           
            if (!IsPostBack)
            {
                DateTime thisDay = DateTime.Today;
                txtFechaAplicacion.Text = thisDay.ToString("yyyy-MM-dd");
                txtFechaAplicacion2.Text = thisDay.ToString("yyyy-MM-dd");
                chkAbierto.Checked = true;
                this.obtieneOrdenesTrabajo();
                this.obtieneSites();
                this.obtieneInvernaderos();
            }
        }
        catch(Exception x)
        {
            Log.Error(x); 
            string s = x.ToString(); //para ver el error
            //popUpMessageControl1.setAndShowInfoMessage("Error al cargar la página, intentelo de nuevo.", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaPaginaError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    protected void ResponseControlEventHandler(object sender, CustomEventArgs e)
    {
        if (e.success)
        {
            popUpMessageControl1.setAndShowInfoMessage(e.message, Common.MESSAGE_TYPE.Success);
            obtieneOrdenesTrabajo();
        }
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(e.message, Common.MESSAGE_TYPE.Error);
            obtieneOrdenesTrabajo();
        }
    }

    //select de SITES
    private void obtieneSites()
    {
        ddlSites.Items.Clear();
        ddlSites.Items.Add(new ListItem(GetLocalResourceObject("seleccione").ToString(), "-1" )); 
        //ddlSites.Items.Add(new ListItem("-Todos-", "-1"));
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        try
        {
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllSites", parameters, this.Session["connection"].ToString());
            ddlSites.DataSource = ds;
            ddlSites.DataValueField = "Farm";
            ddlSites.DataTextField = "Name";
            ddlSites.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaPlantasError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    //select de Invernaderos
    private void obtieneInvernaderos()
    {
        try
        {
            ddlInvernaderos.Items.Clear();
            //ddlInvernaderos.Items.Add(new ListItem("-Todos-", "-1"));
            ddlInvernaderos.Items.Add(new ListItem(GetLocalResourceObject("seleccione").ToString(), "-1")); 

            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllInvernaderos", parameters, this.Session["connection"].ToString());
            ddlInvernaderos.DataSource = ds;
            ddlInvernaderos.DataValueField = "Greenhouse";
            ddlInvernaderos.DataTextField = "Greenhouse";
            ddlInvernaderos.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaInvError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    private void obtieneOrdenesTrabajo()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DateTime fechaMin, fechaMax;

        if (!string.IsNullOrEmpty(txtFechaAplicacion.Text) && DateTime.TryParseExact(txtFechaAplicacion.Text.Trim(), "yyyy-MM-dd", null, DateTimeStyles.None, out fechaMin))
        {
            parameters.Add("@fechaAplicacionMin", fechaMin);
        }
        else
        {
            parameters.Add("@fechaAplicacionMin", null );
        }

        if (!string.IsNullOrEmpty(txtFechaAplicacion2.Text) && DateTime.TryParseExact(txtFechaAplicacion2.Text.Trim(), "yyyy-MM-dd", null, DateTimeStyles.None, out fechaMax))
        {
            parameters.Add("@fechaAplicacionMax", fechaMax);
        }
        else
        {
            parameters.Add("@fechaAplicacionMax", null );
        }
        parameters.Add("@planta", (ddlSites.SelectedValue != "-1" && ddlSites.SelectedValue != "") ? ddlSites.SelectedValue : null);
        parameters.Add("@invernadero", (ddlInvernaderos.SelectedValue != "-1" && ddlInvernaderos.SelectedValue != "") ? ddlInvernaderos.SelectedValue : null);

        parameters.Add("@estadoAbierto", chkAbierto.Checked ? 1 : 0);
        parameters.Add("@estadoPendiente", chkPendiente.Checked ? 1 : 0);
        parameters.Add("@estadoEntregado", chkEntregado.Checked ? 1 : 0);
        parameters.Add("@estadoCancelado", chkCancelado.Checked ? 1 : 0);
        
        try
        {
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectOrdenesTrabajo", parameters, this.Session["connection"].ToString());
            gvOrdenTrabajo.DataSource = ds;
            gvOrdenTrabajo.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void Actualizar(object sender, EventArgs e)
    {
        obtieneOrdenesTrabajo();
    }


    protected void gvOrdenTrabajo_PreRender(object sender, EventArgs e)
    {
        if (gvOrdenTrabajo.HeaderRow != null)
            gvOrdenTrabajo.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    
    //agrega el event click a cada row
    protected void gvOrdenTrabajo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                var dataView = ((DataRowView)e.Row.DataItem).DataView;
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvOrdenTrabajo,
                                                                        ("Select$" +
                                                                        e.Row.RowIndex.ToString()));
                break;
        }
    }

    /*
     * AL DAR CLICK SALGA LA VENTANITA PARA EDITAR DOSIS
     */
    protected void gvOrdenTrabajo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var dataKey = gvOrdenTrabajo.DataKeys[gvOrdenTrabajo.SelectedIndex];
            if (dataKey != null)
                Session["IdOTCookie"] = dataKey.Value.ToString();
            popUpCapturaDosis.Visible = true;
            popUpCapturaDosis.showPopup();
         

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
         
    }

    protected void gvOrdenTrabajo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //validar que tenga confuguracion de capacidad de carga
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idFarm", ddlSites.SelectedValue);
        ddlInvernaderos.Enabled = true;
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderos", parameters, this.Session["connection"].ToString());
            ddlInvernaderos.Items.Clear();
            ListItem l = new ListItem("--Todos--", "-1");
            ddlInvernaderos.Items.Add(l);
            ddlInvernaderos.DataSource = dt;
            ddlInvernaderos.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar los invernaderos: " + ex.Message, Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaInvError").ToString()), Common.MESSAGE_TYPE.Error);
        }
        
    }

}
