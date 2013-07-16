using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;

public partial class Administration_frmRol : BasePage
{
    #region Eventos Pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneRoles();
            }
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }
    protected void btnSaveRol_Click(object sender, EventArgs e)
    {
        try
        {


            if (txtRol.Text.Trim().Equals(""))
            {
                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("campoRequerido"), Common.MESSAGE_TYPE.Error);
            }
            else
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                DataSet ds = null;

                parameters.Add("@rolName", txtRol.Text);
                if (chkRolActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);


                if (Accion.Value == "Añadir")
                {
                    //Verificar que el valor "Rol" a insertar no estan anteriormente agregados
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@rolName", txtRol.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExisteRol", find, this.Session["connection"].ToString()) > 0)
                    {
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("noChangesMade"), Common.MESSAGE_TYPE.Info);
                    }
                    else
                    {
                        ds = DataAccess.executeStoreProcedureDataSet("spr_InsertRol", parameters, this.Session["connection"].ToString());
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGRL03"), Common.MESSAGE_TYPE.Error);
                        else
                        {
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("rolSaved"), txtRol.Text), Common.MESSAGE_TYPE.Success);

                        }
                    }
                }
                else
                {
                    if (Session["IdRolCookie"] == null || Session["IdRolCookie"].ToString() == "")
                        popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGRL02"), Common.MESSAGE_TYPE.Error);
                    else
                    {
                        parameters.Add("@IdRol", Session["IdRolCookie"].ToString());
                        ds = DataAccess.executeStoreProcedureDataSet("spr_UpdateRol", parameters, this.Session["connection"].ToString());
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("RGRL01"), Common.MESSAGE_TYPE.Error);
                        else
                        {
                            String Resultado = ds.Tables[0].Rows[0]["Resultado"].ToString();
                            if (Resultado.Equals("Existe"))
                                popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("noChangesMade"), Common.MESSAGE_TYPE.Info);
                            else
                                if (Resultado.Equals("Update"))
                                    popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("changesMade"), Common.MESSAGE_TYPE.Success);
                        }
                    }
                }
                obtieneRoles();
                VolverAlPanelInicial();

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected void btnCancelRol_Click(object sender, EventArgs e)
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

    protected void gvRol_PreRender(object sender, EventArgs e)
    {
        if (gvRol.HeaderRow != null)
            gvRol.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvRol_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvRol, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    protected void gvRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["IdRolCookie"] = gvRol.DataKeys[gvRol.SelectedIndex].Value.ToString();
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@IdRol", Session["IdRolCookie"]);
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromRolId", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                txtRol.Text = dt.Rows[0]["rol"].ToString().Trim();
                if (dt.Rows[0]["activo"].ToString().Equals("True"))
                    chkRolActivo.Checked = true;
                else
                    chkRolActivo.Checked = false;
                Accion.Value = "Guardar Cambios";
                btnActualizar.Visible = true;
                btnCancelRol.Visible = true;
                btnLimpiar.Visible = false;
                btnSaveRol.Visible = false;
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

    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvRol.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new System.Web.UI.PostBackOptions(gvRol, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex) {
            Log.Error(ex.ToString());
        }
    }
    #endregion

    private void obtieneRoles()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_ObtieneRoles", parameters, this.Session["connection"].ToString());
        ViewState["dsRoles"] = ds;
        gvRol.DataSource = ds;
        gvRol.DataBind();
    }
    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtRol.Text = "";
        chkRolActivo.Checked = true;
        gvRol.Enabled = true;
        btnActualizar.Visible = false;
        btnCancelRol.Visible = false;
        btnLimpiar.Visible = true;
        btnSaveRol.Visible = true;
    }
   
    
    
    
    
   
    
    

    
}