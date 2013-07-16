<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmVolumenAgua.aspx.cs" enableEventValidation="false" Inherits="catalog_frmVolumenAgua" ValidateRequest="false"%>
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

            if ($("#<%=gvVolumenAgua.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvVolumenAgua.ClientID%>")
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

            //valida entradas de numeros decimales desde 0000.00 hasta 9999.99
            $.validator.addMethod("decimal2Float",
                function (value, element) {
                    return this.optional(element) || /^(\d?\d?\d?\d?)(\.\d{1,2})?$/.test(value);
                }, '<%=(string)GetLocalResourceObject("decimal")%>');

            $.validator.addMethod("siteRequired",
                function (value, element) {
                    var siteValue = $('#<%=ddlSite.ClientID %>').val();
                    return siteValue != -1;
                }, '<%=(string)GetLocalResourceObject("NotEmpty")%>');


            //validacion del formulario con la clase validate de jquery
            $('#' + formId).validate({
                errorLabelContainer: "div.alerta span",
                wrapper: "li",
                onfocusout: false,
                onsubmit: false,
                onkeyup: false,
                rules: {
                    '<%=txtEdad.UniqueID %>': { number: true, 'siteRequired': true },
                    '<%=txtVolumen.UniqueID%>': { required: true, decimal2Float: true }
                },
                messages: {
                    '<%=txtEdad.UniqueID %>': { number: '<%=(string)GetLocalResourceObject("edadNumeric")%>', 'siteRequired': '<%=(string)GetLocalResourceObject("FarmRequired")%>' },
                    '<%=txtVolumen.UniqueID%>': { required: '<%=(string)GetLocalResourceObject("VAguaRequired")%>', decimal2Float: '<%=(string)GetLocalResourceObject("vaDecima")%>' }
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
//            $("select:not('.pagesize')").combobox();

        <%if(ddlSite.Enabled)
          { %>
            $("select[name='<%= ddlSite.UniqueID %>']").comboboxPostBack();
        <% } %>

        <%if(ddlTipoBoquilla.Enabled)
          { %>
        $("select[name='<%= ddlTipoBoquilla.UniqueID %>']").combobox();
        <% } %>

        <%if(ddlEquipoAplicacion.Enabled)
          { %>
        $("select[name='<%= ddlEquipoAplicacion.UniqueID %>']").combobox();
        <% } %>

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
    <asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
    <table class="index" style="width:1024px; min-width:1024px; max-width:1024px;">
        <tr>
            
            <td style="width:120px">
            <asp:Literal ID="ltPlanta" runat="server" meta:resourceKey="ltPlanta"></asp:Literal>
            
            </td>
            <td class="margin_top">
                <asp:DropDownList runat="server" ID="ddlSite" AppendDataBoundItems = "true" AutoPostBack ="true"
                    onselectedindexchanged="ddlSite_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
            <asp:Literal ID="ltVolumen" runat="server" meta:resourceKey="ltVolumen"></asp:Literal>
       
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtVolumen" MaxLength="7"></asp:TextBox>
            </td>
            <td>
            <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo"></asp:Literal>
            
            </td>
            <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" /></td>
        </tr>
        <tr>
            <td>
            <asp:Literal ID="ltTipoBoquilla" runat="server" meta:resourceKey="ltTipoBoquilla"></asp:Literal>
            
            </td>
            <td class="margin_top">
                <asp:DropDownList runat="server" ID="ddlTipoBoquilla" AppendDataBoundItems = "true"></asp:DropDownList>
            </td>
            <td></td><td></td>
            <%--<td>Tipo de Aplicaci&oacute;n</td>
            <td class="margin_top">
                <asp:DropDownList runat="server" ID="ddlTipoAplicacion" AppendDataBoundItems = "true"></asp:DropDownList>
            </td>--%>
            <td>
            <asp:Literal ID="ltEquipoAplicacion" runat="server" meta:resourceKey="ltEquipoAplicacion"></asp:Literal>
            
            </td>
            <td class="margin_top" style="width:200px;">
                <asp:DropDownList runat="server" ID="ddlEquipoAplicacion" AppendDataBoundItems = "true"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            <asp:Literal ID="ltEdadCultivo" runat="server" meta:resourceKey="ltEdadCultivo"></asp:Literal>
           
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtEdad" MaxLength="3"></asp:TextBox>
            </td>
            <td>
             <asp:Literal ID="lthcapar" runat="server" meta:resourceKey="lthcapar"></asp:Literal>
            
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkCapar"/>
            </td>
            <td>
             <asp:Literal ID="ltHfHc" runat="server" meta:resourceKey="ltHfHc"></asp:Literal>
            

            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkFin"/>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:HiddenField runat="server" Value="Añadir" id="Accion"/>
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" Visible="false" onclick="Guardar_Actualizar" meta:resourceKey="btnActualizar"/>
                <asp:Button ID="btnSave" runat="server" Text="Guardar" onclick="Guardar_Actualizar" meta:resourceKey="btnSave"/>
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" onclick="Cancelar_Limpiar" meta:resourceKey="btnLimpiar"/>
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" onclick="Cancelar_Limpiar" Visible="False" meta:resourceKey="btnCancel"/>
            </td>
        </tr>
    </table>
    

    <div class="grid">
        <div id="pager" class="pager" style="width:1024px; max-width:1024px; min-width:1024px;">
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
            <asp:GridView ID="gvVolumenAgua" runat="server" 
                    AutoGenerateColumns="False" CssClass="gridView" DataKeyNames="idVolumenAgua" 
                    EmptyDataText="No existen registros" Width="800px"
                    onpageindexchanging="gvVolumenAgua_PageIndexChanging" 
                    onprerender="gvVolumenAgua_PreRender" onrowdatabound="gvVolumenAgua_RowDataBound" 
                    onselectedindexchanged="gvVolumenAgua_SelectedIndexChanged" meta:resourceKey="gvVolumenAgua">
                <Columns>
                    <asp:BoundField DataField="site" HeaderText="IDSITE" HeaderStyle-Width="200px" meta:resourceKey="htSite"/>                            
                    <asp:BoundField DataField="tipoBoquilla" HeaderText="Tipo de boquilla" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px" meta:resourceKey="htTipoBoquilla"/>   
                    <%--<asp:BoundField DataField="tipoAplicacion" HeaderText="Tipo de Aplicación" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px"/>   --%>
                    <asp:BoundField DataField="equipoAplicacion" HeaderText="Equipo de Aplicación" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px" meta:resourceKey="htEquipoAplicacion" />   
                    <asp:BoundField DataField="volumen" HeaderText="Volumen de Agua (l)" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px" meta:resourceKey="htVolumenAgua"/>   
                    <asp:BoundField DataField="edad" HeaderText="Edad de Cultivo" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px" meta:resourceKey="htEc"/>   
                    <asp:TemplateField HeaderText="Hasta Capar" SortExpression="capar" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" meta:resourceKey="htHcapar">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk1" runat="server" Checked='<%# Bind("capar") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCaparGrid" runat="server" Text='<%# (bool)Eval("capar")?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Hasta Fin" SortExpression="fin" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" meta:resourceKey="htHfin">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("fin") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFinGrid" runat="server" Text='<%# (bool)Eval("fin")?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No")  %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>                   
                    <asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" meta:resourceKey="htActivo">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>                   
                </Columns>
            </asp:GridView>
    </div>


    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

    </div>

</asp:Content>

