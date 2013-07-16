<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmSubModulo.aspx.cs" Inherits="Administration_frmSubModulo" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript">

        $(function () {
            //registerControls();
        });

        function registerControls() {
            if ($("#<%=gvSubM.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvSubM.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"),output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}'  });
            } else {
                $("#pager").hide();
            }
        }
 
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
            <h1><asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                  <asp:Panel ID="form" runat="server">
                     <table  class="index">
            <tr>
                <td colspan="4" align="left">
                   <h2>
                   <asp:Literal ID="ltSubtituli" runat="server"  meta:resourceKey="ltSubtituli"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                <td align="right"><asp:Literal ID="ltM" runat="server" Text="*Módulo" meta:resourceKey="ltM"></asp:Literal></td>
                <td align="left"><asp:DropDownList runat="server" ID="ddlModulo" DataTextField="modulo" DataValueField="idModulo" AutoPostBack="True" 
                        ondatabound="ddlModulo_DataBound" CssClass="cajaLarga" 
                        onselectedindexchanged="ddlModulo_SelectedIndexChanged" ></asp:DropDownList></td>                  
                <td align="right"><asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo"></asp:Literal></td>
                <td align="left"><asp:CheckBox runat="server" ID="chkActivo" Checked="True" /></td>     
                           
            </tr>
            <tr>
                <td align="right"><asp:Literal ID="ltSubMP" runat="server"  meta:resourceKey="ltSubMP"></asp:Literal></td>
                <td align="left"><asp:DropDownList runat="server" ID="ddlSunMP" DataTextField="subModulo" CssClas="cajaLarga"
                        DataValueField="idSubModulo" ondatabound="ddlSunMP_DataBound">
                            <asp:ListItem Text="Ninguno" Value="" meta:resourceKey="Ninguno"></asp:ListItem>
                        </asp:DropDownList></td>    
                <td align="right"><asp:Literal ID="ltSubM" runat="server"  meta:resourceKey="ltSubM"></asp:Literal></td>
                <td align="left"><asp:TextBox CssClass="cajaLarga" runat="server" ID="txtSubM" MaxLength="45"></asp:TextBox></td>  
            </tr>
            <tr>
                <td align="right"><asp:Literal runat="server" ID="ltRuta" meta:resourceKey="ltRuta">*Ruta</asp:Literal></td>
                <td colspan="3" align="left"><asp:TextBox runat="server" ID="txtRuta" 
                        CssClass="cajaLarga"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" Value="Añadir" id="Accion"/>
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" 
                        onclick="btnSave_Click" Visible="false" meta:resourceKey="btnActualizar"/>
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" 
                        onclick="btnSave_Click" meta:resourceKey="btnSave" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" 
                        onclick="btnCancel_Click" meta:resourceKey="btnLimpiar"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" meta:resourceKey="btnCancel" 
                        onclick="btnCancel_Click" visible="false"/>
                    
                </td>
            </tr>
            
        </table>
                  </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(registerControls);
                </script>
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
                 <asp:GridView ID="gvSubM" runat="server" EnableModelValidation="True" Width="800px"
                        AutoGenerateColumns="False"  CssClass="gridView" DataKeyNames="idSubModulo" 
                    EmptyDataText="No existen registros" 
                    onprerender="gvSubM_PreRender" onrowdatabound="gvSubM_RowDataBound" 
                    onselectedindexchanged="gvSubM_SelectedIndexChanged" meta:resourceKey="gvSubM">
                        <Columns>
                            <asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120px" meta:resourceKey="htActivo">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No") %>'
                                        Enabled="False" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="modulo" HeaderText="Módulo" meta:resourceKey="bfModulo"/>
                            <asp:BoundField HeaderText="S. Padre" DataField="parent_subModulo" meta:resourceKey="bfParent_subModulo"/>
                            <asp:BoundField HeaderText="Submódulo" DataField="subModulo" meta:resourceKey="bfSubModulo"/>
                            <asp:BoundField DataField="ruta" HeaderText="Ruta" meta:resourceKey="bfRuta"/>
                        </Columns>
                    </asp:GridView>
            </div>
             </ContentTemplate>
            </asp:UpdatePanel>
            <uc1:popupmessagecontrol ID="popUpMessageControl1" runat="server" />
            </div>
</asp:Content>
