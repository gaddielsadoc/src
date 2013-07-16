<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCopiarPrograma.aspx.cs" Inherits="pages_frmCopiarPrograma"  EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
   <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />   

<script type="text/javascript">
    var ddlInvClone;
    var gvQuimicos;
    function pageLoad() {
        ddlInvClone = $("#<%= ddlInvernadero.ClientID %>").parent().clone().html();
        $("#<%= ddlInvernadero.ClientID %>").attr('disabled', '');
        gvQuimicos = $(".tableQuimicoOriginal").clone().html();
        gvInnerTable = $(".tableQuimicoOriginal #<%=gdvPrograma.ClientID %>").parent().clone().html();
        //registerCombobox();

        //le asignamos el primer titulo a la tabla de quimicos  //'<%=(string)GetLocalResourceObject("CorreoRequerido")%>'
        $(".tblTituloOriginal").html($("#<%= ddlInvernadero.ClientID %> option:selected").text() + " - "+'<%=(string)GetLocalResourceObject("FumigacionOriginal") %>'+"");
        //hacemos ineditables todos los campos registrados en registerCombobox
        $('select').click(function () { return false });
    }
    /*
    function registerCombobox() {
        $("select[name='<%= ddlPlanta.UniqueID %>']").comboboxPostBack();
        $("select[name='<%= ddlEdadCultivo.UniqueID %>']").comboboxPostBack();
        $("select[name='<%= ddlEquipoAplicacion.UniqueID %>']").comboboxPostBack();
        $("select[name='<%= ddlTipoBoquilla.UniqueID %>']").comboboxPostBack();
    }
    */
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

     function GetHectareas(valor, objeto) {
         var index = objeto.attr('class').replace("ddlInvernadero", "");
         
         //sacamos lista de quimicos
         var listaQuimicos="";
         var len = $('.tableQuimicos .tableQuimico' + index + ' .quimicoId').length;
         $('.tableQuimicos .tableQuimico' + index + ' .quimicoId').each(function (index, element) {
             listaQuimicos = listaQuimicos + "," + $(this).val();
         });
         listaQuimicos = listaQuimicos.substring(1);

        //sacamos el invernadero
        var invernadero = objeto.children("option:selected").val();
        //sacamos el valor de la fecha
        var fecha = $("#<%= txtFechaAplicacion.ClientID %>").val()

        //validaciones antes de mandar
        errors = false;

        if ( fecha == "" || !(/\d\d\d\d-\d\d-\d\d/.test(fecha)) ) {
            $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("CampoFecha") %>'+" <br>");
            $("#error").show();
            errors = true;
        }
        if (invernadero == "") {
            errors = true;
        }
        
        if(errors==false) {
            //sacamos el invernadero y lo metemos al titulo
            $(".tblTitulo" + index).html(objeto.children("option:selected").text());

            //llamamos el pageMethod
            var indexString = index.toString();
            PageMethods.GetValorInvernadero(invernadero, listaQuimicos, fecha, indexString,$('#<%=txtConnection.ClientID %>').val(), callback);
        }
    }

    function callback(res) {
        var n = res.split("|");

        //borramos la tabla interior que contiene los quimicos
        $(".tableQuimico" + n[1] + " #<%=gdvPrograma.ClientID %>").remove();

        //separamos en el [0] la cadena resultado y en el [1] el index del elemento que estamos trabajando
        var n = res.split("|");

        //le agregamos el index a los campos de quimicoId y CantidadPedida
        /*var gvInnerTable1 = gvInnerTable.replace('name=quimicoId', 'name=quimicoId' + n[1]);
        gvInnerTable1 = gvInnerTable1.replace('name=cantidadPesada', 'name=cantidadPesada' + n[1]);
        */
        if (n[0] == ""+'<%=(string)GetLocalResourceObject("CicloCultivo") %>'+"") {
            $(".tableQuimico" + n[1]).append("<div id='<%=gdvPrograma.ClientID %>'>"+n[0]+"</div>");
        }
        else if (n[0] == "true") {
            //no encontró quimicos en conflicto
            $(".tableQuimico" + n[1] + " div").append(gvInnerTable);
        }
        else {
            //es una lista de quimicos que no se pueden
            var quimicoIds = n[0].split(",");

            //agregamos la tabla
            $(".tableQuimico" + n[1] + " div").append(gvInnerTable);

            //mostramos los botones de quitar en los quimicos en conflicto
            //recorremos la lista de quimicos en la tabla
            $('.tableQuimico' + n[1] + ' .quimicoId').each(function (i, element) {
                //recorremos la lista de quimicos que trajo el sp
                for (j = 0; j < quimicoIds.length; j++) {

                    if ($.trim($(this).val()) == quimicoIds[j]) {
                        //el quimico es igual
                        $(this).parent().parent().find(".visibilityHidden").not('.quimicoId').removeClass("visibilityHidden");
                        $(this).parent().parent().children().attr("disabled", "true");
                    }
                }

            });

        }

        //una vez agregados los campos se le cambia el name en base a su index
        $(".tableQuimico" + n[1] + " .cantidadPedida").attr("name", "cantidadPedida" + n[1]);
        $(".tableQuimico" + n[1] + " .quimicoId").attr("name", "quimicoId" + n[1]);
        $(".tableQuimico" + n[1] + " .reentrada").attr("name", "reentrada" + n[1]);
        //--
        $(".tableQuimico" + n[1] + " .litros").attr("name", "litros" + n[1]);
        $(".tableQuimico" + n[1] + " .dosisMin").attr("name", "dosisMin" + n[1]);
        $(".tableQuimico" + n[1] + " .dosisMax").attr("name", "dosisMax" + n[1]);
        /*
        $(".tableQuimico" + n[1] + " .agua").attr("class", "agua" + n[1]);
        $(".tableQuimico" + n[1] + " .desde").attr("class", "desde" + n[1]);
        $(".tableQuimico" + n[1] + " .hasta").attr("class", "hasta" + n[1]);
        */
        invernadero = $("#tablaInvernaderos .ddlInvernadero" + n[1]).val();  //objeto.children("option:selected").val();
        fecha = $("#<%= txtFechaAplicacion.ClientID %>").val();
        idCopy = $("#<%= idCopy.ClientID %>").val();
        capar = $("#<%= capar.ClientID %>").val();
        fin = $("#<%= fin.ClientID %>").val();
        PageMethods.GetValoresRecalculados(n[1], fecha, invernadero, idCopy, capar, fin, $('#<%=txtConnection.ClientID %>').val(), rellenaResult);
    }

    function rellenaResult(result) {
        var index = result.split("@");
        var rows = index[1].split("|");

        var data;
        if (rows.length > 0) {
            for (i = 0; i < rows.length; i++) {
                data = rows[i].split("$");
                $(".tableQuimico" + index[0] + " .litros:eq(" + i + ")").val(data[1]);
                $(".tableQuimico" + index[0] + " .dosisMin:eq(" + i + ")").val(data[2]);
                $(".tableQuimico" + index[0] + " .dosisMax:eq(" + i + ")").val(data[3]);
                $(".tableQuimico" + index[0] + " .agua:eq(" + i + ")").text(data[1]);
                $(".tableQuimico" + index[0] + " .desde:eq(" + i + ")").text(data[2]);
                $(".tableQuimico" + index[0] + " .hasta:eq(" + i + ")").text(data[3]);
                $(".tableQuimico" + index[0] + " .cantidadPedida:eq(" + i + ")").val(data[4]);

            }
        }
    }
    
    function myValidador() {

        var size = $("#tablaInvernaderos select").size();
        errors = false;

        //*******VALIDACION MANUAL PARA AL MENOS UNA COPIA**************
        $('.tableQuimicos .cantidadPedida').each(function () {
            if (isNaN($(this).val())) {
                $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("Numerico") %>'+"<br>");
                $("#error").show();
                errors = true;
                return false;
            }
            else if ($(this).val() < 0) {
                $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("MenorACero") %>'+"<br>");
                $("#error").show();
                errors = true;
                return false;
            }
        });

        //*******VALIDACION MANUAL PARA AL MENOS UNA COPIA**************
        if (size == 1) {
            $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("AgregarInvernadero") %>'+"<br>");
            $("#error").show();
            errors = true;
            return false;
        }

        if ($(".txtNombre").val() == "") {
            $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("AgregarNombre") %>'+"<br>");
            $("#error").show();
            errors = true;
            return false;
        }

        //*******VALIDACION MANUAL PARA LOS INVERNADEROS DIFERENTES**************
         
         for (i_ = 0; i_ < size; i_++) {
             var value = $("#tablaInvernaderos select")[i_].value;
             if (errors == false) {
                 $("#tablaInvernaderos select").each(function (i, e) {
                     if ($(this).val() == "-- Seleccione --") {
                         $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("SeleccionarInvernaderos") %>'+"<br>");
                         $("#error").show();
                         errors = true;
                         return false;
                     }
                     else if ($(this).val() == value && i_ != i) {
                         $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("InvernaderosDiferentes") %>'+"<br>");
                         $("#error").show();
                         errors = true;
                         return false;
                     }
                 });
             }
         }

        //validamos que no existan quimicos que no se pueden aplicar
         if ($(".borrarQuimico").not(".visibilityHidden").size() > 0) {
             $("div.alerta span").html($("div.alerta span").html() + ""+'<%=(string)GetLocalResourceObject("RemoverQuimicos") %>'+"<br>");
            $("#error").show();
            errors = true;
            return false;
        }


        if (!errors) {
            $("#confirm").show();
             //$("#<%=save.ClientID%>").click();
         }

     }

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
                    dayNames: [""+'<%=(string)GetLocalResourceObject("Domingo") %>'+"",
                               ""+'<%=(string)GetLocalResourceObject("Lunes") %>'+"",
                               ""+'<%=(string)GetLocalResourceObject("Martes") %>'+"",
                               ""+'<%=(string)GetLocalResourceObject("Miércoles") %>'+"",
                               ""+'<%=(string)GetLocalResourceObject("Jueves") %>'+"",
                               ""+'<%=(string)GetLocalResourceObject("Viernes") %>'+"",
                               ""+'<%=(string)GetLocalResourceObject("Sábado") %>'+""],
                    dayNamesMin: [""+'<%=(string)GetLocalResourceObject("Do") %>'+"",
                                  ""+'<%=(string)GetLocalResourceObject("Lu") %>'+"",
                                  ""+'<%=(string)GetLocalResourceObject("Ma") %>'+"",
                                  ""+'<%=(string)GetLocalResourceObject("Mi") %>'+"",
                                  ""+'<%=(string)GetLocalResourceObject("Ju") %>'+"",
                                  ""+'<%=(string)GetLocalResourceObject("Vi") %>'+"",
                                  ""+'<%=(string)GetLocalResourceObject("Sa") %>'+""],
                    dayNamesShort: [""+'<%=(string)GetLocalResourceObject("Dom") %>'+"",
                                    ""+'<%=(string)GetLocalResourceObject("Lun") %>'+"",
                                    ""+'<%=(string)GetLocalResourceObject("Mar") %>'+"",
                                    ""+'<%=(string)GetLocalResourceObject("Mie") %>'+"r",
                                    ""+'<%=(string)GetLocalResourceObject("Jue") %>'+"",
                                    ""+'<%=(string)GetLocalResourceObject("Vie") %>'+"",
                                    ""+'<%=(string)GetLocalResourceObject("Sab") %>'+""],
                    monthNames: [""+'<%=(string)GetLocalResourceObject("Enero") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Febrero") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Marzo") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Abril") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Mayo") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Junio") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Julio") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Agosto") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Septiembre") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Octubre") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Noviembre") %>'+"",
                                 ""+'<%=(string)GetLocalResourceObject("Diciembre") %>'+""],
                    monthNamesShort: [""+'<%=(string)GetLocalResourceObject("Ene") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Feb") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Mmar") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Abr") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("May") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Jun") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Jul") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Ago") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Sep") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Oct") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Nov") %>'+"",
                                      ""+'<%=(string)GetLocalResourceObject("Dic") %>'+""],
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


     var indexInvernadero = 0;
     function addInvernadero() {
         //agregamos otro drop down
         var ddlInvClone1 = ddlInvClone.replace('ddlInvernadero ', 'ddlInvernadero' + indexInvernadero + ' class="ddlInvernadero' + indexInvernadero + '" ');
         
         //agregamos la nueva row con su boton de quitar
         $("#tablaInvernaderos").append("<tr>" +
            "<td>" + '<%=(string)GetLocalResourceObject("_Invernadero") %>' + "</td>" +
            "<td class='tall'>" +
                ddlInvClone1 +
            "</td>" +
            "<td style='text-align:left;'>" +
                "<img id='addInvernaderoBtn" + indexInvernadero + "' alt='"+'<%=(string)GetLocalResourceObject("Quitar") %>'+"' src='../images/remove-icon.png' class='removeInvernadero' />" +
            "</td>" +
         "</tr>");

         $("#tablaInvernaderos .ddlInvernadero" + indexInvernadero).attr("name", "invernaderoIds");
         // reemplazamos el name del input ddl para saber que invernadero corresponde a el número
         //$("select[name='<%= ddlInvernadero.UniqueID %>']").last().attr("name", "invernadero" + i);

         //cambiamos el valor del select a seleccione
         $("#tablaInvernaderos .ddlInvernadero" + indexInvernadero).val(""+'<%=(string)GetLocalResourceObject("SeleccioneJS") %>'+"");
         //registerCombobox();
         $("#tablaInvernaderos .ddlInvernadero" + indexInvernadero).combobox({
             selected: function (event, ui) {
                 GetHectareas($(this).val(), $(this));
             }
         });

         //agregamos otro grid view
         var gvQuimicos1 = gvQuimicos.replace("tblTituloOriginal", "tblTitulo" + indexInvernadero);
         $(".tableQuimicos").append("<table class='tableQuimico" + indexInvernadero + "'>" + gvQuimicos1 + "</table>");
         indexInvernadero++;
         
     }

     //boton remover dosis
     $('.removeInvernadero').live('click', function () {
         $(this).parent().parent().remove();
         var index = $(this).attr('id').replace("addInvernaderoBtn", "");

         $(".tableQuimico" + index).remove();
     });

     //boton remover quimico
     $('.borrarQuimico').live('click', function () {
        $(this).parent().parent().remove();
     });

     function closeConfirmMsg() {
         $("#confirm").hide();
     }

    </script> 

    <style type="text/css">
        .style1
        {
            width: 299px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox ID="txtConnection" runat="server" CssClass="visibilityHidden"></asp:TextBox>
    <asp:TextBox ID="txtListaQuimicos" runat="server" CssClass="visibilityHidden"></asp:TextBox>
    <%--error msg--%>
    <div id="error" class="modalPopup" style="width: 500px; display:none; position: fixed; z-index:9999;">
	    <table style="vertical-align:middle; text-align:center; height:100%; width:100%;">
            <tbody>
			<tr>
                <td style="background:#ccc repeat;">
                    <div id="">
						<div class="alerta">
							<img src="../comun/img/error.png" id="imgMensajeError" alt="" />   
							<span></span>
						</div>
                    </div>
				</td>
			</tr>
            <tr>
                <td colspan="2">
                    <input type="button" value="OK" id="btnMensajeError" class="button" onclick="javascript:closeErrorMsg();" />
                </td>
            </tr>
        </tbody></table>
    </div>
    <%--error msg--%>
    <%--confirm msg--%>
    <div id="confirm" class="modalPopup" style="width: 500px; display:none; position: fixed; z-index:9999;">
	    <table style="vertical-align:middle; text-align:center; height:100%; width:100%;">
            <tbody>
			<tr>
                <td style="background:#ccc repeat;">
                    <div id="Div2">
						<div class="message">
							<img src="../comun/img/alerta.png" id="img1" alt="" /> 
                            <asp:Literal runat="server" meta:resourceKey="ltConfirme">Confirme que desea realizar el copiado</asp:Literal> 
						</div>
                    </div>
				</td>
			</tr>
            <tr>
                <td colspan="2">
                    <asp:Button CssClass="button" runat="server" ID="save" Text="Guardar" OnClick="save_Click" meta:resourceKey="save" />
                    <input type="button"  id="Button1" class="button" onclick="javascript:closeConfirmMsg();" runat="server" />
                </td>
            </tr>
        </tbody></table>
    </div>
    <%--confirm msg--%>

    <div class="container">
         <h1 style="width:870px; max-width:870px; min-width:870px;"><asp:Label ID="lblBienvenido" meta:resourceKey="lblBienvenido" runat="server">Copiado de Programas Semanales de Fumigaci&oacute;n</asp:Label></h1>
    <table class="index" style="width:870px; max-width:870px; min-width:870px;">
        <tr>
            <td colspan="7">
                <h2><asp:Label ID="Label1" meta-resourceKey="lblInstruccion" runat="server">Seleccione nombre y día</asp:Label></h2>
            </td>
        </tr>
        <tr>
            <td style="width:110px">
            <asp:Literal runat="server" meta:resourceKey="ltPlanta">*Planta</asp:Literal></td>
            <td class="tall">
                <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true"  AutoPostBack="true"
                    DataTextField="campoNombre" DataValueField="campoId" Enabled="False" > </asp:DropDownList>   
                <asp:HiddenField ID="equipoApli" runat="server" />
                <asp:HiddenField ID="idCopy" runat="server" />
                <asp:HiddenField ID="capar" runat="server" />
                <asp:HiddenField ID="fin" runat="server" />
            </td>
            <td></td>
            <td></td>
                    <td style="width:200px; text-align:left;">
                                    
            </td>
        </tr>
        <tr>
            <td><asp:Literal runat="server" meta:resourceKey="ltNombre">*Nombre</asp:Literal></td>
            <td>
                <asp:TextBox ID="txtNombre" CssClass="txtNombre" runat="server" Width="208px" MaxLength="20"></asp:TextBox>
            </td>                    
                        
            <td style="width:140px"><asp:Literal runat="server" meta:resourceKey="ltDiaSugerido">*Día sugerido</asp:Literal></td>                        
            <td style="text-align:left;">
                <asp:TextBox ID="txtFechaAplicacion" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
            </td>
                        
        </tr>
        <tr>
                 
            <td >
                <asp:Label ID="lblSemanaCultivo" meta:resourceKey="lblSemanaCultivo" runat="server" Text="*Edad de Cultivo"></asp:Label>
            </td>
            <td class="tall">
                <asp:DropDownList ID="ddlEdadCultivo" runat="server" DataTextField="edad" DataValueField="edad" AppendDataBoundItems="true"  AutoPostBack="true" Enabled="False"> </asp:DropDownList>
            </td>
                <td >
                <asp:Label ID="lblTipoAplicacion" meta:resourceKey="lblTipoAplicacion" runat="server">Tipo Aplicacion</asp:Label>
            </td>
            <td class="tall">
                <asp:DropDownList ID="ddlTipoAplicacion" runat="server" DataTextField="nombre" 
                    DataValueField="idTipoAplicacion" AppendDataBoundItems="true"  AutoPostBack="true" Enabled="False"> </asp:DropDownList>
            </td>
        </tr>
        <tr>   
        <td class="checkboxes"><asp:CheckBox ID="ckCapa" meta:resourceKey="ckCapa" runat="server" Text="Desde/Hasta Capar" Enabled="False"/>
        </td> 
            <td class="checkboxes">   <asp:CheckBox ID="ckFinCiclo" meta:resourceKey="ckFinCiclo" runat="server" Text="Hasta fin de ciclo" Enabled="False"/>
                    
        </td> 
                                
                        <td>
                <asp:Label ID="lblEquipoAplicacion" meta:resourceKey="lblEquipoAplicacion" runat="server" Text="*Equipo de Aplicaci&oacute;n"></asp:Label>
            </td>
            <td class="tall">
                <asp:DropDownList ID="ddlEquipoAplicacion" runat="server" 
                    DataValueField = "idEquipoAplicacion" DataTextField = "nombre" 
                    AppendDataBoundItems="true" AutoPostBack="true" Enabled="False"> </asp:DropDownList> 
            </td>

        <td style="text-align:left;">
                                  
            </td>
                      
        </tr>
        <tr>         
            <td >
                <asp:Label ID="lblTipoBoquilla" meta:resourceKey="lblTipoBoquilla" runat="server" Text="*Tipo Boquilla"></asp:Label>
            </td>
            <td class="tall">
                <asp:DropDownList ID="ddlTipoBoquilla" runat="server" DataTextField="nombre" 
                    DataValueField="idTipoBoquilla" AppendDataBoundItems="true"  AutoPostBack="true" Enabled="False" > </asp:DropDownList>
            </td>
        </tr>
             
    </table>
    <br />
    <table class="index" style="width:870px; max-width:870px; min-width:870px;" id="tablaInvernaderos">
        <tr>        
            <td style="width:150px;"><asp:Literal runat="server" meta:resourceKey="ltInvernadero">*Invernadero</asp:Literal></td>
            <td class="tall" style="width:160px;">
                <asp:DropDownList ID="ddlInvernadero" runat="server" 
                    AppendDataBoundItems="true" DataTextField="invernadero" DataValueField="Greenhouse" Enabled="False"></asp:DropDownList>                
            </td>
            <td style="text-align:left;">
                <img id="addInvernaderoBtn" alt="Agregar"  src="../images/add-icon.png" onclick="javascript:addInvernadero();" />
            </td>
        </tr>
    </table>
    <br />
    
    <br />
    <div class="tableQuimicos">
        <table align="center" class="tableQuimicoOriginal">

        <tr><td><h2 class="tblTituloOriginal"></h2></td></tr>
        <tr><td>
        <asp:GridView ID="gdvPrograma" runat="server" AutoGenerateColumns="False"
                DataKeyNames="idQuim"  CssClass="gridView" HeaderStyle-CssClass="wrapped" 
                onprerender="gdvPrograma_PreRender"
                meta:resourceKey="gv"
                 OnRowDataBound="gdvPrograma_RowDataBound"
                EmptyDataText="No existen registros" EnableModelValidation="True">
            <Columns>
                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                    <ItemTemplate>                                         
                        <asp:ImageButton ID="alerta" runat="server" src="../comun/img/alerta.png" Width="22px" CssClass="visibilityHidden" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="nomQuim"             HeaderText="Químico"      meta:resourceKey="Químico"       ItemStyle-CssClass="fijo"/>
                <asp:BoundField DataField="IngredienteActivo"   HeaderText="Ingr. Activo" meta:resourceKey="IngrActivo"  ItemStyle-CssClass="fijo"/>
                <asp:BoundField DataField="reentrada"           HeaderText="Reentrada"    meta:resourceKey="Reentrada"     ItemStyle-CssClass="fijo"/>
                <asp:BoundField DataField="Dosis"               HeaderText="Dosis"        meta:resourceKey="Dosis"         ItemStyle-CssClass="fijoBig"/>
                <asp:BoundField DataField="Abejorro"            HeaderText="Abejorro"     meta:resourceKey="Abejorro"      ItemStyle-CssClass="fijo"/>
                <%--<asp:BoundField DataField="LitrosUsar" HeaderText="Agua" />
                <asp:BoundField DataField="ApliDesde" HeaderText="Desde" />
                <asp:BoundField DataField="ApliHasta" HeaderText="Hasta" />--%>
                <asp:TemplateField HeaderText="Agua" meta:resourceKey="Agua" ItemStyle-CssClass="fijo">
                    <ItemTemplate>
                        <label class="agua"><%# Eval("LitrosUsar")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Desde"  meta:resourceKey="htDesde" ItemStyle-CssClass="fijo">
                    <ItemTemplate>
                        <label class="desde"><%# Eval("ApliDesde")%></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hasta" meta:resourceKey="htHasta" ItemStyle-CssClass="fijo">
                    <ItemTemplate>
                        <label class="hasta"><%# Eval("apliHasta") %></label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cantidad Solicitada" meta:resourceKey="htCantidadSolicitada" ItemStyle-CssClass="fijo">
                    <ItemTemplate>
                        <%-- <asp:TextBox ID="TextBox2" CssClass="cajaChica cantidadPedida" runat="server" Text='<%#  Eval("cantidadPedida") %>' ></asp:TextBox>
                        <asp:TextBox ID="TextBox1" CssClass="cajaChica visibilityHidden quimicoId" runat="server" Text='<%#  Eval("idQuim") %>' ></asp:TextBox> --%>
                        <input name="cantidadPedida" type="text" class="cajaChica cantidadPedida" value="<%#  Eval("cantidadPedida") %>" />
                        <input name="quimicoId" type="hidden" class="cajaChica quimicoId" value="<%#  Eval("idQuim") %>" />
                        <input name="litros" class="litros" type="hidden" value="<%# Eval("LitrosUsar") %>" />
                        <input name="dosisMin" class="dosisMin" type="hidden" value="<%# Eval("apliDesde") %>" />
                        <input name="dosisMax" class="dosisMax" type="hidden" value="<%# Eval("apliHasta") %>" />
                        <input name="reentrada" class="reentrada" type="hidden" value="<%# Eval("reentrada") %>" />
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:BoundField DataField="cantidadPesada" meta:resourceKey="htCantidadPesada" HeaderText="Pesada" ItemStyle-CssClass="fijo"/>

                 <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-BorderStyle="none">    
                    <ItemTemplate>
                        <img alt="Borrar" src="../comun/img/delete.png" class="visibilityHidden borrarQuimico" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </td>
        </tr>


        </table>
    </div>

    <table width="100%">
        <tr>
        <td style="text-align:center;"> 
        
        <table align="center"  style="width:870px; max-width:870px; min-width:870px; margin:0 auto;" cellspacing="0" cellpadding="0" border="0">
            <tr><td align="left"><asp:Literal runat="server" meta:resourceKey="ltNotas">Notas / Observaciones:</asp:Literal></td></tr>
            <tr><td>
                <asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" MaxLength="1000" Height="86px" Width="870px"></asp:TextBox>
            </td></tr>
            <tr><td>
               <%-- <asp:Button runat="server" meta:resourceKey="btnGuardar" class="button" OnClientClick="javascript:myValidador();" />--%>
                <input type="button"  id="Button2" class="button" runat="server" onclick="javascript:myValidador();" />
            </td></tr>
        </table>
        
        </td></tr>      
    </table>
    </div>

    <asp:Panel ID="pnlCapturaHoras" runat="server" CssClass="modalPopup" Style="" width="450px">
               
      <table class="index3" align="center" style="min-width:430px; margin:5px;">
          <tr>
              <td colspan="2" >  
                <h2><asp:Label runat="server" ID="lblInfo"  meta:resourceKey="lblInfo" Text="Confirme la acción"></asp:Label></h2>  
              </td>                         
           </tr>

          <tr>
              <td style="width:150px;">  
                <h3><asp:Label runat="server" ID="lblPlanta" meta:resourceKey="lblPlanta" Text="Planta"></asp:Label></h3>  
              </td>
              <td >
                <h4>   <asp:Label runat="server"  ID="lblPlanta2" Text=""></asp:Label></h4>
               </td>               
           </tr>

           <tr>
              <td > 
              </td>
              <td >
                 <h4><asp:Label runat="server"  ID="lblInv2" Text=""></asp:Label></h4>  
               </td>              
           </tr>

          <tr>
              <td >  
                <h3><asp:Label runat="server" ID="lbReentrada"  meta:resourceKey="lblReentrada" Text=" Horas Reentrada"></asp:Label></h3>  
              </td>
              <td>
                   <h4><asp:Label runat="server" ID="lblHorasRenntreda" Text=""></asp:Label>  Hrs.</h4>
              </td>
          </tr>
           <tr>
              <td >  
                <h3><asp:Label runat="server" ID="lbCocecha" meta:resourceKey="lblCosecha" Text=" Intervalo seg. a Cocecha"></asp:Label></h3>  
              </td>
              <td >
                <h4>    <asp:Label runat="server" ID="lblCocecha2" Text=""></asp:Label> D&iacute;as.</h4>
               </td>
          
           </tr>
           <tr>
              <td colspan="2" style="text-align:right; background:#ffa05f;" >  
                <h4><asp:Label runat="server" ID="lblPregunta" meta:resourceKey="lblPregunta" Text=" ¿Est&aacute; seguro que desea guardar?"></asp:Label></h4>  
              </td>                         
           </tr>

            <tr>
                <td colspan="2">
                    <asp:Button CssClass="button" runat="server" meta:resourceKey="btnCancel" ID="btnCancel" Text="Cancelar" />
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

           <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
</asp:Content>

