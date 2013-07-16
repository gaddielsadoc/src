<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmModulo.aspx.cs" Inherits="Administration_frmModulo" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            if ($("#<%=gvModulo.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvModulo.ClientID%>")
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container">

            <h1><asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
            <asp:Panel ID="form" runat="server">
                <table  class="index">
            <tr>
                <td colspan="4" align="left">
                   <h2><asp:Literal ID="ltSubtituli" meta:resourceKey="ltSubtituli" runat="server"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                <td align="right"><asp:Literal ID="ltModulo" runat="server" meta:resourceKey="ltModulo"></asp:Literal></td>
                <td><asp:TextBox runat="server" ID="txtModulo" MaxLength="50"></asp:TextBox></td>       
                <td align="right"><asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo"></asp:Literal></td>
                <td>
                    <asp:CheckBox ID="chkActivo" runat="server" Checked="True" />
                </td>             
            </tr>
            <tr>
                <td align="right"><asp:Literal ID="ltRuta" runat="server" meta:resourceKey="ltRuta"></asp:Literal></td>
                <td><asp:TextBox runat="server" ID="txtRuta" CssClass="cajaLarga" MaxLength="250"></asp:TextBox></td>
                <td align="right"><asp:Literal ID="ltOrden" runat="server" meta:resourceKey="ltOrden"></asp:Literal></td>
                <td><asp:TextBox runat="server" ID="txtOrden" MaxLength="25"></asp:TextBox></td>

            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" Value="Añadir"  id="Accion"/>
                    <asp:Button ID="btnActualizar" runat="server" meta:resourceKey="btnActualizar" 
                        Visible="false" onclick="Guardar_Actualizar"/>
                    <asp:Button ID="btnSave" runat="server" meta:resourceKey="btnSave" 
                        onclick="Guardar_Actualizar" />
                    <asp:Button ID="btnLimpiar" runat="server" meta:resourceKey="btnLimpiar" 
                        onclick="Cancelar_Limpiar"/>
                    <asp:Button ID="btnCancel" runat="server" meta:resourceKey="btnCancel"
                        onclick="Cancelar_Limpiar" Visible="False"/>
                    
                </td>
            </tr>
            
        </table>
            </asp:Panel>
            
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
                 <asp:GridView ID="gvModulo" runat="server" EnableModelValidation="True" Width="100%"
                        AutoGenerateColumns="False"  CssClass="gridView" DataKeyNames="idModulo" 
                    EmptyDataText="No existen registros"  meta:resourceKey="gvModulo"
                    onpageindexchanging="gvModulo_PageIndexChanging" 
                    onprerender="gvModulo_PreRender" onrowdatabound="gvModulo_RowDataBound" onselectedindexchanged="gvModulo_SelectedIndexChanged" 
                    >
                        <Columns>                            
                            <asp:TemplateField HeaderText="Activo"  meta:resourceKey="htActivo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" >
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblActivoGrid" runat="server"  Text='<%# (bool)Eval("activo")==true? GetLocalResourceObject("lblActivoGridSi") :GetLocalResourceObject("lblActivoGridNo") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="modulo"  HeaderText="Módulo" meta:resourceKey="bfModulo" />                            
                            <asp:BoundField DataField="ruta" HeaderText="Ruta" meta:resourceKey="bfRuta" />
                            <asp:BoundField DataField="orden" HeaderText="Orden" meta:resourceKey="bfOrden" /> 
                        </Columns>
                    </asp:GridView>
            </div>

            <uc1:popupmessagecontrol ID="popUpMessageControl1" runat="server" />
            </div>
</asp:Content>

