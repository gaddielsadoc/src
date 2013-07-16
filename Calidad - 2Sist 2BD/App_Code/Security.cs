using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Text;


/// <summary>
/// Descripción breve de Security
/// </summary>
public class Security
{
    string _userName = string.Empty;

	public Security(string usernName)
	{
        _userName = usernName;
	}

    public DataTable authenticateUser(string connection)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("@UserName", _userName);
        try
        {
            DataTable dt = DataAccess.executeStoreProcedureDataTable("dbo.sp_getUsers", parameters, connection);
            return dt.Rows.Count > 0 ? dt : null;            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static string EncryptText(string strText, string strEncrKey)
    {
        byte[] byKey = new byte[] { };
        byte[] IV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

    private static string DecryptText(string strText, string strEncrKey)
    {
        byte[] byKey = new byte[] { };
        byte[] IV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        byte[] inputByteArray = new byte[strText.Length];
        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            Encoding enconding = System.Text.Encoding.UTF8;

            return enconding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }

    }

    public static string Encrypt(string strText)
    {
        return EncryptText(strText, "@3nCIzyP7@#");
    }

    public static string Decrypt(string strText)
    {
        return DecryptText(strText, "@3nCIzyP7@#");
    }

}
