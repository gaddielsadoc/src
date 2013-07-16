using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de DataAccess
/// </summary>
public static class DataAccess
{
    //public DataAccess()
    //{
    //    //
    //    // TODO: Agregar aquí la lógica del constructor
    //    //
    //}

    private static string connectionString()
    {
        //return ConfigurationManager.ConnectionStrings["dbConn_MEX"].ToString();
        return ConfigurationManager.ConnectionStrings["dbConn"].ToString();
    }

    private static string connectionString(string ConnectionName)
    {


        return ConfigurationManager.ConnectionStrings[ConnectionName].ToString();
    }

    /// <summary>
    /// Execute any store procedure
    /// </summary>
    /// <param name="spName">Store Procedure Name</param>
    /// <param name="parameters">Parameters Dictionary (string,object)</param>
    /// <returns></returns>
    public static DataTable executeStoreProcedureDataTable(string spName, Dictionary<string, object> parameters)
    {

        SqlConnection dbconn = new SqlConnection(connectionString());
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static DataTable executeStoreProcedureDataTable(string spName, Dictionary<string, object> parameters, string connectionName)
    {

        SqlConnection dbconn = new SqlConnection(connectionString(connectionName));
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static DataSet executeStoreProcedureDataSet(string spName, Dictionary<string, object> parameters, string connectionName)
    {

        SqlConnection dbconn = new SqlConnection(connectionString(connectionName));
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(ds);
            dbconn.Close();
            sqlDA.Dispose();
            cmd.Dispose();

            for (int i = 0; i < ds.Tables.Count; ++i)
            {
                ds.Tables[i].TableName = string.Format("table{0}",i+1);
            }

            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }


    public static DataSet executeStoreProcedureDataSet(string spName, Dictionary<string, object> parameters)
    {

        SqlConnection dbconn = new SqlConnection(connectionString());
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(ds);
            dbconn.Close();
            sqlDA.Dispose();
            cmd.Dispose();

            for (int i = 0; i < ds.Tables.Count; ++i)
            {
                ds.Tables[i].TableName = string.Format("table{0}", i + 1);
            }

            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static void executeStoreProcedureNonQuery(string spName, Dictionary<string, object> parameters)
    {

        SqlConnection dbconn = new SqlConnection(connectionString());
        SqlCommand cmd = new SqlCommand(spName, dbconn);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            cmd.ExecuteNonQuery();
            dbconn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static void executeStoreProcedureNonQuery(string spName, Dictionary<string, object> parameters, string connectionName)
    {

        SqlConnection dbconn = new SqlConnection(connectionString(connectionName));
        SqlCommand cmd = new SqlCommand(spName, dbconn);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            cmd.ExecuteNonQuery();
            dbconn.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }


    public static float executeStoreProcedureFloat(string spName, Dictionary<string, object> parameters)
    {

        SqlConnection dbconn = new SqlConnection(connectionString());
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return float.Parse(dt.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static float executeStoreProcedureFloat(string spName, Dictionary<string, object> parameters, string connectionName)
    {

        SqlConnection dbconn = new SqlConnection(connectionString(connectionName));
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return float.Parse(dt.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static int executeStoreProcedureGetInt(string spName, Dictionary<string, object> parameters)
    {

        SqlConnection dbconn = new SqlConnection(connectionString());
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return Int32.Parse(dt.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }


    public static int executeStoreProcedureGetInt(string spName, Dictionary<string, object> parameters, string connectionName)
    {

        SqlConnection dbconn = new SqlConnection(connectionString(connectionName));
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return Int32.Parse(dt.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }

    public static String executeStoreProcedureString(string spName, Dictionary<string, object> parameters)
    {

        SqlConnection dbconn = new SqlConnection(connectionString());
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return dt.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }


    public static String executeStoreProcedureString(string spName, Dictionary<string, object> parameters, string connectionName)
    {

        SqlConnection dbconn = new SqlConnection(connectionString(connectionName));
        SqlCommand cmd = new SqlCommand(spName, dbconn);

        SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 7200;

        foreach (string item in parameters.Keys)
        {
            cmd.Parameters.AddWithValue(item, parameters[item]);
        }

        try
        {
            dbconn.Open();
            sqlDA.Fill(dt);
            dbconn.Close();
            return dt.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (dbconn.State != ConnectionState.Closed)
            {
                dbconn.Close();
            }
        }
    }
}
