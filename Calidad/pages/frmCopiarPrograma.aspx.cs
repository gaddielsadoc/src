using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Globalization;
using System.Text;

public partial class pages_frmCopiarPrograma : BasePage //System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.txtConnection.Text = this.Session["connection"].ToString();

            if (!IsPostBack)
            {
                if (Session["usernameCalidad"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx", false);
                }

                Button2.Value = (string)GetLocalResourceObject("Button2");
                Button1.Value = (string)GetLocalResourceObject("btnCancelar");

                ViewState["idCopy"] = Convert.ToString(Request.QueryString["idCopy"]) != null ? Convert.ToString(Request.QueryString["idCopy"]) : "-1";

                DateTime thisDay = DateTime.Today;
                txtFechaAplicacion.Text = thisDay.ToString("yyyy-MM-dd");

                //cargaddlBasicos();
                LimpiarDesdePlanta();
                cargaddlPlantas();
                cargaddlTipoAplicacion();

                //viene del manager, si no regresa al manager
                if (Int32.Parse(ViewState["idCopy"].ToString()) > 0)
                {
                    cargaCopia();
                }
                else {
                    Response.Redirect("AplicacionManager.aspx");
                }
            }
            else
            {
                if (Session["usernameCalidad"] == null)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Sesion").ToString(), Common.MESSAGE_TYPE.Warning);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }

    }
    private bool isBisiesto(int año)
    {
        if (año % 4 == 0 && año % 100 != 0 || año % 400 == 0)
            return true;
        return false;
    }
    
    private int numeroSemana()
    {
        DateTime fecha = DateTime.Parse(txtFechaAplicacion.Text.Trim());
        int w = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, fecha.DayOfWeek);
        return w;
    }

    private int numeroYear()
    {
        DateTime fecha = DateTime.Parse(txtFechaAplicacion.Text.Trim());
        return fecha.Year;
    }

    private void cargaCopia()
    {
        //asegura un usuario logeado
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", ViewState["idCopy"].ToString());
        DataTable dt = null;

        idCopy.Value = ViewState["idCopy"].ToString();
        int x = 0;

        //carga los quimicos del programa
        try
        {
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ProgramaChild", parameters, this.Session["connection"].ToString());

            if (dt.Rows.Count > 0)
            {

                if (!String.IsNullOrEmpty(dt.Rows[0]["estatus"].ToString().Trim()))
                {
                    gdvPrograma.DataSource = dt;
                    gdvPrograma.DataBind();
                    ViewState["GridPrograma"] = dt;
                }
                else
                {
                    gdvPrograma.DataSource = null;
                    gdvPrograma.DataBind();
                }

            }


        }
        catch (Exception EX)
        {
            Log.Error(EX);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + EX.Message, Common.MESSAGE_TYPE.Error);
        }


        //cargar lo de arriba (año, semana, planta, inv....)
        if (dt.Rows.Count > 0)
        {
            DateTime t = Convert.ToDateTime(dt.Rows[0]["diaSugerido"].ToString());
            txtFechaAplicacion.Text = t.ToString("yyyy-MM-dd");

            if (dt.Rows[0]["capar"].ToString() != "")
            {
                capar.Value = dt.Rows[0]["capar"].ToString();
            }
            else
            {
                capar.Value = "0";
            }
            if (dt.Rows[0]["fin"].ToString() != "")
            {
                fin.Value = dt.Rows[0]["fin"].ToString();
            }
            else
            {
                fin.Value = "0";
            }

            equipoApli.Value = dt.Rows[0]["idEquipoAplicacion"].ToString();      

            //carga planta
            ddlPlanta.SelectedValue = dt.Rows[0]["idPlanta"].ToString();

            //*****************carga invernadero*******************************
            parameters.Clear();
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            ddlInvernadero.Items.Clear();
            ddlInvernadero.Enabled = true;
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderos", parameters, this.Session["connection"].ToString());
            if (dt2.Rows.Count > 0)
            {
                ddlInvernadero.Items.Add(GetLocalResourceObject("Seleccione").ToString() );
                ddlInvernadero.DataSource = dt2;
                ddlInvernadero.DataBind();
                ddlInvernadero.SelectedValue = dt.Rows[0]["idInvernadero"].ToString();
            }

            //carga los datos configuración
            //mostrar tipo de boquilla
            if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6") //--sis y zap
            {
                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlTipoBoquilla.SelectedValue = dt.Rows[0]["idBoquilla"].ToString();

                //cargar lo de los litros 
                
                var parametersL = new Dictionary<string, object>();
                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);

                if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                    parametersL.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
                                 
            }

            //mostrar lo de edad de cultivo
            else if (ddlPlanta.SelectedValue == "5") //--colima
            {
                cargaddlEdadCultivo(5);
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;
                ddlEdadCultivo.SelectedValue = dt.Rows[0]["iEdad"].ToString();

                //cargar lo de los litros
                var parametersL = new Dictionary<string, object>();
                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);

                if (Int32.TryParse(ddlEdadCultivo.SelectedValue.ToString(), out x))
                    parametersL.Add("@edad", ddlEdadCultivo.SelectedValue);
                

            }
            //mostrar lo de edad de cultivo y equipo de aplicacion
            else if (ddlPlanta.SelectedValue == "8") //--nayarit
            {
                cargaddlEdadCultivo(8);
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;
                ddlEdadCultivo.SelectedValue = dt.Rows[0]["iEdad"].ToString();

                cargaddlEquipoAplicacion();
                //ddlEquipoAplicacion.Enabled = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString();

                ckCapa.Visible = true;
                ckCapa.Checked = dt.Rows[0]["capar"].ToString() == "True" ? true : false;
                ckFinCiclo.Visible = true;
                ckFinCiclo.Checked = dt.Rows[0]["fin"].ToString() == "True" ? true : false;

                //cargar lo de los litros
                var parametersL = new Dictionary<string, object>();
                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);

                if (Int32.TryParse(ddlEdadCultivo.SelectedValue.ToString(), out x))
                    parametersL.Add("@edad", ddlEdadCultivo.SelectedValue);

                parametersL.Add("@capa", ckCapa.Checked == true ? 1 : 0);
                parametersL.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);

                if (Int32.TryParse(ddlEquipoAplicacion.SelectedValue.ToString(), out x))
                    parametersL.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

               
            }
            //tipo de boquilla y equipo de aplicacion
            else if (ddlPlanta.SelectedValue == "4") //--tuxca
            {
                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlTipoBoquilla.SelectedValue = dt.Rows[0]["idBoquilla"].ToString();

                cargaddlEquipoAplicacion();
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                //ddlEquipoAplicacion.Enabled = true;
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString();

                //cargar lo de los litros
                var parametersL = new Dictionary<string, object>();

                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);

                if (Int32.TryParse(ddlEquipoAplicacion.SelectedValue, out x))
                    parametersL.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

                if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                    parametersL.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
            }

            //mostrar el tipo de aplicación
            ddlTipoAplicacion.SelectedValue = dt.Rows[0]["tipoAplicacionId"].ToString();
            //txtObs.Text = dt.Rows[0]["comentarios"].ToString();
        }

    }
    
    [WebMethod]
    public static string GetValorInvernadero(string idInvernadero, string quimicos, string fecha, string index, string connection)
    {
        string sResult="";
        try{
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idPlanta", idInvernadero[0]);
            parameters.Add("@idInvernadero", idInvernadero);
            parameters.Add("@Quimicos", quimicos.Replace(" ", "") );
            parameters.Add("@fechaSugerida", fecha);

            var dt = DataAccess.executeStoreProcedureDataTable("spr_DisponibilidadQuimicoInvernadero", parameters, connection);
            
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows) // Loop over the items.
                {
                    sResult = sResult + item[0].ToString()+",";
                }
                //quitamos la coma del final
                sResult = sResult.Substring(0, sResult.Length - 1);
                sResult = sResult.Replace(" ", "");
            }
            else
            {
                sResult = "true";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }

        return sResult+"|"+index;
        
    }
    [WebMethod]
    public static string GetValoresRecalculados(string index, string fecha, string invernadero, string idCopy, string capar, string fin, string connection)
    {
        string sResult = "";
        try
        {
            var parameters = new Dictionary<string, object>();
            DateTime dfecha = DateTime.Parse(fecha);
            parameters.Add("@fechaSugerida", dfecha);
            parameters.Add("@idInvernadero", invernadero);
            parameters.Add("@idPrograma", idCopy);
            if (capar == "False") capar = "0"; if (capar == "True") capar = "1";
            parameters.Add("@capar", capar);
            if (fin == "False") fin = "0"; if (fin == "True") fin = "1";
            parameters.Add("@fin", fin);

            var dt = DataAccess.executeStoreProcedureDataTable("spr_CopiarProgramas", parameters, connection);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sResult += row[j];
                    if (j == dt.Columns.Count - 1)
                    {
                        if (i != (dt.Rows.Count - 1))
                            sResult += "|";
                    }
                    else
                        sResult += "$";
                }
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }

        return index + "@" + sResult;
    }

    #region limpiaderas
    private void LimpiarDesdePlanta()
    {
        //ddlInvernadero.SelectedIndex = 0;
        ddlInvernadero.Items.Clear();
        ddlInvernadero.Enabled = false;
        
        lblTipoBoquilla.Visible = false;
        ddlTipoBoquilla.Visible = false;

        lblSemanaCultivo.Visible = false;
        ddlEdadCultivo.Visible = false;
        ddlEquipoAplicacion.Visible = false;
        lblEquipoAplicacion.Visible = false;

        ckCapa.Visible = false;
        ckCapa.Checked = false;
        ckFinCiclo.Visible = false;
        ckFinCiclo.Checked = false;

        limpiarPrograma();
    }

    private void LimpiarDesdeInvernadero()
    {
        
        limpiarPrograma();

    }

    private void limpiarPrograma()
    {
        gdvPrograma.DataSource = null;
        gdvPrograma.DataBind();
    }

    #endregion

    #region cargas de ddl's
    //ddl's = drop down lists
    
    private void cargaddlEdadCultivo(int farm)
    {
        ddlEquipoAplicacion.Enabled = false;
        ddlEquipoAplicacion.Items.Clear();
        ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("Seleccione").ToString() );

        ddlEdadCultivo.Items.Clear();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        int x = 0;

        if (Int32.TryParse(farm.ToString(), out x))
            parameters.Add("@idFarm", farm);
        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEdadCultivo", parameters,this.Session["connection"].ToString());
            ddlEdadCultivo.Items.Add(GetLocalResourceObject("Seleccione").ToString() );
            ddlEdadCultivo.DataSource = dt2;
            ddlEdadCultivo.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorEdadDeCultivo").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlEquipoAplicacion()
    {
        ddlEquipoAplicacion.Items.Clear();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();

        int x = 0;

        if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);

        if (ddlPlanta.SelectedValue == "8")
            if (Int32.TryParse(ddlEdadCultivo.SelectedValue.ToString(), out x))
                parameters.Add("@edad", ddlEdadCultivo.SelectedValue); //este solo se una para nayarit

        if (ddlPlanta.SelectedValue == "4")
            if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue); //este solo se usa para tuxca

        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEquipoAplicacion", parameters, this.Session["connection"].ToString());
            ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("Seleccione").ToString() );
            ddlEquipoAplicacion.DataSource = dt2;
            ddlEquipoAplicacion.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorEquipo").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlTipoBoquilla()
    {
        ddlEquipoAplicacion.Enabled = false;
        ddlEquipoAplicacion.Items.Clear();
        ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("Seleccione").ToString() );

        ddlTipoBoquilla.Items.Clear();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        int x = 0;

        if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEquipoBoquilla", parameters, this.Session["connection"].ToString());
            ddlTipoBoquilla.Items.Add(GetLocalResourceObject("Seleccione").ToString() );
            ddlTipoBoquilla.DataSource = dt2;
            ddlTipoBoquilla.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorBoquilla").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlPlantas()
    {
        ddlPlanta.Items.Clear();
        var parameters = new Dictionary<string, object>();
        try
        {
            parameters.Add("@idUser", String.IsNullOrEmpty(Session["userIDCalidad"].ToString()) ? "0" : Session["userIDCalidad"].ToString());
            var dt = DataAccess.executeStoreProcedureDataTable("dbo.spr_GET_ddlPlantas", parameters, this.Session["connection"].ToString());
            ddlPlanta.Items.Add(GetLocalResourceObject("Seleccione").ToString() );
            ddlPlanta.DataSource = dt;
            ddlPlanta.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage( GetLocalResourceObject("ErrorPlanta").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlTipoAplicacion()
    {
        ddlTipoAplicacion.Items.Clear();
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_GET_ddlTipoAplicacion", parameters, this.Session["connection"].ToString());
            ddlTipoAplicacion.DataSource = ds;
            ddlTipoAplicacion.DataValueField = "idTipoAplicacion";
            ddlTipoAplicacion.DataTextField = "nombre";
            ddlTipoAplicacion.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }
    #endregion

    #region botones

    protected void btnValidar_Click(object sender, EventArgs e)
    {
        validacionSeleecionValores(1);
    }
        
    protected void save_Click(object sender, EventArgs e)
    {
        string[] invernaderos = Request.Form["invernaderoIds"].ToString().Split(',');
        int count = 0;
        bool error = false;
        foreach (string value in invernaderos){

            var parameters = new Dictionary<string, object>();

            //obtenemos el nombre
            parameters.Add("@vNombre", this.txtNombre.Text);
            //obtenemos el usuario
            parameters.Add("@vUserCreo", Session["usernameCalidad"].ToString()); 
            //obtenemos la planta
            parameters.Add("@idPlanta", this.ddlPlanta.SelectedValue);
            //obtenemos el invernadero
            parameters.Add("@idInvernadero", value);
            //obtenemos la semana
            DateTime fecha = DateTime.Parse(txtFechaAplicacion.Text.Trim());
            int w = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, fecha.DayOfWeek);
            parameters.Add("@iSemana", w);
            //obtenemos el año
            parameters.Add("@iYear", fecha.Year);
            //obtenemos fecha sugerida
            if (!string.IsNullOrEmpty(this.txtFechaAplicacion.Text) && DateTime.TryParseExact(this.txtFechaAplicacion.Text.Trim(), "yyyy-MM-dd", null, DateTimeStyles.None, out fecha))
            {
                parameters.Add("@dFechaSugerida", fecha);
            }
            //obtenemos edad cultivo
            parameters.Add("@iEdad", this.ddlEdadCultivo.SelectedValue);
            //obtenemos observaciones
            parameters.Add("@vObservaciones", txtObs.Text);
            
            //obtenemos tipo de boquilla
            if (ddlTipoBoquilla.SelectedValue == "" || ddlTipoBoquilla.SelectedValue == GetLocalResourceObject("Seleccione").ToString() )
            {
                parameters.Add("@idTipoBoquilla", 1);
            }
            else {
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
            }
            //obtenemos equipo de aplicacion
            parameters.Add("@idEquipoAplicacion", equipoApli.Value);
            
            //obtenemos horas
            String temporal = Request.Form["reentrada"+count].ToString().Replace(" hrs.","");
            String [] horas = temporal.Split(',');
            Decimal mayor = Decimal.Parse(horas[0]);
            foreach (String h in horas) {
                if (mayor < Decimal.Parse(h)) {
                    mayor = Decimal.Parse(h);
                }                
            }
            parameters.Add("@horas", mayor);
            //obtenemos tipo aplicacion
            parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
            //obtenemos el dagua
            String [] tmp_ = Request.Form["litros" + count].Split(',');
            parameters.Add("@dAgua", tmp_[0]);

            //obtenemos las listas de quimicos
            parameters.Add("@quimicosIds", Request.Form["quimicoId" + count].Replace(" ","") );
            parameters.Add("@cantidadesP", Request.Form["cantidadPedida" + count]);
            parameters.Add("@litros", Request.Form["litros" + count]);
            parameters.Add("@dosisMin", Request.Form["dosisMin" + count]);
            parameters.Add("@dosisMax", Request.Form["dosisMax" + count]);

            //llamamos sp que hace el guardado
            try
            {
                ViewState["idPrograma"] = DataAccess.executeStoreProcedureGetInt("spr_InsertaProgramacion", parameters, this.Session["connection"].ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar los químicos: " + ex.Message, Common.MESSAGE_TYPE.Error);
                error = true;
            }
            count += 1;
        }

        //mandamos mensaje
        if (error == false)
        {
            cargaCopia();
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaGuardado").ToString(), Common.MESSAGE_TYPE.Success);
        }
        else
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorGuardado").ToString(), Common.MESSAGE_TYPE.Error);
        }

    }

    #endregion

    #region validar botonazos

    private bool validacionesLlenarTodosLosCampos()
    {
        bool todoBien = true;
        StringBuilder cadenaErrores = new StringBuilder();

        //--validar que una planta y su invernadero esten seleccionados
        if (ddlPlanta.SelectedIndex == 0 || ddlInvernadero.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("PlantaInvernadero").ToString(), Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }
        if (String.IsNullOrEmpty(txtFechaAplicacion.Text.Trim()))
        {
            popUpMessageControl1.setAndShowInfoMessage("Seleccione la semana", Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }

        //--selecciono Nayarit, verificar que lleno todos los campos
        if (ddlPlanta.SelectedValue == "8")
        {
            if (ddlEdadCultivo.SelectedIndex == 0 || ddlEquipoAplicacion.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("EdadEquipo").ToString(), Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }
        }
        //--selecciono Colima, verificar que lleno todos los campos
        else if (ddlPlanta.SelectedValue == "5")
        {
            if (ddlEdadCultivo.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Edad").ToString(), Common.MESSAGE_TYPE.Error);
                todoBien = false;

            }
        }

         //--selecciono Txc, verificar que lleno todos los campos
        else if (ddlPlanta.SelectedValue == "4")
        {
            if (ddlTipoBoquilla.SelectedIndex == 0 || ddlEquipoAplicacion.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoquillaEquipo").ToString(), Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }
        }

        //--selecciono SIS o ZAP , verificar que lleno todos los campos
        else if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6")
        {
            if (ddlTipoBoquilla.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Boquilla").ToString(), Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }
        }
        return todoBien;
    }

    private bool validacionSeleecionValores(int verMensaje)
    {
        bool todoBien = true;
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idFarm", ddlPlanta.SelectedValue);

        //sis y zap
        if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6")
            parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);

            //col
        else if (ddlPlanta.SelectedValue == "5")
            parameters.Add("@edad", ddlEdadCultivo.SelectedValue);

            //naya
        else if (ddlPlanta.SelectedValue == "8")
        {
            parameters.Add("@edad", ddlEdadCultivo.SelectedValue);
            parameters.Add("@capa", ckCapa.Checked == true ? 1 : 0);
            parameters.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);
            parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
        }

        //tuxca
        else if (ddlPlanta.SelectedValue == "4")
        {
            parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
            parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
        }

        return todoBien;
    }

    private string hayQuimicos()
    {
        string quimicos = null;
        //--guaradr los ids de los qumicos con palomita        
        System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
        xmlString.AppendFormat("<{0}>", "Quimicos");
        bool alMenosUno = false;
        int i = 0;

        //--ver si ya habia datos en el grid de programación, y si hay, guardarlos tambien, para conservar todo
        if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
        {
            i = 0;
            foreach (GridViewRow row in gdvPrograma.Rows)
            {
                xmlString.AppendFormat("<c><id>{0}</id></c>", gdvPrograma.DataKeys[i].Value.ToString());
                i++;
            }
        }

        xmlString.AppendFormat("</{0}>", "Quimicos");

        if (!alMenosUno)
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectQuimico").ToString(), Common.MESSAGE_TYPE.Error);
        else
            quimicos = xmlString.ToString();
        return quimicos;
    }

    private void calculaParaNayarit(string quimicos)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue.ToString());
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedItem.ToString());
        if (ddlEdadCultivo.SelectedIndex != 0)
            parameters.Add("@edad", ddlEdadCultivo.SelectedValue.ToString());
        parameters.Add("@capar", ckCapa.Checked == true ? 1 : 0);
        parameters.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);

        parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue.ToString());
        parameters.Add("@Quimicos", quimicos.ToString());

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters,this.Session["connection"].ToString());
            if (dt.Rows.Count == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SinDatos").ToString(), Common.MESSAGE_TYPE.Error);
            }
            else
            {
                ViewState["GridPrograma"] = dt;
                gdvPrograma.DataSource = dt;
                gdvPrograma.DataBind();
            }
            //ViewState["GridBoletin"] = dt;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message,
                                                       Common.MESSAGE_TYPE.Error);
        }
    }

    private void calculaParaColima(string quimicos)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue.ToString());
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedItem.ToString());
        parameters.Add("@edad", ddlEdadCultivo.SelectedValue.ToString());
        parameters.Add("@Quimicos", quimicos.ToString());

        //validar que exista agua para la semana seleccionada, la capa y el fin

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters,this.Session["connection"].ToString());
            gdvPrograma.DataSource = dt;
            gdvPrograma.DataBind();
            ViewState["GridPrograma"] = dt;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void calculaParaSisZap(string quimicos)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue.ToString());
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedItem.ToString());
        parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue.ToString());
        parameters.Add("@Quimicos", quimicos.ToString());

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            gdvPrograma.DataSource = dt;
            gdvPrograma.DataBind();
            ViewState["GridPrograma"] = dt;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    private void calculaParaTuxca(string quimicos)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue.ToString());
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedItem.ToString());
        parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue.ToString());
        parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue.ToString());
        parameters.Add("@Quimicos", quimicos.ToString());

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            gdvPrograma.DataSource = dt;
            gdvPrograma.DataBind();
            ViewState["GridPrograma"] = dt;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDatos").ToString() + ex.Message, Common.MESSAGE_TYPE.Error);
        }
    }

    #endregion

    #region formatos del grid

    protected void gdvPrograma_PreRender(object sender, EventArgs e)
    {
        if (gdvPrograma.HeaderRow != null)
            gdvPrograma.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvPrograma_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:

                if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[5].Text).Trim()))
                {
                    e.Row.Cells[5].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[5].Text)).ToString();
                }
                
                break;
        }
    }

    #endregion

}