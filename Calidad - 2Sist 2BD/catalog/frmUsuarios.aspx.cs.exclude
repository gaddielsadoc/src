using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class catalog_frmUsuarios : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["usernameCalidad"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx", false);
                }

                ViewState["idBoletin"] = Convert.ToString(Request.QueryString["idBoletin"]) != null ? Convert.ToString(Request.QueryString["idBoletin"]) : "-1";
                LimpiarCampos(); 
                cargaDatos();
            }

            else if (Session["usernameCalidad"] == null)
            {
                popUpMessageControl1.setAndShowInfoMessage("Su sesión ha expirado. Por favor, refresque la página", Common.MESSAGE_TYPE.Warning);
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }          
    }

    private void cargaDatos()
    {
        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@id", null);
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_ObtieneUsuarios", parameters, this.Session["connection"].ToString());
            gvUsuarios.DataSource = dt;
            gvUsuarios.DataBind();            
        }
        catch (Exception EX)
        {
            Log.Error(EX); 
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + EX.Message, Common.MESSAGE_TYPE.Error);
        }
       
    }

    private void LimpiarCampos(){
        txtnombre.Text = String.Empty;
        //txtpass.Text = String.Empty;
        txtUser.Text = String.Empty;
        hdID.Value = "0";
    }
           
    #region botones
    protected void btnSaveUser_Click(object sender, EventArgs e)
    {
        //if (txtConf.Text != txtpass.Text) 
        //{
        //    popUpMessageControl1.setAndShowInfoMessage("Las contraseñas no coinciden.", Common.MESSAGE_TYPE.Error);
        //    return;
        //}
        if (txtnombre.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage("El campo Nombre es requerido.", Common.MESSAGE_TYPE.Error);
            return;
        }
        if (txtUser.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage("El campo Usuario es requerido.", Common.MESSAGE_TYPE.Error);
            return;
        }
        //if (txtpass.Text.Trim().Equals(""))
        //{
        //    popUpMessageControl1.setAndShowInfoMessage("El campo Contraseña es requerido.", Common.MESSAGE_TYPE.Error);
        //    return;
        //}

        var parameters = new Dictionary<string, object>();
        DataTable ds = null;

        //verificar que el nombre de usuario no exista en otro registro
        parameters.Add("@id", Int32.Parse(hdID.Value.ToString()));
        parameters.Add("@cuenta", (txtUser.Text).Trim());

        try
        {
            if ((ds = DataAccess.executeStoreProcedureDataTable("spr_ExisteUsuario", parameters, this.Session["connection"].ToString())).Rows.Count > 0)
            {
                popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios debido a que el nombre de usuario pertenece a otro registro.", Common.MESSAGE_TYPE.Info);
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + ex.Message, Common.MESSAGE_TYPE.Info);
        }

        //guardar
        parameters.Add("@nombre", (txtnombre.Text).Trim());
        //parameters.Add("@pass", (txtpass.Text).Trim());

        if (chkCorreoActivo.Checked)
            parameters.Add("@activo", 1);
        else
            parameters.Add("@activo", 0);

        //ds.Clear();
        try
        {
            ds = DataAccess.executeStoreProcedureDataTable("spr_GuardaUsuarioAdmin", parameters, this.Session["connection"].ToString());
            popUpMessageControl1.setAndShowInfoMessage("El Usuario \"" + txtUser.Text + "\" se guardó exitosamente.", Common.MESSAGE_TYPE.Success);

        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + ex.Message, Common.MESSAGE_TYPE.Info);
        }

        cargaDatos();
        LimpiarCampos(); 
    }

    protected void btnCancelUser_Click(object sender, EventArgs e)
    {
        try
        {
            LimpiarCampos();
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }

    }
    #endregion

    #region formatos del grid
    protected void gvUsuarios_PreRender(object sender, EventArgs e)
    {
        if (gvUsuarios.HeaderRow != null)
            gvUsuarios.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsuarios, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
       
    #endregion
    protected void gvUsuarios_SelectedIndexChanged1(object sender, EventArgs e)
    {
        LimpiarCampos();
        hdID.Value = gvUsuarios.DataKeys[gvUsuarios.SelectedIndex].Value.ToString();
        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@id", Int32.Parse(hdID.Value.ToString() ));
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_ObtieneUsuarios", parameters, this.Session["connection"].ToString());
            txtnombre.Text = dt.Rows[0]["nombre"].ToString();
            //txtpass.Text = String.Empty;// dt.Rows[0]["pass"].ToString();
            txtUser.Text = dt.Rows[0]["usuario"].ToString();

            if (dt.Rows[0]["activo"].ToString().Equals("True"))
                chkCorreoActivo.Checked = true;
            else
                chkCorreoActivo.Checked = false;
        }
        catch (Exception EX)
        {
            Log.Error(EX); 
            popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos: " + EX.Message, Common.MESSAGE_TYPE.Error);
        }

    }
}