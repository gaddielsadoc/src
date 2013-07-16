<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ListaOrdenesTrabajo.aspx.cs" enableEventValidation="false" Inherits="pages_ListaOrdenesTrabajo" ValidateRequest="false"%>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<%@ Register Src="~/controls/ctrlCapturaDosis.ascx" TagName="capturaDosisMessageControl" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
    <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />    

    <script type="text/javascript">

        $(function () {
            $(".fechaDesde").datepicker({
                dateFormat: 'yy-MM-dd',
                onClose: function (selectedDate) {
                    $(".fechaHasta").datepicker("option", "minDate", selectedDate);
                }
            });

            $(".fechaHasta").datepicker({
                dateFormat: 'yy-MM-dd',
                onClose: function (selectedDate) {
                    $(".fechaDesde").datepicker("option", "maxDate", selectedDate);
                }
            });


            if ($("#<%=gvOrdenTrabajo.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvOrdenTrabajo.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"),output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}'  });
            } else {
                $("#pager").hide();
            }

            registerCombobox();

            //*****************validacion de los campos******************************//
            //validacion del formulario con la clase validate de jquery

            //valida entradas de fechas formato dd-MM-yyyy
            $.validator.addMethod("formatoFecha",
                function (value, element) {
                    return this.optional(element) || /\d\d\d\d-\d\d-\d\d/.test(value);
                }, "La fecha debe ingresarse en formato yyyy-MM-dd");

            $('#' + formId).validate({
                errorLabelContainer: "div.alerta span",
                wrapper: "li",
                onfocusout: false,
                onsubmit: false,
                onkeyup: false,
                rules: {
                    '<%=txtFechaAplicacion.UniqueID%>': { formatoFecha: true },
                    '<%=txtFechaAplicacion2.UniqueID%>': { formatoFecha: true }
                },
                messages: {
                    '<%=txtFechaAplicacion.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("jsFechaMinima")%>'  },
                    '<%=txtFechaAplicacion2.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("jsFechaMaxima")%>' }
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
            $("#<%=btnActualizar.ClientID%>").click(submit_Click);

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


        function registerCombobox() {
            $("select[name='<%= ddlSites.UniqueID %>']").comboboxPostBack();
            $("select:not('.pagesize')").not("select[name='<%= ddlSites.UniqueID %>']").combobox();
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
        $(function () {
            $('#<%=chkAll.ClientID%>').click(function () {
                    $('#statusTable *').attr('checked', this.checked);
                }
            );
        });
/*
        function refreshGrid(idUser, connection) {
                PageMethod.obtieneOrdenesTrabajoWM(,,,,,,,,,,, function(tablaHTML){
                
                });
        }
        */
    </script> 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

    <%--error msg--%>
    <div id="error" class="modalPopup" style="width: 500px; display:none; position: fixed; z-index:99999;">
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

    <h1>
    <asp:Literal ID="lblBienvenido" runat="server" meta:resourceKey="ltBienvenido">&Oacute;rdenes de trabajo</asp:Literal></h1>
    <table class="index">
        <tr>
            <td><asp:Literal ID="ltPlanta" runat="server" meta:resourceKey="ltPlanta">Planta</asp:Literal></td>
            <td class="floatnone">
                <asp:DropDownList ID="ddlSites" runat="server" Height="16px" AppendDataBoundItems="true" AutoPostBack="true"
                        DataTextField="campoNombre" DataValueField="campoId"
                        onselectedindexchanged="ddlPlanta_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td><asp:Literal ID="ltInvernadero" runat="server" meta:resourceKey="ltInvernadero">Invernadero</asp:Literal></td>
            <td>
                <asp:DropDownList ID="ddlInvernaderos" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Literal ID="ltFechaDesde" runat="server" meta:resourceKey="ltFechaDesde">Fecha de aplicaci&oacute;n desde</asp:Literal></td>
            <td style="text-align:left;">
                <asp:TextBox ID="txtFechaAplicacion" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
            </td>
            <td><asp:Literal ID="ltFechaHasta" runat="server" meta:resourceKey="ltFechaHasta">Fecha de aplicaci&oacute;n hasta</asp:Literal></td>
            <td style="text-align:left;">
                <asp:TextBox ID="txtFechaAplicacion2" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Literal ID="ltEstado" runat="server" meta:resourceKey="ltEstado">Estado</asp:Literal></td>
            <td style="vertical-align:top;">
                <table id="statusTable" cellpadding="0" cellspacing="0" border="0" style="text-align:left;">
                    <tr>
                        <td class="checkboxes"><asp:CheckBox ID="chkAll" runat="server" text="Seleccionar todos" meta:resourceKey="chkAll" /></td>
                    </tr>
                    <tr>
                        <td class="checkboxes"><asp:CheckBox ID="chkPendiente" runat="server" text="Pendientes"  meta:resourceKey="chkPendiente"  /></td>
                    </tr>
                    <tr>
                        <td class="checkboxes"><asp:CheckBox ID="chkAbierto" runat="server" text="Abiertos"  meta:resourceKey="chkAbierto" /></td>
                    </tr>
                    <tr>
                        <td class="checkboxes"><asp:CheckBox ID="chkEntregado" runat="server" text="Entregados"  meta:resourceKey="chkEntregado"  /></td>
                    </tr>
                    <tr>
                        <td class="checkboxes"><asp:CheckBox ID="chkCancelado" runat="server" text="Cancelados"  meta:resourceKey="chkCancelado" /></td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td></td>
        </tr>

        <tr>
            <td colspan="4" align="right">
                <asp:HiddenField runat="server" Value="Añadir" id="Accion"/>
                <asp:Button ID="btnActualizar" runat="server" Text="Filtrar" onclick="Actualizar" meta:resourceKey="btnActualizar"/>
                
            </td>
        </tr>
    </table>
    

    <div class="grid">
        <div id="pager" class="pager">  
       <table width="100%" align="left"><tr><td> <img alt="first" src="../comun/img/first.png" class="first" />
        <img alt="prev" src="../comun/img/prev.png" class="prev" />
        <input type="text" class="pagedisplay" />
        <img alt="next" src="../comun/img/next.png" class="next" />
        <img alt="last" src="../comun/img/last.png" class="last" /></td>
     <td> <select class="pagesize cajaCh" style="width: 50px; min-width: 50px; float:none; max-width: 50px;">
            <option value="10">10</option>
            <option value="20">20</option>
            <option value="30">30</option>
            <option value="40">40</option>
            <option value="50">50</option>
        </select></td></tr></table>
        </div>
            <asp:GridView ID="gvOrdenTrabajo" 
                    meta:resourceKey="gvOrdenTrabajo"
                    runat="server" 
                    AutoGenerateColumns="False" 
                    CssClass="gridView" 
                    DataKeyNames="idOrdenTrabajoHeader" 
                    EmptyDataText="No existen registros" 
                    Width="800px"
                    onpageindexchanging="gvOrdenTrabajo_PageIndexChanging" 
                    onprerender="gvOrdenTrabajo_PreRender" 
                    onrowdatabound="gvOrdenTrabajo_RowDataBound" 
                    onselectedindexchanged="gvOrdenTrabajo_SelectedIndexChanged" 
                    EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="NoOrden" meta:resourceKey="htOrden"  HeaderText="Orden" HeaderStyle-Width="200px" > <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="planta" meta:resourceKey="htPlanta" HeaderText="Site" HeaderStyle-Width="200px" ><HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="invernadero" meta:resourceKey="htInvernadero" HeaderText="Invernadero" HeaderStyle-Width="200px"> <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="semana" meta:resourceKey="htSemana" HeaderText="Semana" HeaderStyle-Width="200px"> <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaAplicacion" meta:resourceKey="htFechaApliacion" HeaderText="Fecha de aplicación" HeaderStyle-Width="200px" DataFormatString="{0:yyyy-MM-dd}"> <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="hora" meta:resourceKey="htHora" HeaderText="Hora de aplicación" HeaderStyle-Width="200px"><HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Estatus">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# (string)Eval("estatus")=="Abierto"?(string)GetLocalResourceObject("Abierto"):
                                                                                                                (string)Eval("estatus")=="Entregado"?(string)GetLocalResourceObject("Entregado"):
                                                                                                                                                    (string)Eval("estatus")=="Cancelado"?(string)GetLocalResourceObject("Cancelado"):
                                                                                                                                                                                        (string)Eval("estatus")=="Pendiente"?(string)GetLocalResourceObject("Pendiente"): "" %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("estatus") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="200px" />
                    </asp:TemplateField>

                     <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                                <ItemTemplate>                                         
                                    <asp:ImageButton ID="btnDelete" runat="server" src="../comun/img/cancelar.png" Width="14px" Height="14px" OnClick="imgCancelarNominee_Click" 
                                    Visible='<%# (string)Eval("estatus") == "Cancelado" ? false : true %>' />
                                </ItemTemplate>
                                </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
    </div>



    <asp:Panel ID="pnlCapturaDosisControl" runat="server" CssClass="modalPopup" Style="padding:5px; display:none;" width="600px" >

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
               <asp:DropDownList ID="ddlRazones" runat="server" Width="200px" DataTextField="vRazon" DataValueField="idRazonCancelarOT">   </asp:DropDownList> 
            </td>
        </tr>
        
        <tr>
            <td align="right"><asp:Label runat="server" ID="lblComentarios"  meta:resourceKey="lblComentarios"></asp:Label> </td>
       
            <td>
                <asp:TextBox ID="txtComents" runat="server"  TextMode="MultiLine" MaxLength="700" ></asp:TextBox> 
            </td>
        </tr>
       
        <tr>
            <td colspan="2" class="floatnone">           
                
                <asp:Button CssClass="button" runat="server" ID="cancel" meta:resourceKey="cancel"  Text="Cancelar" />
                <asp:Button CssClass="button" runat="server" ID="save" meta:resourceKey="save"  Text="Guardar" OnClick="save2_OnClick"  />
                <asp:Button CssClass="button" runat="server" ID="btnOKMessageGralControl"  meta:resourceKey="btnOKMessageGralControl" Text="Cancelar" style="display: none;" />
            </td>
        </tr>
    </table>

    </asp:Panel>
        <asp:LinkButton runat="server" ID="lnkHiddenMdlPopupControl"  Text=""  Enabled="false"/>
        <ajaxtoolkit:modalpopupextender ID="popUpRazones" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlCapturaDosisControl" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnOKMessageGralControl" />
        

    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />




    <uc2:capturaDosisMessageControl ID="popUpCapturaDosis" runat="server" />


    </div>

</asp:Content>

