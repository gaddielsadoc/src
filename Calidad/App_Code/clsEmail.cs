using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Xml;
using System.Collections.Generic;

/// <summary>
/// Summary description for clsEmail
/// </summary>
public class clsEmail
{
    private string _EmailKey = string.Empty;
    private string _FromAddress = string.Empty;
    private string _ToAddress = string.Empty;
    private string _Subject = string.Empty;
    private string _Resources = string.Empty;
    private string _HTMLFilename = string.Empty;
    private string _strHTML = string.Empty; // Email template
    private string _currentMessageBody = string.Empty;
    private string _commonPath = "Common";
    private Dictionary<string, string> _LinkedResources = new Dictionary<string, string>();
    private Dictionary<string, Stream> _Attachments = new Dictionary<string, Stream>();

    // Get values from the web.config
    //private string _XMLCommonFolder = ConfigurationSettings.AppSettings["XMLCommonFolder"];
    private string _XMLRootFolder = ConfigurationSettings.AppSettings["XMLFolder"];
    private string _sSMTPClient = ConfigurationSettings.AppSettings["SMTPClient"];
    private string _sNetCredUsername = ConfigurationSettings.AppSettings["NCusername"];
    private string _sNetCredPassword = ConfigurationSettings.AppSettings["NCpassword"];
    private int _iPort = Convert.ToInt32(ConfigurationSettings.AppSettings["SMTPPort"].ToString());

	public clsEmail(string EmailKey)
	{
        _EmailKey = EmailKey;
        ReadXML();
        ReadHTMLFile();
        AttachFiles();
	}

    public clsEmail(string EmailKey, string FromAddress, string ToAddress, string Subject)
    {
        _EmailKey = EmailKey;
        _FromAddress = FromAddress;
        _ToAddress = ToAddress;
        _Subject = Subject;
        ReadXML();
        ReadHTMLFile();
        AttachFiles();
    }

    public clsEmail(string EmailKey, string FromAddress, string ToAddress)
    {
        _EmailKey = EmailKey;
        _FromAddress = FromAddress;
        _ToAddress = ToAddress;
        _Subject = string.Empty;
        ReadXML();
        ReadHTMLFile();
        AttachFiles();
    }

