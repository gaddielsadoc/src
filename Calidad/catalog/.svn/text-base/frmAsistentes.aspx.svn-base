<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAsistentes.aspx.cs" Inherits="catalog_frmAsistentes" EnableEventValidation="false"  %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl"    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
        <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript">

        $(function () {
            if ($("#<%=gdvAsistentes.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gdvAsistentes.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"), output: '{page} ' + '<%= (string)GetGlobalResourceObject("Commun","de")%>' + ' {totalPages}' });
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
                        <asp:Literal ID="ltGrower" runat="server" meta:resourceKey="ltGrower"></asp:Literal>
                    </td>
                    <td align="left" class="style1">
                        <asp:DropDownList ID="ddlGrowers" runat="server" AutoPostBack="true" 
                            onselectedindexchanged="ddlGrowers_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" >
                        <asp:Literal ID="ltAsistentes" runat="server" meta:resourceKey="ltAsistentes"></asp:Literal>
                    </td>
                    <td align="left">
                        <asp:CheckBoxList ID="ckListAsistentes" runat="server">
                        </asp:CheckBoxList>
                    </td>
                                        
                </tr>
                                   
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
        <table align="center">
        <tr><td>
            <asp:GridView ID="gdvAsistentes" runat="server" AutoGenerateColumns="False" Width="570px"
                    DataKeyNames="idUsuario"  CssClass="gridView" HeaderStyle-CssClass="wrapped"                
                    onRowDataBound="gdvAsistentes_RowDataBound"                                                     
                    onprerender="gdvAsistentes_PreRender"
                    EmptyDataText="No existen registros" EnableModelValidation="True"
                    meta:resourceKey="gv" 
                onselectedindexchanged="gdvAsistentes_SelectedIndexChanged">
                <Columns> 
                    
                    <asp:BoundField DataField="usuario" meta:resourceKey="grower" SortExpression="usurio" /> 
                    <asp:BoundField DataField="asistentes" meta:resourceKey="asistentes" SortExpression="asistentes" /> 
                </Columns>
            </asp:GridView>
        </td></tr>
        </table>
        </div>                                                               
    
       </div>                     
      <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
                           
                       
</asp:Content>


