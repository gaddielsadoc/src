<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmPlagas.aspx.cs" Inherits="catalog_frmPlagas" ValidateRequest="false" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>    
    <script type="text/javascript">

        $(function () {
            if ($("#<%=gvPlaga.ClientID%>").find("tbody").find("tr").size() > 1) {
                
                $("#<%=gvPlaga.ClientID%>")
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
            <asp:Literal ID="lblBienvenido" meta:resourceKey="lblBienvenido" runat="server">Administraci&oacute;n de Plagas</asp:Literal></h1>
    <table class="index">
        <tr>
            <td style="width:120px"><asp:Literal ID="ltNombreCiencia" meta:resourceKey="ltNombreCiencia" runat="server">*Nombre cient&iacute;fico</asp:Literal> </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="cajaLarga"></asp:TextBox>
            </td>
            <td><asp:Literal ID="ltActivo" meta:resourceKey="ltActivo" runat="server">Activo?</asp:Literal></td>
            <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" /></td>

        </tr>
        <tr>
            <td><asp:Literal ID="ltNombreComun" meta:resourceKey="ltNombreComun" runat="server">*Nombre com&uacute;n</asp:Literal></td>
            <td>
                <asp:TextBox ID="txtNombreComun" runat="server" CssClass="cajaLarga"></asp:TextBox>
            </td>
            <td><asp:Literal ID="ltTipo" meta:resourceKey="ltTipo" runat="server">Tipo</asp:Literal> </td>
            <td class="checkboxes">
                <asp:RadioButton ID="rbPlaga"  meta:resourceKey="rbPlaga" GroupName="tipo" runat="server" Text="Plaga" Checked="true" />
                <asp:RadioButton ID="rbEnfermedad" meta:resourceKey="rbEnfermedad" GroupName="tipo" runat="server" Text="Enfermedad" />
            </td>

        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:HiddenField runat="server" Value="Añadir" id="Accion"/>
                <asp:Button ID="btnActualizar"  meta:resourceKey="btnActualizar"  runat="server" Text="Actualizar" Visible="false" onclick="Guardar_Actualizar"/>
                <asp:Button ID="btnSave"  meta:resourceKey="btnSave" runat="server" Text="Guardar" onclick="Guardar_Actualizar" />
                <asp:Button ID="btnLimpiar"  meta:resourceKey="btnLimpiar" runat="server" Text="Limpiar" onclick="Cancelar_Limpiar"/>
                <asp:Button ID="btnCancel"  meta:resourceKey="btnCancel" runat="server" Text="Cancelar" onclick="Cancelar_Limpiar" Visible="False"/>
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
            <asp:GridView 
                ID="gvPlaga" 
                meta:resourceKey="gvPlaga"
                runat="server" 
                AutoGenerateColumns="False" 
                CssClass="gridView" 
                DataKeyNames="idPlaga" 
                EmptyDataText="No existen registros" 
                Width="800px"
                onpageindexchanging="gvPlaga_PageIndexChanging" 
                onprerender="gvPlaga_PreRender" 
                onrowdatabound="gvPlaga_RowDataBound" 
                onselectedindexchanged="gvPlaga_SelectedIndexChanged" 
            >
                <Columns>
                    <asp:BoundField DataField="nombreCientifico" meta:resourceKey="htNombreCientifico"  HeaderText="Nombre Cientifico" HeaderStyle-Width="200px" />                            
                    <asp:BoundField DataField="nombreComun" meta:resourceKey="htNombreComun" HeaderText="Nombre Comun" HeaderStyle-CssClass="wrap"  ItemStyle-CssClass="wrap" HeaderStyle-Width="200px"/>   
                    <%--<asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")?"Sí":"No" %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Tipo" meta:resourceKey="htTipo"  SortExpression="tipo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("esPlaga") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEsPlagaGrid" runat="server" Text='<%# (bool)Eval("esPlaga")?(string)GetLocalResourceObject("Plaga") :(string)GetLocalResourceObject("Enfermedad") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>  
                    <asp:CheckBoxField DataField="activo" HeaderText="Activo" meta:resourceKey="htActivo"  HeaderStyle-Width="100px"/>
                                                                       
                </Columns>
            </asp:GridView>
    </div>

    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

    </div>

</asp:Content>

