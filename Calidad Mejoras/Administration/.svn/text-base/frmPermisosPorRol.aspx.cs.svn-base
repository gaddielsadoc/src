using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;

public partial class Administration_frmPermisosPorRol : BasePage
{
    
    #region Eventos Pagina
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtieneRoles();
                ObtieneModulos();
            }
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            var pathList = string.Empty;
            foreach (ListItem item in listaSecciones.Items)
            {
                if (item.Selected)
                {
                    pathList += item.Value + '|';
                }
            }
            int idRol, idModulo;
            int.TryParse(ddlRol.SelectedValue, out idRol);
            int.TryParse(ddlModulo.SelectedValue, out idModulo);

            guardaAsignacionPermisos(idRol, idModulo, pathList);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ObtieneSecciones();
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }

    protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ObtieneSecciones();
        }
        catch (Exception exception)
        {
            Log.Error(exception.ToString());
        }
    }

    #endregion

    #region Auxiliares
    private void ObtieneRoles()
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("activo", true);
        var ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllRol", parameters, this.Session["connection"].ToString());
        ddlRol.Items.Add(new ListItem((string)GetLocalResourceObject("Seleccione"), string.Empty));
        ddlRol.DataSource = ds;
        ddlRol.DataTextField = "rol";
        ddlRol.DataValueField = "idRol";
        ddlRol.DataBind();
    }
    private void ObtieneModulos()
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("activo", true);
        var ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllModulo", parameters, this.Session["connection"].ToString());
        ddlModulo.Items.Add(new ListItem((string)GetLocalResourceObject("Seleccione"), string.Empty));
        ddlModulo.DataSource = ds;
        ddlModulo.DataTextField = "modulo";
        ddlModulo.DataValueField = "idModulo";
        ddlModulo.DataBind();
    }
    private void ObtieneSecciones()
    {
        listaSecciones.Items.Clear();
        if (string.IsNullOrEmpty(ddlRol.SelectedValue) || string.IsNullOrEmpty(ddlModulo.SelectedValue))
        {            
            return;
        }
        var parameters = new Dictionary<string, object>();
        parameters.Add("activo", true);
        parameters.Add("idModulo", ddlModulo.SelectedValue);
        parameters.Add("idRol", ddlRol.SelectedValue);

        var ds = DataAccess.executeStoreProcedureDataTable("spr_SelectSubModulos", parameters, this.Session["connection"].ToString());

        //DataTable dt = ds.Clone();
        string indent = string.Empty;
        foreach (DataRow item in ds.Select("idSubModuloParent is null"))
        {
            //item["subModulo"] = indent + item["subModulo"];
            ListItem listItem = new ListItem();
            listItem.Text = item["subModulo"].ToString();
            listItem.Value = item["idSubModulo"].ToString();
            listaSecciones.Items.Add(listItem);
            //dt.ImportRow(item);
            addChilds(item, listaSecciones, ds, 1);
        }

        //listaSecciones.DataSource = dt;
        listaSecciones.DataTextField = "subModulo";
        listaSecciones.DataValueField = "idSubModulo";
        listaSecciones.DataBind();

        DataRow[] pathsPermitidos = ds.Select("tienePermiso = 1");
        foreach (var pathsPermitido in pathsPermitidos)
        {
            ListItem item = listaSecciones.Items.FindByValue(pathsPermitido["idSubModulo"].ToString());
            if (null != item)
            {
                item.Selected = true;
            }
        }
    }

    private void addChilds(DataRow item, CheckBoxList dtOut, DataTable dsIn, int level)
    {
        int idSubModulo;
        idSubModulo = (int)item["idSubModulo"];

        foreach (DataRow childItem in dsIn.Select("idSubModuloParent = " + idSubModulo))
        {
            string indent = string.Empty;
            int padding = 20 * level;
            ListItem listItem = new ListItem();
            listItem.Attributes.CssStyle.Add("padding-left", padding + "px");
            listItem.Attributes.Add("class", "submodulo");
            listItem.Text = childItem["subModulo"].ToString();
            listItem.Value = childItem["idSubModulo"].ToString();
            listaSecciones.Items.Add(listItem);
            //for (int i = 0; i < level; i++)
            //{
            //    indent = indent + Server.HtmlDecode("&nbsp;&#8226;");
            //}
            //indent = indent + Server.HtmlDecode("&nbsp;");
            //childItem["subModulo"] = indent + childItem["subModulo"];
            //dtOut.ImportRow(childItem);
            addChilds(childItem, dtOut, dsIn, level + 1);
        }
    }

    private void guardaAsignacionPermisos(int idRol, int idModulo, string idSubModuloList)
    {
        try
        {
            //Error free, hace el insert...
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idRol", idRol);
            parameters.Add("@idSubModuloList", idSubModuloList);
            parameters.Add("@idModulo", idModulo);

            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SaveAsignacionPermisos", parameters, this.Session["connection"].ToString());
            popUpMessageControl1.setAndShowInfoMessage( (string)GetLocalResourceObject("Guardadoexitosamente"), Common.MESSAGE_TYPE.Success);
        }
        catch (Exception ex)
        {
            popUpMessageControl1.setAndShowInfoMessage((string)GetLocalResourceObject("NoDatosProporcionados"), Common.MESSAGE_TYPE.Error);
            Log.Error(ex.ToString());
        }
    }

    #endregion

    protected void listaSecciones_DataBound(object sender, EventArgs e)
    {
        foreach (ListItem item in listaSecciones.Items)
        {
            item.Text = HttpUtility.HtmlEncode(item.Text);
        }
    }
    
   
        
}
