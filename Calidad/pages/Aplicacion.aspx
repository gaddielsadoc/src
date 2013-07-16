<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Aplicacion.aspx.cs" Inherits="pages_Aplicacion"  EnableEventValidation="false" ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<%--<%@ Register Src="~/controls/ctrlHorasFumigacion.ascx" TagName="HorasFumigacion" TagPrefix="uc2" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


<script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
   <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
   <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />   

<%--
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.min.js"></script>
    <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/themes/base/jquery-ui.css"/>--%>

<script type="text/javascript">

    function pageLoad() {
        registerCombobox();
        plagasLoad();
    }

//    function registerCombobox() {
//            $("select").comboboxPostBack();       
//        }

    function plagasLoad() {
        var plagas = $('#<%=PlagasHidden.ClientID %>').val();
        $('#<%=PlagasHidden.ClientID %>').val("");
        if (plagas) {
            var itemArray = plagas.split('|');
            $.each(itemArray, function (index, value) {
                //alert(value);
                if (value) {
                    var ddl = $("#<%=ddlFiltro.ClientID %>").clone().html();
                    if (index > 0) {
                        var html = '<tr><td colspan="4">  ';
                        html += '<table align="center"><tr><td style="width:37px;"> &nbsp;</td><td><select name="<%= ddlFiltro.UniqueID %>">' + ddl + '</select></td><td><img alt="Quitar" src="../images/remove-icon.png" class="btnRemoverPlaga"/></td></tr></table></td></tr>';    
                        $('#tblPlagas').append(html);
                    }
                    $('#tblPlagas select:last').val(value);
                }
            });
            registerCombobox();
        }
    }

    function registerCombobox() {
        

        <%if(ddlPlanta.Enabled)
          { %>
            $("select[name='<%= ddlPlanta.UniqueID %>']").comboboxPostBack();
        <% } %>

        <%if(ddlInvernadero.Enabled)
          { %>
        $("select[name='<%= ddlInvernadero.UniqueID %>']").comboboxPostBack();
        <% } %>

        <%if(ddlEdadCultivo.Enabled)
          { %>
        $("select[name='<%= ddlEdadCultivo.UniqueID %>']").comboboxPostBack();
       <% } %>

       <%if(ddlTipoBoquilla.Enabled)
          { %>
        $("select[name='<%= ddlTipoBoquilla.UniqueID %>']").comboboxPostBack();
        <% } %>

        <%if(ddlFiltro.Enabled)
          { %>
        $("select[name='<%= ddlFiltro.UniqueID %>']").combobox();
        <% } %>
    }


    $(function () {
        var startDate;
        var endDate;

        var selectCurrentWeek = function () {
            window.setTimeout(function () {
                $('.week-picker').find('.ui-datepicker-current-day a').addClass('ui-state-active')
            }, 1);
        }

        $('.week-picker').datepicker({
            showOtherMonths: true,
            selectOtherMonths: true,
            onSelect: function (dateText, inst) {
                var date = $(this).datepicker('getDate');
                startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
                $('#startDate').text($.datepicker.formatDate(dateFormat, startDate, inst.settings));
                $('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));

                selectCurrentWeek();
            },
            beforeShowDay: function (date) {
                var cssClass = '';
                if (date >= startDate && date <= endDate)
                    cssClass = 'ui-datepicker-current-day';
                return [true, cssClass];
            },
            onChangeMonthYear: function (year, month, inst) {
                selectCurrentWeek();
            }
        });

        $('.week-picker .ui-datepicker-calendar tr').live('mousemove', function () { $(this).find('td a').addClass('ui-state-hover'); });
        $('.week-picker .ui-datepicker-calendar tr').live('mouseleave', function () { $(this).find('td a').removeClass('ui-state-hover'); });
    });
</script>


 <script type="text/javascript">

     $(function () {
         
         //*****************validacion de los campos******************************//
         //validacion del formulario con la clase validate de jquery

         //valida entradas de fechas formato dd-MM-yyyy
         $.validator.addMethod("formatoFecha",
                function (value, element) {
                    return this.optional(element) || /\d\d-\d\d-\d\d\d\d/.test(value);
                }, '<%=(string)GetLocalResourceObject("FormatoFecha") %> yyyy-MM-dd'); 

         $('#' + formId).validate({
             errorLabelContainer: "div.alerta span",
             wrapper: "li",
             onfocusout: false,
             onsubmit: false,
             onkeyup: false,
             rules: {
                 '<%=txtFechaAplicacion.UniqueID%>': { formatoFecha: true }
             },
             messages: {
                 '<%=txtFechaAplicacion.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("FormatoFecha") %> dd-MM-yyyy' }
             },
             showErrors: function (errorMap, errorList) {
                 var html = '';
                 for (var i = 0; i < errorList.length; i++) {
                     html += errorList[i].message + '<br />';
                 }
                 $("div.alerta span").html(html);
             },
             invalidHandler: function (form, v) {
                 var errors = v.numberOfInvalids();
                 if (errors) {
                     $("#error").show();
                 } else {
                     $("#error").hide();
                 }
             }            
         });
        

         function submit_Click() {
             return $("#" + formId).valid();
         }

         //*****************fin validacion de los campos******************************//

     });

     //boton de cerrar mensaje de error

     function closeErrorMsg() {
         $("div.alerta span").html('');
         $("#error").hide();
     }

     $(function () {

         $(".datepicker").datepicker(
                {
                    dateFormat: "yy-mm-dd",
                    buttonImage: "../images/calendar.png",
                    showOn: "both",
                    dayNames: ["" + '<%=(string)GetLocalResourceObject("Domingo") %>' + "",
                               "" + '<%=(string)GetLocalResourceObject("Lunes") %>' + "",
                               "" + '<%=(string)GetLocalResourceObject("Martes") %>' + "",
                               "" + '<%=(string)GetLocalResourceObject("Miércoles") %>' + "",
                               "" + '<%=(string)GetLocalResourceObject("Jueves") %>' + "",
                               "" + '<%=(string)GetLocalResourceObject("Viernes") %>' + "",
                               "" + '<%=(string)GetLocalResourceObject("Sábado") %>' + ""],
                    dayNamesMin: ["" + '<%=(string)GetLocalResourceObject("Do") %>' + "",
                                  "" + '<%=(string)GetLocalResourceObject("Lu") %>' + "",
                                  "" + '<%=(string)GetLocalResourceObject("Ma") %>' + "",
                                  "" + '<%=(string)GetLocalResourceObject("Mi") %>' + "",
                                  "" + '<%=(string)GetLocalResourceObject("Ju") %>' + "",
                                  "" + '<%=(string)GetLocalResourceObject("Vi") %>' + "",
                                  "" + '<%=(string)GetLocalResourceObject("Sa") %>' + ""],
                    dayNamesShort: ["" + '<%=(string)GetLocalResourceObject("Dom") %>' + "",
                                    "" + '<%=(string)GetLocalResourceObject("Lun") %>' + "",
                                    "" + '<%=(string)GetLocalResourceObject("Mar") %>' + "",
                                    "" + '<%=(string)GetLocalResourceObject("Mie") %>' + "r",
                                    "" + '<%=(string)GetLocalResourceObject("Jue") %>' + "",
                                    "" + '<%=(string)GetLocalResourceObject("Vie") %>' + "",
                                    "" + '<%=(string)GetLocalResourceObject("Sab") %>' + ""],
                    monthNames: ["" + '<%=(string)GetLocalResourceObject("Enero") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Febrero") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Marzo") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Abril") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Mayo") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Junio") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Julio") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Agosto") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Septiembre") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Octubre") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Noviembre") %>' + "",
                                 "" + '<%=(string)GetLocalResourceObject("Diciembre") %>' + ""],
                    monthNamesShort: ["" + '<%=(string)GetLocalResourceObject("Ene") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Feb") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Mmar") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Abr") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("May") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Jun") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Jul") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Ago") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Sep") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Oct") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Nov") %>' + "",
                                      "" + '<%=(string)GetLocalResourceObject("Dic") %>' + ""],
                    changeYear: true,
                    changeMonth: true
                }
            );
     });

     function myTextExtraction(node) {
         var x = $(node)[0];
         if ($(x)[0].children.length > 0) {
             var input = $(x)[0].children[0];
             if (input.type === 'submit') {
                 return input.value;
             }
         }
         else {

             return node.innerHTML;
         }
     }

     
            
        
    </script> 

