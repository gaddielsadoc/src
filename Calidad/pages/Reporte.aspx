<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Reporte.aspx.cs" Inherits="pages_Reporte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            //no se seleccionaran todos los combos, por que si no, no me sirve el de la pagimacion
            // $("select").combobox(); 
            $("select[name='<%= ddlPlanta.UniqueID %>']").combobox();
            $("select[name='<%= ddlInvernadero.UniqueID %>']").combobox();
            $("select[name='<%= ddlGrower.UniqueID %>']").combobox();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        <asp:Literal runat="server" meta:resourceKey="ltReporteDeProgramas">Reporte de Programas</asp:Literal>
        </h1>
        <div>
            <table width="100%" class="index">
                <tr>
                    <td colspan="8">
                        <h2>
                            <asp:Literal ID="Literal1" runat="server" meta:resourceKey="ltBuscar">Buscar</asp:Literal>
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal2" runat="server" meta:resourceKey="ltPlanta">Planta</asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true" DataTextField="campoNombre"
                            DataValueField="campoId">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Literal ID="Literal3" runat="server" meta:resourceKey="ltInvernadero">Invernadero</asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlInvernadero" runat="server" AppendDataBoundItems="true"
                            DataTextField="invernadero" DataValueField="invernadero">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Literal ID="Literal4" runat="server" meta:resourceKey="ltProductor">Grower</asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGrower" runat="server" AppendDataBoundItems="true" DataTextField="vUserCreo"
                            DataValueField="vUserCreo">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal5" runat="server" meta:resourceKey="ltCreadoDesde">Creado desde</asp:Literal>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtDesde" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Literal ID="Literal6" runat="server" meta:resourceKey="ltHasta">Hasta</asp:Literal>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtHasta" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
                    </td>

                    <td>
                        <asp:Literal ID="Literal7" runat="server" meta:resourceKey="ltEstatus">Estatus</asp:Literal>
                    </td>
                <td>
                    <asp:DropDownList ID="ddlEstatus" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                </td>

                    <td colspan="4">
                        <asp:Button ID="btnBuscar"  meta:resourceKey="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </td>

                </tr>
            </table>
        </div>
        <br />
        <table class="index">
            <tr>
                <td>
                    <asp:Button ID="btnExcel1"  meta:resourceKey="btnXport" runat="server" Text="Exportar" OnClick="btnExcel_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater runat="server" ID="rpt" OnItemDataBound="rpt_RowDataBound" EnableViewState="false">
                        <HeaderTemplate>
                            <table class="repeater">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <table style="width: 800px;">
                                        <tr>
                                            <td style="padding:0;">
                                                <h2>
                                                    <%# DataBinder.Eval(Container.DataItem, "idPlanta")%> 
                                                </h2>                                                
                                            </td> 
                                             <td style="padding:0;">
                                                 <h3 style="text-align: left; margin:0;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../comun/img/abejorro.png" Visible='<%# DataBinder.Eval(Container.DataItem, "abejorro")=="1"? true: false%>' />
                                                    &nbsp;<asp:Literal ID="ltInvernadero" runat="server" meta:resourceKey="ltInvernadero">Invernadero</asp:Literal>
                                                    <%# DataBinder.Eval(Container.DataItem, "idInvernadero")%>
                                                     (
                                                        <%# (string)GetLocalResourceObject((string)DataBinder.Eval(Container.DataItem, "estatus"))%>
                                                     )
                                                     <%# DataBinder.Eval(Container.DataItem, "abejorro")=="1"? "Con Abejorro": ""%>
                                                </h3>
                                            </td> 
                                            <td style="padding:0;">
                                                <h3 style="text-align: left; width: 150px; color: #000; margin:0;">
                                                    <%# DataBinder.Eval(Container.DataItem, "vNombre")%></h3>
                                            </td> 
                                               <%--<td style="text-align: left;">
                                                <h4>
                                                    Comentarios</h4>
                                            </td>--%> 
                                              <td style="padding:0;">
                                                 <h3 style="text-align: left; width: 100px; color: #000;">
                                                    <%# DataBinder.Eval(Container.DataItem, "dAgua")%> Lts.</h4>
                                            </td>
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "fecha")%>
                                                </td>
                                        </tr> 
                                        <tr>
                                            <td style="vertical-align: top; padding:0px; height:auto; text-align: left;" colspan="5" class="big">
                                                <asp:GridView ID="GridView1" meta:resourceKey="gv" runat="server" AutoPostBack="false" HorizontalAlign="Center"
                                                    AutoGenerateColumns="False" CssClass="gridView" Width="100%" HeaderStyle-CssClass="gridViewHeader"
                                                    ItemStyle-CssClass="center" AlternatingRowStyle-CssClass="gridViewAlt" PagerStyle-CssClass="gridViewPagerStyle"
                                                    EmptyDataRowStyle-BackColor="#BFCBBE" EmptyDataText="NO REGISTROS" EmptyDataRowStyle-CssClass="gridEmptyData"
                                                    OnRowDataBound="GridView1_RowDataBound" DataKeyNames="idProgramacionHeader">
                                                    <Columns>
                                                        <asp:BoundField DataField="quimico" ReadOnly="True" Visible="True" HeaderText="Producto" meta:resourceKey="htProducto"
                                                            HeaderStyle-Width="350px" ItemStyle-CssClass="center" />
                                                        <asp:BoundField DataField="canPedida" runat="server" meta:resourceKey="htDosis" HeaderText="Dosis" HeaderStyle-Width="240px"
                                                            ItemStyle-CssClass="center" />
                                                        <asp:BoundField DataField="cosecha" runat="server" meta:resourceKey="htCosecha" HeaderText="Cosecha" HeaderStyle-Width="240px"
                                                            ItemStyle-CssClass="center" />
                                                        <asp:BoundField DataField="reentrada" runat="server" meta:resourceKey="htReentrada" HeaderText="Reentrada" HeaderStyle-Width="240px"
                                                            ItemStyle-CssClass="center" />
                                                         <asp:BoundField DataField="Dpedida" runat="server" meta:resourceKey="htPedidas" HeaderText="C. Pedidas" HeaderStyle-Width="220px"
                                                            ItemStyle-CssClass="center" />
                                                            <asp:BoundField DataField="Dpesada" runat="server" meta:resourceKey="htPesadas" HeaderText="C. Pesadas" HeaderStyle-Width="220px"
                                                            ItemStyle-CssClass="center" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                           <%-- <td style="vertical-align: top; white-space: normal; word-break: break-all; width: 240px;
                                                text-align: left;">
                                                <asp:Literal ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comentario")%>'
                                                    Visible='<%# DataBinder.Eval(Container.DataItem, "comentario")==""? false: true%>'></asp:Literal>
                                            </td>--%>
                                        </tr>
                                        <tr>                                            
                                            <td colspan ="5" style="text-align: center; padding:0px;">
                                                <asp:Literal ID="TextBox2" runat="server" Text= '<%#(string)GetLocalResourceObject( "comentario") +" "+ DataBinder.Eval(Container.DataItem, "comentario")%>'
                                                    Visible='<%# DataBinder.Eval(Container.DataItem, "comentario")==""? false: true%>'></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                        </SeparatorTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnExcel2" meta:resourceKey="btnXport" runat="server" Text="Exportar" OnClick="btnExcel_Click" />
                </td>
            </tr>
        </table>
        <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
    </div>
</asp:Content>
