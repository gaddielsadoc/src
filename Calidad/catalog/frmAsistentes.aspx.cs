using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class catalog_frmAsistentes : BasePage
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
            ltAsistentes.Visible = false; 
            obtieneGrowers();
            cargaDatos();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }   
    }

    private void cargaDatos()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        try
        {
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_AsistidoAsistentes", parameters, this.Session["connection"].ToString());
            gdvAsistentes.DataSource = ds;
            gdvAsistentes.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    private void obtieneGrowers()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        try{
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlGrowersXAdministrador", parameters, this.Session["connection"].ToString());
            ddlGrowers.AppendDataBoundItems = true;
            ddlGrowers.Items.Add(GetLocalResourceObject("seleccione").ToString() );
            ddlGrowers.DataTextField = "nombre";
            ddlGrowers.DataValueField = "idUsuario";
            ddlGrowers.DataSource = ds;
            ddlGrowers.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected void ddlGrowers_SelectedIndexChanged(object sender, EventArgs e)
    {
        ckListAsistentes.Items.Clear();
        ltAsistentes.Visible = false;

        if (ddlGrowers.SelectedIndex == 0)
        {
            return;
        }
        traerAsistentes();
    }

    private void  limpiar()
    {
        ddlGrowers.SelectedIndex = 0;
        ckListAsistentes.Items.Clear();
        ltAsistentes.Visible = false;
    }

    private void traerAsistentes()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@grower", ddlGrowers.SelectedValue.ToString());
        try
        {
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ListadoAsistentes", parameters, this.Session["connection"].ToString());
            ckListAsistentes.DataTextField = "nombre";
            ckListAsistentes.DataValueField = "idUsuario";
            ckListAsistentes.DataSource = ds;
            ckListAsistentes.DataBind();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                ltAsistentes.Visible = true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    #region botones
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        limpiar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string asistentes = String.Empty;
        foreach ( ListItem item in ckListAsistentes.Items)
        {
            if (item.Selected)
            asistentes += item.Value + "|";
        }

        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@grower", ddlGrowers.SelectedValue.ToString());
        parameters.Add("@asistentes", asistentes);
        try
        {
            DataAccess.executeStoreProcedureNonQuery ("spr_INSERT_AsistidoAsistentes", parameters, this.Session["connection"].ToString());
            limpiar();
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SaveIt").ToString(), Common.MESSAGE_TYPE.Success);
            cargaDatos();

        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorEnInsert").ToString(), Common.MESSAGE_TYPE.Warning); 
        }
    }
    #endregion

    #region gridview
    protected void gdvAsistentes_PreRender(object sender, EventArgs e)
    {
        if (gdvAsistentes.HeaderRow != null)
            gdvAsistentes.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvAsistentes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                try
                {

                    e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvAsistentes, ("Select$" + e.Row.RowIndex.ToString())); 
                    if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[1].Text).Trim()))
                    {
                        e.Row.Cells[1].Text = (e.Row.Cells[1].Text).Replace("@", "<br/>");
                    }
                }
                catch (Exception ex)
                {

                }
                break;
        } 
    }

    protected void gdvAsistentes_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlGrowers.Items.FindByValue(gdvAsistentes.SelectedDataKey.Value.ToString()) != null)
        {
            ddlGrowers.SelectedValue = gdvAsistentes.SelectedDataKey.Value.ToString();
            traerAsistentes();
            //marcar asistentes
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@grower", ddlGrowers.SelectedValue.ToString());
            try
            {
                DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_AsistentesXAsistido", parameters, this.Session["connection"].ToString());
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ckListAsistentes.Items.FindByValue(row[0].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

        }
    }
   
    #endregion

    
}