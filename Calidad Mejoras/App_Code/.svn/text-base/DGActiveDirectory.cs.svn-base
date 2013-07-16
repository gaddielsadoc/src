using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.DirectoryServices;

/// <summary>
/// Descripción breve de DGActiveDirectory
/// </summary>
public static class DGActiveDirectory
{
   
    private static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
        {
            return searchResult.Properties[PropertyName][0].ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    //public void updatePhoneNumber()
    //{
    //    String domainAndUsername = myDomain + @"\" + myUsername;
    //    DirectoryEntry entry = new DirectoryEntry(myPath, domainAndUsername, myPassword);

    //    DirectorySearcher objDSearch = new DirectorySearcher(entry);

    //    SearchResult objRSearch;
    //    DataTable dtResult;

    //    objDSearch.Filter = "(SAMAccountName=" + myUsername + ")";
    //    objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + userName + ")");
    //    objRSearch = objDSearch.FindOne();
    //    DirectoryEntry d = new DirectoryEntry();
    //    d.Path = objRSearch.Path;
    //    d.Properties["mail"].Value = "carlosgg@desertglory.com.mx";
    //    d.CommitChanges();
    //}



    public static DataTable getGeneralInfoAll(string username, string domain)
    {
        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResult objRSearch;
        DataTable dtResult;

        try
        {
            string USADomain = ConfigurationManager.AppSettings["USADomain"];
            string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];

            string AppUser = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUser" : "GDLAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUserPass" : "GDLAppUserPass");

            string domainAndAppUsername = domain + @"\" + AppUser;

            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain), domainAndAppUsername, AppUserPass);
            objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + username + ")");
            objRSearch = objDSearch.FindOne();
            dtResult = new DataTable();
            dtResult.Columns.Clear();

            foreach (string item in objRSearch.Properties.PropertyNames)
            {
                dtResult.Columns.Add(item);
            }

            SearchResultCollection src = objDSearch.FindAll();

            dtResult.Rows.Clear();
            foreach (SearchResult item in src)
            {

                DataRow dr = dtResult.NewRow();
                foreach (string itemS in item.Properties.PropertyNames)
                {
                    if( dtResult.Columns.Contains(itemS))
                        dr[itemS] = GetProperty(item, itemS);
                }
                dtResult.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return dtResult;
    }

    public static DataTable getGeneralInfo(string SAMAccount, string domain)
    {
        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResult objRSearch;
        DataTable dtResult;
        //Testing propose. DELETE ALL testMsj Referencess
        try
        {
            string USADomain = ConfigurationManager.AppSettings["USADomain"];
            string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];

            string AppUser = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUser" : "GDLAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUserPass" : "GDLAppUserPass");

            string domainAndAppUsername = domain + @"\" + AppUser;

            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain), domainAndAppUsername, AppUserPass);
            objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + SAMAccount + ")");
            
            objRSearch = objDSearch.FindOne();
            
            dtResult = new DataTable();
            dtResult.Columns.Clear();
            dtResult.Columns.Add("Account");
            dtResult.Columns.Add("FirstName");
            dtResult.Columns.Add("LastName");
            dtResult.Columns.Add("Email");
            dtResult.Columns.Add("Department");
            dtResult.Columns.Add("Phone");
            
            if (objRSearch != null)
            {
                dtResult.Rows.Clear();
                dtResult.Rows.Add();
                dtResult.Rows[0]["Account"] = SAMAccount;
                dtResult.Rows[0]["FirstName"] = GetProperty(objRSearch, "givenName");
                dtResult.Rows[0]["LastName"] = GetProperty(objRSearch, "sn");
                dtResult.Rows[0]["Email"] = GetProperty(objRSearch, "mail");
                dtResult.Rows[0]["Department"] = GetProperty(objRSearch, "department");
                dtResult.Rows[0]["Phone"] = GetProperty(objRSearch, "homephone") == "" ? GetProperty(objRSearch, "homephone") : GetProperty(objRSearch, "mobile");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return dtResult;
    }

    //public static DataTable getGeneralInfo(string username, string domain)
    //{
    //    DirectoryEntry entry;
    //    DirectorySearcher objDSearch;
    //    SearchResult objRSearch;
    //    DataTable dtResult;
    //    //Testing propose. DELETE ALL testMsj Referencess
    //    string testMsj = "";
    //    try
    //    {
    //        testMsj += "Antes del entry";

    //        entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain));
    //        objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + username + ")");

    //        testMsj += "\nAntes de buscar uno.";
    //        objRSearch = objDSearch.FindOne();

    //        testMsj += "\ndespués de buscar uno.";
    //        dtResult = new DataTable();
    //        dtResult.Columns.Clear();
    //        dtResult.Columns.Add("Account");
    //        dtResult.Columns.Add("FirstName");
    //        dtResult.Columns.Add("LastName");
    //        dtResult.Columns.Add("Email");
    //        dtResult.Columns.Add("Department");
    //        dtResult.Columns.Add("Phone");

    //        testMsj += "\n Columnas agregadas";
    //        if (objRSearch != null)
    //        {
    //            dtResult.Rows.Clear();
    //            dtResult.Rows.Add();
    //            dtResult.Rows[0]["Account"] = username;
    //            testMsj += "\n Buscando valores";
    //            dtResult.Rows[0]["FirstName"] = GetProperty(objRSearch, "givenName");
    //            dtResult.Rows[0]["LastName"] = GetProperty(objRSearch, "sn");
    //            dtResult.Rows[0]["Email"] = GetProperty(objRSearch, "mail");
    //            dtResult.Rows[0]["Department"] = GetProperty(objRSearch, "department");
    //            testMsj += "\n Antes del teléfono";
    //            dtResult.Rows[0]["Phone"] = GetProperty(objRSearch, "homephone") == "" ? GetProperty(objRSearch, "homephone") : GetProperty(objRSearch, "mobile");
    //            testMsj += "\n Valores encontrados";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message + "  :  " + testMsj);
    //    }

    //    return dtResult;
    //}

    public static DataTable getGeneralInfoByLastName(string username, string firstname, string lastname, string domain)
    {
        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResultCollection objRSearch;
        DataTable dtResult;
        string sFilter;
        try
        {
            if (username.Trim().Length == 0)
                sFilter = "(&(givenName=*" + (firstname.Trim().Length > 0 ? firstname.Trim() + "*" : "") + ")(sn=*" + (lastname.Trim().Length > 0 ? lastname.Trim() + "*" : "") + "))";
            else
                sFilter = "(SAMAccountName=" + username + ")";

            string USADomain = ConfigurationManager.AppSettings["USADomain"];
            string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];

            string AppUser = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUser" : "GDLAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUserPass" : "GDLAppUserPass");

            string domainAndAppUsername = domain + @"\" + AppUser;

            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain), domainAndAppUsername, AppUserPass);
            objDSearch = new DirectorySearcher(entry, sFilter);
            objRSearch = objDSearch.FindAll();

            dtResult = new DataTable();
            dtResult.Columns.Clear();
            dtResult.Columns.Add("Username");
            dtResult.Columns.Add("FirstName");
            dtResult.Columns.Add("LastName");
            dtResult.Columns.Add("Email");
            dtResult.Columns.Add("Department");

            dtResult.Rows.Clear();
            foreach (SearchResult sResult in objRSearch)
            {
                int iRow;
                dtResult.Rows.Add();
                iRow = dtResult.Rows.Count - 1;

                dtResult.Rows[iRow]["Username"] = GetProperty(sResult, "SAMAccountName");
                dtResult.Rows[iRow]["FirstName"] = GetProperty(sResult, "givenName");
                dtResult.Rows[iRow]["LastName"] = GetProperty(sResult, "sn");
                dtResult.Rows[iRow]["Email"] = GetProperty(sResult, "mail");
                dtResult.Rows[iRow]["Department"] = GetProperty(sResult, "department");
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }

        return dtResult;
    }

    public static string getSAMAccountByFullName(string fullName, string domain)
    {
        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResult objRSearch;
        string SAMAccount = string.Empty;
        try
        {

            string USADomain = ConfigurationManager.AppSettings["USADomain"];
            string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];

            string AppUser = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUser" : "GDLAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get(domain == USADomain ? "USAAppUserPass" : "GDLAppUserPass");
            string domainAndAppUsername = domain + @"\" + AppUser;

            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", domain), domainAndAppUsername, AppUserPass);
            objDSearch = new DirectorySearcher(entry, "(cn=" + fullName + "*)");
            objDSearch.PropertiesToLoad.Add("samaccountname");
            objRSearch = objDSearch.FindOne();

            if (objRSearch != null)
            {
                SAMAccount = GetProperty(objRSearch, "samaccountname");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return SAMAccount;

    }

    private static bool userExistOnADGDL(string SAMAccount)
    {
        
        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResult objRSearch;
        try
        {
            //string domainAndUsername = domain + @"\" + userName;

            string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];
            string AppUser = ConfigurationManager.AppSettings.Get("GDLAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get("GDLAppUserPass");

            //GDL
            string domainAndAppUsername = GDLDomain + @"\" + AppUser;
            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", GDLDomain), domainAndAppUsername, AppUserPass);

            objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + SAMAccount + ")");
            objDSearch.PropertiesToLoad.Add("samaccountname");
            objRSearch = objDSearch.FindOne();

            return objRSearch == null ? false : true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private static bool userExistOnADUSA(string SAMAccount)
    {

        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResult objRSearch;
        try
        {
            //string domainAndUsername = domain + @"\" + userName;

            string USADomain = ConfigurationManager.AppSettings["USADomain"];

            string AppUser = ConfigurationManager.AppSettings.Get("USAAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get("USAAppUserPass");

            //USA
            string domainAndAppUsername = USADomain + @"\" + AppUser;
            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", USADomain), domainAndAppUsername, AppUserPass);

            objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + SAMAccount + ")");
            objDSearch.PropertiesToLoad.Add("samaccountname");
            objRSearch = objDSearch.FindOne();

            return objRSearch == null ? false : true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool userExistOnActiveDirectory(string SAMAccount)
    {
        if (!userExistOnADGDL(SAMAccount))
        {
            if (!userExistOnADUSA(SAMAccount))
            {
                return false;
            }
        }
        return true;
    }


    public static List<string> getEmailUsersAdminNominee(DataTable UserList)
    {
        DirectoryEntry entry;
        DirectorySearcher objDSearch;
        SearchResult objRSearch;
        List<string> UserEmails = new List<string>();
        try
        {
            string Domain = ConfigurationManager.AppSettings["GDLDomain"];
            string AppUser = ConfigurationManager.AppSettings.Get("GDLAppUser");
            string AppUserPass = ConfigurationManager.AppSettings.Get("GDLAppUserPass");
            string domainAndAppUsername = Domain + @"\" + AppUser;
            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", Domain), domainAndAppUsername, AppUserPass);

            foreach (DataRow itemRow in UserList.Rows)
            {
                string item = itemRow["UserName"].ToString();
                objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + item + ")");
                objRSearch = objDSearch.FindOne();

                if (objRSearch != null)
                {
                    string email = GetProperty(objRSearch, "mail");
                    if (email != "")
	                {	 
                        UserEmails.Add(email);
                    }
                }
            }

            string domainAppUser = ConfigurationManager.AppSettings.Get("emailApp");
            
            //Sí, es un pequeño ajuste para evitar consultas al Active Directory
            int indexAt = domainAppUser.IndexOf('@');
            domainAppUser = domainAppUser.Substring(indexAt, domainAppUser.Length - indexAt);

            Domain = ConfigurationManager.AppSettings["USADomain"];
            AppUser = ConfigurationManager.AppSettings.Get("USAAppUser");
            AppUserPass = ConfigurationManager.AppSettings.Get("USAAppUserPass");
            domainAndAppUsername = Domain + @"\" + AppUser;
            entry = new DirectoryEntry(string.Format("LDAP://{0}:389", Domain), domainAndAppUsername, AppUserPass);

            foreach (DataRow itemRow in UserList.Rows)
            {
                string item = itemRow["UserName"].ToString();
                
                //Sí, es un pequeño ajuste para evitar consultas al Active Directory
                if (!UserEmails.Contains(item + domainAppUser))
                {
                    objDSearch = new DirectorySearcher(entry, "(SAMAccountName=" + item + ")");
                    objRSearch = objDSearch.FindOne();
                    string email = GetProperty(objRSearch, "mail");
                    if (email != "")
                    {
                        UserEmails.Add(email);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return UserEmails;
    }

    public static DataTable getGeneralInfoBothDomains(string SAMAccount)
    {
        string USADomain = ConfigurationManager.AppSettings["USADomain"];
        string GDLDomain = ConfigurationManager.AppSettings["GDLDomain"];
        DataTable dt;
        try
        {
            dt = getGeneralInfo(SAMAccount,GDLDomain);
        }
        catch (Exception)
        { dt = null; }

        try
        {
            if (dt == null || dt.Rows.Count < 1)
            {
                dt = getGeneralInfo(SAMAccount, USADomain);
            }
        }
        catch (Exception)
        {}
        return dt;
    }
}
