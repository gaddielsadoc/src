<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmRol.aspx.cs" Inherits="Administration_frmRol" ValidateRequest="false" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
     <script type="text/javascript" src="../scripts/jquery.ui.accordion.js"></script>
    <script type="text/javascript">

        $(function () {
            if ($("#<%=gvRol.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvRol.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager") ,output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}' });
            } else {
                $("#pager").hide();
            }
        });
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="container">
            <h1><asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
            <table  class="index" style="width:800px; max-width:800px; min-width:800px;">
            <tr>
                <td colspan="4" align="left">
                   <h2><asp:Literal ID="Literal1" runat="server" meta:resourceKey="Literal1"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                <td align="right" style="width:200px;">
                <asp:Literal ID="ltRol" runat="server" Text="*Rol" meta:resourceKey="ltRol"></asp:Literal></td>
                <td style="width:200px;">
                <asp:TextBox runat="server" ID="txtRol" MaxLength="45" ></asp:TextBox>
                </td> 
                 <td align="right" style="width:75px;" >
                    <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo"> </asp:Literal>
                </td>
                <td><asp:CheckBox runat="server" ID="chkRolActivo" Checked="True" /></td>
                              
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" Value="Añadir" id="Accion"/>
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" onclick="btnSaveRol_Click" Visible="false" meta:resourceKey="btnActualizar"/>
                    <asp:Button ID="btnSaveRol" runat="server"  onclick="btnSaveRol_Click" meta:resourceKey="btnSaveRol"/>
                    <asp:Button ID="btnLimpiar" runat="server" onclick="btnCancelRol_Click" meta:resourceKey="btnLimpiar"/>
                    
                   
                    <asp:Button ID="btnCancelRol" runat="server"  onclick="btnCancelRol_Click" visible="false" meta:resourceKey="btnCancelRol"/>
  
                   
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
                 <asp:GridView ID="gvRol" runat="server" EnableModelValidation="True" 
                        AutoGenerateColumns="False" Width="800px"
                    onprerender="gvRol_PreRender" CssClass="gridView" DataKeyNames="idRol" 
                    EmptyDataText="No existen registros"                     
                    onrowdatabound="gvRol_RowDataBound" 
                    onselectedindexchanged="gvRol_SelectedIndexChanged" meta:resourceKey="gvRol">
                        <Columns>
                            <asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center" meta:resourceKey="htActivo"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No") %>'
                                        Enabled="False" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rol"  SortExpression="rol" meta:resourceKey="htRol"
                                HeaderStyle-Width="550px" >
<HeaderStyle Width="463px"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
            </div>

            <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
            </div>

</asp:Content>
