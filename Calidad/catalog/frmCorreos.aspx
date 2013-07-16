<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmCorreos.aspx.cs" Inherits="catalog_frmCorreos" %>

<%@ Register TagPrefix="uc1" TagName="popUpMessageControl" Src="~/controls/popUpMessageControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>


    <script type="text/javascript">
        function ValidTec(e) {
            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
                   
            if (key < 46 || key > 57) {
                if (key < 64 || key > 90) {
                    if (key < 97 || key > 122) {
                        return false;
                    }
                }
            }
            return true;
        }
</script>


    <script type="text/javascript">
        function submit_Click() {
            return $("#" + formId).valid();
        }

        function closeErrorMsg() {
            $("#error").hide();
        }

        $(function () {

            $("#" + formId).validate({
                onfocusout: false,
                onsubmit: false,
                rules: {
                    '<%= txtCorreo.UniqueID %>': { 'required': true, 'email': true }
                },

                messages: {
                    '<%= txtCorreo.UniqueID %>': { 'required': '<%=(string)GetLocalResourceObject("CorreoRequerido")%>', 'email': '<%=(string)GetLocalResourceObject("jsCorreoCorrecto")%>' }
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

            $("#<%=btnSaveCorreo.ClientID%>").click(submit_Click);
            $("#<%=btnActualizar.ClientID%>").click(submit_Click);

            if ($("#<%=gvCorreo.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvCorreo.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"), output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}' });
            } else {
                $("#pager").hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div id="error" class="modalPopup" style="width: 500px; display: none; position: fixed;
            z-index: 9999;">
            <table style="vertical-align: middle; text-align: center; height: 100%; width: 100%;">
                <tbody>
                    <tr>
                        <td style="background: #ccc repeat;">
                            <div id="">
                                <div class="alerta">
                                    <img src="../images/error.png" id="imgMensajeError" alt="" />
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
        <h1>
            <asp:Literal ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido" >Administraci&oacute;n de Correos a Enviar Bolet&iacute;n</asp:Literal></h1>
        <table class="index" style="width: 800px; max-width: 800px; min-width: 800px;">
            <tr>
                <td colspan="4" align="left">
                    <h2>
                        <asp:Literal ID="ltMensage" meta:resourceKey="ltMensage" runat="server" Text="Capture o edite los correos"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                
                 <td><asp:Literal runat="server" ID="ltPlanta"  meta:resourceKey="ltPlanta" >*Planta:</asp:Literal></td>
                    <td><asp:DropDownList runat="server" ID="ddlPlanta"></asp:DropDownList></td>

                <td align="right">
                    <asp:Literal ID="ltCorreo"  meta:resourceKey="ltCorreo" runat="server" Text="*Correo"></asp:Literal>
                </td>
                <td >
                    <asp:TextBox runat="server" ID="txtCorreo" MaxLength="255" CssClass="cajaLarga" onkeypress="javascript:return ValidTec( event );"></asp:TextBox>
                </td>
                <td align="right" style="width: 75px;">
                    <asp:Literal ID="ltActivo" meta:resourceKey="ltActivo" runat="server">&iquest;Activo?</asp:Literal>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCorreoActivo" Checked="True" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" Value="Añadir" ID="Accion" />
                    <asp:Button ID="btnActualizar" meta:resourceKey="btnActualizar" runat="server" Text="Actualizar" OnClick="btnSaveCorreo_Click"
                        Visible="false" />
                    <asp:Button ID="btnSaveCorreo" meta:resourceKey="btnSaveCorreo" runat="server" Text="Guardar" OnClick="btnSaveCorreo_Click" />
                    <asp:Button ID="btnLimpiar" meta:resourceKey="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnCancelCorreo_Click" />
                    <asp:Button ID="btnCancelCorreo" meta:resourceKey="btnCancelCorreo" runat="server" Text="Cancelar" OnClick="btnCancelCorreo_Click"
                        Visible="false" />
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
                <asp:Button ID="btnExportPdf" runat="server" Text="Genera PDF" 
                    onclick="btnExportPdf_Click" Visible="false" />
            </div>
            <asp:GridView ID="gvCorreo"
                meta:resourceKey="gvCorreo" 
                runat="server" 
                EnableModelValidation="True" 
                AutoGenerateColumns="False"
                OnPreRender="gvCorreo_PreRender" 
                DataKeyNames="idCorreo" 
                CssClass="gridView" 
                width="800px"
                EmptyDataText="No existen registros" 
                OnRowDataBound="gvCorreo_RowDataBound" 
                OnSelectedIndexChanged="gvCorreo_SelectedIndexChanged">
                <Columns>
                    <%--<asp:TemplateField HeaderText="Activo" meta:resourceKey="htActivo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?"Sí":"No" %>'
                                Enabled="False" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>--%>
                     <asp:CheckBoxField DataField="Activo" HeaderText="Activo" meta:resourceKey="htActive"  HeaderStyle-Width="100px"/>

                     <asp:BoundField DataField="Name" HeaderText="Planta" meta:resourceKey="htPlanta"  SortExpression="Name" HeaderStyle-Width="550px">
                        <HeaderStyle Width="463px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="correo" HeaderText="Correo" meta:resourceKey="htCorreo"  SortExpression="correo" HeaderStyle-Width="550px">
                        <HeaderStyle Width="463px"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
    </div>
</asp:Content>
