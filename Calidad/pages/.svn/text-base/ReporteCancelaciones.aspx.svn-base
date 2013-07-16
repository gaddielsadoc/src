<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReporteCancelaciones.aspx.cs" Inherits="pages_ReporteCancelaciones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
    <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />
    <%-- <script type="text/javascript">

         $(function () {
             if ($("#<%=gdvProgramaManager.ClientID%>").find("tbody").find("tr").size() > 1) {

                 $("#<%=gdvProgramaManager.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager") });
             } else {
                 $("#pager").hide();
             }
         });
    </script>--%>
    <script type="text/javascript">

        function pageLoad() {
            registerCombobox();
        }

        function registerCombobox() {
            //no se seleccionaran todos los combos, por que si no, no me sirve el de la paginacion
            // $("select").combobox(); 
            $("select[name='<%= ddlPlanta.UniqueID %>']").combobox();
            $("select[name='<%= ddlInvernadero.UniqueID %>']").combobox();          
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
                    return this.optional(element) || /\d\d\d\d-\d\d-\d\d/.test(value);
                }, '<%=(string)GetLocalResourceObject("FormatoFecha") %> yyyy-mm-dd');


            $('#' + formId).validate({
                errorLabelContainer: "div.alerta span",
                wrapper: "li",
                onfocusout: false,
                onsubmit: false,
                onkeyup: false,
                rules: {
                    '<%=txtDesde.UniqueID%>': { formatoFecha: true }
                 , '<%=txtHasta.UniqueID%>': { formatoFecha: true }
                },
                messages: {
                    '<%=txtDesde.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("FormatoFechaMin") %> yyyy-MM-dd' }
                 , '<%=txtHasta.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("FormatoFechaMin") %> yyyy-MM-dd' }
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

            $("#<%=btnBuscar.ClientID%>").click(submit_Click);

            function submit_Click() {
                return $("#" + formId).valid();
            }

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


    <%--<script type="text/javascript">
        function lnkViejoP_Click(e) {
           // alert(e.attributes.este.value.toString());
            PageMethods.lnkViejoP_Click2(e.attributes.este.value.toString(), lnkViejo_CallBack);
        }

        function lnkViejo_CallBack(e) {
            
        }

    </script>--%>




     <style type="text/css">
        
/* =========== Modal Pop-up =============*/

.modalBackground 
{
    background-color:Black;
    -filter:alpha(opacity=70);
    -opacity:0.7; 
    z-index:-1;
} 

.modalPopup
{
    border-width: 5px;
    border-style: outset;
    border-bottom-color:Black;
    font-weight: bold;
    padding: 0px;
    overflow:auto;
    z-index:99999;
    position:fixed;
    top:20%;
    left:40%;
    width:300px;
    height:auto;    
    
    background-color:#003f19;
 
    border: 2px solid #FEE435;
    -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    -border-radius: 10px;
    
    -moz-box-shadow: 0px 0px 15px 0px #010;
    -webkit-box-shadow: 0px 0px 15px 0px #010;
    -box-shadow: 0px 0px 15px 0px #010;
    
    padding:30px;
}
    </style>

<script>
$(function() {
$( "#modalPopup" ).draggable();
});
</script>

 <script type="text/javascript">
        function lnkViejoP_Click(e) {
            //PageMethods.GetComments(data, addCallBack);
           
             PageMethods.lnkViejoP_Click2(e.attributes.este.value.toString(), '<% = Session["connection"] %>')
             document.getElementById('divCallBack').style.visibility = "visible";
             document.getElementById('divCallBack').style.display = "block";
        }

        function addCallBack(result) {
            if (document.getElementById) {
                var el = document.getElementById('divCallBack');
                el.style.display = 'block';
            }
            //document.getElementById('ctl00_ContentPlaceHolder1_divCallBack').innerHTML = result;
//            document.getElementById('divCallBack').style.visibility = "visible";
//            document.getElementById('divCallBack').style.display = "block";
            //loadGraph();
        }

        function hiden() {
            document.getElementById('divCallBack').style.visibility = "hidden";
            document.getElementById('divCallBack').style.display = "none";
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="divCallBack" class="modalPopup" style="Height:auto; visibility:hidden; display:none; word-wrap:break-word;" >
    <asp:Label ID="Label1" runat="server" Text="refill"></asp:Label>
    <input id="Button1" type="button" value="" class="eraseButton"  onclick="javascript:hiden();"  />
</div>
<%--error msg--%>
    <div id="error" class="modalPopup" style="width: 500px; display: none; position: fixed;
        z-index: 99999;">
        <table style="vertical-align: middle; text-align: center; height: 100%; width: 100%;">
            <tbody>
                <tr>
                    <td style="background: #ccc repeat;">
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
            </tbody>
        </table>
    </div>
    <%--Tutilo--%>
    <div class="container">
        <h1>
        <asp:Literal ID="Literal1" runat="server" meta:resourceKey="ltReporteDeProgramas">Reporte de Programas</asp:Literal>
        </h1>
        <div>
            <table width="100%" class="index">
                <tr>
                    <td colspan="8">
                        <h2>
                            <asp:Literal ID="Literal2" runat="server" meta:resourceKey="ltBuscar">Buscar</asp:Literal>
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal3" runat="server" meta:resourceKey="ltPlanta">Planta</asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true" DataTextField="campoNombre"
                            DataValueField="campoId">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Literal ID="Literal4" runat="server" meta:resourceKey="ltInvernadero">Invernadero</asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInvernadero" runat="server" AppendDataBoundItems="true"
                            DataTextField="invernadero" DataValueField="invernadero">
                        </asp:DropDownList>
                    </td>
                   
                   <td>
                        <asp:Literal ID="Literal8" runat="server" meta:resourceKey="ltEstatus">Estatus</asp:Literal>
                    </td>
                <td>
                    <asp:DropDownList ID="ddlEstatus" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                </td>

                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal6" runat="server" meta:resourceKey="ltCreadoDesde">Creado desde</asp:Literal>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtDesde" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Literal ID="Literal7" runat="server" meta:resourceKey="ltHasta">Hasta</asp:Literal>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtHasta" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
                    </td>

                    

                    <td colspan="4">
                        <asp:Button ID="btnBuscar"  meta:resourceKey="btnBuscar" runat="server" 
                            Text="Buscar" onclick="btnBuscar_Click"  />
                    </td>

                </tr>
            </table>
        </div>

           <br />
           <br />
        
        <asp:GridView ID="GridView1" meta:resourceKey="gv" runat="server" 
            AutoPostBack="false" HorizontalAlign="Center" AutoGenerateColumns="False" 
            CssClass="gridView" HeaderStyle-CssClass="gridViewHeader" ItemStyle-CssClass="center" 
            AlternatingRowStyle-CssClass="gridViewAlt" PagerStyle-CssClass="gridViewPagerStyle"                                                     
            EmptyDataRowStyle-BackColor="#BFCBBE" EmptyDataText="NO REGISTROS" EmptyDataRowStyle-CssClass="gridEmptyData"                                                    
            OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle CssClass="gridViewAlt"></AlternatingRowStyle>
            <Columns>
                    <asp:BoundField DataField="planta" HeaderText="Planta" 
                        ItemStyle-CssClass="center" meta:resourceKey="htPlanta"  >



<ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="idInvernadero" HeaderText="Invernadero" HeaderStyle-Width="100px"
                        ItemStyle-CssClass="center" meta:resourceKey="htInvernadero">



<ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="vNombre" ReadOnly="True" Visible="True" 
                        HeaderText="Nombre" meta:resourceKey="htNombre" ItemStyle-CssClass="center" >

                        <ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>

                   <%--  <asp:TemplateField HeaderText="Nombre">
                         <EditItemTemplate>
                               
                         </EditItemTemplate>
                         <ItemTemplate>
                             <asp:LinkButton ID="lnkViejoP" runat="server" OnClientClick="lnkViejoP_Click(this ); return false;" Text='<%# Eval("vNombre").ToString() %>' este='<%# Eval("idPrograma").ToString() %>'  >LinkButton</asp:LinkButton>
                         </ItemTemplate>
                         <HeaderStyle Width="350px" />
                         <ItemStyle CssClass="center" />
                    </asp:TemplateField>
--%>
                    
                    <asp:BoundField DataField="vUserCreo" runat="server" meta:resourceKey="htvUserCreo" 
                        HeaderText="UserCreo"  HeaderStyle-Width="100px"
                        ItemStyle-CssClass="center" >


                <ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="dFechaCreacion" HeaderText="fechaCreacion" HeaderStyle-Width="100px"  meta:resourceKey="htdFechaCreacion"  />
                    
                <asp:BoundField DataField="vUserMod" runat="server" 
                        meta:resourceKey="htUserMod" HeaderText="UserMod" HeaderStyle-Width="110px"
                        ItemStyle-CssClass="center" >


                <ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="dFechaMod" meta:resourceKey="htfechaMod" HeaderText="fechaMod" HeaderStyle-Width="110px"/>
                    <asp:BoundField DataField="vRazon" runat="server" HeaderStyle-Width="100px"
                        meta:resourceKey="htRazon" HeaderText="Razon" 
                        ItemStyle-CssClass="center" >
           

                <ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>
                        <asp:BoundField DataField="VNombreNuevo" runat="server" 
                        meta:resourceKey="htNombreNuevo" HeaderText="NombreNuevo" 
                        ItemStyle-CssClass="center" >


                <ItemStyle CssClass="center"></ItemStyle>
                    </asp:BoundField>
                    
            </Columns>

<EmptyDataRowStyle BackColor="#BFCBBE" CssClass="gridEmptyData"></EmptyDataRowStyle>

            

<HeaderStyle CssClass="gridViewHeader"></HeaderStyle>

<PagerStyle CssClass="gridViewPagerStyle"></PagerStyle>
                                                </asp:GridView>

    </div>

    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
</asp:Content>

