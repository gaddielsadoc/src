<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmQuimico.aspx.cs" enableEventValidation="false" Inherits="catalog_frmQuimico" ValidateRequest="false"%>

<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/overlib_mini.js"></script>
    <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript">

        $(function () {

            $("#ctl00_ContentPlaceHolder1_ddlTipoAplicacion").attr('name', 'ddlTipoApli[]');



            plagasLoad();
            dosisLoad();
            //gvQuimico
            if ($("#<%=gvQuimico.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvQuimico.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"),output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}'  });
            } else {
                $("#pager").hide();
            }

            //funcion de cambiar el texto de maxima cantidad
            $("#<%=rbMaxNumero.ClientID%>").change(function () {
                $("#<%=ltMaxAplicacionTipo.ClientID%>").html('<%= (string)GetLocalResourceObject("Aplicaciones") %>');
            });
            $("#<%=rbMaxCantidad.ClientID%>").change(function () {
                var unidad1 = $(".txtDosisInf").first().val();
                var unidad2 = $(".txtDosisSup").first().val();
                $("#<%=ltMaxAplicacionTipo.ClientID%>").html(unidad1 + " / " + unidad2);
            });

            registerCombobox();

            //limpia el formulario antes de mandarlo para que no mande error al subir un archivo muy grande
            $("#<%=btnLimpiar.ClientID%>").click(function () {
                $('#' + formId).trigger('reset');
            });

            //*****************validacion de los campos******************************//
            //agregamos los metodos de validacion de archivos a la clase validate de jquery
            $("#<%=btnActualizar.ClientID%>").click(submitClick);
            function submitClick() {
                return $("#" + formId).valid();
            }
            var exts = ['pdf'];
            $.validator.addMethod("fileFichaTecnica",
                function (value, element) {
                    var file = $('#<%=fupFichaTecnica.ClientID%>').val();
                    if (file)
                        return (isValidExtension(file, exts, true));
                    else return true;
                }, '<%= (string)GetLocalResourceObject("fichaPDF") %>');

            $.validator.addMethod("fileHojaSeguridad",
                function (value, element) {
                    var file = $('#<%=fupHojaSeguridad.ClientID%>').val();
                    if (file)
                        return (isValidExtension(file, exts, true));
                    else return true;
                }, '<%= (string)GetLocalResourceObject("hojaPDF") %>');

            $.validator.addMethod("fileRegistroC",
                function (value, element) {
                    var file = $('#<%=fupRegistroC.ClientID%>').val();
                    if (file)
                        return (isValidExtension(file, exts, true));
                    else return true;
                }, '<%= (string)GetLocalResourceObject("registroPDF") %>');

            //valida entradas de numeros decimales desde 00.00 hasta 99.99
            $.validator.addMethod("decimal2Float",
            function (value, element) {
                return this.optional(element) || /^(\d?\d?)(\.\d{1,2})?$/.test(value);
            }, '<%= (string)GetLocalResourceObject("decimal99") %>');

            //valida entradas de numeros decimales desde 0000.0000 hasta 9999.9999
            $.validator.addMethod("decimal4Float",
            function (value, element) {
                return this.optional(element) || /^(\d?\d?\d?\d?)(\.\d{1,4})?$/.test(value);
            }, '<%= (string)GetLocalResourceObject("decimal9999") %>');

            //valida entradas de numeros decimales desde 0000.00 hasta 9999.99
            $.validator.addMethod("decimal42Float",
            function (value, element) {
                return this.optional(element) || /^(\d?\d?\d?\d?)(\.\d{1,2})?$/.test(value);
            }, '<%= (string)GetLocalResourceObject("decimal9999") %>');

//            $.validator.addMethod("existePlaga",
//                function (value, element) {
//                    var resultExistePlaga = false;
//                    //si recorre el arreglo de plagas y encuentra un ID diferente de -1 regresa verdadero
//                    $('#tblPlagas select').each(function () {
//                        if ($(this).val() != "-1") {
//                            resultExistePlaga = true;
//                            //este return false detiene el each para ahorrar procesos (no es el return de la función)
//                            return false;
//                        }
//                    });
//                    //si no encontró plagas regresa falso
//                    return resultExistePlaga;
//                }, '<%= (string)GetLocalResourceObject("unaPlaga") %>');


            //funcion que utilizan las validaciones de archivos
            function isValidExtension(file, arrayExtensions, required) {
                if (file) {
                    // split file name at dot
                    var getExt = file.split('.');
                    // reverse name to check extension
                    getExt = getExt.reverse();
                    if ($.inArray(getExt[0].toLowerCase(), arrayExtensions) > -1) {
                        return true;
                    } else {
                        return false;
                    }
                } else {
                    return !required;
                }
            }
            //se le agrega al btnActualizar que regrese validacion del formulario
            $("#<%=btnActualizar.ClientID%>").click(function () {
                return $('#' + formId).valid();
            });

            $('form').live('submit', function () {
                $("div.alerta span").append('<%= (string)GetLocalResourceObject("errorP") %>');

                $('#' + formId).validate({

            });
        });

        //validacion del formulario con la clase validate de jquery
        $('#' + formId).validate({
            errorLabelContainer: "div.alerta span",
            wrapper: "li",
            onfocusout: false,
            onsubmit: false,
            onkeyup: false,
            rules: {
                '<%=txtIngrediente.UniqueID%>': { required: true, rangelength: [0, 60] },
                '<%=txtTolerancia.UniqueID%>': { required: true, rangelength: [0, 15] },
                '<%=txtEpa.UniqueID%>': { required: true, rangelength: [0, 15] },
                '<%=txtIntervalo.UniqueID%>': { required: true, number: true, decimal2Float: true },
                '<%=txtTiempo.UniqueID%>': { required: true, number: true, decimal2Float: true },
                '<%=txtPersist.UniqueID%>': { number: true, decimal2Float: true },
//                '<%=txtMaxAplCiclo.UniqueID%>': { required: true, number: true, "existePlaga": true, decimal4Float: true },
                '<%=txtTiempoAplicacionMin.UniqueID%>': { number: true, required: true, min: 0 },
                '<%=txtTiempoAplicacionHrs.UniqueID%>': { number: true, required: true, min: 0 },

                'txtDosisSup': { required: true, number: true, decimal4Float: true },
                'txtDosisInf': { required: true, number: true, decimal4Float: true },
                'txtDosisCantidad': { required: true, number: true, decimal4Float: true },

                '<%= fupFichaTecnica.UniqueID %>': "fileFichaTecnica",
                '<%= fupHojaSeguridad.UniqueID %>': "fileHojaSeguridad",
                '<%= fupRegistroC.UniqueID %>': "fileRegistroC"
            },
            messages: {
                '<%=txtIngrediente.UniqueID%>': { required: '<%= (string)GetLocalResourceObject("iaR") %>', rangelength: '<%= (string)GetLocalResourceObject("iaE") %>' },
                '<%=txtTolerancia.UniqueID%>': { required: '<%= (string)GetLocalResourceObject("tolerancia") %>', rangelength: '<%= (string)GetLocalResourceObject("toleranciaE") %>' },
                '<%=txtEpa.UniqueID%>': { required: '<%= (string)GetLocalResourceObject("EPA") %>', rangelength: '<%= (string)GetLocalResourceObject("EPAE") %>' },
                '<%=txtIntervalo.UniqueID%>': { required: '<%= (string)GetLocalResourceObject("Intervalo") %>', number: '<%= (string)GetLocalResourceObject("IntervaloN") %>', decimal2Float: '<%= (string)GetLocalResourceObject("IntervaloD") %>' },
                '<%=txtTiempo.UniqueID%>': { required: '<%= (string)GetLocalResourceObject("reentrada") %>', number: '<%= (string)GetLocalResourceObject("reentradaN") %>', decimal2Float: '<%= (string)GetLocalResourceObject("reentradaD") %>' },
                '<%=txtPersist.UniqueID%>': { number: '<%= (string)GetLocalResourceObject("persistencia") %>', decimal2Float: '<%= (string)GetLocalResourceObject("persistenciaD") %>' },
                '<%=txtMaxAplCiclo.UniqueID%>': { required: '<%= (string)GetLocalResourceObject("maxA") %>', number: '<%= (string)GetLocalResourceObject("maxAN") %>',
//                    existePlaga: '<%= (string)GetLocalResourceObject("maxAP") %>', 
                decimal4Float: '<%= (string)GetLocalResourceObject("maxAD") %>' },
                '<%=txtTiempoAplicacionMin.UniqueID%>': { number: '<%= (string)GetLocalResourceObject("minTN") %>', required: '<%= (string)GetLocalResourceObject("minT") %>', min: '<%= (string)GetLocalResourceObject("minTE") %>' },
                '<%=txtTiempoAplicacionHrs.UniqueID%>': { number: '<%= (string)GetLocalResourceObject("horasTN") %>', required: '<%= (string)GetLocalResourceObject("horasT") %>', min: '<%= (string)GetLocalResourceObject("horasTE") %>' },

                'txtDosisSup': { required: '<%= (string)GetLocalResourceObject("DosisM") %>', number: '<%= (string)GetLocalResourceObject("DosisMN") %>', decimal4Float: '<%= (string)GetLocalResourceObject("DosisMD") %>' },
                'txtDosisInf': { required: '<%= (string)GetLocalResourceObject("DosisMin") %>', number: '<%= (string)GetLocalResourceObject("DosisMinN") %>', decimal4Float: '<%= (string)GetLocalResourceObject("DosisMinD") %>' },
                'txtDosisCantidad': { required: '<%= (string)GetLocalResourceObject("UDosis") %>', number: '<%= (string)GetLocalResourceObject("UDosisN") %>', decimal4Float: '<%= (string)GetLocalResourceObject("UDosisD") %>' },


                '<%= fupFichaTecnica.UniqueID %>': { fileFichaTecnica: '<%= (string)GetLocalResourceObject("fichaPDF") %>' },
                '<%= fupHojaSeguridad.UniqueID %>': { fileHojaSeguridad: '<%= (string)GetLocalResourceObject("hojaPDF") %>' },
                '<%= fupRegistroC.UniqueID %>': { fileRegistroC: '<%= (string)GetLocalResourceObject("registroPDF") %>' }
            },
            showErrors: function (errorMap, errorList) {
                var html = '';
                for (var i = 0; i < errorList.length; i++) {
                    html = html.replace(errorList[i].message + '<br />', '');
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
    });

        //carga las plagas del quimico si existen
        function plagasLoad() {
            var plagas = $('#<%=plagasTmp.ClientID %>').val();
            $('#<%=plagasTmp.ClientID %>').val("");
            if (plagas) {
                var itemArray = plagas.split('|');
                $.each(itemArray, function (index, value) {
                    if (value) {
                        var ddl = $("#<%=ddlPlagas.ClientID %>").clone().html();
                        if (index > 0) {
                            var html = '<tr><td>  ';
                            html += '<select name="<%=ddlPlagas.UniqueID %>">' + ddl + '</select>';
                            html += '</td><td><img alt="Quitar" src="../images/remove-icon.png" class="btnRemoverPlaga" /></td></tr>';
                            $('#tblPlagas').append(html);
                        }
                        $('#tblPlagas select:last').val(value);
                    }
                });
                registerCombobox();
            }
        }
        //carga las dosis del quimico a partir de dosisTmp
        function dosisLoad() {
            var dosis = $('#<%=dosisTmp.ClientID %>').val();
            $('#<%=dosisTmp.ClientID %>').val("");
            if(dosis){
                var itemArray = dosis.split('@');
                //separamos por las @ que son las dosis
                var first=true;
                $.each(itemArray, function (index, value){
                    if(!first){ addDosis(); }
                    else { $("#addDosisBtn").show(); }
                    first=false;

                    var datosArray = value.split('|');
                    $(".ddlTipoAplicacion:last").val(datosArray[0]);
                    $(".txtDosisInf:last").val(datosArray[1]);
                    $(".txtDosisSup:last").val(datosArray[2]);
                    $(".ddlDosisUni1:last").val(datosArray[3]);
                    $(".ddlDosisUni2:last").val(datosArray[4]);
                    $(".txtDosisCantidad:last").val(datosArray[5]);
                });
            }
        }

        //boton agregar plaga
        function addPlaga() {
            var ddl = $("#<%= ddlPlagas.ClientID %>").clone().html();
            var html = '<tr><td>  ';
            /*Aqui iria codigo de select*/
            html += '<select name="<%= ddlPlagas.UniqueID %>">' + ddl + '</select>';
            html += '</td><td><img alt="Quitar" src="../images/remove-icon.png" class="btnRemoverPlaga"/></td></tr>';
            $('#tblPlagas').append(html);
            registerCombobox();
        }
        //boton remover plaga
        $('.btnRemoverPlaga').live('click', function () {
            $(this).parent().parent().remove();
        });
        function registerCombobox() {
            $("select[name='<%= ddlPlagas.UniqueID %>']").combobox();
        }
        function closeErrorMsg() {
            $("div.alerta span").html('');
            $("#error").hide();
        }

        var i = 0;
        //boton agregar dosis
        function addDosis() {
            i++;
            var ddlTipo = $("#<%= ddlTipoAplicacion.ClientID %>").parent().clone().html();

            //quitamos la opcion de unica
            ddlTipo = ddlTipo.replace("<option value=\"-1\">" + '<%= (string)GetLocalResourceObject("Unica") %>' +" </option>", "");
            ddlTipo = ddlTipo.replace("<OPTION selected value=-1>" + '<%= (string)GetLocalResourceObject("Unica") %>' + " </OPTION>", "");

            ddlTipo = ddlTipo.replace("<tr id=\"dosisTitle\"><td><h2>" + '<%= (string)GetLocalResourceObject("dosis1") %>' + " </h2></td></tr>", "");
            ddlTipo = ddlTipo.replace("<TR id=dosisTitle><TD><H2>" + '<%= (string)GetLocalResourceObject("dosis1") %>' + " </H2></TD></TR><TR>", "");

            //t = ddlTipo.replace("name=\"" + $("#ctl00_ContentPlaceHolder1_ddlTipoAplicacion").attr('name') + "\"", "name=\"ddlTipoApli[]\"");
            //ddlTipo = ddlTipo.replace("src=\"../images/add-icon.png\" onclick=\"addDosis();\"", "src=\"../images/remove-icon.png\" class='removeDosis'");
            //console.log(ddlTipo);
            //$('#tableDosis').append("<tr>"+ddlTipo+"</tr>")
            
            $('#tableDosis').append("<tr><td class='margin_top'> " + ddlTipo +
                        "</td>"+
                        "<td>"+
                            "<img alt='Quitar' src='../images/remove-icon.png' class='removeDosis'"+
                        "</td>"+
                        "<td style='width:200px;'>" + '<%= (string)GetLocalResourceObject("min") %>' + "</td><td><input name='txtDosisInf" + i + "' type='text' class='cajaCh txtDosisInf'></td>" +
                        "<td>Máxima</td><td><input name='txtDosisSup" + i + "' type='text' class='cajaCh txtDosisSup'></td>" +
                        "<td style='white-space:nowrap;'>"+
                            "<select name='ddlDosisUni1" + i + "' class='cajaCh ddlDosisUni1' style='min-width:80px; float:none;'>" +
		                        "<option value='L'>" + '<%= (string)GetLocalResourceObject("Litros") %>' + "</option>" +
		                        "<option value='gr'>" + '<%= (string)GetLocalResourceObject("Gramos") %>' + "</option>" +
		                        "<option value='ml'>" + '<%= (string)GetLocalResourceObject("Mililitros") %>' + "</option>" +
		                        "<option value='kg'>" + '<%= (string)GetLocalResourceObject("Kilogramos") %>' + "</option>" +
		                        "<option value='Esponja'>" + '<%= (string)GetLocalResourceObject("Esponjas") %>' + "</option>" +
		                        "<option value='cc'>" + '<%= (string)GetLocalResourceObject("cc") %>' + "</option>" +
	                        "</select>&nbsp;/&nbsp;</td>" +
                        "<td>"+
                            "<input name='txtDosisCantidad" + i + "' type='text' value='1' class='cajaCh txtDosisCantidad'>" +
                        "</td>"+
                        "<td>"+
                            "<select name='ddlDosisUni2" + i + "' class='cajaCh ddlDosisUni2' style='min-width:80px;'>" +
		                        "<option value='Ha'>" + '<%= (string)GetLocalResourceObject("Hectareas") %>' + "</option>" +
		                        "<option value='agua'>" + '<%= (string)GetLocalResourceObject("litrosAg") %>' + "</option>" +
		                        "<option value='m2'>" + '<%= (string)GetLocalResourceObject("MetrosQ") %>' + "</option>" +
	                        "</select>" +
                        "</td></tr>");

            $("input[type=text]").live("keyup", function (e) {
                //if enter key is pressed
                if (e.keyCode == 13) {
                    return false;
                }
            }); 

            //AGREGAR VALIDACIONES A LOS NUEVOS CAMPOS
         
//             
//             $(".txtDosisSup:last").rules("add", {
//                required: true, number: true, decimal4Float: true,
//                messages: { required: "Todas las dosis máximas son requeridas", number: 'La Dosis máxima debe ser un número', decimal4Float: 'La Dosis máxima debe ser decimal de 0000.0000 hasta 9999.9999' }
//            });
//             $(".txtDosisInf:last").rules("add", {
//                required: true, number: true, decimal4Float: true,
//                messages: { required: "Todas las dosis máximas son requeridas", number: 'La Dosis máxima debe ser un número', decimal4Float: 'La Dosis máxima debe ser decimal de 0000.0000 hasta 9999.9999' }
//            });
//             $(".txtDosisCantidad:last").rules("add", {
//                required: true, number: true, decimal4Float: true,
//                messages: { required: "Todas las dosis máximas son requeridas", number: 'La Dosis máxima debe ser un número', decimal4Float: 'La Dosis máxima debe ser decimal de 0000.0000 hasta 9999.9999' }
//            });
            /*
            $(".txtDosisSup").filtetr().last(function (index, value) {
                $(value).rules("add", {
                    required: true, number: true, decimal4Float: true,
                    messages: { required: "Todas las dosis máximas son requeridas", number: 'La Dosis máxima debe ser un número', decimal4Float: 'La Dosis máxima debe ser decimal de 0000.0000 hasta 9999.9999' }
                });
            });
            $(".txtDosisInf").last(function (index, value) {
                $(value).rules("add", {
                    required: true, number: true, decimal4Float: true,
                    messages: { required: "Todas las dosis mínimas son requeridas", number: 'La Dosis mínima debe ser un número', decimal4Float: 'La Dosis mínima debe ser decimal de 0000.0000 hasta 9999.9999' }
                });
            });
            $(".txtDosisCantidad").last(function (index, value) {
                $(value).rules("add", {
                    required: true, number: true, decimal4Float: true,
                    messages: { required: "Todas las dosis mínimas son requeridas", number: 'La Dosis mínima debe ser un número', decimal4Float: 'La Dosis mínima debe ser decimal de 0000.0000 hasta 9999.9999' }
                });
            });
             */
            
        }
        
        //boton remover dosis
        $('.removeDosis').live('click', function () {
            $(this).parent().parent().remove();
        });

        //muestra y oculta el boton de agregar dosis 
        $("#<%= ddlTipoAplicacion.ClientID %>").live('change', function () {
            if ($(this).val() != -1) {
                $("#addDosisBtn").show();
            }
            else {
                $("#addDosisBtn").hide();
                $("#<%= ddlTipoAplicacion.ClientID %>").parent().parent().siblings().not("#dosisTitle").remove();
            }

        });

        

       function myValidador()
        {
            var i_=0;
            var error = false;

            var bol=$('#' + formId).validate().element('#<%=txtIngrediente.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtTolerancia.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtEpa.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtIntervalo.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtTiempo.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtPersist.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtMaxAplCiclo.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtTiempoAplicacionMin.ClientID%>')&&
                $('#' + formId).validate().element('#<%=txtTiempoAplicacionHrs.ClientID%>')&&
                $('#' + formId).validate().element('#<%=fupFichaTecnica.ClientID %>')&&
                $('#' + formId).validate().element('#<%=fupHojaSeguridad.ClientID %>')&&
                $('#' + formId).validate().element('#<%=fupRegistroC.ClientID %>');

            if(!bol)
                $("#<%=btnActualizar.ClientID%>").click();

            //*******VALIDACION MANUAL PARA LOS TIPOS DE APLICACION DE LAS DOSIS DIFERENTES**************
            var size = $(".ddlTipoAplicacion").size();
            for (i_ = 0; i_ < size; i_++) {
                var value = $(".ddlTipoAplicacion")[i_].value;

                if (error == false) {
                    $(".ddlTipoAplicacion").each(function (i, e) {
                        if ($(this).val() == value&& i_ != i) {
                            $("div.alerta span").html($("div.alerta span").html() + '<%= (string)GetLocalResourceObject("aplicacionD") %>');
                            $("#error").show();
                            error = true;
                            return false;
                        }
                    });
                }

            }

            //*******VALIDACION MANUAL PARA LAS DOSIS MINIMAS**************
            var size=$(".txtDosisInf").size();
            for (i_=0;i_<size;i_++)
            {
                var regex = /^(\d?\d?\d?\d?)(\.\d{1,4})?$/;
                var text = $(".txtDosisInf")[i_].value;
                if(regex.exec(text) === null){
                    $("div.alerta span").html($("div.alerta span").html() + '<%= (string)GetLocalResourceObject("DosisMinDecimal") %>');
                   $("#error").show();
                   error = true;
               }
               else if(text == ""){
                   $("div.alerta span").html($("div.alerta span").html() + '<%= (string)GetLocalResourceObject("DosisMinR") %>' );
                   $("#error").show();
                   error = true;
               }
            }

            
            //*******VALIDACION MANUAL PARA LAS DOSIS MAXIMAS**************
            size=$(".txtDosisSup").size();
            for (i_=0;i_<size;i_++)
            {
                var regex = /^(\d?\d?\d?\d?)(\.\d{1,4})?$/;
                var text = $(".txtDosisSup")[i_].value;
                if(regex.exec(text) === null){
                    $("div.alerta span").html($("div.alerta span").html() + '<%= (string)GetLocalResourceObject("DosisMaxDecimal") %>');
                   $("#error").show();
                   error = true;
               }
               else if(text == ""){
                   $("div.alerta span").html($("div.alerta span").html() + '<%= (string)GetLocalResourceObject("DosisMaxR") %>');
                   $("#error").show();
                   error = true;
               }
            }
            
            //*******VALIDACION MANUAL PARA LAS DOSIS CANTIDAD**************
            size=$(".txtDosisCantidad").size();
            for (i_=0;i_<size;i_++)
            {
                var regex = /^(\d?\d?\d?\d?)(\.\d{1,4})?$/;
                var text = $(".txtDosisCantidad")[i_].value;
                if(regex.exec(text) === null){
                    $("div.alerta span").html($("div.alerta span").html() + '<%= (string)GetLocalResourceObject("cantidadDosis") %>');
                   $("#error").show();
                   error = true;
               }
               else if(text == ""){
                   $("div.alerta span").html($("div.alerta span").html()+'<%= (string)GetLocalResourceObject("cantidadDosisR") %>');
                   $("#error").show();
                   error = true;
               }
            }

           if (bol && !error) {

               //__doPostBack("#<%=btnActualizar.ClientID%>", "OnClick");
                //__doPostback(,'');
                $("#<%=btnActualizar.ClientID%>").click();
            }


        }

    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <asp:HiddenField ID="plagasTmp" runat="server" />
        <asp:HiddenField ID="dosisTmp" runat="server" />
                
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

        <h1 style="display:table; width:1325px; margin-left:auto; margin-right:auto;"><asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
        <asp:Panel ID="form" runat="server">
        <table  class="index">
            <tr>
                <td colspan="6">
                   <h2>
                   <asp:Literal ID="ltSubtituli" runat="server" Text="Seleccione y edite los extras del químico" meta:resourceKey="ltSubtituli"></asp:Literal>
                   </h2>
                </td>
            </tr>
            <tr>
                <td style="white-space:nowrap;">
                <asp:Literal ID="ltNombreComercial" runat="server"  meta:resourceKey="ltNombreComercial"></asp:Literal>
                    
                </td>
                <td colspan="5" style="text-align:left">
                    <asp:Literal ID="lbquimico" runat="server" Text="" ></asp:Literal>
                </td>
            </tr>
            
            <tr>
                <td><asp:Literal ID="Literal1" runat="server" Text="Ingrediente activo" meta:resourceKey="Literal1"></asp:Literal></td>
                <td style="width:150px; min-width:150px; max-width:150px;"><asp:TextBox ID="txtIngrediente" runat="server" CssClass="cajaMed" ></asp:TextBox></td>
                <td><asp:Literal ID="Literal5" runat="server" Text="EPA Ref.#180" meta:resourceKey="Literal5"></asp:Literal></td>
                <td><asp:TextBox ID="txtEpa" runat="server"></asp:TextBox></td>
                <td><asp:Literal ID="Literal3" runat="server" Text="Tolerancia EPA. ppm" meta:resourceKey="Literal3"></asp:Literal></td>
                <td><asp:TextBox ID="txtTolerancia" runat="server" CssClass="cajaMed"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td><asp:Literal ID="Literal2" runat="server" Text="Intervalo de seguridad a cosecha" meta:resourceKey="Literal2"></asp:Literal></td>
                <td style="text-align:left; min-width:150px; max-width:150px; width:150px;"><asp:TextBox ID="txtIntervalo" runat="server" CssClass="cajaCh" ></asp:TextBox> <%= (string)GetLocalResourceObject("dias") %></td>
                <td><asp:Literal ID="Literal4" runat="server" Text="Tiempo de reentrada" meta:resourceKey="Literal4"></asp:Literal></td>
                <td style="text-align:left;"><asp:TextBox ID="txtTiempo" runat="server" CssClass="cajaCh"></asp:TextBox> <%= (string)GetLocalResourceObject("horas") %></td>
                <td><asp:Literal ID="Literal6" runat="server" Text="Indicaciones" meta:resourceKey="Literal6"></asp:Literal></td>
                <td><asp:TextBox ID="txtIndicaciones" runat="server"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td><asp:Literal ID="Literal10" runat="server" Text="Grupo Qu&iacute;mico" meta:resourceKey="Literal10"></asp:Literal></td>
                <td  style="width:150px; min-width:150px; max-width:150px;"><asp:TextBox ID="txtGrupo" runat="server" CssClass="cajaMed"></asp:TextBox></td>
                <td style="white-space:nowrap;">
                <asp:Literal ID="Literal11" runat="server" Text="Compatibilidad con abejorros" meta:resourceKey="Literal11"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAbgrr" runat="server" CssClass="cajaMed">
                        <asp:ListItem Value="Cubrir"  meta:resourceKey="Cubrir"></asp:ListItem>
                        <asp:ListItem Value="Desconocido" meta:resourceKey="Desconocido"> </asp:ListItem>
                        <asp:ListItem Value="Incompatible" meta:resourceKey="Incompatible"></asp:ListItem>
                        <asp:ListItem Value="Remover" meta:resourceKey="Remover"></asp:ListItem>
                        <asp:ListItem Value="Sin Proteccion" meta:resourceKey="SinProteccion"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="white-space:nowrap;">
                <asp:Literal ID="Literal12" runat="server" Text="Persistencia en d&iacute;as para abejorros" meta:resourceKey="Literal12"></asp:Literal>
                </td>
                <td><asp:TextBox ID="txtPersist" runat="server" CssClass="cajaCh"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td><asp:Literal ID="Literal8" runat="server" Text="Tipo de Qu&iacute;mico" meta:resourceKey="Literal8"></asp:Literal></td>
                <td  style="width:150px; min-width:150px; max-width:150px;">
                    <asp:DropDownList ID="ddlTipoQuimico" runat="server">
                        <asp:ListItem Value="Insecticida" meta:resourceKey="Insecticida" ></asp:ListItem>
                        <asp:ListItem Value="Fungicida" meta:resourceKey="Fungicida" ></asp:ListItem>
                        <asp:ListItem Value="Coadyuvante" meta:resourceKey="Coadyuvante" ></asp:ListItem>
                        <asp:ListItem Value="Insecticida Orgánico" meta:resourceKey="InsecticidaOrganico" ></asp:ListItem>
                        <asp:ListItem Value="Fungicida Orgánico" meta:resourceKey="FungicidaOrganico" ></asp:ListItem>
                    </asp:DropDownList>
                </td>
                
                <td><asp:Literal ID="Literal14" runat="server" Text="Etiqueta M&aacute;xima aplicaci&oacute;n" meta:resourceKey="Literal14"></asp:Literal></td>
                <td><asp:TextBox ID="txtMaxAplicEtiqu" runat="server" CssClass="cajaLarga" Width="300"></asp:TextBox></td>
                
                <td><asp:Literal ID="Literal9" runat="server" Text="Tolerancia entre Aplicaciones (hrs)" meta:resourceKey="Literal9"></asp:Literal></td>
                <td class="none">
                    <asp:TextBox ID="txtTiempoAplicacionHrs" runat="server" CssClass="cajaCh none"></asp:TextBox> : <asp:TextBox ID="txtTiempoAplicacionMin" runat="server" CssClass="cajaCh"></asp:TextBox>
                </td>
            </tr>

            <!-- DOSIS, UNIDADES DE MEDIDA Y APLICACIÓN MÁXIMA -->
            <tr>  
                <td colspan="3">  
                <table id="tableDosis">
                    <tr id="dosisTitle"><td><h2>
                    <asp:Literal ID="Literal21" runat="server" meta:resourceKey="Literal21"></asp:Literal>
                    
                    </h2></td>
                    <td class='margin_top' colspan="3"> 
                            <asp:DropDownList ID="ddlTipoAplicacion" runat="server" AppendDataBoundItems="true" CssClass="ddlTipoAplicacion"></asp:DropDownList>
                        </td>
                        <td>
                           <img id="addDosisBtn" alt="Agregar" src="../images/add-icon.png" onclick="addDosis();" style="display:none" />
                        </td>
                    </tr>
                    <%--
                    <tr>
                        <td class="margin_top">
                            <asp:DropDownList ID="ddlTipoAplicacion" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                        </td>
                        <td>
                            <img id="addDosisBtn" alt="Agregar" src="../images/add-icon.png" onclick="addDosis();" style="display:none" />
                        </td>   
                            
                        <td style="width:200px;">M&iacute;nima</td><td><asp:TextBox ID="txtDosisInf" runat="server" CssClass="cajaCh txtDosisInf"></asp:TextBox></td>
                        <td>M&aacute;xima</td><td><asp:TextBox ID="txtDosisSup" runat="server" CssClass="cajaCh txtDosisSup"></asp:TextBox></td>
                        <td style="white-space:nowrap;">
                            <asp:DropDownList ID="ddlDosisUni1" runat="server" CssClass="cajaCh" style="min-width:80px;">
                                <asp:ListItem value="L">Litros</asp:ListItem>
                                <asp:ListItem value="gr">Gramos</asp:ListItem>
                                <asp:ListItem value="ml">Mililitros</asp:ListItem>
                                <asp:ListItem value="kg">Kilogramos</asp:ListItem>
                                <asp:ListItem value="Esponja">Esponjas</asp:ListItem>
                                <asp:ListItem value="cc">cc</asp:ListItem>
                            </asp:DropDownList>&nbsp;/&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtDosisCantidad" runat="server" Text="1" CssClass="cajaCh txtDosisCantidad"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDosisUni2" runat="server" CssClass="cajaCh" style="min-width:80px;">
                                <asp:ListItem value="Ha">Hect&aacute;rea</asp:ListItem>
                                <asp:ListItem value="agua">Litros de agua</asp:ListItem>
                                <asp:ListItem value="m2">Metros cuadrados</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    --%>

                    <tr>
                        <td>

                        <asp:Literal ID="Literal13" runat="server" meta:resourceKey="Literal13"></asp:Literal>
                        </td><td><input name='txtDosisInf0' type='text' class='cajaCh txtDosisInf' /></td>
                        <td>
                        <asp:Literal ID="Literal15" runat="server" meta:resourceKey="Literal15"></asp:Literal>
                        
                        </td><td><input name='txtDosisSup0' type='text' class='cajaCh txtDosisSup' /></td>
                        <td style='white-space:nowrap;'>
                            <select name='ddlDosisUni10' class='cajaCh ddlDosisUni1' style='min-width:80px; float:none;'>

                              <option value='L'>  <%= (string)GetLocalResourceObject("Litros") %>  </option> 
		                        <option value='gr'>  <%= (string)GetLocalResourceObject("Gramos") %>  </option>
		                        <option value='ml'>  <%= (string)GetLocalResourceObject("Mililitros") %>  </option>
		                        <option value='kg'>  <%= (string)GetLocalResourceObject("Kilogramos") %>  </option>
		                        <option value='Esponja'>  <%= (string)GetLocalResourceObject("Esponjas") %> </option>
		                        <option value='cc'>  <%= (string)GetLocalResourceObject("cc") %>  </option>
<%--
		                        <option value='L'>Litros</option>
		                        <option value='gr'>Gramos</option>
		                        <option value='ml'>Mililitros</option>
		                        <option value='kg'>Kilogramos</option>
		                        <option value='Esponja'>Esponjas</option>
		                        <option value='cc'>cc</option>--%>
	                        </select>&nbsp;/&nbsp;</td>
                        <td>
                        
                            <input name='txtDosisCantidad0' type='text' value='1' class='cajaCh txtDosisCantidad' />
                        </td>
                        <td>
                            <select name='ddlDosisUni20' class='cajaCh ddlDosisUni2' style='min-width:80px;'>
                                <option value='Ha'> <%= (string)GetLocalResourceObject("Hectareas") %></option> 
		                        <option value='agua'> <%= (string)GetLocalResourceObject("litrosAg") %>  </option>
		                        <option value='m2'> <%= (string)GetLocalResourceObject("MetrosQ") %>  </option>
		                        <%--<option value='Ha'>Hectárea</option>
		                        <option value='agua'>Litros de agua</option>
		                        <option value='m2'>Metros cuadrados</option>--%>
	                        </select>
                        </td></tr>
                </table>
                </td>

                <td colspan="2" style="text-align:center;"> 
                <table cellpadding="0" cellspacing="0" >
                    <tr>  
                        <td colspan="3"><h2>
                        <asp:Literal ID="Literal19" runat="server" Text="M&aacute;xima aplicación por ciclo" meta:resourceKey="Literal19"></asp:Literal>
                        </h2></td>
                    </tr>
                    <tr>
                        <td class="checkboxes">
                            <asp:RadioButton ID="rbMaxCantidad" runat="server" GroupName="maxAppli" Text="Cantidad de producto" meta:resourceKey="rbMaxCantidad"/>
                        </td>
                          <td rowspan="2">
                            <asp:TextBox ID="txtMaxAplCiclo" runat="server" CssClass="cajaCh"></asp:TextBox>
                        </td>
                        <td rowspan="2" style="width:300px; text-align:left;">
                            <asp:Label ID="ltMaxAplicacionTipo" runat="server" Text="Aplicaciones " meta:resourceKey="ltMaxAplicacionTipo"></asp:Label>
                            <asp:Literal ID="Literal7" runat="server" Text=" Por Ciclo de cultivo" meta:resourceKey="Literal7"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="checkboxes">
                            <asp:RadioButton ID="rbMaxNumero" meta:resourceKey="rbMaxNumero" runat="server" GroupName="maxAppli" Text="N&uacute;mero de aplicaciones" Checked="true" />
                        </td>
                    </tr>
                </table>
                </td>
                <td>
                    <table>
                    <tr>
                        <td>
                            <asp:Literal ID="ltComodin" runat="server" Text="Comodín" meta:resourceKey="ltComodin"></asp:Literal>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkComodin" runat="server" />
                        </td>
                    </tr>
                    </table>                     
                </td>    
            </tr>

            <!-- SUBIR PDF'S -->
            <tr>
                <td>
                <asp:Literal ID="Literal16" runat="server" meta:resourceKey="Literal16"></asp:Literal>
                </td>
                <td>
                    <asp:HyperLink NavigateUrl="" ID="ltFichaTecnica" runat="server" />
                    <asp:FileUpload ID="fupFichaTecnica" runat="server" />
                    <asp:HiddenField ID="hiddenFichaTecnica" runat="server" />
                </td>
                <td>
                <asp:Literal ID="Literal17" runat="server" meta:resourceKey="Literal17"></asp:Literal>
              
                </td>
                <td style="text-align:left;">
                    <asp:HyperLink NavigateUrl="" ID="ltHojaSeguridad" runat="server" />
                    <asp:FileUpload ID="fupHojaSeguridad" runat="server" />
                    <asp:HiddenField ID="hiddenHojaSeguridad" runat="server" />
                </td>
                <td>
                                <asp:Literal ID="Literal18" runat="server" meta:resourceKey="Literal18"></asp:Literal>
             
                </td>
                <td>
                    <asp:HyperLink NavigateUrl="" ID="ltRegistroC" runat="server" />
                    <asp:FileUpload ID="fupRegistroC" runat="server" />
                    <asp:HiddenField ID="hiddenRegistroC" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" style="vertical-align:center;">
                   <asp:Literal ID="Literal20" runat="server" meta:resourceKey="Literal20"></asp:Literal>
                   
                </td>
                <td colspan="5" align="center">
                    <table id="tblPlagas">
                        <tr >
                            <td class="margin_top">
                                <asp:DropDownList ID="ddlPlagas" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                            </td>
                            <td>
                                <img id="imgAdd" alt="Agregar" src="../images/add-icon.png" onclick="addPlaga();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                <td colspan="6" align="right">
                    <asp:Button ID="btnActualizar" runat="server" Text="Guardar" onclick="Actualizar" CssClass="visibilityHidden"  meta:resourceKey="btnActualizar" />
                    <%--<input id="Button1" type="button" value='<%= (string)GetLocalResourceObject("Button1") %>' runat="server" onclick="javascript:myValidador();" visible="false" /> --%>           
                    <input id="Button1" type="button"  runat="server" onclick="javascript:myValidador();" visible="false" />        
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" onclick="Cancelar_Limpiar"  meta:resourceKey="btnLimpiar"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" onclick="Cancelar_Limpiar" Visible="False"  meta:resourceKey="btnCancel"/>
                    
                </td>
            </tr>
            
        </table>
            
            </asp:Panel>
            
            <div class="grid" >
                <div id="pager" class="pager" style="width:1440px; min-width:1440px; max-width:1440px;">
                <img alt="first" src="../comun/img/first.png" class="first" />
                <img alt="prev" src="../comun/img/prev.png" class="prev" />
                <input type="text" class="pagedisplay" />
                <img alt="next" src="../comun/img/next.png" class="next" />
                <img alt="last" src="../comun/img/last.png" class="last" />
                <select class="pagesize cajaCh" style="width: 50px; min-width: 50px; max-width: 50px;">
                    <option value="10">10</option>
                    <option value="20">20</option>
                    <option value="30">30</option>
                    <option value="40">40</option>
                    <option value="50">50</option>
                </select>
                </div>
                 <asp:GridView ID="gvQuimico" runat="server" EnableModelValidation="True" HeaderStyle-CssClass="wrapped" 
                        AutoGenerateColumns="False"  CssClass="gridView" DataKeyNames="ITEMNMBR" 
                        EmptyDataText="No existen registros" 
                        onpageindexchanging="gvQuimico_PageIndexChanging" meta:resourceKey="gvQuimico" 
                        onprerender="gvQuimico_PreRender" onrowdatabound="gvQuimico_RowDataBound" onselectedindexchanged="gvQuimico_SelectedIndexChanged" 
                        >
                        <Columns>
                            <asp:BoundField	DataField="ITMSHNAM"	HeaderText="Nombre Comercial" meta:resourceKey="ITMSHNAM" SortExpression="ITMSHNAM" />			
                            <asp:BoundField	DataField="d6_INGACT"	HeaderText="Ingrediente Activo" meta:resourceKey="d6_INGACT" SortExpression="d6_INGACT" />			
                            <asp:BoundField	DataField="d6_EPA180"	HeaderText="EPA Ref.#180" meta:resourceKey="d6_EPA180" SortExpression="d6_EPA180" />			
                            <asp:BoundField	DataField="d6_EPATOL"	HeaderText="Tolerancia EPA. ppm" meta:resourceKey="d6_EPATOL" SortExpression="d6_EPATOL" />		
                            <asp:BoundField	DataField="d6_REENT"	HeaderText="Tiempo de reentrada" meta:resourceKey="d6_REENT" SortExpression="d6_REENT" />	
                            <%-- INDICACIONES --%>
                            <asp:TemplateField HeaderText="Indicaciones"  HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" meta:resourceKey="Indicaciones">
                                <ItemTemplate>
                                    <%--
                                    <asp:Label ID="texto" runat="server" Text='<%# Eval("d6_INDIC")%>' />
                                    <asp:Image ID="verMas" runat="server" Text='Ver mas' ImageUrl="~/comun/img/lupa.png" Width="15px" />
                                    --%>
                                    <asp:HyperLink ID="texto" runat="server" NavigateUrl='#' Text='<%# Eval("d6_INDIC")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <%--<asp:BoundField	DataField="d6_INDIC"	HeaderText="Indicaciones" ItemStyle-CssClass="wrapped" />--%>		
                            <%-- INDICACIONES --%>	
                            <asp:BoundField	DataField="d6_GPOQUIM"	HeaderText="Grupo Químico" meta:resourceKey="d6_GPOQUIM" SortExpression="d6_GPOQUIM"  />
                            <%--ABEJORROS --%>
                            <asp:TemplateField HeaderText="Comp. Abejorros"  HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" meta:resourceKey="CompA" SortExpression="d6_Abgrr">
                                <ItemTemplate>
                                    <asp:Label ID="Label0" runat="server" Text='<%# Eval("d6_Abgrr")%>'  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <%--ABEJORROS --%>
                            <asp:BoundField	DataField="d6_PERSIS"	HeaderText="Persistencia Días" meta:resourceKey="d6_PERSIS" SortExpression="d6_PERSIS" />			
                            <%--DOSIS --%>
                           <%-- <asp:TemplateField HeaderText="Dosis"  HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="Label0" runat="server" Text='<%# Eval("d6_DOSISINF")%>'  />
                                    a
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("d6_DOSISSUP")%>'  />
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("d6_DOSISUNI1")%>'  />
                                    /
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("d6_DOSISUNI2")%>'  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>
                             <asp:BoundField	DataField="dosis"	HeaderText="Dosis" meta:resourceKey="dosis" />	
                            <%--TERMINA DOSIS --%>
                            <%--<asp:BoundField	DataField="d6_DOSISUNI1"  HeaderText="Unidades de medida" />--%>
                            <asp:BoundField	DataField="d6_MAXAPLETIQ" HeaderText="Etiqueta de máxima aplicación" ItemStyle-CssClass="wrapped" meta:resourceKey="d6_MAXAPLETIQ"   />	 				
                            <asp:BoundField	DataField="plagas"          HeaderText="Plagas que controla" ItemStyle-CssClass="wrapped" meta:resourceKey="plagas"   />
                            <asp:BoundField	DataField="d6_TYPEQUIM" HeaderText="Tipo de químico" meta:resourceKey="d6_TYPEQUIM"  SortExpression="d6_TYPEQUIM" />	 				
                            
                            <%--Tolerancia entre Aplicaciones --%>
                            <asp:TemplateField HeaderText="Tolerancia entre Aplicaciones"  HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" meta:resourceKey="ToleranciaApli">
                                <ItemTemplate>
                                    <asp:Label ID="Label110" runat="server" Text='<%# Eval("d6_TIEMPOAPLIHRS")%>'  />
                                    :
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("d6_TIEMPOAPLIMIN")%>'  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <%--TERMINA Tolerancia entre Aplicaciones --%>
                        </Columns>
                    </asp:GridView>
            </div>
            <div id="pnlGraph" class="modalPopup" style="Height:auto;visibility:hidden;display:none; word-wrap: break-word;" >
            
                 <input id="Button2" type="button" value="" class="eraseButton"  onclick="javascript:hiden();"  />
                 <div id="divGraph">
                 </div>
            
            </div>
            
            
            <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
            
            </div>
</asp:Content>
