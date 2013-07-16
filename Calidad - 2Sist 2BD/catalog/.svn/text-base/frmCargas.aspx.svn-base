<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmCargas.aspx.cs" enableEventValidation="false" Inherits="catalog_frmCargas" ValidateRequest="false"%>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

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
            if ($("#<%=gvCarga.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvCarga.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"),output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}'  });
            } else {
                $("#pager").hide();
            }

            //*****************validacion de los campos******************************//
            //valida entradas de numeros decimales desde 000000000.00 hasta 999999999.99
            $.validator.addMethod("decimal4Float",
            function (value, element) {
                return this.optional(element) || /^(\d?\d?\d?\d?\d?\d?\d?\d?\d?)(\.\d{1,2})?$/.test(value);
            }, '<%=(string)GetLocalResourceObject("jsCapacidadNumerica")%>');
            //validacion del formulario con la clase validate de jquery
            $('#' + formId).validate({
                errorLabelContainer: "div.alerta span",
                wrapper: "li",
                onfocusout: false,
                onsubmit: false,
                onkeyup: false,
                rules: {
                    '<%=txtCapacidad.UniqueID%>': { required: true, decimal4Float: true }
                },
                messages: {
                    '<%=txtCapacidad.UniqueID%>': { required: '<%=(string)GetLocalResourceObject("CapacidadRequerida")%>', decimal4Float: '<%=(string)GetLocalResourceObject("jsCapacidadNumerica")%>' }
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
            $("#<%=btnSave.ClientID%>").click(submit_Click);
            $("#<%=btnActualizar.ClientID%>").click(submit_Click);

            function submit_Click() {
                return $("#" + formId).valid();
            }
            //*****************fin validacion de los campos******************************//

            registerCombobox();
        });

        function registerCombobox() {
            $("select:not('.pagesize')").combobox();
        }

        //boton de cerrar mensaje de error
        function closeErrorMsg() {
            $("div.alerta span").html('');
            $("#error").hide();
        }

    </script> 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

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

    <h1>
    <asp:Literal ID="ltBienvenido" runat="server" meta:resourceKey="ltBienvenido">Cátalogo de cargas</asp:Literal></h1>
    <table class="index">
        <tr>
            <td style="width:120px">
                <asp:Literal ID="ltPlanta" runat="server" meta:resourceKey="ltPlanta">*Planta</asp:Literal> 
             </td>         
            <td>
                <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true" > </asp:DropDownList>   
            </td>
            <td>
                <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo">Activo?</asp:Literal>               
            </td>
            <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" MaxLength="255" /></td>
        </tr>
        <tr>
            <td><asp:Literal ID="ltCapacidad" runat="server" meta:resourceKey="ltCapacidad">*Capacidad (Lts.)</asp:Literal></td>
            <td style="text-align:left;">
                <asp:TextBox ID="txtCapacidad" runat="server"></asp:TextBox>
            </td>
            <td><asp:Literal ID="ltEquipo" runat="server" meta:resourceKey="ltEquipo">Equipo de Aplicación</asp:Literal></td>
            <td>
                <asp:DropDownList ID="ddlEquipoAplicacion" runat="server" AppendDataBoundItems="true" > </asp:DropDownList>   
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:HiddenField runat="server" Value="Añadir" id="Accion"/>
                <asp:Button ID="btnActualizar" meta:resourceKey="btnActualizar" runat="server" Text="Actualizar" Visible="false" onclick="Guardar_Actualizar"/>
                <asp:Button ID="btnSave" meta:resourceKey="btnSave" runat="server" Text="Guardar" onclick="Guardar_Actualizar" />
                <asp:Button ID="btnLimpiar" meta:resourceKey="btnLimpiar" runat="server" Text="Limpiar" onclick="Cancelar_Limpiar"/>
                <asp:Button ID="btnCancel" meta:resourceKey="btnCancel" runat="server" Text="Cancelar" onclick="Cancelar_Limpiar" Visible="False"/>
            </td>
        </tr>
    </table>
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
            <asp:GridView ID="gvCarga" 
                meta:resourceKey="gvCarga"
                runat="server" 
                AutoGenerateColumns="False" 
                CssClass="gridView" 
                DataKeyNames="idCarga" 
                EmptyDataText="No existen registros" 
                Width="800px"
                onprerender="gvCarga_PreRender" 
                onrowdatabound="gvCarga_RowDataBound" 
                onselectedindexchanged="gvCarga_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="planta" HeaderText="Planta" meta:resourceKey="htPlanta" HeaderStyle-Width="200px" />                            
                    <asp:BoundField DataField="equipoAplicacion" HeaderText="Equipo de Aplicacion" meta:resourceKey="htEquipo" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px"/>   
                    <asp:BoundField DataField="capacidad" HeaderText="Capacidad" meta:resourceKey="htCapacidad" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px"/>   
                    <asp:CheckBoxField DataField="activo" HeaderText="Activo" meta:resourceKey="htActivo"  HeaderStyle-Width="100px"/>

                   <%-- <asp:TemplateField HeaderText="Activo" meta:resourceKey="htActivo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                       <ItemTemplate>
                            <asp:Literal ID="lblActivoGrid" meta:resourceKey="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?"Sí":"No" %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>   --%>                
                </Columns>
            </asp:GridView>
    </div>


    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

    </div>

</asp:Content>

