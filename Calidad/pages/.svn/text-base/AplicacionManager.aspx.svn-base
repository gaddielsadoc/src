<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AplicacionManager.aspx.cs" Inherits="pages_AplicacionManager" EnableEventValidation="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<%--<%@ Register Src="~/controls/ctrlRazonesCancelar.ascx" TagName="razonesCancelarMessageControl" TagPrefix="uc2" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
   <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
   <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />  


     <script type="text/javascript">

         $(function () {
             if ($("#<%=gdvProgramaManager.ClientID%>").find("tbody").find("tr").size() > 1) {

                 $("#<%=gdvProgramaManager.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"),output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}'  });
             } else {
                 $("#pager").hide();
             }
         });
    </script>

    <script type="text/javascript">

        function pageLoad() {
            registerCombobox();
        }

        function registerCombobox() {
                //no se seleccionaran todos los combos, por que si no, no me sirve el de la pagimacion
            // $("select").combobox(); 
            $("select[name='<%= ddlPlanta.UniqueID %>']").combobox();
            $("select[name='<%= ddlInvernadero.UniqueID %>']").combobox();
            $("select[name='<%= ddlEstatus.UniqueID %>']").combobox();
            $("select[name='<%= ddlNombre.UniqueID %>']").combobox();                
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
                }, '<%=(string)GetLocalResourceObject("FormatoFecha") %>');

         $('#' + formId).validate({
             errorLabelContainer: "div.alerta span",
             wrapper: "li",
             onfocusout: false,
             onsubmit: false,
             onkeyup: false,
             rules: {
                 '<%=txtDesde.UniqueID%>': { formatoFecha: true }
                 ,'<%=txtHasta.UniqueID%>': { formatoFecha: true }
             },
             messages: {
                 '<%=txtDesde.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("FormatoFecha") %>' }
                 , '<%=txtHasta.UniqueID%>': { formatoFecha: '<%=(string)GetLocalResourceObject("FormatoFecha") %> ' }
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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


    <%--Tutilo--%>
   <div class="container">
        <h1 style="width:1024px; max-width:1024px; min-width:1024px;"><asp:Literal runat="server" ID="ltAdminProg" meta:resourceKey="ltAdminProg" >Administrador de Programas</asp:Literal></h1>
       
    <div>
        <table width="100%" class="index" cellpadding="0" cellspacing="0" style="width:900px; max-width:900px; min-width:900px;">
            <tr>
                <td colspan="8"> <h2> <asp:Literal runat="server"  meta:resourceKey="ltBuscar" >Buscar </asp:Literal> </h2>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="Literal1" runat="server"  meta:resourceKey="ltNombre" >Nombre</asp:Literal> </td>                   
                <td>
                <asp:DropDownList ID="ddlNombre" runat="server" AppendDataBoundItems="true" DataTextField="vNombre" DataValueField="vNombre"  ></asp:DropDownList>
                </td>
                <td><asp:Literal ID="Literal2" runat="server"  meta:resourceKey="ltPlanta" >Planta</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true" DataTextField="campoNombre" DataValueField="campoId" ></asp:DropDownList>
                </td>
                <td><asp:Literal ID="Literal3" runat="server"  meta:resourceKey="ltInvernadero" >Invernadero</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddlInvernadero" runat="server" AppendDataBoundItems="true" DataTextField="invernadero" DataValueField="invernadero"></asp:DropDownList>
                </td>
                <td><asp:Literal ID="Literal4" runat="server"  meta:resourceKey="ltEstatus" >Estatus</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddlEstatus" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                </td>                
            </tr>
            <tr>
                <td><asp:Literal ID="Literal5" runat="server"  meta:resourceKey="ltCreado" >Creado desde</asp:Literal></td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtDesde" runat="server" CssClass="datepicker cajaChica"></asp:TextBox>
                </td>
                <td><asp:Literal ID="Literal6" runat="server"  meta:resourceKey="ltHasta" >Hasta</asp:Literal></td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtHasta" runat="server"  CssClass="datepicker cajaChica" ></asp:TextBox>
                </td>
                <td colspan="4">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" meta:resourceKey="btnBuscar"
                        onclick="btnBuscar_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="grid">

         <div id="pager" class="pager">
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

                        <asp:GridView ID="gdvProgramaManager" runat="server"   CssClass="gridView" DataKeyNames="idProgramacionHeader"                     
                                AutoGenerateColumns="False" Width="800px"  
                                onRowDataBound="gdvProgramaManager_RowDataBound"
                                onprerender="gdvProgramaManager_PreRender"
                                EmptyDataText="No existen registros" EnableModelValidation="True"
                                OnSelectedIndexChanged ="gdvProgramaManager_SelectedIndexChanged"
                                 meta:resourceKey="gv"                    
                                >
                            <Columns>
                               <%-- <asp:BoundField DataField="idBoletinHeader" HeaderText="ID"  />--%>
                               <%-- <asp:BoundField DataField="dateFechaInicioBoletinHeader" HeaderText="Fecha Inicio"   />
                                <asp:BoundField DataField="dateFechaFinBoletinHeader" HeaderText="Fecha Fin" />--%>
                                <asp:BoundField DataField="vNombre"         meta:resourceKey="htNombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="estatus"         meta:resourceKey="htEstatus" HeaderText="Estatus" />
                                <asp:BoundField DataField="vUserCreo"       meta:resourceKey="htCreado" HeaderText="Creado Por" HeaderStyle-Width="350px">   <HeaderStyle Width="350px"></HeaderStyle></asp:BoundField>
                                <asp:BoundField DataField="dFechaCreacion"  meta:resourceKey="htCreacion" HeaderText="Fecha Creación" HeaderStyle-Width="350px" ><HeaderStyle Width="350px"></HeaderStyle></asp:BoundField>
                                <asp:BoundField DataField="idPlanta"        meta:resourceKey="htPlanta" HeaderText="Planta" />
                                <asp:BoundField DataField="idInvernadero"   meta:resourceKey="htInvernadero" HeaderText="Invernadero" />
                                <asp:BoundField DataField="vUserEnvio"          meta:resourceKey="htLider" HeaderText="Líder" />
                                <asp:BoundField DataField="dFechaPlanAplicar"   meta:resourceKey="htAplicar" HeaderText="Aplicar en" />
                                <asp:BoundField DataField="vUserPeso"           meta:resourceKey="htUsuario" HeaderText="Usuario Almacén" />
                                <asp:BoundField DataField="vUserCancelo"        meta:resourceKey="htCancelado" HeaderText="Cancelado por" />
                               
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                                    <ItemTemplate>                                         
                                        <asp:ImageButton ID="btnCopy" runat="server" CommandName="copy" src="../comun/img/duplicate.png" OnClick="copyAplication" Width="22px" />
                                    </ItemTemplate>
                                    <HeaderStyle BorderStyle="None"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                                <ItemTemplate>                                         
                                    <asp:ImageButton ID="btnDelete" runat="server" src="../comun/img/cancelar.png" Width="14px" Height="14px" OnClick="imgCancelarNominee_Click" 
                                    Visible='<%#
                                    string.Compare(GetValor((string)Eval("estatus")),"Cancelado_", true) == 0 || string.Compare(GetValor((string)Eval("estatus")),"Ejecutado_".ToString(), false) == 0   || string.Compare(GetValor((string)Eval("estatus")),"Entregado_", false) == 0     ? false : true 
                                    %>'
                                    />
                                </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
           <asp:Button ID="btnNuevo" runat="server" Text="Nuevo"  meta:resourceKey="Nuevo" onclick="btnNuevo_Click" />
    </div>

  
   </div>  

     <asp:Panel ID="pnlCapturaHoras" runat="server" CssClass="modalPopup" Style="" width="420px">               
      <table class="index3" align="center" style="min-width:400px; margin:5px;">          
          <tr>
              <td  style="text-align:right; background:#ffa05f;" >  
                <h4><asp:Label runat="server" ID="lblInfo" meta:resourceKey="lblInfo" Text=" ¿Seguro qué desea cancelar el programa?"></asp:Label></h4>  
              </td>                         
           </tr> 
                <tr>
                    <td >                    
                        <asp:Button CssClass="button" runat="server" meta:resourceKey="btnSi" ID="save" Text="Sí" OnClick="save_Click" />
                        <asp:Button CssClass="button" runat="server" meta:resourceKey="btnNo" ID="btnCancel" Text="No" />
                    </td>
                </tr> 
        </table>
    </asp:Panel>
        <asp:LinkButton runat="server" ID="lnkHiddenMdlPopupControl"  Text=""  Enabled="false"/>
        <asp:ModalPopupExtender ID="mdlPopupMessageGralControl" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlCapturaHoras" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnCancel" />


    
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
                <asp:Button CssClass="button" runat="server" ID="Button2" meta:resourceKey="save2"  Text="Guardar" OnClick="save2_OnClick" />
                <asp:Button CssClass="button" runat="server" ID="btnOKMessageGralControl"  meta:resourceKey="btnOKMessageGralControl" Text="Cancelar" style="display: none;" />
            </td>
        </tr>
    </table>

    </asp:Panel>
        <asp:LinkButton runat="server" ID="LinkButton1"  Text=""  Enabled="false"/>
        <asp:ModalPopupExtender ID="popUpRazones" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlCapturaDosisControl" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnOKMessageGralControl">
        </asp:ModalPopupExtender>

   
   <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
   
   <%--<uc2:razonesCancelarMessageControl ID="popUpRazones" runat="server" />--%>

</asp:Content>

