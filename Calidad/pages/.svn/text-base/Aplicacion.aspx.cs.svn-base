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
using System.IO;


public partial class pages_Aplicacion : BasePage 
{
        
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["usernameCalidad"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx", false);
                }

                ViewState["idPrograma"] = Convert.ToString(Request.QueryString["idPrograma"]) != null ? Convert.ToString(Request.QueryString["idPrograma"]) : "-1";

                DateTime thisDay = DateTime.Today;
                txtFechaAplicacion.Text = thisDay.ToString("yyyy-MM-dd");

                //cargaddlBasicos();
                LimpiarDesdePlanta();
                cargaddlPlantas();
                cargaddlTipoAplicacion();
                cargaddlPlagas();
                btnAdd.Visible = false;
                //viene del manager
                if (Int32.Parse(ViewState["idPrograma"].ToString()) > 0)
                {
                    cargaDatos();                    
                }
                else
                    etiquetaBotonGuardar();
            }
            else
            {
                if (Session["usernameCalidad"] == null)
                {
                    
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SesionExpirada").ToString(),Common.MESSAGE_TYPE.Warning); //popUpMessageControl1.setAndShowInfoMessage("Su sesión ha expirado. Por favor, refresque la página", Common.MESSAGE_TYPE.Warning);
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

    /*private int correspondenSemanas()
    {
        //DateTime SemanaInicial = new DateTime(Int32.Parse(ddlYear.SelectedValue.ToString()), DateTime.Today.Month, DateTime.Today.Day);
        //DateTime SemanaFinal = new DateTime(Int32.Parse(ddlYear.SelectedValue.ToString()), 12, 31);
        //int weeks = 0;
        //int w = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(SemanaInicial, CalendarWeekRule.FirstFourDayWeek, SemanaInicial.DayOfWeek);
        //if (SemanaFinal.DayOfWeek.Equals(DayOfWeek.Thursday) || (isBisiesto(SemanaFinal.Year) && (SemanaFinal.DayOfWeek.Equals(DayOfWeek.Thursday) || SemanaFinal.DayOfWeek.Equals(DayOfWeek.Friday))))
        //    weeks = 53;
        //else weeks = 52;
        //return weeks;
        return 0;
    }*/

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

    private void cargaDatos()
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }  

        //cargar grid
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", ViewState["idPrograma"].ToString());
        DataTable dt = null;

        int x = 0;


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
                
                txtNombre.Text = dt.Rows[0]["vNombre"].ToString();
            }

        }
        catch (Exception EX)
        {
            Log.Error(EX); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorProcesoDeDatos").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar los datos", Common.MESSAGE_TYPE.Error);
        }
                

        //cargar lo de arriba (año, semana, planta, inv....)
        if (dt.Rows.Count > 0)
        {

            etiquetaBotonGuardar();
            
            //int semana = DateTime.Now.Year + DateTime.Now.
            //DateTime t = new DateTime(Int32.Parse(dt.Rows[0]["iYear"].ToString()), 1, 1).AddDays(Int32.Parse(dt.Rows[0]["iSemana"].ToString()) * 7);
            DateTime t = Convert.ToDateTime( dt.Rows[0]["diaSugerido"].ToString() );
            txtFechaAplicacion.Text = t.ToString("yyyy-MM-dd");
            
            //carga planta
            ddlPlanta.SelectedValue = dt.Rows[0]["idPlanta"].ToString();
           
            //carga invernadero
            parameters.Clear();
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            ddlInvernadero.Items.Clear();
            ddlInvernadero.Enabled = true;
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderos", parameters, this.Session["connection"].ToString());
            if (dt2.Rows.Count > 0)
            {
                ddlInvernadero.Items.Add(GetLocalResourceObject("seleccione").ToString());
                ddlInvernadero.DataSource = dt2;
                ddlInvernadero.DataBind();
                ddlInvernadero.SelectedValue = dt.Rows[0]["idInvernadero"].ToString();

                parameters.Clear();
                parameters.Add("@Greenhouse", ddlInvernadero.SelectedValue);
                var dt4 = DataAccess.executeStoreProcedureDataTable("spr_GET_InvernaderoInfo", parameters, this.Session["connection"].ToString());
                if (dt4.Rows.Count > 0)
                {
                    lblHec.Visible = true;
                    lblHec2.Visible = true;
                    lblHec2.Text = dt4.Rows[0]["Hectares"].ToString();
                }
            }

            //carga los datos configuración
            //mostrar tipo de boquilla
            if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6" || ddlPlanta.SelectedValue == "7") //--sis y zap
            {
                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlTipoBoquilla.SelectedValue = dt.Rows[0]["idBoquilla"].ToString();

                //carga equipo
                cargaddlEquipoAplicacion();
                ddlEquipoAplicacion.Enabled = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString();

                //cargar lo de los litros 
                var parametersL = new Dictionary<string, object>();
                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);

                if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                    parametersL.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
                try
                {
                    var dtL = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parametersL, this.Session["connection"].ToString());
                    if (dtL.Rows.Count > 0)
                    {
                        lblLts.Visible = true;
                        LblLts2.Visible = true;
                        LblLts2.Text = String.Empty;
                        LblLts2.Text = dtL.Rows[0]["volumen"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex); 
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros" , Common.MESSAGE_TYPE.Error);
                }
            }
                
                
            //mostrar lo de edad de cultivo
            else if (ddlPlanta.SelectedValue == "5") //--colima
            {
                cargaddlEdadCultivo(5);
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;
                ddlEdadCultivo.SelectedValue = dt.Rows[0]["iEdad"].ToString();


                //carga equipo
                cargaddlEquipoAplicacion();
                ddlEquipoAplicacion.Enabled = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString();


                //cargar lo de los litros
                var parametersL = new Dictionary<string, object>();
                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);

                if (Int32.TryParse(ddlEdadCultivo.SelectedValue.ToString(), out x))
                    parametersL.Add("@edad", ddlEdadCultivo.SelectedValue);
                try
                {
                    var dtL = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parametersL, this.Session["connection"].ToString());
                    if (dtL.Rows.Count > 0)
                    {
                        lblLts.Visible = true;
                        LblLts2.Visible = true;
                        LblLts2.Text = String.Empty;
                        LblLts2.Text = dtL.Rows[0]["volumen"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex); 
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros", Common.MESSAGE_TYPE.Error);
                }

            }
            //mostrar lo de edad de cultivo y equipo de aplicacion
            else if (ddlPlanta.SelectedValue == "8") //--nayarit
            {
                cargaddlEdadCultivo(8);
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;
                ddlEdadCultivo.SelectedValue = dt.Rows[0]["iEdad"].ToString();

                cargaddlEquipoAplicacion();
                ddlEquipoAplicacion.Enabled = true; 
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

                if (dt.Rows[0]["capar"].ToString() == "1" || dt.Rows[0]["capar"].ToString() == "True")
                    ckCapa.Checked = true;
                else
                    ckCapa.Checked = false;

                if (dt.Rows[0]["fin"].ToString() == "1" || dt.Rows[0]["fin"].ToString() == "True")
                    ckFinCiclo.Checked = true;
                else
                    ckFinCiclo.Checked = false;

               parametersL.Add("@capa", ckCapa.Checked == true ? 1 : 0);
               parametersL.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);

                if (Int32.TryParse(ddlEquipoAplicacion.SelectedValue.ToString(), out x))
                    parametersL.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
               
                try
                {
                    var dtL = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parametersL, this.Session["connection"].ToString());
                    if (dtL.Rows.Count > 0)
                    {
                        lblLts.Visible = true;
                        LblLts2.Visible = true;
                        LblLts2.Text = String.Empty;
                        LblLts2.Text = dtL.Rows[0]["volumen"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex); 
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros" , Common.MESSAGE_TYPE.Error);
                }
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
                ddlEquipoAplicacion.Enabled = true;
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString();

                //cargar lo de los litros
                var parametersL = new Dictionary<string, object>();



                if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
                    parametersL.Add("@idFarm", ddlPlanta.SelectedValue);


                if (Int32.TryParse(ddlEquipoAplicacion.SelectedValue, out x))
                    parametersL.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);


                if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                    parametersL.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);

                try
                {
                    var dtL = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parametersL, this.Session["connection"].ToString());
                    if (dtL.Rows.Count > 0)
                    {
                        lblLts.Visible = true;
                        LblLts2.Visible = true;
                        LblLts2.Text = String.Empty;
                        LblLts2.Text = dtL.Rows[0]["volumen"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros", Common.MESSAGE_TYPE.Error);
                }
            }


            else
            {
                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlTipoBoquilla.SelectedValue = dt.Rows[0]["idBoquilla"].ToString();

                

                cargaddlEdadCultivo(Int32.Parse(ddlPlanta.SelectedValue));
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;
                ddlEdadCultivo.SelectedValue = dt.Rows[0]["iEdad"].ToString();


                ckCapa.Visible = true;
                ckCapa.Checked = dt.Rows[0]["capar"].ToString() == "True" ? true : false;
                ckFinCiclo.Visible = true;
                ckFinCiclo.Checked = dt.Rows[0]["fin"].ToString() == "True" ? true : false;


                //carga equipo
                cargaddlEquipoAplicacion();
                ddlEquipoAplicacion.Enabled = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                ddlEquipoAplicacion.SelectedValue = dt.Rows[0]["idEquipoAplicacion"].ToString();

                parameters.Clear();
                parameters.Add("@idFarm", ddlPlanta.SelectedValue);

                if (ddlEdadCultivo.SelectedIndex > 0) 
                    parameters.Add("@edad", ddlEdadCultivo.SelectedValue);
                else
                    parameters.Add("@edad", 0);

                parameters.Add("@capa", ckCapa.Checked == true ? 1 : 0);
                parameters.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);


                if (ddlEquipoAplicacion.SelectedIndex > 0) 
                    parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
                else
                    parameters.Add("@idEquipoAplicacion", 0);

                if(ddlTipoBoquilla.SelectedIndex > 0)
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
                else
                    parameters.Add("@idTipoBoquilla", 0);

                try
                {
                    var dt4 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parameters, this.Session["connection"].ToString());
                    if (dt4.Rows.Count > 0)
                    {
                        lblLts.Visible = true;
                        LblLts2.Visible = true;
                        LblLts2.Text = String.Empty;
                        LblLts2.Text = dt4.Rows[0]["volumen"].ToString();
                    }
                    else
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente a los litros. Por favor, asegurese de que los datos introducidos son correctos", Common.MESSAGE_TYPE.Error);

                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente a los litros", Common.MESSAGE_TYPE.Error);
                }


            }
            //mostrar el tipo de aplicación
            ddlTipoAplicacion.SelectedValue = dt.Rows[0]["tipoAplicacionId"].ToString();

            txtObs.Text = dt.Rows[0]["comentarios"].ToString();


            //si ya esta cancelado o contestado por almacen, ocultar cosas
            string staus = dt.Rows[0]["estatus"].ToString();
            if (staus == "Cancelado" || staus == "Entregado" || staus == "Ejecutado" || staus == "Entrega Parcial" || staus == "Cancelo Almacen")
            {
                btnBuscar.Visible = false;
                ddlFiltro.Enabled = false;
                btnGuardar.Visible = false;                
                ddlPlanta.Enabled = false;
                ddlInvernadero.Enabled = false;
                ddlTipoBoquilla.Enabled = false;
                ddlEquipoAplicacion.Enabled = false;
                ddlEdadCultivo.Enabled = false;                
                ddlTipoAplicacion.Enabled = false;
                txtNombre.Enabled = false;
                txtFechaAplicacion.Enabled = false;
                ckCapa.Enabled = false;
                ckFinCiclo.Enabled = false;
                btnAutorizar.Visible = false;

            }

            //aqui guardaremos el rol del usuiario que entro, solo para poder usar la variable despues, a la hora de msotrar los botones correspondientes
            //para autorizar la aplicacion o enviar el correo
            //etiquetaBotonGuardar();
        }

    }

    public bool getNumero(string inferior, string superior, decimal solicitado)
    {
        bool isNumero = true;
        string cadena1 = String.Empty;

        if (inferior.IndexOf(' ') > 0)
            cadena1 = inferior.Substring(0, inferior.IndexOf(' '));
        else
            cadena1 = inferior;

        decimal d;
        if (!decimal.TryParse(cadena1, out d))
            return false;

        decimal d2;
        string cadena2 = String.Empty;
        if (superior.IndexOf(' ') > 0)
            cadena2 = superior.Substring(0, superior.IndexOf(' '));
        else
            cadena2 = superior;

        if (!decimal.TryParse(cadena2, out d2))
            return false;

        if (solicitado > d2 || solicitado < d)
            isNumero = false;

        return isNumero;
    }

    #region limpiaderas
    private void LimpiarDesdePlanta() 
    {
        //ddlInvernadero.SelectedIndex = 0;
        ddlInvernadero.Items.Clear();
        ddlInvernadero.Enabled = false;
        
        lblHec.Visible = false;
        lblHec2.Visible = false;
        lblHec2.Text = String.Empty;

        lblTipoBoquilla.Visible = false;
        ddlTipoBoquilla.Visible = false;

        lblSemanaCultivo.Visible = false;
        ddlEdadCultivo.Visible = false; 
        ddlEquipoAplicacion.Visible = false;
        lblEquipoAplicacion.Visible = false;
        
        lblLts.Visible = false;
        LblLts2.Visible = false;
        LblLts2.Text = String.Empty;

        ckCapa.Visible = false;
        ckCapa.Checked = false;
        ckFinCiclo.Visible = false;
        ckFinCiclo.Checked = false;

        ckTratamiento.Checked = false;
        txtTratamientoNom.Text = String.Empty;

        gdvQumicosBuscados.DataSource = null;
        gdvQumicosBuscados.DataBind();

        gdvTratamientosBuscados.DataSource = null;
        gdvTratamientosBuscados.DataBind();

        limpiarPrograma();
    }
       
    private void LimpiarDesdeInvernadero()
    {       
        lblHec.Visible = false;
        lblHec2.Visible = false;
        lblHec2.Text = String.Empty;

        if (gdvPrograma.Rows.Count > 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("CambioInvernadero").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("El cambio en Invernadero provoca limpiar los químicos encontrados.", Common.MESSAGE_TYPE.Error);
        }

        gdvTratamientosBuscados.DataSource = null;
        gdvTratamientosBuscados.DataBind();

        gdvQumicosBuscados.DataSource = null;
        gdvQumicosBuscados.DataBind();

        limpiarPrograma();
        
    }

    private void limpiarPrograma()
    {
        gdvPrograma.DataSource = null;
        gdvPrograma.DataBind();
        btnGuardar.Visible = true;
        ckTratamiento.Checked = false;
        txtTratamientoNom.Text = String.Empty;
    }

    //mostrar boton que corresponde
    private void etiquetaBotonGuardar()
    {
        var parameters = new Dictionary<string, object>();
        parameters.Clear();
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        try
        {
            var ds2 = DataAccess.executeStoreProcedureDataTable("spr_AccesoUsuario", parameters, this.Session["connection"].ToString());
            int rol = 0;
            if (ds2.Rows.Count > 0)
            {
                rol = Int32.Parse(ds2.Rows[0]["roleIds"].ToString());
                
                ViewState["idRol"] = rol;

                if (rol > 0)
                {
                    if (rol == 1 || rol == 2)
                    {
                        btnGuardar.Text = GetLocalResourceObject("Guardar").ToString();                      
                        btnAutorizar.Text = GetLocalResourceObject("Autorizar").ToString();
                        if (Int32.Parse(ViewState["idPrograma"].ToString()) > 0)
                            btnAutorizar.Visible = true;
                        else
                            btnAutorizar.Visible = false;
                    }
                    else
                    {
                        //enviar correo 
                        btnGuardar.Text = GetLocalResourceObject("EnviarCorreo").ToString();
                        btnAutorizar.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardandoQuimicos").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }
   
    private void LimpiarLitros()
    {
        lblLts.Visible = false;
        LblLts2.Visible = false;
        LblLts2.Text = String.Empty;
       
    }

    #endregion

    #region cargas de ddl's

   
    private void cargaddlEdadCultivo(int farm) 
    {
        ddlEquipoAplicacion.Enabled = false;
        ddlEquipoAplicacion.Items.Clear();
        ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString()); 

        ddlEdadCultivo.Items.Clear();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        int x = 0;

        if (Int32.TryParse(farm.ToString(), out x))
            parameters.Add("@idFarm",farm);
        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEdadCultivo", parameters, this.Session["connection"].ToString());
            ddlEdadCultivo.Items.Add(GetLocalResourceObject("seleccione").ToString());
            ddlEdadCultivo.DataSource = dt2;
            ddlEdadCultivo.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorEdadDelCultivo").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar la edad del cultivo", Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlEquipoAplicacion()
    {       
        ddlEquipoAplicacion.Items.Clear();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();

        int x = 0;

        if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
        parameters.Add("@idFarm", ddlPlanta.SelectedValue); 

        if(ddlPlanta.SelectedValue == "8")
            if (Int32.TryParse(ddlEdadCultivo.SelectedValue.ToString(), out x))
                parameters.Add("@edad", ddlEdadCultivo.SelectedValue); //este solo se una para nayarit

        if (ddlPlanta.SelectedValue == "4")
            if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue); //este solo se usa para tuxca

        if (ddlPlanta.SelectedValue == "3" || ddlPlanta.SelectedValue == "9")
            if (Int32.TryParse(ddlTipoBoquilla.SelectedValue.ToString(), out x))
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue); //este solo se usa para tuxca

       
        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEquipoAplicacion", parameters, this.Session["connection"].ToString());            
            if (dt2.Rows.Count > 0)
            {
                ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString()); 
                ddlEquipoAplicacion.DataSource = dt2;
                ddlEquipoAplicacion.DataBind();
            }
            else
            {
                parameters.Clear();
                parameters.Add("@idFarm", ddlPlanta.SelectedValue);
                dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEquipoAplicacionXCarga", parameters, this.Session["connection"].ToString());
                if (dt2.Rows.Count > 0)
                {
                    ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString());
                    ddlEquipoAplicacion.DataSource = dt2;
                    ddlEquipoAplicacion.DataBind();
                }                
            }
          
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorEquipoDeAplicacion").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar el equipo de aplicación" , Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlTipoBoquilla()
    {
        ddlEquipoAplicacion.Enabled = false;
        ddlEquipoAplicacion.Items.Clear();
        ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString()); 
        
        ddlTipoBoquilla.Items.Clear();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        int x = 0;

        if (Int32.TryParse(ddlPlanta.SelectedValue.ToString(), out x))
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);        
        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlEquipoBoquilla", parameters, this.Session["connection"].ToString());
            ddlTipoBoquilla.Items.Add(GetLocalResourceObject("seleccione").ToString());
            ddlTipoBoquilla.DataSource = dt2;
            ddlTipoBoquilla.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorBoquilla").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar el tipo de boquilla", Common.MESSAGE_TYPE.Error);
        }
    }
    
    private void cargaddlPlagas() 
    {
        var parameters = new Dictionary<string, object>();

        try
        {
            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlPlaga", parameters, this.Session["connection"].ToString());
            ddlFiltro.DataValueField = "campoId";
            ddlFiltro.DataTextField = "campoNombre";
            ddlFiltro.DataSource = dt2;
            ddlFiltro.DataBind();

        }catch(Exception ex){
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorPlagas").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar las plagas" , Common.MESSAGE_TYPE.Error);
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
            ddlPlanta.Items.Add(GetLocalResourceObject("seleccione").ToString());            
            ddlPlanta.DataSource = dt;
            ddlPlanta.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorPlantas").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar las plantas", Common.MESSAGE_TYPE.Error);
        }
    }

    private void cargaddlTipoAplicacion(){
        ddlTipoAplicacion.Items.Clear();
        try
        {
            ddlTipoAplicacion.Items.Clear();
            ddlTipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString()); 
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
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorTiposDeAplicacion").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar los tipos de aplicación", Common.MESSAGE_TYPE.Error);
        }
    }
    #endregion

    #region selected dopdownlist

    protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
    {
        LimpiarDesdePlanta();
        if (ddlPlanta.SelectedIndex == 0)
        {            
            return;
        }
        else 
        {            
            //validar que tenga confuguracion de capacidad de carga
            var parameters = new Dictionary<string, object>();
            //validar que el equipo de aplicacion seleccionado, tiene confugurada capacidad
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            var existe = DataAccess.executeStoreProcedureDataTable("spr_ExisteCapacidadXPlanta", parameters, this.Session["connection"].ToString());
            if (existe.Rows.Count == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorCapacidadCarga").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("La planta "+ ddlPlanta.SelectedItem + " no tiene configurada la capacidad de carga. Configure la carga en el catálogo de CARGAS", Common.MESSAGE_TYPE.Error);
                ddlPlanta.SelectedIndex = 0;
                return;
            }

            parameters.Clear();
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            ddlInvernadero.Enabled = true;
            try
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlInvernaderos", parameters, this.Session["connection"].ToString());
                ddlInvernadero.Items.Add(GetLocalResourceObject("seleccione").ToString());                
                ddlInvernadero.DataSource = dt;
                ddlInvernadero.DataBind();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorInvernaderos").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar los invernaderos " , Common.MESSAGE_TYPE.Error);
            }

            //mostrar tipo de boquilla
            if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6" || ddlPlanta.SelectedValue == "7" ) //--sis y zap, cdi
            {
                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true; cargaddlEquipoAplicacion();
            }

            //mostrar lo de edad de cultivo
            else if (ddlPlanta.SelectedValue == "5") //--colima
            {
                cargaddlEdadCultivo(5);
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;

            }
            //mostrar lo de edad de cultivo y equipo de aplicacion
            else if (ddlPlanta.SelectedValue == "8") //--nayarit
            {
                cargaddlEdadCultivo(8); 
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;                
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                ckCapa.Visible = true;
                ckCapa.Checked = false;
                ckFinCiclo.Visible = true;
                ckFinCiclo.Checked = false;
            }
            //tipo de boquilla y equipo de aplicacion
            else if (ddlPlanta.SelectedValue == "4") //--tuxca
            {
                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true; 
            }

            else
            {
                cargaddlEdadCultivo(Int32.Parse( ddlPlanta.SelectedValue)  );
                lblSemanaCultivo.Visible = true;
                ddlEdadCultivo.Visible = true;

                ckCapa.Visible = true;
                ckCapa.Checked = false;
                ckFinCiclo.Visible = true;
                ckFinCiclo.Checked = false;

                cargaddlTipoBoquilla();
                lblTipoBoquilla.Visible = true;
                ddlTipoBoquilla.Visible = true;
                ddlEquipoAplicacion.Visible = true;
                lblEquipoAplicacion.Visible = true;
                cargaddlEquipoAplicacion();
            }

        }
    }
    
    protected void ddlInvernadero_SelectedIndexChanged(object sender, EventArgs e)
    {
        LimpiarDesdeInvernadero(); 
        if (ddlInvernadero.SelectedIndex == 0)
        {            
            return;
        }
        else
        {            
            var parameters = new Dictionary<string, object>();
            parameters.Add("@Greenhouse", ddlInvernadero.SelectedValue);
            try 
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_InvernaderoInfo", parameters, this.Session["connection"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblHec.Visible = true;
                    lblHec2.Visible = true;
                    lblHec2.Text = dt.Rows[0]["Hectares"].ToString();                    
                }

            }catch(Exception ex){
                Log.Error(ex); 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorInvernadero").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente al invernadero " , Common.MESSAGE_TYPE.Error);
            }
        }
    }

    protected void ddlTipoBoquilla_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiarPrograma();
        if (ddlTipoBoquilla.SelectedIndex == 0)
        {
            LimpiarLitros();
            ddlEquipoAplicacion.Items.Clear();
            ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString());
            ddlEquipoAplicacion.Enabled = false;
            return;
        }

        if (ddlPlanta.SelectedValue == "4" || ddlPlanta.SelectedValue == "3" || ddlPlanta.SelectedValue == "9")
        {
            lblLts.Visible = false;
            LblLts2.Visible = false;
            ddlEquipoAplicacion.Enabled = true;
            cargaddlEquipoAplicacion();
        }

        else
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
            try
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parameters, this.Session["connection"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblLts.Visible = true;
                    LblLts2.Visible = true;
                    LblLts2.Text = String.Empty;
                    LblLts2.Text = dt.Rows[0]["volumen"].ToString();

                    ddlEquipoAplicacion.Enabled = true;
                    cargaddlEquipoAplicacion();
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex); 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros ", Common.MESSAGE_TYPE.Error);
            }
        }
    }

    protected void ddlEdadCultivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiarPrograma(); 
        if (ddlEdadCultivo.SelectedIndex == 0)
        {
            lblLts.Visible = false;
            LblLts2.Visible = false;
            ddlEquipoAplicacion.Items.Clear();
            ddlEquipoAplicacion.Items.Add(GetLocalResourceObject("seleccione").ToString()); 
            ddlEquipoAplicacion.Enabled = false;
            return;
        }

        if (ddlPlanta.SelectedValue == "8")
        {
            lblLts.Visible = false;
            LblLts2.Visible = false; 
            ddlEquipoAplicacion.Enabled = true;
            cargaddlEquipoAplicacion();
        }

        else
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            parameters.Add("@edad", ddlEdadCultivo.SelectedValue);
            try
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parameters, this.Session["connection"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblLts.Visible = true;
                    LblLts2.Visible = true;
                    LblLts2.Text = String.Empty;
                    LblLts2.Text = dt.Rows[0]["volumen"].ToString();
                    ddlEquipoAplicacion.Enabled = true;                    
                    cargaddlEquipoAplicacion();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex); 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros " , Common.MESSAGE_TYPE.Error);
            }
        }
    }

    protected void ddlEquipoAplicacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //limpiarPrograma(); 
        if (ddlEquipoAplicacion.SelectedIndex == 0)
        {
            LimpiarLitros();
            return;
        }
        
        var parameters = new Dictionary<string, object>();
        //validar que el equipo de aplicacion seleccionado, tiene confugurada capacidad
        parameters.Add("@idFarm", ddlPlanta.SelectedValue);
        parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

        var existe = DataAccess.executeStoreProcedureDataTable("spr_ExisteCapacidadEquipoAplicacion", parameters, this.Session["connection"].ToString());
        if (existe.Rows.Count == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorCapacidadCarga").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("El equipo de aplicación seleccionado no tiene configurada la capacidad de carga. Elija uno diferente o configure el equipo en el catálogo de CARGAS", Common.MESSAGE_TYPE.Error);
            ddlEquipoAplicacion.SelectedIndex = 0;
            return;
        }

        
        parameters.Clear();
        parameters.Add("@idFarm", ddlPlanta.SelectedValue);
        if (ddlPlanta.SelectedValue == "8" || ddlPlanta.SelectedValue == "3" || ddlPlanta.SelectedValue == "9")
        {
            if(ddlEdadCultivo.SelectedIndex > 0)
                parameters.Add("@edad", ddlEdadCultivo.SelectedValue);
            else
                parameters.Add("@edad", 0);
            parameters.Add("@capa", ckCapa.Checked == true ? 1 : 0);
            parameters.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);
        }

        parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

        if (ddlPlanta.SelectedValue == "4" || ddlPlanta.SelectedValue == "3" || ddlPlanta.SelectedValue == "9")
        {
            if(ddlTipoBoquilla.SelectedIndex > 0)
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
            else
                parameters.Add("@idTipoBoquilla", 0);

        }
        if (ddlPlanta.SelectedValue == "4" || ddlPlanta.SelectedValue == "8" || ddlPlanta.SelectedValue == "3" || ddlPlanta.SelectedValue == "9")
        {
            try
            {
                var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parameters, this.Session["connection"].ToString());
                if (dt.Rows.Count > 0)
                {
                    lblLts.Visible = true;
                    LblLts2.Visible = true;
                    LblLts2.Text = String.Empty;
                    LblLts2.Text = dt.Rows[0]["volumen"].ToString();
                }
                else
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente a los litros. Por favor, asegurese de que los datos introducidos son correctos", Common.MESSAGE_TYPE.Error);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente a los litros", Common.MESSAGE_TYPE.Error);
            }
        }

    }

    #endregion

    #region botones

    #region axiliares de botonozos

    protected bool validarTratamiento()
    {
        if (String.IsNullOrEmpty(txtTratamientoNom.Text.Trim()))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("AgregarTratamiento").ToString(), Common.MESSAGE_TYPE.Error); //Para guardar un Tratamiento es necesario introducir un nombre", Common.MESSAGE_TYPE.Error);
            return false; ;
        }

        System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
        xmlString.AppendFormat("<{0}>", "Quimicos");
        int z = 0;
        if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
        {
            foreach (GridViewRow row in gdvPrograma.Rows)
            {
                xmlString.AppendFormat("<c><id>{0}</id>", gdvPrograma.DataKeys[z].Value.ToString());
                xmlString.AppendFormat("<agua>{0}</agua>", gdvPrograma.Rows[z].Cells[7].Text); //--agua
                xmlString.AppendFormat("<caninf>{0}</caninf>", gdvPrograma.Rows[z].Cells[8].Text);//--desde
                xmlString.AppendFormat("<cansup>{0}</cansup>", gdvPrograma.Rows[z].Cells[9].Text);//--hasta
                xmlString.AppendFormat("<canped>{0}</canped></c>", Decimal.Parse(((TextBox)gdvPrograma.Rows[z].FindControl("TextBox2")).Text));
                z++;
            }
        }

        xmlString.AppendFormat("</{0}>", "Quimicos");
        var parameters = new Dictionary<string, object>();
        parameters.Clear();
        parameters.Add("@nombre", txtTratamientoNom.Text.Trim());
        parameters.Add("@planta", ddlPlanta.SelectedValue);
        parameters.Add("@Quimicos", xmlString.ToString());

        try
        {
            // -1 = nombre repetido           
            //x = id de tratamiento con esos quimicos
            float permiso = DataAccess.executeStoreProcedureFloat("spr_Valida_Tratamientos", parameters, this.Session["connection"].ToString());

            if (permiso == -1)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("TratamientoRepetido").ToString(), Common.MESSAGE_TYPE.Warning);
                txtTratamientoNom.Text = String.Empty;
                return false;
            }
            else if (permiso > 0)
            {
                //cualquier otro valor, nos indica el id del tratamiento que tiene los quimicos enviados            
                parameters.Clear();
                parameters.Add("@idTratamiento", permiso);
                var dt2 = DataAccess.executeStoreProcedureDataTable("Spr_get_InfoTratamientoRepetido", parameters, this.Session["connection"].ToString());
                string error = string.Format((GetLocalResourceObject("ExistQuimicos").ToString()), dt2.Rows[0]["nomPlanta"].ToString(), dt2.Rows[0]["vNombre"].ToString());
                popUpMessageControl1.setAndShowInfoMessage(error, Common.MESSAGE_TYPE.Warning);
                return false;
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorGuardarTratamiento").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar el Tratamiento " , Common.MESSAGE_TYPE.Error);
        }

        return true;
    }

    protected Dictionary<string, object> parametrosParaGuardarAplicacion()
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        parameters.Add("@nombre", txtNombre.Text.Trim());
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue);
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedValue);
        parameters.Add("@year", numeroYear());
        parameters.Add("@semana", numeroSemana());
        parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);

        if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6")
            parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);

        else if (ddlPlanta.SelectedValue == "4")
        {
            parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue);
        }

        else if (ddlPlanta.SelectedValue == "5")
            parameters.Add("@edad", ddlEdadCultivo.SelectedValue);

        else if (ddlPlanta.SelectedValue == "8")
        {
            parameters.Add("@edad", ddlEdadCultivo.SelectedValue);
        }
        else
        {

            if (ddlEdadCultivo.SelectedIndex > 0)
                parameters.Add("@edad", ddlEdadCultivo.SelectedValue);

            if (ddlTipoBoquilla.SelectedIndex > 0)
                parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue.ToString());
        }

        //leer los quimicos del gdv
        System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
        xmlString.AppendFormat("<{0}>", "Quimicos");
        int z = 0;
        if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
        {
            foreach (GridViewRow row in gdvPrograma.Rows)
            {
                xmlString.AppendFormat("<c><id>{0}</id>", gdvPrograma.DataKeys[z].Value.ToString());
                xmlString.AppendFormat("<agua>{0}</agua>", gdvPrograma.Rows[z].Cells[7].Text); //--agua
                xmlString.AppendFormat("<caninf>{0}</caninf>", gdvPrograma.Rows[z].Cells[8].Text);//--desde
                xmlString.AppendFormat("<cansup>{0}</cansup>", gdvPrograma.Rows[z].Cells[9].Text);//--hasta
                xmlString.AppendFormat("<canped>{0}</canped></c>", Decimal.Parse(((TextBox)gdvPrograma.Rows[z].FindControl("TextBox2")).Text));
                z++;
            }
        }

        xmlString.AppendFormat("</{0}>", "Quimicos");
        parameters.Add("@Quimicos", xmlString.ToString());
        parameters.Add("@vObser", txtObs.Text);
        parameters.Add("@horas", lblHorasRenntreda.Text.Trim());
        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        return parameters;
    }

    protected void guardarAplicacionNueva()
    {
        var parameters = new Dictionary<string, object>();
        parameters = parametrosParaGuardarAplicacion();

        try
        {
            ViewState["idPrograma"] = DataAccess.executeStoreProcedureGetInt("spr_INSERT_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaGuardado").ToString(), Common.MESSAGE_TYPE.Success); //popUpMessageControl1.setAndShowInfoMessage("Programa guardado exitosamente", Common.MESSAGE_TYPE.Success);
            autorizarPrograma(0);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardandoQuimicos").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar los químicos ", Common.MESSAGE_TYPE.Error);
        }

        if (ckTratamiento.Checked)
        {
            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
            xmlString.AppendFormat("<{0}>", "Quimicos");
            int z = 0;
            if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
            {
                foreach (GridViewRow row in gdvPrograma.Rows)
                {
                    xmlString.AppendFormat("<c><id>{0}</id>", gdvPrograma.DataKeys[z].Value.ToString());
                    xmlString.AppendFormat("<agua>{0}</agua>", gdvPrograma.Rows[z].Cells[7].Text); //--agua
                    xmlString.AppendFormat("<caninf>{0}</caninf>", gdvPrograma.Rows[z].Cells[8].Text);//--desde
                    xmlString.AppendFormat("<cansup>{0}</cansup>", gdvPrograma.Rows[z].Cells[9].Text);//--hasta
                    xmlString.AppendFormat("<canped>{0}</canped></c>", Decimal.Parse(((TextBox)gdvPrograma.Rows[z].FindControl("TextBox2")).Text));
                    z++;
                }
            }

            xmlString.AppendFormat("</{0}>", "Quimicos");

            parameters.Clear();
            parameters.Add("@nombre", txtTratamientoNom.Text.Trim());
            parameters.Add("@planta", ddlPlanta.SelectedValue);
            parameters.Add("@Quimicos", xmlString.ToString());

            try
            {
                // -1 = nombre repetido
                // 0 = guardado
                //x = id de tratamiento con esos quimicos
                float permiso = DataAccess.executeStoreProcedureFloat("spr_INSERT_Tratamientos", parameters, this.Session["connection"].ToString());

                if (permiso == -1)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("TratamientoRepetido").ToString(), Common.MESSAGE_TYPE.Warning);
                    txtTratamientoNom.Text = String.Empty;
                }
                else if (permiso == 0)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Guardado").ToString(), Common.MESSAGE_TYPE.Success);
                    ckTratamiento.Checked = false;
                    txtTratamientoNom.Text = String.Empty;

                }
                else //cualquier otro valor, nos indica el id del tratamiento que tiene los quimicos enviados
                {
                    parameters.Clear();
                    parameters.Add("@idTratamiento", permiso);
                    var dt2 = DataAccess.executeStoreProcedureDataTable("Spr_get_InfoTratamientoRepetido", parameters, this.Session["connection"].ToString());
                    string error = string.Format((GetLocalResourceObject("ExistQuimicos").ToString()), dt2.Rows[0]["nomPlanta"].ToString(), dt2.Rows[0]["vNombre"].ToString());
                    popUpMessageControl1.setAndShowInfoMessage(error, Common.MESSAGE_TYPE.Warning);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorGuardarTratamiento").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar el Tratamiento " , Common.MESSAGE_TYPE.Error);
            }
        }

    }

    protected void guardarAplicacionModificada()
    {
        var parameters = new Dictionary<string, object>();
        parameters = parametrosParaGuardarAplicacion();
        int idprogramaACancelar = 0;
        idprogramaACancelar = Int32.Parse(ViewState["idPrograma"].ToString());
        //quitar el id del programa, para que lo guarde como nuevo:
        if (parameters.ContainsKey("@idPrograma"))
            parameters.Remove("@idPrograma");
        try
        {
            ViewState["idPrograma"] = DataAccess.executeStoreProcedureGetInt("spr_INSERT_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaGuardado").ToString(), Common.MESSAGE_TYPE.Success); //popUpMessageControl1.setAndShowInfoMessage("Programa guardado exitosamente", Common.MESSAGE_TYPE.Success);
            autorizarPrograma(1);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardandoQuimicos").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar los químicos ", Common.MESSAGE_TYPE.Error);
        }

        //guardar en el log de cancelaciones
        parameters.Clear();
        parameters.Add("@idPrograma", idprogramaACancelar);
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        parameters.Add("@idRazon", ddlRazones.SelectedValue);
        parameters.Add("@bcancel", 1); //1 = modificado
        parameters.Add("@idProgramaNuevo", ViewState["idPrograma"].ToString());

        try
        {
            //guardar el log de cancelaciones
            DataAccess.executeStoreProcedureNonQuery("spr_INSERT_LogCancelacionesModificaciones", parameters, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("error").ToString(), Common.MESSAGE_TYPE.Warning);
            Log.Error(ex);
        }

        //cancelar el antigo programa
        parameters.Clear();
        parameters.Add("@idPrograma", idprogramaACancelar);
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        try
        {
            DataAccess.executeStoreProcedureNonQuery("spr_CANCEL_ProgramaHeader", parameters, this.Session["connection"].ToString());
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardandoQuimicos").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar los químicos ", Common.MESSAGE_TYPE.Error);
        }

        if (ckTratamiento.Checked)
        {
            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
            xmlString.AppendFormat("<{0}>", "Quimicos");
            int z = 0;
            if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
            {
                foreach (GridViewRow row in gdvPrograma.Rows)
                {
                    xmlString.AppendFormat("<c><id>{0}</id>", gdvPrograma.DataKeys[z].Value.ToString());
                    xmlString.AppendFormat("<agua>{0}</agua>", gdvPrograma.Rows[z].Cells[7].Text); //--agua
                    xmlString.AppendFormat("<caninf>{0}</caninf>", gdvPrograma.Rows[z].Cells[8].Text);//--desde
                    xmlString.AppendFormat("<cansup>{0}</cansup>", gdvPrograma.Rows[z].Cells[9].Text);//--hasta
                    xmlString.AppendFormat("<canped>{0}</canped></c>", Decimal.Parse(((TextBox)gdvPrograma.Rows[z].FindControl("TextBox2")).Text));
                    z++;
                }
            }

            xmlString.AppendFormat("</{0}>", "Quimicos");

            parameters.Clear();
            parameters.Add("@nombre", txtTratamientoNom.Text.Trim());
            parameters.Add("@planta", ddlPlanta.SelectedValue);
            parameters.Add("@Quimicos", xmlString.ToString());

            try
            {
                // -1 = nombre repetido
                // 0 = guardado
                //x = id de tratamiento con esos quimicos
                float permiso = DataAccess.executeStoreProcedureFloat("spr_INSERT_Tratamientos", parameters, this.Session["connection"].ToString());

                if (permiso == -1)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("TratamientoRepetido").ToString(), Common.MESSAGE_TYPE.Warning);
                    txtTratamientoNom.Text = String.Empty;
                }
                else if (permiso == 0)
                {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Guardado").ToString(), Common.MESSAGE_TYPE.Success);
                    ckTratamiento.Checked = false;
                    txtTratamientoNom.Text = String.Empty;

                }
                else //cualquier otro valor, nos indica el id del tratamiento que tiene los quimicos enviados
                {
                    parameters.Clear();
                    parameters.Add("@idTratamiento", permiso);
                    var dt2 = DataAccess.executeStoreProcedureDataTable("Spr_get_InfoTratamientoRepetido", parameters, this.Session["connection"].ToString());
                    string error = string.Format((GetLocalResourceObject("ExistQuimicos").ToString()), dt2.Rows[0]["nomPlanta"].ToString(), dt2.Rows[0]["vNombre"].ToString());
                    popUpMessageControl1.setAndShowInfoMessage(error, Common.MESSAGE_TYPE.Warning);
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorGuardarTratamiento").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar guardar el Tratamiento " , Common.MESSAGE_TYPE.Error);
            }
        }

    }

    public void setValoresPopUp(string quimicos)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@Quimicos", quimicos);
        BulletedList1.Items.Clear();
        try
        {
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_ShowQuimicosComodin", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                lblComodines.Visible = true;
                foreach (DataRow row in dt.Rows)
                {
                    BulletedList1.Items.Add(row["ITEMDESC"].ToString().Trim());
                }
            }
            else
                lblComodines.Visible = false;
        }
        catch (Exception e)
        {
            Log.Error(e);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorComodines").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error de proceso al intentar extraer la cantidad de horas " , Common.MESSAGE_TYPE.Error);
        }
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        try
        {
            DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_GET_HorasFumigacion", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                lblHorasRenntreda.Text = dt.Rows[0]["reentrada"].ToString();
                lblCocecha2.Text = dt.Rows[0]["cocecha"].ToString();
                lblPlanta2.Text = ddlPlanta.SelectedItem.ToString();//dt.Rows[0]["planta"].ToString();
                lblInv2.Text = ddlInvernadero.SelectedItem.ToString();//dt.Rows[0]["invernadero"].ToString();
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorHoras").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error de proceso al intentar extraer la cantidad de horas " , Common.MESSAGE_TYPE.Error);
        }


    }

    private void seleccionarRazonCancelar()
    {
        //sacar las razones que pertenecen a la planta de ese programa

        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");

        try
        {
            var ds = DataAccess.executeStoreProcedureDataSet("dbo.spr_GET_RazonesXPlantaXPrograma", parameters, this.Session["connection"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRazones.DataSource = ds;
                ddlRazones.DataBind();
                popUpRazones.Show();
            }
            else
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Norasones").ToString(), Common.MESSAGE_TYPE.Warning);
            }
        }
        catch (Exception ex)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("error").ToString(), Common.MESSAGE_TYPE.Warning);
            Log.Error(ex);
        }


    }

    private void autorizarPrograma( int esCambio)
    {
        //sacar el rol de usuario, para saber si se manda el correo (en caso de ser asistente) o si se crean las ordenes de trabajo 
        var parameters = new Dictionary<string, object>();
        parameters.Clear();
        parameters.Add("@user", Session["usernameCalidad"].ToString());
        try
        {
            var ds2 = DataAccess.executeStoreProcedureDataTable("spr_AccesoUsuario", parameters, this.Session["connection"].ToString());
            int rol = 0;
            if (ds2.Rows.Count > 0)
            {
                rol = Int32.Parse(ds2.Rows[0]["roleIds"].ToString());
                if (rol > 0)
                {
                    if (rol == 1 || rol == 2)
                    {
                        //crear ordenes de trabajo
                        parameters.Clear();
                        parameters.Add("@idHeader", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
                        DataAccess.executeStoreProcedureNonQuery("spr_divideCargasOrdenesTrabajo", parameters, this.Session["connection"].ToString());
                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaGuardadoOrdenTrabajo").ToString(), Common.MESSAGE_TYPE.Success);
                    }
                    else
                    {
                        //enviar correo --solo cuando es creación                       

                       // if (esCambio == 0 )
                       // {
                            parameters.Clear();
                            parameters.Add("@asistente", Int32.Parse(ds2.Rows[0]["idUsuario"].ToString()));
                            parameters.Add("@planta", ddlPlanta.SelectedValue);
                            var dt = DataAccess.executeStoreProcedureDataTable("spr_ObtieneCorreosGrowerByAsistente", parameters, this.Session["connection"].ToString());
                            if (dt.Rows.Count > 0)
                            {

                                foreach (DataRow row in dt.Rows)
                                {
                                    try
                                    {
                                        if (DBNull.Value != row["mail"] && !string.IsNullOrEmpty(row["mail"].ToString()))
                                        {
                                            var pv = new Dictionary<string, string>();
                                            var files = new Dictionary<string, Stream>();

                                            Common.SendMailByDictionary(pv, files, row["mail"].ToString(), "Aplicacion");
                                            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("CorreoExito").ToString(), Common.MESSAGE_TYPE.Success);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Log.Error(ex);
                                        popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("CorreoError").ToString(), Common.MESSAGE_TYPE.Warning);
                                    }
                                }//foreach
                            }//if (dt.Rows.Count > 0)
                        //}//
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardandoQuimicos").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }

    #endregion

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
        if(String.IsNullOrEmpty( txtFechaAplicacion.Text.Trim()) )
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorDiaSemana").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione un día de la semana", Common.MESSAGE_TYPE.Error);
            return;
        }

        if (ddlTipoAplicacion.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorTipoDeAplicacion").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione el Tipo de Aplicación", Common.MESSAGE_TYPE.Error);
            return;
        }
       
        if (ddlPlanta.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorSelecPlanta").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione la planta", Common.MESSAGE_TYPE.Error);
            return;
        }
        if (ddlInvernadero.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorSelecInv").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione el invernadero", Common.MESSAGE_TYPE.Error);
            return;
        }

        gdvQumicosBuscados.DataSource = null;
        gdvQumicosBuscados.DataBind();

        gdvTratamientosBuscados.DataSource = null;
        gdvTratamientosBuscados.DataBind();

        btnAdd.Visible = false;

        string plagas = string.Empty;
        plagas = Request.Form[ddlFiltro.UniqueID];        
        plagas = plagas.Replace(',', '|');
        //se eliminan los campos vacios
        plagas = plagas.Replace("-1|", "");
        plagas = plagas.Replace("-1", "");

        PlagasHidden.Value = plagas;

        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPlaga", plagas);
        parameters.Add("@idFarm", ddlPlanta.SelectedValue);
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedValue);
        parameters.Add("@year", numeroYear());
        parameters.Add("@semana", numeroSemana());
        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        DataTable dt = new DataTable();
        try
        {
            dt = DataAccess.executeStoreProcedureDataTable("spr_GET_QuimicosConPlagaValidados", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                gdvQumicosBuscados.DataSource = dt;
                gdvQumicosBuscados.DataBind();
                btnAdd.Visible = true;               
            }
            else
                btnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorCamposVacios").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Cerciórese de que todos los campos han sido seleccionados", Common.MESSAGE_TYPE.Error);
        }

        if (dt.Rows.Count > 0)
        {
            parameters.Clear();
            parameters.Add("@idFarm", ddlPlanta.SelectedValue);
            parameters.Add("@idInvernadero", ddlInvernadero.SelectedValue);
            parameters.Add("@year", numeroYear());
            parameters.Add("@semana", numeroSemana());
            parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
            parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
            //de los quimicos encontrados, sacar sus tratamientos
            //mandar quimicos que salieron para esa plaga:
            System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
            xmlString.AppendFormat("<{0}>", "Quimicos");  
            int i = 0;
            foreach (GridViewRow row in gdvQumicosBuscados.Rows)
            {
                xmlString.AppendFormat("<c><id>{0}</id></c>", gdvQumicosBuscados.DataKeys[i].Value.ToString());                
                i++;
            } 
            xmlString.AppendFormat("</{0}>", "Quimicos");

            string quimicos = xmlString.ToString();
            parameters.Add("@Quimicos2", quimicos);

            var dt2 = DataAccess.executeStoreProcedureDataTable("spr_GET_TratamientosByQuimicos", parameters, this.Session["connection"].ToString());
            if (dt2.Rows.Count > 0)
            {
                gdvTratamientosBuscados.DataSource = dt2;
                gdvTratamientosBuscados.DataBind();
            }
        }
    }
       
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }

        //ver que el quimico que intenta agregar no existe ya en el grid

        if (!validacionesLlenarTodosLosCampos())
            return;

        if (!validacionSeleecionValores(0))
            return;
        
        string quimicos = hayQuimicos();

        if (String.IsNullOrEmpty(quimicos) && String.IsNullOrEmpty(hayTratamientos()))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectQuimico").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione al menos un químico o un tratamiento. No se agregarán químicos repetidos al programa.", Common.MESSAGE_TYPE.Error);
            return;
        }

        if (ddlPlanta.SelectedValue == "8")
            calculaParaNayarit(quimicos);
        else if
            (ddlPlanta.SelectedValue == "5")
            calculaParaColima(quimicos);
        else if
            (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6" || ddlPlanta.SelectedValue == "7")
            calculaParaSisZap(quimicos);
        else if
            (ddlPlanta.SelectedValue == "4")
            calculaParaTuxca(quimicos);

        else
            calculaOtras(quimicos);
        if (gdvPrograma.Rows.Count > 0)
        {
            txtObs.Text = String.Empty;
            txtObs.Text = GetLocalResourceObject("Quimico").ToString() + "          " + GetLocalResourceObject("abejorro").ToString() + "      " + GetLocalResourceObject("persistencia").ToString() + "\r\n";
            for (int i = 0; i < gdvPrograma.Rows.Count; i++)
            {
                if (gdvPrograma.Rows[i].Cells[1].Text.Trim().Length > 20)
                    txtObs.Text += gdvPrograma.Rows[i].Cells[1].Text.Trim() + "          " + gdvPrograma.Rows[i].Cells[5].Text.Trim() + "          " + gdvPrograma.Rows[i].Cells[6].Text.Trim() + "\r\n";
                else
                {
                    txtObs.Text += gdvPrograma.Rows[i].Cells[1].Text.Trim();
                    for (int t = gdvPrograma.Rows[i].Cells[1].Text.Trim().Length; t <= 20; t++)
                    {
                        txtObs.Text += " ";
                    }
                    txtObs.Text += gdvPrograma.Rows[i].Cells[5].Text.Trim() + "          " + gdvPrograma.Rows[i].Cells[6].Text.Trim() + "\r\n";
                }
            }
            btnGuardar.Visible = true;            
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
        
        
        //validar que tenga al menos un químco
        if (gdvPrograma.Rows.Count <= 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("GuardarAplicacion").ToString(), Common.MESSAGE_TYPE.Error); //La Aplicación requiere tener, al menos, un químico para ser guardada.", Common.MESSAGE_TYPE.Error);
            return;
        }

        //revisar si quiere guardar un tratamiento:
        if (ckTratamiento.Checked)
        {
            if (!validarTratamiento())
                return;
        }
        
        if (!validacionesLlenarTodosLosCampos())
           return;
        
        if (!validacionSeleecionValores(0))
            return;
        
        if (String.IsNullOrEmpty(txtNombre.Text.Trim()))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NombreAplicacion").ToString(),Common.MESSAGE_TYPE.Error); //"El nombre para la Aplicación es necesario             
            return;
        }
        
        //validar los quimicos del gdv
       System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
       xmlString.AppendFormat("<{0}>", "Quimicos");
       if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
       {
           int i = 0;
           decimal d;
           foreach (GridViewRow row in gdvPrograma.Rows)
           {
               xmlString.AppendFormat("<c><id>{0}</id>", gdvPrograma.DataKeys[i].Value.ToString());
               xmlString.AppendFormat("<agua>{0}</agua>", gdvPrograma.Rows[i].Cells[7].Text); //--agua
               xmlString.AppendFormat("<caninf>{0}</caninf>", gdvPrograma.Rows[i].Cells[8].Text);//--desde
               xmlString.AppendFormat("<cansup>{0}</cansup>", gdvPrograma.Rows[i].Cells[9].Text);//--hasta
               if (!decimal.TryParse(((TextBox)gdvPrograma.Rows[i].FindControl("TextBox2")).Text, out d)  )
               {
                   popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("QuimicoIncorrecto").ToString(),Common.MESSAGE_TYPE.Error);//Uno o más químicos tiene valor incorrecto en el campo 'Cantidad solicitada' ", Common.MESSAGE_TYPE.Error); 
                   return;
               }

               if (d * -1 > 0 )
               {
                   popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("QuimicoNegativo").ToString(), Common.MESSAGE_TYPE.Error); //No puede aplicar valores negativos a los químicos ", Common.MESSAGE_TYPE.Error);
                   return;
               }

               if (d == 0)
               {
                   popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("QuimicoCero").ToString(), Common.MESSAGE_TYPE.Error);//No puede aplicar valores en cero a los químicos ", Common.MESSAGE_TYPE.Error);
                   return;
               }

               if (!getNumero(gdvPrograma.Rows[i].Cells[8].Text, gdvPrograma.Rows[i].Cells[9].Text, d))
               {
                   popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Rango").ToString(), Common.MESSAGE_TYPE.Error); //Verifique que las cantidades solicitadas se encuentren dentro del rango ", Common.MESSAGE_TYPE.Error);
                   return;
               }

               xmlString.AppendFormat("<canped>{0}</canped></c>", ((TextBox)gdvPrograma.Rows[i].FindControl("TextBox2")).Text );
               i++;
           }

       }
       
        xmlString.AppendFormat("</{0}>", "Quimicos");             
       setValoresPopUp(xmlString.ToString());
       mdlPopupMessageGralControl.Show();      
    }

    protected void btnAutorizar_Click(object sender, EventArgs e)
    {
        //crear ordenes de trabajo
        var parameters = new Dictionary<string, object>(); 
        parameters.Clear();
        parameters.Add("@idHeader", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        try
        {
            DataAccess.executeStoreProcedureNonQuery("spr_divideCargasOrdenesTrabajo", parameters, this.Session["connection"].ToString());
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ProgramaGuardadoOrdenTrabajo").ToString(), Common.MESSAGE_TYPE.Success);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("OrdenTrabajoNoEnviada").ToString(), Common.MESSAGE_TYPE.Error);
        }
    }
    protected void save_Click(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
                      

        foreach (GridViewRow row in gdvPrograma.Rows)
        {
            int i = 0;
            try
            {
                decimal x = Decimal.Parse(((TextBox)gdvPrograma.Rows[i].FindControl("TextBox2")).Text);
            }
            catch (Exception ek)
            {
                Log.Error(ek); 
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("CantidadDeQuimico").ToString(),Common.MESSAGE_TYPE.Error); //Verifique que la cantidad solicitada en cada químico sea un número válido ", Common.MESSAGE_TYPE.Error);
                return;
            }
            i++;

        }      
        
            //si ViewState["idPrograma"] == 0, es por que el programa es nuevo
            if (Int32.Parse(ViewState["idPrograma"].ToString()) <= 0)
            {
                guardarAplicacionNueva();
            }
            //sino, puede ser una modificacion o una autorizadción
            else
            {
                seleccionarRazonCancelar();                
            }
    }

    protected void save2_OnClick(object sender, EventArgs e)
    {
        if (Session["usernameCalidad"] == null)
        {
            Response.Redirect("~/frmLogin.aspx", false);
        }
        guardarAplicacionModificada();
        popUpRazones.Hide();
    }
    
    protected void btnCancelar_Click(object sender, EventArgs e)
    {

    }

    protected void imgDeleteNominee_Click(object sender, EventArgs e)
    {
        Session["row"] = null;
        ImageButton imgButton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)imgButton.NamingContainer;
        Session["row"] = row;

        mdlPopupMessageConfirm.Show();
    }

    protected void bntSiEliminarQuim_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)Session["row"];
        gdvPrograma.DeleteRow(row.RowIndex);

        txtObs.Text = String.Empty;

        if (gdvPrograma.Rows.Count > 0)
        {            
            txtObs.Text = GetLocalResourceObject("Quimico").ToString() + "          " + GetLocalResourceObject("abejorro").ToString() + "      " + GetLocalResourceObject("persistencia").ToString() + "\r\n";
            for (int i = 0; i < gdvPrograma.Rows.Count; i++)
            {
                if (gdvPrograma.Rows[i].Cells[1].Text.Trim().Length > 20)
                    txtObs.Text += gdvPrograma.Rows[i].Cells[1].Text.Trim() + "          " + gdvPrograma.Rows[i].Cells[5].Text.Trim() + "          " + gdvPrograma.Rows[i].Cells[6].Text.Trim() + "\r\n";
                else
                {
                    txtObs.Text += gdvPrograma.Rows[i].Cells[1].Text.Trim();
                    for (int t = gdvPrograma.Rows[i].Cells[1].Text.Trim().Length; t <= 20; t++)
                    {
                        txtObs.Text += " ";
                    }
                    txtObs.Text += gdvPrograma.Rows[i].Cells[5].Text.Trim() + "          " + gdvPrograma.Rows[i].Cells[6].Text.Trim() + "\r\n";
                }
            }           
        }

    }

    #region validar botonazos

    private bool validacionesLlenarTodosLosCampos()
    {
        bool todoBien = true;
        StringBuilder cadenaErrores = new StringBuilder();

        //--validar que una planta y su invernadero esten seleccionados
        if (ddlPlanta.SelectedIndex == 0 || ddlInvernadero.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("PlantaInvernadero").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione planta e invernadero", Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }       
        if (String.IsNullOrEmpty(txtFechaAplicacion.Text.Trim()))
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Semana").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione la semana", Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }

        //--Validar que selecione el tipo de aplicacion
        if(ddlTipoAplicacion.SelectedIndex == 0 )
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorTipoDeAplicacion").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione el tipo de aplicación", Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }

        //--Validar el equipo de aplicacion
        if(ddlEquipoAplicacion.SelectedIndex == 0)
        {
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelecEquipo").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione el Equipo de aplicación", Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }
        
        //--selecciono Nayarit, verificar que lleno todos los campos
        if (ddlPlanta.SelectedValue == "8")
        {
            if (ddlEdadCultivo.SelectedIndex == 0 || ddlEquipoAplicacion.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("EdadEquipo").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione la 'edad de cultivo' y el 'equipo de aplicación'", Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }
        }
        //--selecciono Colima, verificar que lleno todos los campos
        else if (ddlPlanta.SelectedValue == "5")
        {
            if (ddlEdadCultivo.SelectedIndex == 0)
            {
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Edad").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione la 'edad de cultivo'", Common.MESSAGE_TYPE.Error);
                    todoBien = false;
               
            }           
        }

         //--selecciono Txc, verificar que lleno todos los campos
        else if (ddlPlanta.SelectedValue == "4")
        {
            if (ddlTipoBoquilla.SelectedIndex == 0 || ddlEquipoAplicacion.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("BoquillaEquipo").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione el tipo de boquilla y el equipo de apliación", Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }
        }

        //--selecciono SIS o ZAP , verificar que lleno todos los campos
        else if (ddlPlanta.SelectedValue == "2" || ddlPlanta.SelectedValue == "6")
        {
            if (ddlTipoBoquilla.SelectedIndex == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SelectBoquilla").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Seleccione el tipo de boquilla", Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }
        }        
        return todoBien;
    }

    private bool validacionSeleecionValores( int verMensaje)
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

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_ddlLitrosInfo", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                lblLts.Visible = true;
                LblLts2.Visible = true;
                LblLts2.Text = String.Empty;
                LblLts2.Text = dt.Rows[0]["volumen"].ToString();

                if (verMensaje == 1) 
                    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ValCorrecta").ToString(),Common.MESSAGE_TYPE.Success); //popUpMessageControl1.setAndShowInfoMessage("Validación correcta.", Common.MESSAGE_TYPE.Success);

            }
            else
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("NoCoinciden").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Uno o más datos referentes a la edad del cultivo y/o equipo a usar en la fumigación no coinciden. Por favor, asegurese de que los datos introducidos son correctos.", Common.MESSAGE_TYPE.Error);
                todoBien = false;
            }


        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("ErrorLitros").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos al intentar cargar información referente los litros " , Common.MESSAGE_TYPE.Error);
            todoBien = false;
        }
        if (todoBien)
            btnBuscar.Visible = true;
        else
            btnBuscar.Visible = false;

        return todoBien;
    }

    private string hayQuimicos()
    {
        string quimicos = String.Empty;
        //--guaradr los ids de los qumicos con palomita        
        System.Text.StringBuilder xmlString = new System.Text.StringBuilder();
        xmlString.AppendFormat("<{0}>", "Quimicos");
        bool alMenosUno = false;
        bool igual = false;
        int i = 0;

        //--este es el grid donde estan los resultados de la busqueda
        foreach (GridViewRow row in gdvQumicosBuscados.Rows)
        {
            if (((CheckBox)row.FindControl("CheckBox1")).Checked)
            {
                //ver que no se guarde un quimico que ya este en el grid del programa
                if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
                {
                    int x = 0;
                    igual = false;
                    foreach (GridViewRow rowX in gdvPrograma.Rows)
                    {
                        if (gdvPrograma.DataKeys[x].Value.ToString() == gdvQumicosBuscados.DataKeys[i].Value.ToString())
                        {
                            igual = true;
                            break;
                        }                        
                        x++;
                    }
                }

                if (!igual)
                {
                    //está seleccionado, guardarlo
                    alMenosUno = true;
                    xmlString.AppendFormat("<c><id>{0}</id></c>", gdvQumicosBuscados.DataKeys[i].Value.ToString());
                }
            }
            i++;
        }

        //--ver si ya habia datos en el grid de programación, y si hay, guardarlos tambien, para conservar todo
        if (gdvPrograma.DataSource != null || gdvPrograma.Rows.Count > 0)
        {
            i = 0;
            foreach (GridViewRow row in gdvPrograma.Rows)
            {
                xmlString.AppendFormat("<c><id>{0}</id></c>", gdvPrograma.DataKeys[i].Value.ToString());
                alMenosUno = true;
                i++;
            }

        }
        

        xmlString.AppendFormat("</{0}>", "Quimicos");

        if (!alMenosUno)
        { }//    popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("").ToString(),Common.MESSAGE_TYPE); //popUpMessageControl1.setAndShowInfoMessage("Seleccione al menos un químico. No se agregarán químicos repetidos al programa.", Common.MESSAGE_TYPE.Error);
        else
            quimicos = xmlString.ToString();        
        return quimicos;
    }

    private string hayTratamientos()
    {
        string tratamientos = String.Empty;
        //--guaradr los ids de los qumicos con palomita        
        int i = 0;

        //--este es el grid donde estan los resultados de la busqueda
        foreach (GridViewRow row in gdvTratamientosBuscados.Rows)
        {
            if (((CheckBox)row.FindControl("cbTratamiento")).Checked)

                tratamientos = tratamientos + gdvTratamientosBuscados.DataKeys[i].Value.ToString() + '|';              
            i++;
        }
        return tratamientos;
    }

    private void calculaOtras(string quimicos)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("@idPrograma", Int32.Parse(ViewState["idPrograma"].ToString()) > 0 ? ViewState["idPrograma"].ToString() : "0");
        parameters.Add("@idPlanta", ddlPlanta.SelectedValue.ToString());
        parameters.Add("@idInvernadero", ddlInvernadero.SelectedItem.ToString());
        parameters.Add("@edad", ddlEdadCultivo.SelectedValue.ToString());
        parameters.Add("@capar", ckCapa.Checked == true ? 1 : 0);
        parameters.Add("@fin", ckFinCiclo.Checked == true ? 1 : 0);
        if (ddlTipoBoquilla.SelectedIndex > 0)
            parameters.Add("@idTipoBoquilla", ddlTipoBoquilla.SelectedValue.ToString());
        else
            parameters.Add("@idTipoBoquilla", 0);

        if (ddlEquipoAplicacion.SelectedIndex > 0)
            parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue.ToString());
        else
            parameters.Add("@idEquipoAplicacion", 0);

        parameters.Add("@Quimicos", quimicos.ToString());

        if (ddlTipoAplicacion.SelectedIndex > 0)
            parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        else
            parameters.Add("@tipoAplicacion", 0);

        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@idTratamientos", hayTratamientos());

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SinConfig").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("No es posible mostrar datos. Es posible que no exista configuración para esa edad de cultivo si selecciono 'Desde/Hasta Capar' y/o 'Hasta fin de ciclo'. Por favor, cerciore los datos introducidos.", Common.MESSAGE_TYPE.Error);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    gdvPrograma.DataSource = dt;
                    gdvPrograma.DataBind();
                    ViewState["GridPrograma"] = dt;
                    //agregar los comentarios si los tiene
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["mensaje"].ToString() != "")
                        {
                            txtObs.Text = txtObs.Text + dt.Rows[i]["mensaje"] + "\n";
                        }
                    }
                }
            }
            //ViewState["GridBoletin"] = dt;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Datos").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos " ,   Common.MESSAGE_TYPE.Error);
        }

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
        parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@idTratamientos", hayTratamientos() );

        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count == 0)
            {
                popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("SinConfig").ToString(), Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("No es posible mostrar datos. Es posible que no exista configuración para esa edad de cultivo si selecciono 'Desde/Hasta Capar' y/o 'Hasta fin de ciclo'. Por favor, cerciore los datos introducidos.", Common.MESSAGE_TYPE.Error);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    gdvPrograma.DataSource = dt;
                    gdvPrograma.DataBind();
                    ViewState["GridPrograma"] = dt;
                    //agregar los comentarios si los tiene
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["mensaje"].ToString() != "")
                        {
                            txtObs.Text = txtObs.Text + dt.Rows[i]["mensaje"] + "\n";
                        }
                    }
                }
            }
            //ViewState["GridBoletin"] = dt;
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Datos").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos " ,   Common.MESSAGE_TYPE.Error);
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
        parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@idTratamientos", hayTratamientos());
        //validar que exista agua para la semana seleccionada, la capa y el fin
                
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                gdvPrograma.DataSource = dt;
                gdvPrograma.DataBind();
                ViewState["GridPrograma"] = dt;
                //agregar los comentarios si los tiene
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["mensaje"].ToString() != "")
                    {
                        txtObs.Text = txtObs.Text + dt.Rows[i]["mensaje"] + "\n";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Datos").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos " , Common.MESSAGE_TYPE.Error);
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
        parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@idTratamientos", hayTratamientos());
        parameters.Add("@idEquipoAplicacion", ddlEquipoAplicacion.SelectedValue);
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters, this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                gdvPrograma.DataSource = dt;
                gdvPrograma.DataBind();
                ViewState["GridPrograma"] = dt;
                //agregar los comentarios si los tiene
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["mensaje"].ToString() != "")
                    {
                        txtObs.Text = txtObs.Text + dt.Rows[i]["mensaje"] + "\n";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Datos").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos " , Common.MESSAGE_TYPE.Error);
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
        parameters.Add("@tipoAplicacion", ddlTipoAplicacion.SelectedValue);
        parameters.Add("@fechaSugerida", txtFechaAplicacion.Text.Trim());
        parameters.Add("@idTratamientos", hayTratamientos()); 
        try
        {
            var dt = DataAccess.executeStoreProcedureDataTable("spr_GET_NuevosQuimicosToPrograma", parameters,this.Session["connection"].ToString());
            if (dt.Rows.Count > 0)
            {
                gdvPrograma.DataSource = dt;
                gdvPrograma.DataBind();
                ViewState["GridPrograma"] = dt;
                //agregar los comentarios si los tiene
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["mensaje"].ToString() != "")
                    {
                        txtObs.Text = txtObs.Text + dt.Rows[i]["mensaje"] + "\n";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex); 
            popUpMessageControl1.setAndShowInfoMessage(GetLocalResourceObject("Datos").ToString(),Common.MESSAGE_TYPE.Error); //popUpMessageControl1.setAndShowInfoMessage("Error en el proceso de datos ", Common.MESSAGE_TYPE.Error);
        }
    }

    #endregion
    
    #endregion

    #region formatos del grid
    protected void gdvQumicosBuscados_PreRender(object sender, EventArgs e)
    {
        if (gdvQumicosBuscados.HeaderRow != null)
            gdvQumicosBuscados.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvQumicosBuscados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                try
                {
                 


                    if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[2].Text).Trim()))
                    {


                        e.Row.Cells[2].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[2].Text).Trim()).ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                break;
        }
    }

    protected void gdvTratamientosBuscados_PreRender(object sender, EventArgs e)
    {
        if (gdvQumicosBuscados.HeaderRow != null)
            gdvQumicosBuscados.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gdvTratamientosBuscados_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

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
                try
                {

                    if (!string.IsNullOrEmpty(HttpUtility.HtmlDecode(e.Row.Cells[5].Text).Trim()))
                    {


                        e.Row.Cells[5].Text = GetLocalResourceObject(HttpUtility.HtmlDecode(e.Row.Cells[5].Text).Trim()).ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                break;
        }
        //switch (e.Row.RowType)
        //{
        //    case DataControlRowType.DataRow:
        //        e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gdvResultados, ("Select$" + e.Row.RowIndex.ToString()));
        //        break;
        //}
    }

    protected void gdvPrograma_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            ((DataTable)ViewState["GridPrograma"]).Rows.Remove(((DataTable)ViewState["GridPrograma"]).Rows[e.RowIndex]);
            DataTable dt = (DataTable)ViewState["GridPrograma"];
            gdvPrograma.DataSource = dt;
            gdvPrograma.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
   
    #endregion


    
}