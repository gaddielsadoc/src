using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class catalog_frmRazonesCambio : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack) return;
            if (Session["usernameCalidad"] == null)
            {
                Response.Redirect("~/frmLogin.aspx", false);
            }
            obtienePlantas();
            cargaDatos();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
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
            ddlPlanta.Items.Insert(0, new ListItem(GetLocalResourceObject("Seleccione").ToString(), "-1"));
            ddlPlanta.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargaPlantasError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaDatos()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@user", String.IsNullOrEmpty(Session["usernameCalidad"].ToString()) ? "0" : Session["usernameCalidad"].ToString());
        try
        {
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_RazonesCambio", parameters, this.Session["connection"].ToString());
            gvRazon.DataSource = ds;
            gvRazon.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorCargarDatos").ToString(), Common.MESSAGE_TYPE.Warning);
        }
    }

    private void Limpiar()
    {
        txtRazon.Text = String.Empty;
        chkActivo.Checked = true;
        hdIdRazon.Value = "0";
        ddlPlanta.SelectedIndex = 0;

        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }

    #region botones
    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        Limpiar();
    }

    
    protected void Guardar_Actualizar(object sender, EventArgs e)
    {
        if(String.IsNullOrEmpty( txtRazon.Text.ToString().Trim()))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Razonrequerida").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }
        if (ddlPlanta.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Plantarequerida").ToString(), Common.MESSAGE_TYPE.Warning);
            return;
        }

        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IDrazon", hdIdRazon.Value  );
        parameters.Add("@planta", ddlPlanta.SelectedValue.ToString());
        parameters.Add("@razon", txtRazon.Text.Trim() );
        parameters.Add("@activo", chkActivo.Checked ? true : false);
        try
        {
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_INSERT_RazonesCambio", parameters, this.Session["connection"].ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Guardado").ToString(), Common.MESSAGE_TYPE.Success);
            Limpiar();
            cargaDatos();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorGuradar").ToString(), Common.MESSAGE_TYPE.Warning);
        }

    }   
    #endregion

    #region grid
    protected void gvRazon_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdIdRazon.Value = gvRazon.DataKeys[gvRazon.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@idrazon", hdIdRazon.Value);
        DataTable dt = null; ;

        try
        {
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_RazonesCambio", parameters, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("ErrorCargarDatos").ToString()), Common.MESSAGE_TYPE.Error);
        }

        if (dt.Rows.Count > 0)
        {
            if (ddlPlanta.Items.FindByValue(dt.Rows[0]["Farm"].ToString()) != null)
                ddlPlanta.SelectedValue = dt.Rows[0]["Farm"].ToString();
            txtRazon.Text = dt.Rows[0]["vRazon"].ToString();
            if (dt.Rows[0]["bActivo"].ToString().Equals("True"))
                chkActivo.Checked = true;
            else
                chkActivo.Checked = false;
            btnActualizar.Visible = true;
            btnCancel.Visible = true;
            btnLimpiar.Visible = false;
            btnSave.Visible = false;
        }
    }

    protected void gvRazon_PreRender(object sender, EventArgs e)
    {
        if (gvRazon.HeaderRow != null)
            gvRazon.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvRazon_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvRazon, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
#endregion
}