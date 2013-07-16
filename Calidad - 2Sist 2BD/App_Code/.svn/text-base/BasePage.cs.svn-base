using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.ObjectModel;
using System.Globalization;
using log4net;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger(typeof(BasePage));

    public static ILog Log
    {
        get { return log; }
    }

    private string _pageName;
    private Collection<string> _pagesForRole;
    private enum AccessDeniedEnum
    {
        NoDefined,
        RedirectToLoginPage,
        ThrowHttpAccessDeniedException
    }
    #region Properties
    public string PageName
    {
        get { return _pageName; }
    }
    public Collection<string> PagesForRole
    {
        get
        {
            return _pagesForRole != null ? _pagesForRole : new Collection<string>();
        }
    }
    #endregion

    #region Constructors
    protected BasePage()
    {
        this.Init += new EventHandler(BasePage_Init);
        this.Load += new EventHandler(BasePage_Load);
    }
    #endregion
    #region PageEvents
    private void BasePage_Load(object sender, EventArgs e)
    {
        
    }

    

    private void BasePage_Init(object sender, EventArgs e)
    {
        if (!IsAutorizedAccess())        
        {
            this.AccessDeniedAction();
        }
        _pageName = GetPageName();
    }
    #endregion
    #region Virtual Methods
    protected virtual bool IsAutorizedAccess()
    {
        _pagesForRole = LoadRolePermissions();
        string currentPageName = GetPageName();
        // Si la página está en la lista, el usuario está autorizado
        //if (_pagesForRole.Contains(currentPageName.Substring(4, currentPageName.Length - 4)))
        if (_pagesForRole.Contains(currentPageName))
        {
            return true;
        }
        if (null != Session["usernameCalidad"])
            return true;
        return false;
    }
    #endregion
    #region Private Methods
    /// <summary>
    /// Acción que se ejecuta al intentar ingresar a una página
    /// a la que no se tiene acceso.
    /// La acción se puede modificar en el constructor
    /// de cada página.
    /// </summary>
    private void AccessDeniedAction()
    {
        //if (this._accessDeniedActionEnm == AccessDeniedEnum.NoDefined)
        //{
        //    // acción por defecto
        //    this._accessDeniedActionEnm = AccessDeniedEnum.ThrowHttpAccessDeniedException;
        //}
        //switch (_accessDeniedActionEnm)
        //{
        //    case AccessDeniedEnum.RedirectToLoginPage:
        //        Response.Redirect("~/loginPage.aspx", true);
        //        break;
        //    case AccessDeniedEnum.ThrowHttpAccessDeniedException:
        //        throw new HttpException(403, "No tiene los permisos necesarios para acceder a esta página");
        //        break;
        //}
        Response.Redirect("~/frmLogin.aspx", true);
    }

    private Collection<string> LoadRolePermissions()
    {
        Collection<string> pagesForRole = new Collection<string>();
        
            if (null != Session["dtUserInfoCalidad"])
            {
                //Es administrador
                DataTable dtUserInfo = (DataTable)Session["dtUserInfoCalidad"];
                bool activo = (bool)dtUserInfo.Rows[0]["activo"];
                if (!activo)
                {
                    Session.Clear();
                    Response.Redirect("~/error/NoAccess.aspx");
                }
                int roleId = dtUserInfo.Rows[0]["roleIds"] != DBNull.Value ? (int)dtUserInfo.Rows[0]["roleIds"] : -1;

                if (roleId > 0) {
                    pagesForRole = getPermisos(roleId);
                }
                
            }
            else
            {
                //No autenticado
                Response.Redirect("~/error/NoAccess.aspx");
            }
            

        return pagesForRole;
    }

    private string GetPageName()
    {
        string aux = this.Page.Request.AppRelativeCurrentExecutionFilePath.ToLower(CultureInfo.InvariantCulture);
        if (!string.IsNullOrEmpty(aux) && aux.Length > 1) {
            aux = aux.Substring(1);
        }
        else
        {
            aux = string.Empty;
        }
        return aux;
    }
    #endregion

    private Collection<string> getPermisos(int idRol)
    {
        Collection<string> pagesForRole = new Collection<string>();
        var parameters = new Dictionary<string, object>();
        parameters.Add("activo", true);
        parameters.Add("idRol", idRol);
        var ds = DataAccess.executeStoreProcedureDataTable("spr_SelectSubModulos", parameters, this.Session["connection"].ToString());

        //foreach modulo
        var distinctModules = (from row in ds.AsEnumerable()
                               where row.Field<bool>("modulo_activo")
                               select new
                               {
                                   modulo = row.Field<string>("modulo"),
                                   ruta = row.Field<string>("modulo_ruta"),
                                   idModulo = row.Field<int>("idModulo")
                               }).Distinct();
        string _htmlMenu = string.Empty;
        var distinctModulesArray = distinctModules.ToArray();
        for (int i = 0; i < distinctModulesArray.Count(); i++)
        {
            //open tag modulo            
            string htmlModulo = string.Empty;
            bool hasAccess = false;
            var modulo = distinctModulesArray[i];
            if (!string.IsNullOrEmpty(modulo.ruta))
            {
                pagesForRole.Add(modulo.ruta.ToLower());
            }
            htmlModulo += "\n\t<li><a " + (i == 0 ? "class=\"first\"" : i == distinctModulesArray.Count() - 1 ? "class=\"last\"" : string.Empty) + " href=\"" + (string.IsNullOrEmpty(modulo.ruta) ? "#" : this.Page.Request.ApplicationPath + modulo.ruta) + "\">" + modulo.modulo + "</a>";
            htmlModulo += "\n\t\t<ul>";
            //foreach (DataRow item in ds.Select("idSubModuloParent is null"))
            var itemArray = ds.Select("idSubModuloParent is null and idModulo = " + modulo.idModulo).ToArray();
            for (int j = 0; j < itemArray.Count(); j++)
            {
                var item = itemArray[j];                
                if ((int)item["tienePermiso"] == 1)
                {
                    hasAccess = true;
                    if (!string.IsNullOrEmpty((string)item["ruta"]))
                    {
                        pagesForRole.Add(((string)item["ruta"]).ToLower());
                    }
                    //open tag submodulo
                    htmlModulo += "\n\t\t\t<li><a " + (j == 0 ? "class=\"first\"" : j == itemArray.Count() - 1 ? "class=\"last\"" : string.Empty) + " href=\"" + (string.IsNullOrEmpty((string)item["ruta"]) ? "#" : this.Page.Request.ApplicationPath + item["ruta"]) + "\">" + item["subModulo"] + "</a>";
                    htmlModulo += "\n\t\t\t\t<ul>";
                    //foreach submodulo child
                    htmlModulo += addChilds(item, ds, 1, pagesForRole);
                    //close submodulo
                    htmlModulo += "\n\t\t\t\t</ul>";
                    htmlModulo += "\n\t\t\t</li>";
                }
            }

            //close tag modulo
            htmlModulo += "\n\t\t</ul>";
            htmlModulo += "\n\t</li>";
            if (hasAccess)
                _htmlMenu += htmlModulo;
        }
        Session["menu"] = _htmlMenu;
        return pagesForRole;
    }
    private string addChilds(DataRow itemP, DataTable ds, int level, Collection<string> pagesForRole)
    {
        string ret = string.Empty;
        int idSubModulo;
        idSubModulo = (int)itemP["idSubModulo"];
        var itemArray = ds.Select("idSubModuloParent = " + idSubModulo).ToArray();
        for (int j = 0; j < itemArray.Count(); j++)
        {
            var item = itemArray[j];
            if ((int)item["tienePermiso"] == 1)
            {
                if (!string.IsNullOrEmpty((string)item["ruta"]))
                {
                    pagesForRole.Add(((string)item["ruta"]).ToLower());
                }
                ret += "\n\t\t\t<li><a " + (j == 0 ? "class=\"first\"" : j == itemArray.Count() - 1 ? "class=\"last\"" : string.Empty) + " href=\"" + (string.IsNullOrEmpty((string)item["ruta"]) ? "#" : this.Page.Request.ApplicationPath + item["ruta"]) + "\">" + item["subModulo"] + "</a>";
                ret += "\n\t\t\t\t<ul>";
                ret += addChilds(item, ds, level + 1, pagesForRole);
                //close submodulo
                ret += "\n\t\t\t\t</ul>";
                ret += "\n\t\t\t</li>";
            }
        }
        return ret;
    }

    protected override void InitializeCulture()
    {


        if (this.Session["uiCulture"] != null)
        {
            
            //Session["Locale"] = Request.Form["ctl00$ddlLocale"];
            //for UI elements
            UICulture = this.Session["uiCulture"].ToString();

            //for region specific formatting
            Culture = this.Session["uiCulture"].ToString();
        }
        else
        {
            if (null != Session["uiCulture"])
            {
                UICulture = (string)Session["uiCulture"];
                Culture = (string)Session["uiCulture"];
            }
            else
            {
                Session["uiCulture"] = CultureInfo.CurrentCulture.Name;
                UICulture = (string)Session["uiCulture"];
                Culture = (string)Session["uiCulture"];
            }
        }        
        //if (Request.Form["ctl00$ddlLocale"] != null)
        //{
        //    Session["Locale"] = Request.Form["ctl00$ddlLocale"];
        //    //for UI elements
        //    UICulture = Request.Form["ctl00$ddlLocale"];

        //    //for region specific formatting
        //    Culture = Request.Form["ctl00$ddlLocale"];
        //}
        //else
        //{
        //    if (null != Session["Locale"])
        //    {
        //        UICulture = (string)Session["Locale"];
        //        Culture = (string)Session["Locale"];
        //    }
        //    else
        //    {
        //        Session["Locale"] = CultureInfo.CurrentCulture.Name;
        //        UICulture = (string)Session["Locale"];
        //        Culture = (string)Session["Locale"];
        //    }
        //}        
        base.InitializeCulture();
    }


    private Collection<string> getPermisosProv()
    {
        var pagesForRole = new Collection<string>();
        pagesForRole.Add("/prov/BienvenidaProveedores.aspx".ToLower());
        pagesForRole.Add("/prov/frmVendor.aspx".ToLower());
        pagesForRole.Add("/prov/frmMyInformation.aspx".ToLower());
        var htmlModulo = string.Empty;
        htmlModulo += "\n\t<li><a " + "class=\"first\"" + " href=\"" + Page.Request.ApplicationPath + "/prov/frmMyInformation.aspx" + "\">" + "Mis Datos" + "</a>";
        htmlModulo += "\n\t\t</li>";
        htmlModulo += "\n\t<li><a " + "class=\"first\"" + " href=\"" + Page.Request.ApplicationPath + "/prov/frmPagos.aspx" + "\">" + "Pagos" + "</a>";
        htmlModulo += "\n\t\t</li>";
        htmlModulo += "\n\t<li><a " + "class=\"first\"" + " href=\"" + Page.Request.ApplicationPath + "/prov/frmVendor.aspx" + "\">" + "Facturas" + "</a>";
        htmlModulo += "\n\t\t</li>";
        Session["menu"] = htmlModulo;
        return pagesForRole;
    }

    private Collection<string> getPermisosProvTemporal()
    {
        var pagesForRole = new Collection<string>();
        pagesForRole.Add("/prov/frmCompleteVendorInfo.aspx".ToLower());
        var htmlModulo = string.Empty;
        htmlModulo += "\n\t<li><a " + "class=\"first\"" + " href=\"" + Page.Request.ApplicationPath + "/prov/frmCompleteVendorInfo.aspx" + "\">" + "Mis Datos" + "</a>";
        htmlModulo += "\n\t\t</li>";
        Session["menu"] = htmlModulo;
        return pagesForRole;
    }
}