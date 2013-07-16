<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmRazonesCambio.aspx.cs" Inherits="catalog_frmRazonesCambio" EnableEventValidation="false" ValidateRequest="false" %>
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
            if ($("#<%=gvRazon.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvRazon.ClientID%>")
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


    <style type="text/css">
        .style1
        {
            width: 120px;
            height: 5px;
        }
        .style2
        {
            height: 5px;
        }
    </style>


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
    <asp:Literal ID="ltBienvenido" runat="server" meta:resourceKey="ltBienvenido">Cátalogo de cargas</asp:Literal></h1>
    <table class="index">
        <tr>
            <td class="style1">
                <asp:Literal ID="ltPlanta" runat="server" meta:resourceKey="ltPlanta">*Planta</asp:Literal> 
             </td>         
            <td class="style2">
                <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true" > </asp:DropDownList>   
            </td>

            <td class="style1">
                <asp:Literal ID="ltRazon" runat="server" meta:resourceKey="ltRazon">*Razón</asp:Literal> 
             </td>         
            <td class="style2">
                <asp:TextBox ID="txtRazon" runat="server" MaxLength="100" Height="16px" 
                    Width="255px"></asp:TextBox>  
            </td>
            <td class="style2">
                <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo">Activo?</asp:Literal>               
            </td>
            <td class="style2"><asp:CheckBox ID="chkActivo" runat="server" Checked="true" /></td>
        </tr>
       
        <tr>
            <td colspan="4" align="right">
                <asp:HiddenField runat="server" Value="0" id="hdIdRazon"/>
                <asp:Button ID="btnActualizar" meta:resourceKey="btnActualizar" runat="server" Text="Actualizar" Visible="false" onclick="Guardar_Actualizar"/>
                <asp:Button ID="btnSave" meta:resourceKey="btnSave" runat="server" Text="Guardar" onclick="Guardar_Actualizar" />
                <asp:Button ID="btnLimpiar" meta:resourceKey="btnLimpiar" runat="server" Text="Limpiar" onclick="Cancelar_Limpiar"/>
                <asp:Button ID="btnCancel" meta:resourceKey="btnCancel" runat="server" Text="Cancelar" onclick="Cancelar_Limpiar" Visible="False"/>
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
            <asp:GridView ID="gvRazon" 
                meta:resourceKey="gvRazon"
                runat="server" 
                AutoGenerateColumns="False" 
                CssClass="gridView" 
                DataKeyNames="idRazonCambio" 
                EmptyDataText="No existen registros" 
                Width="500px"
                onprerender="gvRazon_PreRender" 
                onrowdatabound="gvRazon_RowDataBound" 
                onselectedindexchanged="gvRazon_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="planta" HeaderText="planta" meta:resourceKey="htplanta" HeaderStyle-Width="200px" SortExpression="planta" />
                    <asp:BoundField DataField="vRazon" HeaderText="Razon" meta:resourceKey="htRazon" HeaderStyle-Width="200px" SortExpression="vRazon" />                            
                    <asp:CheckBoxField DataField="bActivo" HeaderText="Activo" meta:resourceKey="htActivo"  HeaderStyle-Width="100px" SortExpression="bActivo"/>
                              
                </Columns>
            </asp:GridView>
    </div>


    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

    </div>

</asp:Content>