<script type="text/javascript">
    
    //boton agregar plaga
    function addPlaga() {
        var ddl = $("#<%= ddlFiltro.ClientID %>").clone().html();
        var html = '<tr><td colspan="4">  ';
        /*Aqui iria codigo de select*/
        html += '<table align="center"><tr><td style="width:37px;">&nbsp;</td><td><select name="<%= ddlFiltro.UniqueID %>">' + ddl + '</select></td><td><img alt="Quitar" src="../images/remove-icon.png" class="btnRemoverPlaga"/></td></tr></table>';
        $('#tblPlagas').append(html);
        registerCombobox();
    }
    
    //boton remover plaga
    $('.btnRemoverPlaga').live('click', function () {
        $(this).parent().parent().parent().parent().parent().parent().remove();
    });



</script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    

    <div class="container">
         <h1 style="width:870px; max-width:870px; min-width:870px;"><asp:Label meta:resourceKey="lblBienvenido" ID="lblBienvenido" runat="server">Programa Semanal de Fumigaci&oacute;n</asp:Label></h1>
    <table class="index" style="width:870px; max-width:870px; min-width:870px;">
        <tr>
            <td colspan="7">
                <h2><asp:Label ID="lbIns1" meta:resourceKey="lbIns1" runat="server">Seleccione los valores</asp:Label></h2>
            </td>
        </tr>
 
       <tr>
                        <td style="width:110px"><asp:Literal runat="server" ID="ltPlanta" meta:resourceKey="ltPlanta">*Planta</asp:Literal></td>
                        <td class="tall">
                            <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true"  AutoPostBack="true"
                                DataTextField="campoNombre" DataValueField="campoId" 
                                onselectedindexchanged="ddlPlanta_SelectedIndexChanged"> </asp:DropDownList>               
                        </td>
                        <td><asp:Literal runat="server" ID="ltInvernadero" meta:resourceKey="ltInvernadero">*Invernadero</asp:Literal></td>
                        <td class="tall">
                            <asp:DropDownList ID="ddlInvernadero" runat="server" 
                                AppendDataBoundItems="true" AutoPostBack="true" DataTextField="invernadero" 
                                DataValueField="Greenhouse" 
                                onselectedindexchanged="ddlInvernadero_SelectedIndexChanged"  ></asp:DropDownList>                
                        </td> 
                               <td style="width:200px; text-align:left;">
                           <h3> <asp:Label ID="lblHec" meta:resourceKey="lblHec" runat="server" Text ="Hect&aacute;reas:"></asp:Label>               
                            <asp:Label ID="lblHec2" runat="server"></asp:Label>   </h3>         
                        </td>
            </tr>
                    <tr>
                        <td><asp:Literal runat="server" meta:resourceKey="nombre">*Nombre</asp:Literal></td>
                        <td><asp:TextBox ID="txtNombre"  runat="server" Width="208px" MaxLength="20"></asp:TextBox></td>                    
                        <td style="width:140px"><asp:Literal runat="server" meta:resourceKey="ltDiaSugerido" >*Día sugerido</asp:Literal></td>                        
                        <td style="text-align:left;"><asp:TextBox ID="txtFechaAplicacion" runat="server" CssClass="datepicker cajaChica"></asp:TextBox></td>
                        
                    </tr>
                    <tr>
                        <td ><asp:Label ID="lblSemanaCultivo" meta:resourceKey="lblSemanaCultivo" runat="server" Text="*Edad de Cultivo"></asp:Label></td>
                        <td class="tall"><asp:DropDownList ID="ddlEdadCultivo" runat="server" DataTextField="edad" DataValueField="edad" AppendDataBoundItems="true"  AutoPostBack="true" onselectedindexchanged="ddlEdadCultivo_SelectedIndexChanged"> </asp:DropDownList></td>
                    </tr>
                    <tr>   
                    <td class="checkboxes"><asp:CheckBox ID="ckCapa" meta:resourceKey="ckCapa"  runat="server" Text="Desde/Hasta Capar"/></td> 
                    <td class="checkboxes">   <asp:CheckBox ID="ckFinCiclo" meta:resourceKey="ckFinCiclo" runat="server" Text="Hasta fin de ciclo"/></td> 
                    <td><asp:Label ID="lblEquipoAplicacion" meta:resourceKey="lblEquipoAplicacion" runat="server" Text="*Equipo de Aplicaci&oacute;n"></asp:Label></td>
                    <td class="tall"><asp:DropDownList ID="ddlEquipoAplicacion" runat="server" DataValueField = "idEquipoAplicacion" DataTextField = "nombre" AppendDataBoundItems="true" AutoPostBack="true" onselectedindexchanged="ddlEquipoAplicacion_SelectedIndexChanged"> </asp:DropDownList> </td>
                    <td style="text-align:left;"><h3 style="text-align:center;"> <asp:Label ID="lblLts" meta:resourceKey="bllLts"  runat="server" Text ="Lts./Hect&aacute;rea:"></asp:Label>               <asp:Label ID="LblLts2" runat="server"></asp:Label>   </h3>    </td>
                    </tr>
                    <tr>
                        <td ><asp:Label ID="lblTipoBoquilla" runat="server" meta:resourceKey="lblTipoBoquilla" Text="*Tipo Boquilla"></asp:Label></td>
                        <td class="tall">
                            <asp:DropDownList ID="ddlTipoBoquilla" runat="server" DataTextField="nombre" DataValueField="idTipoBoquilla" AppendDataBoundItems="true"  AutoPostBack="true" onselectedindexchanged="ddlTipoBoquilla_SelectedIndexChanged"> </asp:DropDownList>
                        </td>

                         <td >
                            <asp:Label ID="lblTipoAplicacion" meta:resourceKey="lblTipoAplicacion" runat="server" Text="*Tipo Aplicacion"></asp:Label>
                        </td>
                        <td class="tall">
                            <asp:DropDownList ID="ddlTipoAplicacion" runat="server" DataTextField="nombre" 
                                DataValueField="idTipoAplicacion" AppendDataBoundItems="true"  
                                AutoPostBack="false"> </asp:DropDownList>
                        </td>
                            <td style="text-align:left;">
                           <h3 style="text-align:center;"></h3>        
                        </td>
                    </tr>
                         
    </table>
    <br />
    <table class="index" id="tbl_quim" style="width:870px; max-width:870px; min-width:870px;">
         <tr>
            <td colspan="2">
                <h2><asp:Label ID="lblInst2" meta:resourceKey="lblInst2" runat="server">Seleccione las plagas</asp:Label></h2>
            </td>
           
        </tr>
        <tr>

                <td colspan="4" align="center" style="text-align:center;">
                    <table id="tblPlagas" align="center">
                        <tr ><td><asp:Literal runat="server" meta:resourceKey="ltPlaga" >Plagas:</asp:Literal></td>
                            <td class="margin_top">
                                <asp:DropDownList ID="ddlFiltro" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                            </td>
                            <td>
                                <img id="imgAdd"  alt="Agregar" src="../images/add-icon.png" onclick="addPlaga();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>  <td colspan="4">
                    <asp:HiddenField ID="PlagasHidden" runat="server" />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" meta:resourceKey="btnBuscar" onclick="btnBuscar_Click" />
                </td></tr>
        <tr>
            <td colspan="2" >
                <asp:GridView ID="gdvQumicosBuscados" runat="server" DataKeyNames="ITEMNMBR"
                    CssClass="gridView" 
                    HeaderStyle-HorizontalAlign="Center"
                    AutoGenerateColumns="False" 
                    onRowDataBound="gdvQumicosBuscados_RowDataBound"                                                     
                    onprerender="gdvQumicosBuscados_PreRender"
                    EmptyDataText="No existen registros" EnableModelValidation="True"
                    meta:resourceKey="gv"
                    >
                    <AlternatingRowStyle BackColor="#D6DFD0"></AlternatingRowStyle>
                    <Columns>                       
                        <asp:TemplateField  ItemStyle-Width = "10%" ItemStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ID="Image6" runat="server" ImageUrl="../images/superstar.png" Visible='<%# Eval("comodin") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ITEMDESC" HeaderText="Nombre" meta:resourceKey="htNombre" ItemStyle-CssClass="center" >
                            <ItemStyle CssClass="center"></ItemStyle>
                        </asp:BoundField>                        
                        <asp:BoundField DataField="abejorro" HeaderText="Abejorros" meta:resourceKey="htAbejorros" ItemStyle-CssClass="center"/>

                        <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                            <ItemTemplate> 
                                <asp:Image ID="Image1" runat="server" Width="22px"  ImageUrl="../comun/img/small_error.png" 
                                    ToolTip='<%# Eval("mensaje")%>'
                            
                                    Visible ='<%# string.Compare((string)Eval("mensaje"), "", false) == 0 ? false : true %>'
                                      />
                            </ItemTemplate>
                            <HeaderStyle BorderStyle="None"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>

                        
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#F0F5E5" BorderColor="#ADC995"  ></HeaderStyle>
                </asp:GridView>
            </td>
            <td colspan="2" >
                <asp:GridView ID="gdvTratamientosBuscados"  meta:resourceKey="gv" runat="server" DataKeyNames="idTratamiento"  CssClass="gridView" HeaderStyle-HorizontalAlign="Center"
                    AutoGenerateColumns="False" onRowDataBound="gdvTratamientosBuscados_RowDataBound" onprerender="gdvTratamientosBuscados_PreRender"
                    EmptyDataText="No existen registros" EnableModelValidation="True"  >
                    <AlternatingRowStyle BackColor="#D6DFD0"></AlternatingRowStyle>
                    <Columns>                       
                        <asp:TemplateField  ItemStyle-Width = "10%" ItemStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbTratamiento" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Name" HeaderText="Planta" meta:resourceKey="htPlanta" ItemStyle-CssClass="center" />

                        <asp:BoundField DataField="vNombre" HeaderText="Tratamiento" meta:resourceKey="htTratamiento" ItemStyle-CssClass="center" >
                            <ItemStyle CssClass="center" ></ItemStyle>
                        </asp:BoundField>                        
                        
                         <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                            <ItemTemplate> 
                                <asp:Image ID="Image2" runat="server" Width="22px"  ImageUrl="../comun/img/lupa.png" 
                                    ToolTip='<%# Eval("quimicos")%>'                            
                                    Visible="true" />
                            </ItemTemplate>
                            <HeaderStyle BorderStyle="None"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" BackColor="#F0F5E5" BorderColor="#ADC995"  ></HeaderStyle>
                </asp:GridView>
            </td>
            
        </tr>
        <tr>
            <td align="left" colspan="4">
                <asp:Button ID="btnAdd" meta:resourceKey="btnAdd" runat="server" Text="Agregar" onclick="btnAdd_Click"  />                
            </td>
        </tr>
    </table>
    <br />
    <table class="index" id="Table1" style="width:870px; max-width:870px; min-width:870px;">
    <tr>
        <td style="width:200px;" >
            <asp:CheckBox ID="ckTratamiento" meta:resourceKey="ckTratamiento" runat="server" Text="Crear Tratamiento Nuevo"/> 
        </td>
        <td style="width:200px;" >
            <asp:Literal runat="server" meta:resourceKey="ltNuevoTratamiento">*Nombre Tratamiento:</asp:Literal>
        </td>
        <td>
             <asp:TextBox ID="txtTratamientoNom" runat="server" Width="208px" MaxLength="50"></asp:TextBox>
        </td>
                
    </tr>
    </table>
    <br />

        <table align="center"><tr><td>
        <asp:GridView ID="gdvPrograma" runat="server" AutoGenerateColumns="False" Width="870px"
                DataKeyNames="idQuim"  CssClass="gridView" HeaderStyle-CssClass="wrapped" 
                OnRowDeleting = "gdvPrograma_RowDeleting"
                onRowDataBound="gdvPrograma_RowDataBound"                                                     
                onprerender="gdvPrograma_PreRender"
            EmptyDataText="No existen registros" EnableModelValidation="True"
            meta:resourceKey="gv">
            <Columns>
                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                    <ItemTemplate> 
                        <asp:Image ID="Image1" runat="server" Width="22px"  ImageUrl="../comun/img/small_error.png" 
                            ToolTip="Este producto puede causar sobredosis"
                            meta:resourceKey="imgToolTip"
                            Visible ='<%# string.Compare((string)Eval("mensaje"), "", false) == 0 ? false : true %>'
                              />
                    </ItemTemplate>
                    <HeaderStyle BorderStyle="None"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                </asp:TemplateField>


                <asp:BoundField DataField="nomQuim"           meta:resourceKey="nomQuim"           HeaderText="Químico" />
                <asp:BoundField DataField="IngredienteActivo" meta:resourceKey="IngredienteActivo" HeaderText="Ingr. Activo" />
                <asp:BoundField DataField="reentrada"         meta:resourceKey="reentrada"         HeaderText="Reentrada" />
                <asp:BoundField DataField="Dosis"             meta:resourceKey="Dosis"             HeaderText="Dosis" />
                <asp:BoundField DataField="Abejorro"          meta:resourceKey="Abejorro"          HeaderText="Abejorro"/>
                <asp:BoundField DataField="presistencia"      meta:resourceKey="presistencia"      HeaderText="persistencia"/>
                <asp:BoundField DataField="LitrosUsar"        meta:resourceKey="LitrosUsar"        HeaderText="Agua" />
                <asp:BoundField DataField="ApliDesde"         meta:resourceKey="ApliDesde"         HeaderText="Desde" />
                <asp:BoundField DataField="ApliHasta"         meta:resourceKey="ApliHasta"         HeaderText="Hasta" />
                <asp:TemplateField HeaderText="Cantidad Solicitada"   meta:resourceKey="CantidadSolicitada"  HeaderStyle-Width="100px">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" CssClass="cajaChica" runat="server" Text='<%#  Eval("cantidadPedida") %>' ></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                 <asp:BoundField DataField="cantidadPesada" meta:resourceKey="CantidadPesada" HeaderText="Pesada"  HeaderStyle-Width="120px"/>

                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                    <ItemTemplate>                                         
                        <asp:ImageButton ID="btnDelete" runat="server"  src="../comun/img/delete.png" OnClick="imgDeleteNominee_Click" Width="22px" />
                    </ItemTemplate>
