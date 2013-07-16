using System;
using System.Data;
//using System.Web.Security;
using System.DirectoryServices;
using System.Configuration;
using System.Linq;


public partial class controls_ctrlLoginLdap : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tblConections.Visible = false;
        this.Session["uiCulture"] = ConfigurationManager.AppSettings["Idioma"];//"es-MX";

        if (!this.Page.IsPostBack)
        {
            //Session.RemoveAll(); 
            txtUsername.Focus();
            txtPassword.Attributes.Add("OnKeyDown", "if(event.wich || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {" +
                                           " document.getElementById('" + lnkLogin.ClientID + "').click();return false;}} else {return true}; ");
            txtUsername.Text = "adominguez";
            //txtPassword.Text = "123";

           
        }
    }

    public string UserName
    {
        
        set { txtUsername.Text = value; }
    }

    //protected void lnkLogin_Click(object sender, EventArgs e)
    //{
    //    /*You can use any method to athentificat the user
    //     *DATA BASE: Check stored procedures, names and create them
    //     *ACTIVE DIRECTORY: On web.config check the access to AD if it is correcto or if that you need.
    //     */
    //    bool authenticated = false;
      
    //    if (ConfigurationManager.AppSettings["bTesting"] == "True")
    //    {            
    //        //if (String.Compare(txtUsername.Text, "admin", StringComparison.Ordinal) == 0 && String.Compare(txtPassword.Text, "pass", StringComparison.Ordinal) == 0)
    //        //{
    //        //    Session["usernameCalidad"] = txtUsername.Text;
    //        //    authenticated = true;
    //        //}  
    //        authenticated = userExistsOnDataBase(txtUsername.Text);
    //    }

    //    else
    //    {
    //        //---------------validar ActiveDirectory-----------------//
    //        if (ConfigurationManager.AppSettings["validoActiveDirectory"] == "True")
    //        {
    //            authenticated = ActiveDirectoryAuthentification(txtUsername.Text.Trim(), txtPassword.Text.Trim());

    //            if(authenticated)
    //                authenticated = userExistsOnDataBase(txtUsername.Text);
    //        }

    //        //-----------------validar ReqLogic----------------//
    //        //authenticated = RecLogicAuthentification(txtUsername.Text.Trim()) ;               

           
            
    //    }
    //    if (authenticated)
    //    {
    //        Session["usernameCalidad"] = txtUsername.Text;              
    //        Response.Redirect("~/pages/BoletinPublicado.aspx", false);  
    //    }
    //    else
    //    {
    //        lblError.Text = "El nombre de usuario y/o contraseña no son correctos.";
    //        lblError.Visible = true;
    //    }

    //}


    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        /*You can use any method to athentificat the user
         *DATA BASE: Check stored procedures, names and create them
         *ACTIVE DIRECTORY: On web.config check the access to AD if it is correcto or if that you need.
         */
        bool authenticated = false;
        DataSet dt=null;
        if (ConfigurationManager.AppSettings["bTesting"] == "True")
        {            
            //if (String.Compare(txtUsername.Text, "admin", StringComparison.Ordinal) == 0 && String.Compare(txtPassword.Text, "pass", StringComparison.Ordinal) == 0)
            //{
            //    Session["usernameCalidad"] = txtUsername.Text;
            //    authenticated = true;
            //}  
            dt = userExistsOnDataBase(txtUsername.Text);
        }

        else
        {
            //---------------validar ActiveDirectory-----------------//
            if (ConfigurationManager.AppSettings["validoActiveDirectory"] == "True")
            {
                authenticated = ActiveDirectoryAuthentification(txtUsername.Text.Trim(), txtPassword.Text.Trim());

                if(authenticated)
                    dt = userExistsOnDataBase(txtUsername.Text);
            }


            //-----------------validar ReqLogic----------------//
            //authenticated = RecLogicAuthentification(txtUsername.Text.Trim()) ;   

        }

        this.ViewState["tbls"] = dt;
        if (dt != null && dt.Tables.Count >= 0)
        {
            authenticated = true;
        }


        //if (authenticated && dt.Tables[0] != null && dt.Tables[1] != null && dt.Tables[0].Rows.Count > 0 && dt.Tables[1].Rows.Count > 0)
        //{
        //    //Session["usernameCalidad"] = txtUsername.Text;              
        //    this.lblInfoCon.Text = string.Format(GetLocalResourceObject("userIntwo").ToString(), this.txtUsername.Text);
            
        //    this.Visible = true;
        //    this.tblLogin.Visible = false;
        //    this.tblConections.Visible = true;
           
        //}
        //else 
        if (authenticated)
        {
            
            SetValuesInSession(dt);
            Response.Redirect("~/pages/BoletinPublicado.aspx", false);  
        }
        else
        {
            lblError.Text = GetLocalResourceObject("badUser").ToString();
            lblError.Visible = true;
        }
    }
    private bool RecLogicAuthentification(string userName)
    {
        //System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        //parameters.Add("@userName", userName);
        
        //try
        //{
        //    DataTable dt =
        //            DataAccess.executeStoreProcedureDataTable("spr_GET_UserReqLogicExist", parameters);

        //    return dt.Rows.Count > 0 ? true : false;
        //}
        //catch (Exception ex)
        //{
        //    lblError.Text = "Error en los permisos, reintenta o contacta al administrador";
            return false;
        //}
    }

    private bool ActiveDirectoryAuthentification(string userName, string password)
    {
        bool bIsOnActiveDirectory = false;
        try
        {
            bIsOnActiveDirectory = isOnActiveDirectory(txtUsername.Text.Trim(), txtPassword.Text.Trim());
        }
        catch
        {
            lblError.Text = System.Configuration.ConfigurationManager.AppSettings.Get("LoginError");
            return false;
        }
        //Check if the user was correct authentificated on AD
        if (bIsOnActiveDirectory)
        { return true; }
        else
        {
            lblError.Text = System.Configuration.ConfigurationManager.AppSettings.Get("LoginError");
            return false;
        }
    }

    //private bool userExistsOnDataBase(string userName)
    //{
    //    System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
    //    parameters.Add("@user", userName);
    //    //parameters.Add("@pass", pass);
    //    try
    //    {
    //        DataTable dt =
    //                DataAccess.executeStoreProcedureDataTable("dbo.spr_AccesoUsuario",parameters);
            




    //        if (dt.Rows.Count > 0)
    //        {
    //            Session["dtUserInfoCalidad"] = dt.Rows.Count > 0 ? dt : null;
    //            Session["userIDCalidad"] = dt.Rows.Count > 0 ? dt.Rows[0]["idUsuario"].ToString() : null;
    //        }
    //        return dt.Rows.Count > 0 ? true : false;

    //    }
    //    catch (Exception ex)
    //    {
    //        lblError.Text = "Error en los permisos, reintenta o contacta al administrador";
    //        return false;
    //    }
    //}



     private DataSet userExistsOnDataBase(string userName)
    {
        System.Collections.Generic.Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@user", userName);
        //parameters.Add("@pass", pass);
         DataSet dt= new DataSet();
         
        try
        {
            //DataTable dtMX =
            //        DataAccess.executeStoreProcedureDataTable("dbo.spr_AccesoUsuario",parameters,"dbConn_MEX");
            //DataTable dtUSA =
            //        DataAccess.executeStoreProcedureDataTable("dbo.spr_AccesoUsuario",parameters,"dbConn_USA");

            DataTable dtBD =
                    DataAccess.executeStoreProcedureDataTable("dbo.spr_AccesoUsuario", parameters, "dbConn");
            


            //dt.Tables.Add(dtMX);
            //dt.Tables.Add(dtUSA);

            dt.Tables.Add(dtBD);

            this.ViewState["tbls"] = dt;

            //var union = dtMX.AsEnumerable().Union(dtUSA.AsEnumerable()).CopyToDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    Session["dtUserInfoCalidad"] = dt.Rows.Count > 0 ? dt : null;
            //    Session["userIDCalidad"] = dt.Rows.Count > 0 ? dt.Rows[0]["idUsuario"].ToString() : null;
            //}
            //return union.Rows.Count > 0 ? dt : null;

            return dt;

        }
        catch (Exception ex)
        {
            lblError.Text = GetLocalResourceObject("PermisosError").ToString();
            return null;
        }

     }
    private bool isOnActiveDirectory(string userName, string password)
    {
        string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];

        string errorSpeech = string.Empty;
        //DataTable dt = getUserLogInformation(userName, Security.Encrypt(password),"");
        //IF EXITS ON USER LOG
        try
        {
            errorSpeech = this.IsAuthenticated("LDAP://" + GDLDomain, GDLDomain, userName, password);
            if (errorSpeech == "")
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private string IsAuthenticated(string _path, string domain, string username, string pwd)
    {
        string domainAndUsername = domain + @"\" + username;
        //string errorSpeech = lablesXML.getNameSpanish("errorSpeech");
        DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain), domainAndUsername, pwd);

        try
        {
            //Bind to the native AdsObject to force authentication.
            //object obj = entry.NativeObject;

            DirectorySearcher search = new DirectorySearcher(entry);

            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();

            if (null == result)
            {
                return "Error";// errorSpeech;
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }

        return "";
    }




    
    protected void lbSalir_Click(object sender, EventArgs e)
    {
        //this.tblConections.Visible = false;
        this.tblLogin.Visible = true;

    }


    public void SetValuesInSession(DataSet dts)
    {
        //DataSet dts = (DataSet)this.ViewState["tbls"];
        DataTable dtc = dts.Tables[0];
        //bool encontrado = false;
        //if (dtc != null && dtc.Rows.Count > 0)
        //{
        //    this.Session["connection"] = "dbConn_MEX";
        //    encontrado = true;
        //}

        //if (!encontrado)
        //{
        //    dtc = dts.Tables[1]; 
        //    this.Session["connection"] = "dbConn_USA";
        //}

        this.Session["connection"] = "dbConn"; 

        //DataTable dtc = (DataTable)this.Session["tblUSA"];

        Session["dtUserInfoCalidad"] = dtc.Rows.Count > 0 ? dtc : null;
        Session["userIDCalidad"] = dtc.Rows.Count > 0 ? dtc.Rows[0]["idUsuario"].ToString() : null;
        Session["usernameCalidad"] = txtUsername.Text;
    }
    //protected void lbUsa_Click(object sender, EventArgs e)
    //{
    //    DataSet dts = (DataSet)this.ViewState["tbls"];

    //    SetValuesInSession(dts);
    //    this.Session["connection"] = "dbConn_USA";
    //    Response.Redirect("~/pages/BoletinPublicado.aspx", false);  
    //}
    //protected void lbMex_Click(object sender, EventArgs e)
    //{
    //    DataSet dts = (DataSet)this.ViewState["tbls"];
    //    SetValuesInSession(dts);
    //    this.Session["connection"] = "dbConn_MEX";
    //    Response.Redirect("~/pages/BoletinPublicado.aspx", false);  
    //}



}
