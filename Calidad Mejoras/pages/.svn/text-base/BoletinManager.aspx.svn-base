<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BoletinManager.aspx.cs" Inherits="pages_BoletinManager" EnableEventValidation="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>
    <script type="text/javascript">

        function pageLoad() {

            $(function () {
                if ($("#<%=gdvBoletinManager.ClientID%>").find("tbody").find("tr").size() > 1) {

                    $("#<%=gdvBoletinManager.ClientID%>")
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
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel runat="server">
<ContentTemplate>
 <%--Tutilo--%>
   <div class="container">
        <h1>
            <asp:Literal runat="server" ID="ltTitulo"  meta:resourceKey="ltTitulo"> Administrador de Bolet&iacute;nes </asp:Literal>           </h1>
                
            </h1>
    <%--    <table class="index">
    
     <tr>
            <td>
                <a href="Boletin.aspx">Nuevo...</a>
            </td>
       </tr>
     </table>--%>
  
    <table class="index">
                <tr>
                    <td><asp:Literal ID="ltPlanta" runat="server" meta:resourceKey="ltPlanta">Planta:</asp:Literal></td>
                    <td><asp:DropDownList runat="server" ID="ddlPlanta" AutoPostBack="true" 
                            onselectedindexchanged="ddlPlanta_SelectedIndexChanged"></asp:DropDownList></td>
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

                        <asp:GridView ID="gdvBoletinManager"
                                meta:resourceKey="gdvBoletinManager" 
                                runat="server"   
                                CssClass="gridView" 
                                DataKeyNames="idBoletinHeader"                     
                                AutoGenerateColumns="False" 
                                Width="800px" 
                                onRowDataBound="gdvBoletinManager_RowDataBound"
                                onprerender="gdvBoletinManager_PreRender"
                                EmptyDataText="No existen registros" 
                                EnableModelValidation="True"
                                OnSelectedIndexChanged ="gdvBoletinManager_SelectedIndexChanged"                    
                                >
                            <Columns>
                               <%-- <asp:BoundField DataField="idBoletinHeader" HeaderText="ID"  />--%>
                                 <asp:BoundField DataField="vNombreBoletinHeader" HeaderText="Nombre" meta:resourceKey="htNombre" HeaderStyle-Width="350px"/>   
                               <%-- <asp:BoundField DataField="dateFechaInicioBoletinHeader" HeaderText="Fecha Inicio"   />
                                <asp:BoundField DataField="dateFechaFinBoletinHeader" HeaderText="Fecha Fin" />--%>
                                 <asp:BoundField DataField="vUserCreacionBoletinHeader" HeaderText="Creado por"  meta:resourceKey="htCreatedBy" HeaderStyle-Width="350px" />                                                                                                                          
                                <asp:CheckBoxField DataField="iEstatusBoletinHeader" HeaderText="Activo" meta:resourceKey="htActive"  HeaderStyle-Width="100px"/>                                                                                 
                            </Columns>
                        </asp:GridView>
           <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" meta:resourceKey="btnNuevo" onclick="btnNuevo_Click" />
    </div>

  
   </div>  
<uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnNuevo" />
        <asp:PostBackTrigger ControlID="gdvBoletinManager" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>

