using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class catalog_frmPlantillaInvernadero : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            cargaDatos();
        }
        else
        {
 
        }
    }

    private void cargaDatos()
    {
        ddlPlanta.Items.Clear();
        ddlPlanta.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione", "-1"));
        try
        {
            Dictionary<string, object> prm = new Dictionary<string, object>();
            prm.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            DataSet dt = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlPlantas", prm);
            ddlPlanta.DataSource = dt;
            ddlPlanta.DataValueField = "campoId";
            ddlPlanta.DataTextField = "campoNombre";
            ddlPlanta.DataBind();
        }
        catch (Exception x)
        {
            Log.Error(x);
            popUpMessageControl1.setAndShowInfoMessage(x.Message, Common.MESSAGE_TYPE.Error);
        }
        
    }
    
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlPlanta.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage("Seleccione la planta", Common.MESSAGE_TYPE.Error);
            return;
        }
        try
        {
            Dictionary<string, object> prm = new Dictionary<string, object>();
            prm.Add("@idFarm", ddlPlanta.SelectedValue);
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlInvernaderos", prm);
            prm.Clear();
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ActivosEquipoBoquilla", prm);
            dt.TableName = "Boquillas";
            ds.Tables.Add(dt);

            prm.Add("@idFarm", ddlPlanta.SelectedValue);
            DataTable dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_Plantilla", prm);
            dt2.TableName = "Volumenes";
            ds.Tables.Add(dt2);
            Plantilla c = new Plantilla("Inverndaderos", ds, this.Response);
            
        }
        catch (Exception x)
        {
            Log.Error(x);
            popUpMessageControl1.setAndShowInfoMessage(x.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        string Destino = "";
        try
        {

            if (!Path.GetFileName(File1.PostedFile.FileName).Equals(""))
            {
                string extension = System.IO.Path.GetExtension(File1.PostedFile.FileName);
                if (extension == ".xls")
                {
                    try
                    {
                        Destino = Server.MapPath(null) + "\\TempImportedFiles\\" + Path.GetFileName(File1.PostedFile.FileName);
                        File1.PostedFile.SaveAs(Destino);
                        try
                        {
                            if (LecturaDeArchivoYCreacionDeTablas(Destino))
                            {

                                //tablaDatos.Visible = true;
                                //repRows.Visible = true;
                                //btnSave.Visible = true;
                            }
                            else
                            {
                                //tablaDatos.Visible = false;

                            }
                        }
                        catch (Exception)
                        {
                            popUpMessageControl1.setAndShowInfoMessage("Error interno.", Common.MESSAGE_TYPE.Error);
                        }
                    }
                    catch (Exception)
                    {
                        popUpMessageControl1.setAndShowInfoMessage("No se pudo escribir en el servidor.", Common.MESSAGE_TYPE.Error);
                    }
                }
                else
                    popUpMessageControl1.setAndShowInfoMessage("La extensión del archivo no es válida.", Common.MESSAGE_TYPE.Error);
            }
            else
                popUpMessageControl1.setAndShowInfoMessage("Seleccione un archivo para importar.", Common.MESSAGE_TYPE.Info);

        }
        catch (Exception ex)
        {

            Log.Error(ex.ToString());
        }
        finally
        {
            if (File.Exists(Destino))
                File.Delete(Destino);
        }
    }

    private bool LecturaDeArchivoYCreacionDeTablas(string Destino)
    {
        CustomOleDbConnection cn = new CustomOleDbConnection(Destino);       
        DataSet ds = null;
        DataSet dsPlanta = null;

        try
        {
            cn.Open();

            //saco invernaderos            
            cn.setCommand("SELECT * FROM Volumen");
            ds = cn.executeQuery();

            //saco de que planta es:
            cn.setCommand("SELECT * FROM Planta");
            dsPlanta = cn.executeQuery();

            cn.Close();
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
            popUpMessageControl1.setAndShowInfoMessage("No fue posible leer desde el archivo excel.", Common.MESSAGE_TYPE.Info);
        }
        finally
        {
            cn.Close();
        }

        if( ds != null)
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
                xmlString.AppendFormat("<{0}>", "Plantilla"); 
                
                foreach (DataRow row in ds.Tables[0].Rows) 
                {     
                    for (int i = 1; i < ds.Tables[0].Columns.Count -1 ; i++)
                    {                      
                        row.ItemArray[i].ToString();
                        xmlString.AppendFormat("<c><idPlanta>{0}</idPlanta>", dsPlanta.Tables[0].Rows[0]["Planta"].ToString() );//Planta
                        xmlString.AppendFormat("<idInv>{0}</idInv>", row.ItemArray[0].ToString().Trim());//Invernadero
                        xmlString.AppendFormat("<nomBoquilla>{0}</nomBoquilla>", ds.Tables[0].Columns[i + 1].ColumnName.ToString().Trim()); //nombre de boquilla
                        xmlString.AppendFormat("<volumen>{0}</volumen></c>", String.IsNullOrEmpty(row.ItemArray[i+1].ToString().Trim() ) ? "0" : row.ItemArray[i+1].ToString().Trim()  ); //volumen                       

                    }
                    
                }
                xmlString.AppendFormat("</{0}>", "Plantilla");

                var parameters = new Dictionary<string, object>();
                parameters.Add("@Plantilla", xmlString.ToString());
                try
                {
                    DataAccess.executeStoreProcedureNonQuery("spr_INSERT_Plantilla", parameters, this.Session["connection"].ToString());
                    popUpMessageControl1.setAndShowInfoMessage("Datos Guardados", Common.MESSAGE_TYPE.Success);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                    popUpMessageControl1.setAndShowInfoMessage("No fue posible insertar datos.", Common.MESSAGE_TYPE.Info);
                }
            }





        
        

        return true;
    }

}