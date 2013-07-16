using System;
using System.Collections.Generic;
using System.Data;


public partial class catalog_frmMargenError : BasePage// System.Web.UI.Page
{
        
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtieneMargenes();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    private void obtieneMargenes()
    {
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_MargenesError", parameters, this.Session["connection"].ToString());

        
        if (dt.Rows.Count > 0)
        {
            txtDesgaste.Text = dt.Rows[0]["error_boquillas"].ToString().Trim();
            txtSobrante.Text = dt.Rows[0]["error_sobranteSolucion"].ToString().Trim();
        }
        else
        {
            popUpMessageControl1.setAndShowInfoMessage("Error la base de datos no contiene registros.", Common.MESSAGE_TYPE.Error);
        }
    }


    protected void Guardar_Actualizar(object sender, EventArgs e)
    {
        if (txtSobrante.Text.Trim().Equals("") || txtDesgaste.Text.Trim().Equals(""))
        {
            popUpMessageControl1.setAndShowInfoMessage("Ambos datos son requeridos.", Common.MESSAGE_TYPE.Error);
        }
        else
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            parameters.Add("@sobrante", txtSobrante.Text);
            parameters.Add("@boquilla", txtDesgaste.Text);

            DataAccess.executeStoreProcedureDataSet("spr_UpdateMargenesError", parameters, this.Session["connection"].ToString());

            obtieneMargenes();
            VolverAlPanelInicial();

        }

    }

    protected void Cancelar_Limpiar(object sender, EventArgs eventArgs)
    {
        VolverAlPanelInicial();
    }

    protected void habilitaEdicion(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnHabilita.Visible = false;
        btnLimpiar.Visible = true;

        txtDesgaste.Enabled = true;
        txtSobrante.Enabled = true;
    }

    protected void VolverAlPanelInicial()
    {
        btnSave.Visible = false;
        btnHabilita.Visible = true;
        btnLimpiar.Visible = false;

        txtDesgaste.Enabled = false;
        txtSobrante.Enabled = false;

    }
}

