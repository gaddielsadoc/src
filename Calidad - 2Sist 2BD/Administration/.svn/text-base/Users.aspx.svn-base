<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Users.aspx.cs" Inherits="Administration_Users" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
        <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript">

        $(function () {
            if ($("#<%=grViewPendings.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=grViewPendings.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] } 
                    })
                    .tablesorterPager({ container: $("#pager"),output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}' });
            } else {
                $("#pager").hide();
            }
        });
    </script>
        
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                    
                         <div class="container">
                                     <h1>   <asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
                                <table width="100%" class="index">
                                    <tr>
                                        <td align="left" colspan="4">
                                            <h2><asp:Literal ID="Literal1" runat="server" Text="" meta:resourceKey="Literal1"></asp:Literal></h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 210px;">
                                            <asp:Literal ID="ltCuenta" runat="server" meta:resourceKey="ltCuenta"></asp:Literal>
                                        </td>
                                        <td align="left" class="style1">
                                            <asp:TextBox ID="txtCuenta" runat="server" MaxLength="20" AutoPostBack="true"
                                                ontextchanged="txtCuenta_TextChanged"></asp:TextBox>
                                        </td>
                                        <td align="right" >
                                            <asp:Literal ID="lbDescricion" runat="server" meta:resourceKey="lbDescricion"></asp:Literal>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlTipo" runat="server" AppendDataBoundItems="true">                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">        <%--<asp:Literal ID="Literal2" runat="server">Centro de costo:</asp:Literal>--%></td>
                                        <td rowspan="4" class="floatnone">      
                                            <asp:CheckBoxList ID="ddlPlanta" runat="server" RepeatColumns="2">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="ltNombre" runat="server" meta:resourceKey="ltNombre"></asp:Literal>
                                        </td>
                                        <td align="left" colspan="3" style="text-align:left;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Literal ID="ltNombreUsuario" runat="server"></asp:Literal>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtCuenta" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="Literal3" runat="server" meta:resourceKey="Literal3"></asp:Literal>
                                        </td>
                                        <td align="left" colspan="3" style="text-align:left;">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="ltEmail" runat="server"></asp:TextBox>                                                    
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtCuenta" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo"></asp:Literal></td>
                                        <td align="left" class="style1">
                                            <asp:CheckBox ID="checkActivo" runat="server" Checked="true" /></td>
                                        <td align="right">
                                    
                                        </td>
                                        <td align="left">
                                      
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right">
                                            <asp:Literal ID="ltMateriales" meta:resourceKey="ltMateriales" Text="Materiales" runat="server" />
                                        </td>
                                        <td colspan="4">
                                            <asp:CheckBoxList runat="server" ID="chlMateriales" RepeatDirection="Horizontal">                                                   
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                            <asp:HiddenField ID="hdIdItem" runat="server" />
                                        </td>  
                                        <td align="left" >
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" meta:resourceKey="btnCancelar" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" meta:resourceKey="btnGuardar"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                        
                            <div class="grid">
                                <div id="pager" class="pager">
            <img alt="first" src="../comun/img/first.png" class="first" />
            <img alt="prev" src="../comun/img/prev.png" class="prev" />
            <input type="text" class="pagedisplay"/>
            <img alt="next" src="../comun/img/next.png" class="next" />
            <img alt="last" src="../comun/img/last.png" class="last" />
            <select class="pagesize cajaCh" style="width:50px; min-width:50px; max-width:50px;">
                <option value="10">10</option>
                <option value="20">20</option>
                <option value="30">30</option>
                <option value="40">40</option>
                <option value="50">50</option>
            </select>
        </div>
                                        <asp:GridView ID="grViewPendings" runat="server" AutoGenerateColumns="False" CssClass="gridView"
                                            HeaderStyle-CssClass="gridViewHeader" AlternatingRowStyle-CssClass="gridViewAlt" Width="800px"
                                            AllowPaging="False" AllowSorting="False" EmptyDataText="No existen registros con Usuarios NS"
                                            EmptyDataRowStyle-CssClass="gridEmptyData" 
                                            OnSorting="grViewPendings_Sorting" PageSize="10"
                                            PagerStyle-CssClass="gridViewPagerStyle" DataKeyNames="idUsuario" OnSelectedIndexChanged="grViewPendings_SelectedIndexChanged"
                                            OnRowDataBound="grViewPendings_RowDataBound" 
                                            OnRowDeleting="grViewPendings_RowDeleting" 
                                            onpageindexchanging="grViewPendings_PageIndexChanging" 
                                            onprerender="grViewPendings_PreRender" meta:resourceKey="grViewPendings">
                                            <AlternatingRowStyle CssClass="gridViewAlt"></AlternatingRowStyle>
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
                                                <asp:BoundField HeaderText="Nombre" DataField="nombre" SortExpression="nombre"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="280px" meta:resourceKey="htNombre">
                                                    <HeaderStyle Width="280px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Cuenta" DataField="usuario" SortExpression="usuario" meta:resourceKey="htCuenta"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Tipo" DataField="Tipo_Usr" SortExpression="Tipo_Usr" meta:resourceKey="htTipo"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="idUsuario" ReadOnly="True" Visible="false" />
                                                <%--<asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Borrar" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-Width="80px" />--%>                                                
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="gridEmptyData"></EmptyDataRowStyle>
                                            <HeaderStyle CssClass="gridViewHeader"></HeaderStyle>
                                            <PagerStyle CssClass="gridViewPagerStyle"></PagerStyle>
                                        </asp:GridView>                                   
                            </div>
                            
                            <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
                            </div>
                       
</asp:Content>

