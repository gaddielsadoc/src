using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/* Class _Default
 * 
 * Esta clase es la que define el comportamiento de los elementos creados y definidos en Default.aspx
 * */
public partial class catalog_frmPlagas : BasePage
{
    /* Page_Load (object sender, EventArgs e)
     * Esta funcion es llamada al cargar la página
     * dentro especificamos que si no es una llamada post se ejecute obtienePlagas()
     * para más información hacer mouse over en (!IsPostBack)
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.obtienePlagas();
            }
        }catch(Exception ex)
        {
            Log.Error(ex); 
        }
    }

    /* obtienePlagas()
     * Esta función privada ejecuta el stored procedure llamado spr_SelectAllPlagas sin enviar ningún parametro
     * y el resultado del stored procedure es colocado en la grid view llamada gvPlaga
     */
    private void obtienePlagas()
    {
        try
        {
            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
            DataSet ds = DataAccess.executeStoreProcedureDataSet("spr_SelectAllPlagas", parameters, this.Session["connection"].ToString());
            gvPlaga.DataSource = ds;
            gvPlaga.DataBind();
        }
        catch(Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("CargarDatosError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    /* Cancelar_Limpiar
     * Esta función es llamada por el botón ID="btnCancel"
     * exactamente la parte que dice onclick="Cancelar_Limpiar"
     * Lo que hace es llamar la función VolverAlPanelInicial()
     */
    protected void Cancelar_Limpiar(object sender, EventArgs e)
    {
        VolverAlPanelInicial();
    }

    /* Guardar_Actualizar
     * EN RESUMEN ESTA FUNCIÓN SE ENCARGA DE VALIDAR REGISTROS SI PASAN LAS VALIDACIONES
     * SE ENCARGA DE GUARDAR O ACTUALIZAR EL REGISTRO SEGUN SEA EL CASO
     * 
     * Esta función valida que los campos de texto txtNombre, txtNombreComun y txtDescripcion
     * tengan contenido, de no ser así manda un pop up con el error
     * 
     * de tener contenido los 3 campos se almacenan junto con el checkbox 
     * en un objeto tipo Dictionary llamado parameters
     * 
     * después revisamos el valor del campo Accion (campo oculto)
     * 
     * si el valor de Accion es "Añadir" entonces verificamos que la plaga no haya sido agregada
     * anteriormente con ayuda del stored procedure spr_ExistePlaga
     * si se encontró registro manda un error, de otra manera se guarda el nuevo registro
     * con ayuda del stored procedure spr_InsertPlaga
     * Se le da el resultado del insert al usuario (positivo o negativo)
     * 
     * Si el valor de Accion no es "Añadir" entonces se realizan las mismas validaciones
     * que en el paso anterior con la diferencia que en lugar de hacer un insert se hace
     * un update con ayuda del stored procedure "spr_UpdatePlaga" y agregando el parametro 
     * idPlaga que es extraido del cookie Session["IdPlagaCookie"].
     * 
     * Al final se actualiza el grid y el formulario con ayuda de obtienePlagas() y VolverAlPanelInicial();
     * */
    protected void Guardar_Actualizar(object sender, EventArgs e)
    {

        try
        {
            if (txtNombre.Text.Trim().Equals("") || txtNombreComun.Text.Trim().Equals(""))
            {
                //popUpMessageControl1.setAndShowInfoMessage("Ambos campos son requeridos.", Common.MESSAGE_TYPE.Error);
                popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("AmbosRequeridos").ToString()), Common.MESSAGE_TYPE.Error);
            }
            else
            {
                Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
                parameters.Add("@nombreCientifico", txtNombre.Text);
                parameters.Add("@nombreComun", txtNombreComun.Text);
                if (rbPlaga.Checked)
                    parameters.Add("@esPlaga", 1);
                else
                    parameters.Add("@esPlaga", 0);

                if (chkActivo.Checked)
                    parameters.Add("@activo", 1);
                else
                    parameters.Add("@activo", 0);

                if (Accion.Value == "Añadir")
                {
                    Dictionary<string, object> find = new System.Collections.Generic.Dictionary<string, object>();
                    find.Add("@nombreCientifico", txtNombre.Text);

                    if (DataAccess.executeStoreProcedureGetInt("spr_ExistePlaga", find, this.Session["connection"].ToString()) > 0)
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios debido a que el nombre de la plaga ya existe.", Common.MESSAGE_TYPE.Info);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("PlagaExiste").ToString()), Common.MESSAGE_TYPE.Error);

                    }
                    else
                    {
                        String Rs = DataAccess.executeStoreProcedureString("spr_InsertPlaga", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("Repetido"))
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("Ese registro ya había sido capturado.", Common.MESSAGE_TYPE.Error);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("YaExiste").ToString()), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("La Plaga \"" + txtNombre.Text + "\" se guardó exitosamente.", Common.MESSAGE_TYPE.Success);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("Guardado"), txtNombre.Text.ToString()), Common.MESSAGE_TYPE.Success);

                        }
                    }
                }
                else
                {
                    if (Session["IdPlagaCookie"] == null || Session["IdPlagaCookie"].ToString() == "")
                    {
                        //popUpMessageControl1.setAndShowInfoMessage("Error #RGRL02: Se perdió la información del ID actual", Common.MESSAGE_TYPE.Error);
                        popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("RGRL02").ToString()), Common.MESSAGE_TYPE.Error);
                    }
                    else
                    {
                        parameters.Add("@IdPlaga", Session["IdPlagaCookie"].ToString());
                        String Rs = DataAccess.executeStoreProcedureString("spr_UpdatePlaga", parameters, this.Session["connection"].ToString());
                        if (Rs.Equals("error"))
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("No se efectuarán cambios intentelo de nuevo.", Common.MESSAGE_TYPE.Info);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("IntenteDeNuevo").ToString()), Common.MESSAGE_TYPE.Error);
                        }
                        else
                        {
                            //popUpMessageControl1.setAndShowInfoMessage("Cambios realizados.", Common.MESSAGE_TYPE.Success);
                            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("Cambios").ToString()), Common.MESSAGE_TYPE.Success);
                        }

                    }
                }
                obtienePlagas();
                VolverAlPanelInicial();

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
        }

    }

    /* gvPlaga_SelectedIndexChanged
     * Esta funcion es llamada cada vez que se hace un click en el grid view gvPlaga
     * 
     * Primero guarda el Id del registro seleccionado en una cookie ( Session["IdMedidaCookie"] )
     * y también la guarda en un objeto dictionary llamado parameters
     * después ese parameters es usado en conjunto con el stored procedure "spr_SelectFromPlagaId"
     * para traer la información de ese registro, y la acomoda en el formulario de arriba.
     * 
     * Tambien cambia el valor de Accion a "guardar cambios"
     * finalmente se encarga de mostrar los botones de actualizar y cancelar y esconder los demás
     */
    protected void gvPlaga_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["IdPlagaCookie"] = gvPlaga.DataKeys[gvPlaga.SelectedIndex].Value.ToString();
        Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
        parameters.Add("@IdPlaga", Session["IdPlagaCookie"]);
        DataTable dt = DataAccess.executeStoreProcedureDataTable("spr_SelectFromPlagaId", parameters, this.Session["connection"].ToString());
        if (dt.Rows.Count > 0)
        {
            txtNombre.Text = dt.Rows[0]["nombreCientifico"].ToString().Trim();
            txtNombreComun.Text = dt.Rows[0]["nombreComun"].ToString().Trim();
            if (dt.Rows[0]["activo"].ToString().Equals("True"))
                chkActivo.Checked = true;
            else
                chkActivo.Checked = false;
            if (dt.Rows[0]["esPlaga"].ToString().Equals("True"))
            {
                rbEnfermedad.Checked = false;
                rbPlaga.Checked = true;
            }
            else
            {
                rbPlaga.Checked = false;
                rbEnfermedad.Checked = true;
            }

            Accion.Value = "Guardar Cambios";
            btnActualizar.Visible = true;
            btnCancel.Visible = true;
            btnLimpiar.Visible = false;
            btnSave.Visible = false;

        }
    }

    /* gvPlaga_PreRender
     * Esta función es llamada antes de crear el grid view
     * y nos ayuda a que no ponga la tag de <tableHeader> en la tabla
     * es necesario por el plugin de paginación que se usa
     */
    protected void gvPlaga_PreRender(object sender, EventArgs e)
    {
        if (gvPlaga.HeaderRow != null)
            gvPlaga.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    /* gvPlaga_PageIndexChanging
     * Esta funcion se llama cuando el indice del grid view cambia
     * y se encarga de rellenar nuevamente los datos del grid.
     */
    protected void gvPlaga_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (null != ViewState["dsPlaga"])
            {
                DataSet ds = ViewState["dsPlaga"] as DataSet;

                if (ds != null)
                {
                    gvPlaga.DataSource = ds;
                    gvPlaga.DataBind();
                }
            }
            ((GridView)sender).PageIndex = e.NewPageIndex;
            ((GridView)sender).DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("IndexChangingError").ToString()), Common.MESSAGE_TYPE.Error);
        }

    }

    /* gvPlaga_RowDataBound
     * cada vez que se vaya a enlazar información de nuestra base de datos a 
     * una fila del GridView este evento se ejecuta. y se encarga
     * de agregar el evento onClick a cada row
     */
    protected void gvPlaga_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes["OnClick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPlaga, ("Select$" + e.Row.RowIndex.ToString()));
                break;
        }
    }
    
    /* Render
     * esta funcion se ejecuta cada que se renderiza el aspx
     * lo que hace es registrar cada row como postBack para evitar que asp
     * nos mande error cuando hacemos click en los rows
     */
    protected override void Render(HtmlTextWriter writer)
    {
        try
        {
            for (int i = 0; i < gvPlaga.Rows.Count; i++)
            {
                Page.ClientScript.RegisterForEventValidation(new PostBackOptions(gvPlaga, "Select$" + i.ToString()));
            }
            base.Render(writer);
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            //popUpMessageControl1.setAndShowInfoMessage(ex.Message, Common.MESSAGE_TYPE.Error);
            popUpMessageControl1.setAndShowInfoMessage(string.Format((string)GetLocalResourceObject("RenderError").ToString()), Common.MESSAGE_TYPE.Error);
        }
    }

    /* VolverAlPanelInicial
     * esta función se encarga de limpiar todos los campos del formulario y 
     * regresar todos los valores a su estado original
     */
    protected void VolverAlPanelInicial()
    {
        Accion.Value = "Añadir";
        txtNombre.Text = "";
        txtNombreComun.Text = "";
        rbPlaga.Checked = true;
        chkActivo.Checked = true;
        gvPlaga.Enabled = true;
        btnActualizar.Visible = false;
        btnCancel.Visible = false;
        btnLimpiar.Visible = true;
        btnSave.Visible = true;
    }

}