<HeaderStyle BorderStyle="None"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        </td>
        </tr>

        
       <tr>
        <td style="text-align:center;"> 
        <table align="center"  style="width:870px; max-width:870px; min-width:870px;" cellspacing="0" cellpadding="0" border="0">
        <tr><td align="left"> <asp:Literal runat="server" meta:resourceKey="Notas"> Notas / Observaciones:</asp:Literal></td></tr>
        <tr><td>
            <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" MaxLength="1000" 
                Height="86px" Width="870px"></asp:TextBox></td></tr>
                <tr><td>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"  meta:resourceKey="btnGuardar" onclick="btnGuardar_Click"/>
                             <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" meta:resourceKey="btnAutorizar" onclick="btnAutorizar_Click" />
                            
                </td></tr></table>
        </td></tr>      
        </table>
    </div>
    </div>

    <asp:Panel ID="pnlCapturaHoras" runat="server" CssClass="modalPopup" Style="" width="470px">
               
      <table class="index3" align="center" style="min-width:430px; margin:5px;">
          
          <tr>
              <td colspan="2" >  
                <h2><asp:Label runat="server" ID="lblInfo" meta:resourceKey="lblInfo"  Text="Confirme la acción"></asp:Label></h2>  
              </td>                         
           </tr>

          <tr>
              <td style="width:150px;">  
                <h3><asp:Label runat="server" ID="lblPlanta"  meta:resourceKey="lblPlanta" Text=" Planta"></asp:Label></h3>  
              </td>
              <td >
                <h4>   <asp:Label runat="server" ID="lblPlanta2"  Text=""></asp:Label></h4>
               </td>               
           </tr>

           <tr>
              <td >  
                <h3><asp:Label runat="server" ID="lblInv"  meta:resourceKey="lblInv" Text=" Invernadero"></asp:Label></h3>  
              </td>
              <td >
                 <h4><asp:Label runat="server" ID="lblInv2" Text=""></asp:Label></h4>  
               </td>              
           </tr>

          <tr>
              <td >  
                <h3><asp:Label runat="server" ID="lbReentrada"  meta:resourceKey="lblReentrada" Text=" Horas Reentrada"></asp:Label></h3>  
              </td>
              <td >
                   <h4><asp:Label runat="server" ID="lblHorasRenntreda" Text=""></asp:Label>   Hrs.</h4>
               </td>
          
           </tr>
           <tr>
              <td >  
                <h3><asp:Label runat="server" ID="lbCocecha"  meta:resourceKey="lblCosecha" Text=" Intervalo seg. a Cocecha"></asp:Label></h3>  
              </td>
              <td >
                <h4>    <asp:Label runat="server" ID="lblCocecha2" Text=""></asp:Label> D&iacute;as.</h4>
               </td>
          
           </tr>

           <tr>
              <td >  
                <h3><asp:Label runat="server" ID="lblComodines"  meta:resourceKey="lblComodines" Text=" Comodines"></asp:Label></h3>  
              </td>
              <td >
                <h4>   
                    &nbsp;<asp:BulletedList ID="BulletedList1" runat="server"> </asp:BulletedList>                     
                    <h4>
                    </h4>
                  </h4>
               </td>
          
           </tr>

           <tr>
              <td colspan="2" style="text-align:right; background:#ffa05f;" >  
                <h4><asp:Label runat="server" ID="lblPregunta" meta:resourceKey="lblPregunta" Text=" ¿Est&aacute; seguro que desea guardar?"></asp:Label></h4>  
              </td>                         
           </tr>

                <tr>
                    <td colspan="2">
                    
                        <asp:Button CssClass="button" runat="server" meta:resourceKey="save" ID="save" Text="Guardar" OnClick="save_Click" />
                        <asp:Button CssClass="button" runat="server" meta:resourceKey="btnCancelar" ID="btnCancel" Text="Cancelar" />
                    </td>
                </tr> 
        </table>
    </asp:Panel>
        <asp:LinkButton runat="server" ID="lnkHiddenMdlPopupControl"  Text=""  Enabled="false"/>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupMessageGralControl" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlCapturaHoras" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnCancel" />


         <asp:Panel ID="pnlConfirm" runat="server" CssClass="modalPopup" Style="" width="450px">
               
      <table class="index3" align="center" style="min-width:430px; margin:5px;">
          
          <tr>
              <td colspan="2" >  
                <h2><asp:Label runat="server" ID="lblConfirmar"  meta:resourceKey="lblConfirmar" Text=" Confirme la acción"></asp:Label></h2>  
              </td>                         
           </tr>
           
           <tr>
              <td colspan="2" style="text-align:right; background:#ffa05f;" >  
                <h4><asp:Label runat="server" ID="Label12"  meta:resourceKey="lblP2" Text=" ¿Est&aacute; seguro que desea eliminar el qu&iacute;mico de la lista?"></asp:Label></h4>  
              </td>                         
           </tr>

                <tr>
                    <td colspan="2">
                    
                        <asp:Button CssClass="button" runat="server"  meta:resourceKey="btnSi" ID="bntSiEliminarQuim" Text="Sí" OnClick="bntSiEliminarQuim_Click" />
                        <asp:Button CssClass="button" runat="server"  meta:resourceKey="btnNo" ID="btnNoEliminarQuim" Text="No" />
                    </td>
                </tr> 
        </table>
    </asp:Panel>
        <asp:LinkButton runat="server" ID="LinkButton1"  Text=""  Enabled="false"/>
        <ajaxToolkit:ModalPopupExtender ID="mdlPopupMessageConfirm" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlConfirm" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnNoEliminarQuim" />
           <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />


     <asp:Panel ID="pnlCapturaDosisControl" runat="server" CssClass="modalPopup" Style="padding:5px; display:none;" width="500px" >

    <table class="index3" align="center" style="min-width:400px; margin:5px;">
        <tr>
            <td colspan="2" style="text-align:right; background:#ffa05f;" >
                <h4><asp:Label runat="server" ID="lblBienvenida"  meta:resourceKey="lblBienvenida"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label runat="server" ID="lbRazon"  meta:resourceKey="lbRazon"></asp:Label>
            </td>
            <td>
               <asp:DropDownList ID="ddlRazones" runat="server" Width="200px" DataTextField="vRazon" DataValueField="idRazonCambio">   </asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td colspan="2" class="floatnone">           
                
                <asp:Button CssClass="button" runat="server" ID="Button1" meta:resourceKey="Button1"  Text="Cancelar" />
                <asp:Button CssClass="button" runat="server" ID="Button2" meta:resourceKey="save"  Text="Guardar" OnClick="save2_OnClick" />
                <asp:Button CssClass="button" runat="server" ID="btnOKMessageGralControl"  meta:resourceKey="btnOKMessageGralControl" Text="Cancelar" style="display: none;" />
            </td>
        </tr>
    </table>

    </asp:Panel>
        <asp:LinkButton runat="server" ID="LinkButton2"  Text=""  Enabled="false"/>
        <ajaxToolkit:ModalPopupExtender ID="popUpRazones" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlCapturaDosisControl" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnOKMessageGralControl" />
       <%-- </asp:ModalPopupExtender>--%>


   <%-- <uc2:HorasFumigacion ID="popUpCapturaHoras" runat="server" />--%>
</asp:Content>

