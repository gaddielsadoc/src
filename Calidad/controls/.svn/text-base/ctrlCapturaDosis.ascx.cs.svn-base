using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using log4net;

public delegate void RevisionEventHandler(object sender, CustomEventArgs e);

public partial class controls_ctrlCapturaDosis : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger(typeof(BasePage));

    public event RevisionEventHandler ResponseControlEventHandler;
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void CerrarVentana(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        //desbloquea la programacion al abrir
        try
        {
            Dictionary<string, object> candado = new System.Collections.Generic.Dictionary<string, object>();
            candado.Add("@idPrograma", Session["IdOTCookie"]);
            candado.Add("@candado", 0);
            DataAccess.executeStoreProcedureDataSet("dbo.spr_UPDATE_CandadoPrograma", candado, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            log.Error(ex);
        }

    }

    protected void cancelarOrden(object sender, EventArgs e)
    {
        //popUpRazones.Show();
        //razonesCancelar2.Visible = true;
        //razonesCancelar2.showPopup(1);
    }
    protected void guardar_dosis(object sender, EventArgs e)
    {
        var errors = false;
        var mensajeError = String.Empty;
        var inputs = String.Empty;
        var ids = String.Empty;
        
        //rellenamos una tabla virtual para mandarla al stored procedure
        DataTable dt = new DataTable();
        dt.Columns.Add("id", typeof(string));
        dt.Columns.Add("valor", typeof(decimal));
        foreach (GridViewRow row in gvDosis.Rows)
        {
            if(row.RowType == DataControlRowType.DataRow)
            {
                var ctlCantidadMinima = row.FindControl("hiddenMinima") as TextBox;
                var ctlCantidadMaxima = row.FindControl("hiddenMaxima") as TextBox;
                var idQuimico = row.FindControl("idQuimico") as HiddenField;
                var dosisCapturada = row.FindControl("txtInput") as TextBox;
                var nombreQuimico = row.FindControl("nombreQuimico") as Label;

                var cantidadIngresada = Double.Parse(dosisCapturada.Text);
                var cantidadMinima = Double.Parse(ctlCantidadMinima.Text);
                var cantidadMaxima = Double.Parse(ctlCantidadMaxima.Text);

                //validacion server side de que los campos sean iguales
                if( !(cantidadIngresada >= cantidadMinima && cantidadIngresada <= cantidadMaxima ))
                {
                    errors = true;
                    mensajeError += GetLocalResourceObject("ErrorEn").ToString() + nombreQuimico.Text + GetLocalResourceObject("ErrorRangoFuera").ToString();
                                
                }
                else
                {
                    //si son iguales rellenamos la tabla
                    DataRow dr = dt.NewRow();
                    dr[0] = idQuimico.Value;
                    dr[1] = Double.Parse(dosisCapturada.Text);
                    dt.Rows.Add(dr);
                }
                
            }
        }

        if(errors)
        {
            popUpMessageControl1.setAndShowInfoMessage(mensajeError, Common.MESSAGE_TYPE.Error);
            this.showPopup();
        }
        else
        {
            
            try
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@idOTHeader", Session["IdOTCookie"]);
                parameters.Add("@datosCapturados", dt);

                //guarda
                //String Rs = DataAccess.executeStoreProcedureString("spr_UpdateDosisProgramacion", parameters);
                DataTable Rs = DataAccess.executeStoreProcedureDataTable("spr_UpdateDosisProgramacion", parameters, this.Session["connection"].ToString());
                if (Rs.Equals("error"))
                {
                    //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios intentelo de nuevo.", Common.MESSAGE_TYPE.Info);
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NoCambios").ToString(), Common.MESSAGE_TYPE.Warning);
                }
                else
                {
                    //libera el candado
                    Dictionary<string, object> candado = new System.Collections.Generic.Dictionary<string, object>();
                    candado.Add("@idPrograma", Session["IdOTCookie"]);
                    candado.Add("@candado", 0);
                    DataAccess.executeStoreProcedureDataSet("dbo.spr_UPDATE_CandadoPrograma", candado, this.Session["connection"].ToString());

                    var args = new CustomEventArgs { success = true, message = GetLocalResourceObject("SiCambios").ToString() /*"Cambios realizados"*/ };
                    ResponseControlEventHandler(sender, args);

                    //popUpMessageControl1.setAndShowInfoMessage("Cambios realizados.", Common.MESSAGE_TYPE.Success);
                }
            }
            catch (Exception ex)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NoCambios").ToString(), Common.MESSAGE_TYPE.Warning); 
                log.Error(ex);
            }
             
        }
        

    }
    public void hidePopup() {

        mdlPopupMessageGralControl.Hide();
    }

    public void showPopup()
    {
        try
        {
            if (Session["IdOTCookie"] == null || Session["IdOTCookie"].ToString() == "")
            {
                //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL02: Se perdió la información del ID actual", Common.MESSAGE_TYPE.Error);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("RGRL02").ToString(), Common.MESSAGE_TYPE.Warning);
            }
            else
            {
                //bloquea la programacion al abrir
                Dictionary<string, object> candado = new System.Collections.Generic.Dictionary<string, object>();
                candado.Add("@idPrograma", Session["IdOTCookie"]); 
                candado.Add("@candado", 1);
                DataAccess.executeStoreProcedureDataSet("dbo.spr_UPDATE_CandadoPrograma", candado, this.Session["connection"].ToString());

                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@idOrdenTrabajoHeader", Session["IdOTCookie"]);

                /*Buscar registro en BD*/
                DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromIdProgramacionH", parameters, this.Session["connection"].ToString());
        
                /*Prellenar campos*/
                gvDosis.DataSource = dt;
                gvDosis.DataBind();

                if (dt.Rows.Count > 0)
                {
                    if ((dt.Rows[0]["dFechaCancelado"] == null || dt.Rows[0]["dFechaCancelado"].ToString() == "") && (dt.Rows[0]["dFechaPlanAplicar"].ToString() != "") && (dt.Rows[0]["dFechaPesaje"] == null || dt.Rows[0]["dFechaPesaje"].ToString() == "") )
                    {
                        save.Visible = true;
                    }
                    else
                    {
                        save.Visible = false;
                    }
                }
                mdlPopupMessageGralControl.Show();

            }
        }
        catch (Exception ex)
        {
            log.Error(ex); 
            //popUpMessageControl1.setAndShowInfoMessage("Error de sistema: Se perdió la información del ID actual", Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("RGRL02").ToString(), Common.MESSAGE_TYPE.Warning);
        }
    }



}
