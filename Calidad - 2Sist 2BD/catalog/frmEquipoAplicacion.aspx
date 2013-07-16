<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmEquipoAplicacion.aspx.cs" enableEventValidation="false" Inherits="catalog_frmEquipoAplicacion" ValidateRequest="false"%>
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
            if ($("#<%=gvEquipoAplicacion.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvEquipoAplicacion.ClientID%>")
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
            //validacion del formulario con la clase validate de jquery
            $('#' + formId).validate({
                errorLabelContainer: "div.alerta span",
                wrapper: "li",
                onfocusout: false,
                onsubmit: false,
                onkeyup: false,
                rules: {
                    '<%=txtNombre.UniqueID%>': { required: true }
                },
                messages: {
                    '<%=txtNombre.UniqueID%>': { required: '<%=(string)GetLocalResourceObject("NombreRequerido")%>' }
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

        });

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
    <asp:Literal ID="lblBienvenido" meta:resourceKey="lblBienvenido" runat="server">Equipos de Aplicaci&oacute;n</asp:Literal></h1>
    <table class="index">
        <tr>
            <td style="width:120px">
                <asp:Literal ID="ltNombre" meta:resourceKey="ltNombre" runat="server">*Nombre</asp:Literal>
                </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="cajaLarga" MaxLength="64"></asp:TextBox>
            </td>
            <td><asp:Literal ID="ltActivo" meta:resourceKey="ltActivo" runat="server">Activo</asp:Literal></td>
            <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" MaxLength="255" /></td>
        </tr>
        <tr>
            <td><asp:Literal ID="ltDescripcion" meta:resourceKey="ltDescripcion" runat="server">Descripci&oacute;n</asp:Literal> </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="cajaLarga"></asp:TextBox>
            </td>
            <td></td>
            <td></td>

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
            <asp:GridView ID="gvEquipoAplicacion"
                meta:resourceKey="gvEquipoAplicacion" 
                runat="server" 
                AutoGenerateColumns="False" 
                CssClass="gridView" 
                DataKeyNames="idEquipoAplicacion" 
                EmptyDataText="No existen registros" 
                Width="800px"
                onpageindexchanging="gvEquipoAplicacion_PageIndexChanging" 
                onprerender="gvEquipoAplicacion_PreRender" 
                onrowdatabound="gvEquipoAplicacion_RowDataBound" 
                onselectedindexchanged="gvEquipoAplicacion_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" meta:resourceKey="htNombre"  HeaderStyle-Width="200px" />                            
                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" meta:resourceKey="htDescripcion" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px"/>   
                   <asp:CheckBoxField DataField="activo" HeaderText="Activo" meta:resourceKey="htActive"  HeaderStyle-Width="100px"/>
                    <%--<asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?"Sí":"No" %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField> --%>                  
                </Columns>
            </asp:GridView>
    </div>


    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

    </div>

</asp:Content>

