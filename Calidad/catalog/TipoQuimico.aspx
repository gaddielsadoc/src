<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TipoQuimico.aspx.cs" Inherits="catalog_TipoQuimico" ValidateRequest="false" %>
    <%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>
    <script type="text/javascript">
        $(function () {
            if ($("#<%=gvTipoQuimico.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvTipoQuimico.ClientID%>")
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h1>
            <asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
        <table class="index">
            <tr>
                <td>
                    <asp:Literal ID="Literal1" runat="server" meta:resourceKey="Literal1"></asp:Literal>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtTipoQuimico" runat="server"></asp:TextBox>
                </td>
                <td>
                <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo"></asp:Literal>
                    
                </td>
                <td>
                    <asp:CheckBox ID="chkActivo" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" Value="Añadir" ID="Accion" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" Visible="false" OnClick="Guardar_Actualizar" meta:resourceKey="btnActualizar"/>
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" OnClick="Guardar_Actualizar" meta:resourceKey="btnSave"/>
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="Cancelar_Limpiar" meta:resourceKey="btnLimpiar"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="Cancelar_Limpiar" meta:resourceKey="btnCancel"
                        Visible="False" />
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
            <asp:GridView ID="gvTipoQuimico" runat="server" AutoGenerateColumns="False" CssClass="gridView"
                DataKeyNames="idTipoQuimico" EmptyDataText="No existen registros" OnPageIndexChanging="gvTipoQuimico_PageIndexChanging"
                OnPreRender="gvTipoQuimico_PreRender" OnRowDataBound="gvTipoQuimico_RowDataBound"
                OnSelectedIndexChanged="gvTipoQuimico_SelectedIndexChanged" meta:resourceKey="gvTipoQuimico">
                <Columns>
                    <asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" meta:resourceKey="nombre" />
                </Columns>
            </asp:GridView>
        </div>
        <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
    </div>
</asp:Content>