    private void ReadXML()
    {
        try
        {
            string _XmlPath = _XMLRootFolder + ConfigurationSettings.AppSettings["XMLEmail"];
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath(_XmlPath));

            // ReadNode By KeyName
            XmlNodeList xmlNodes = xmlDoc.SelectNodes("Email/Layout");
            foreach (XmlNode node in xmlNodes)
            {
                if (_EmailKey == node.Attributes["key"].Value)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "From":
                                if (_FromAddress.Trim().Equals(String.Empty))
                                    _FromAddress = childNode.InnerXml;
                                break;
                            case "To":
                                if (_ToAddress.Trim().Equals(String.Empty))
                                    _ToAddress = childNode.InnerXml;
                                break;
                            case "Subject":
                                if (_Subject.Trim().Equals(String.Empty))
                                    _Subject = childNode.InnerXml;
                                break;
                            case "HTMLBody":
                                if (_HTMLFilename.Trim().Equals(String.Empty))
                                    _HTMLFilename = childNode.InnerXml;
                                break;
                            case "Resources":
                                XmlNodeList xmlResources =  childNode.SelectNodes("Item");
                                foreach (XmlNode item in xmlResources)
                                {
                                    _LinkedResources.Add(item.Attributes["Name"].Value, item.Attributes["FileName"].Value);
                                }

                                break;
                        };
                    }
                    break;
                }
            }
            
        }
        catch (XmlException XMLex)
        {
            throw new Exception("XMLException: " + XMLex.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ReadHTMLFile()
    {
        try
        {
            _strHTML = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(_XMLRootFolder + _HTMLFilename));
            _currentMessageBody = _strHTML;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void AttachFiles()
    {

        //string[] Resources;
        //Resources = _Resources.Split(';');
        //LinkedResource resource = new LinkedResource();
        //resource.ContentId = "";
    }

    public void setFiles(Dictionary<string, Stream> dicFiles)
    {
        _Attachments = dicFiles;
    }

    /// <summary>
    /// All left string that match with HTM will be remplace
    /// </summary>
    /// <param name="dt"></param>
    public void setMessage(Dictionary<string,string> dicVariables)
    {
        _currentMessageBody = _strHTML;
        foreach (KeyValuePair<string,string> var in dicVariables)
        {
            _currentMessageBody = _currentMessageBody.Replace(var.Key, var.Value);
        }
    }
    /// <summary>
    /// All column's name on DataTable that match with HTM will be remplace
    /// </summary>
    /// <param name="dt"></param>
    public void setMessage(DataTable dt)
    {
        _currentMessageBody = _strHTML;
        foreach (DataColumn column in dt.Columns)
        {
            _currentMessageBody = _currentMessageBody.Replace(column.ColumnName, dt.Rows[0][column.ColumnName].ToString());
        }
    }

    public DataTable getDataTableSP(string spName, Dictionary<string,object> parameters, string connection)
    {
        try 
	    {
            return DataAccess.executeStoreProcedureDataTable(spName, parameters, connection);
	    }
	    catch (Exception ex)
	    {
            throw new Exception(ex.Message);
	    }
    }

    /// <summary>
    /// Give format to emails through datatable
    /// </summary>
    /// <param name="dt">Data table with emails </param>
    /// <returns></returns>
    public string formatEmail(DataTable dt)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (DataRow itemRow in dt.Rows)
        {
            foreach (DataColumn itemCol in dt.Columns)
	        {
                sb.AppendFormat("{0};", itemRow[itemCol.ColumnName].ToString());
	        }
        }
        return sb.ToString();
    }

    public void Send()
    {
        //SmtpClient client = new SmtpClient(_sSMTPClient, _iPort);
        SmtpClient client = new SmtpClient(_sSMTPClient);
        //client.EnableSsl = true;        
        client.Credentials = new System.Net.NetworkCredential(_sNetCredUsername, _sNetCredPassword);
        MailAddress ma_From = new MailAddress(_FromAddress, "NatureSweet",System.Text.Encoding.UTF8);
        MailMessage message = new MailMessage();
        message.From = ma_From;

        string[] mailAddresses = _ToAddress.Split(new char[] { ',', ';' });
        foreach (string mail in mailAddresses)
        {
            if (mail != "" && !message.To.Contains(new MailAddress(mail)))
                message.To.Add(mail);
        }
        //message.Bcc.Add("carlosg@naturesweet.com.mx");
        //message.Bcc.Add("jamadorfi@gmail.com");
        message.Subject = _Subject;

        // Se necesita crear una vista de texto plano para exploradores que no soporten html
        // AlternateView plainView = AlternateView.CreateAlternateViewFromString("Plain Text", null, "text/plain");
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_currentMessageBody, null, "text/html");
        if (_LinkedResources != null)
        {
            foreach (string Keys in _LinkedResources.Keys)
            {
                LinkedResource linkResource = new LinkedResource(
                                                            HttpContext.Current.Server.MapPath(string.Format("{0}{1}/{2}",
                                                                 _XMLRootFolder
                                                                , _commonPath
                                                                , _LinkedResources[Keys])));

                linkResource.ContentId = Keys;

                htmlView.LinkedResources.Add(linkResource);
            }
        }

        if (_Attachments != null)
        {
            foreach (string Keys in _Attachments.Keys)
            {
                /*LinkedResource linkResource = new LinkedResource(
                                                            HttpContext.Current.Server.MapPath(string.Format("{0}{1}/{2}",
                                                                 _XMLRootFolder
                                                                , _commonPath
                                                                , _LinkedResources[Keys])));*/

                //LinkedResource linkResource = new LinkedResource(string.Format("{0}",
                //                                                  _LinkedResources[Keys]));

                //linkResource.ContentId = Keys;                
                //htmlView.LinkedResources.Add(linkResource);

                var attachment = new Attachment(_Attachments[Keys], Keys);
                message.Attachments.Add(attachment);
            }
        }

        // Add the views
        //message.AlternateViews.Add(plainView);
        message.AlternateViews.Add(htmlView);
        
        try
        {
            client.Send(message);
        }
        catch (SmtpFailedRecipientsException ex)
        {
            throw new Exception(String.Format("Failed to deliver message to {0}", ex.FailedRecipient));
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Exception caught in RetryIfBusy(): {0}", ex.ToString()));
        }
    }


    public void Send(string razon, string comentarios, string fumigacion)
    {
        //SmtpClient client = new SmtpClient(_sSMTPClient, _iPort);
        SmtpClient client = new SmtpClient(_sSMTPClient);
        //client.EnableSsl = true;        
        client.Credentials = new System.Net.NetworkCredential(_sNetCredUsername, _sNetCredPassword);
        MailAddress ma_From = new MailAddress(_FromAddress, "NatureSweet", System.Text.Encoding.UTF8);
        MailMessage message = new MailMessage();
        message.From = ma_From;

        string[] mailAddresses = _ToAddress.Split(new char[] { ',', ';' });
        foreach (string mail in mailAddresses)
        {
            if (mail != "" && !message.To.Contains(new MailAddress(mail)))
                message.To.Add(mail);
        }
        //message.Bcc.Add("carlosg@naturesweet.com.mx");
        //message.Bcc.Add("jamadorfi@gmail.com");
        message.Subject = _Subject;

        // Se necesita crear una vista de texto plano para exploradores que no soporten html
        // AlternateView plainView = AlternateView.CreateAlternateViewFromString("Plain Text", null, "text/plain");
        _currentMessageBody = _currentMessageBody.Replace("@razon@", razon);
        _currentMessageBody = _currentMessageBody.Replace("@comentarios@", comentarios);
        _currentMessageBody = _currentMessageBody.Replace("@fumigacion@", fumigacion);
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_currentMessageBody, null, "text/html");
        if (_LinkedResources != null)
        {
            foreach (string Keys in _LinkedResources.Keys)
            {
                LinkedResource linkResource = new LinkedResource(
                                                            HttpContext.Current.Server.MapPath(string.Format("{0}{1}/{2}",
                                                                 _XMLRootFolder
                                                                , _commonPath
                                                                , _LinkedResources[Keys])));

                linkResource.ContentId = Keys;

                htmlView.LinkedResources.Add(linkResource);
            }
        }

        if (_Attachments != null)
        {
            foreach (string Keys in _Attachments.Keys)
            {
                /*LinkedResource linkResource = new LinkedResource(
                                                            HttpContext.Current.Server.MapPath(string.Format("{0}{1}/{2}",
                                                                 _XMLRootFolder
                                                                , _commonPath
                                                                , _LinkedResources[Keys])));*/

                //LinkedResource linkResource = new LinkedResource(string.Format("{0}",
                //                                                  _LinkedResources[Keys]));

                //linkResource.ContentId = Keys;                
                //htmlView.LinkedResources.Add(linkResource);

                var attachment = new Attachment(_Attachments[Keys], Keys);
                message.Attachments.Add(attachment);
            }
        }

        // Add the views
        //message.AlternateViews.Add(plainView);
        message.AlternateViews.Add(htmlView);

        try
        {
            client.Send(message);
        }
        catch (SmtpFailedRecipientsException ex)
        {
            throw new Exception(String.Format("Failed to deliver message to {0}", ex.FailedRecipient));
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("Exception caught in RetryIfBusy(): {0}", ex.ToString()));
        }
    }


    public string ToAdress
    {
        get { return _ToAddress; }
        set { _ToAddress = value; }
    }
}
