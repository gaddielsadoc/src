<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Boletin.aspx.cs" Inherits="pages_Boletin" EnableEventValidation="false"  ValidateRequest="false"%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<%--<%@ Register Src="~/controls/ctrlNewQuimicoToBoletin.ascx" TagName="NewQuimicoToBoletin" TagPrefix="uc2" %>--%> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="../JS/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../JS/Utils.js" type="text/javascript"></script>
    <script src="../JS/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>   
    <script src="../JS/LocalizationDefinitions.js" type="text/javascript"></script>
    <link href="../CSS/controlesJQuery.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/overlib_mini.js"></script>
    <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />  


    <script type="text/javascript">
        function pageLoad() {
            registerCombobox();
        }

        function registerCombobox() {
            $("select[name='<%= ddlFiltro.UniqueID %>']").combobox(); 
        }


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

            if (key == 32) {
                return true;
            }

            if (key < 48 || key > 57) {
                if (key < 65 || key > 90) {
                    if (key < 97 || key > 122) {
                        return false;
                    }
                }
            }
            return true;
        }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            
      
    <div class="container">

        <%--Tutilo--%>
            <h1>
            <asp:Literal runat="server" ID="ltTitulo"  meta:resourceKey="ltTitulo">Bolet&iacute;n</asp:Literal>

            </h1> 
            <%--Tabla de boletin -cabecera--%>
            <table class="index">
                <tr>
                    <td><asp:Literal runat="server" ID="ltPlanta"  meta:resourceKey="ltPlanta" >Planta:</asp:Literal></td>
                    <td><asp:DropDownList runat="server" ID="ddlPlanta"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Literal ID="ltVersion"  meta:resourceKey="ltVersion" runat="server">*Versión:</asp:Literal></td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" CssClass="cajaLarga" onkeypress="javascript:return ValidTec( event );"></asp:TextBox>
                    </td>               
                </tr>
             </table>
             <br />
             <br />
             <table class="index">
             <tr><td colspan="3"><h2><asp:Literal runat="server" meta:resourceKey="ltBuscador">Buscador</asp:Literal></h2></td></tr>
                <tr >
                <td colspan="3">
                    <table style="width:100%">
                        <tr>
                        <td style="width:200px"></td>
                            
                            <td style="vertical-align:top;float:right">
                                <asp:Literal  runat="server" ID="ltPlaga" meta:resourceKey="ltPlaga">Plaga:</asp:Literal>
                                </td>
                            
                            <td class="buscador" >
                                <asp:DropDownList ID="ddlFiltro" style="float:left" runat="server"> </asp:DropDownList>
                                </td>    
                            
                            <td >
                                <asp:Button ID="btnBuscar" style="float:right"  meta:resourceKey="btnBuscar" runat="server" Text="Buscar"  onclick="btnBuscar_Click" />
                                </td>
                            </tr>
                        </table>
                </td>
                    
                </tr>
                <tr>
                  <td colspan="3">
                     <div class="instrucciones"> 
                        <h2><asp:Label ID="ltInstrucciones" runat="server" meta:resourceKey="ltInstrucciones" ></asp:Label></h2>
                        <br />
                        <ol>
                            <li><asp:Literal ID="ltR1" meta:resourceKey="ltR1" runat="server" > De clic en uno o m&aacute;s qu&iacute;micos de la lista de Resultados para pasarlos a la lista de Seleccionados.</asp:Literal></li>
                            <li><asp:Literal ID="ltR2" meta:resourceKey="ltR2" runat="server"> Despu&eacute;s presione el bot&oacute;n Agregar para agregar los qu&iacute;micos seleccionados al Bolet&iacute;n.</asp:Literal></li>
                            <li><asp:Literal ID="ltR3" meta:resourceKey="ltR3" runat="server"> Para quitar alg&uacute;n qu&iacute;mico de la lista de Seleccionados, de clic en &eacute;ste y aparecer&aacute; nuevamente en la lista de resultados.</asp:Literal></li> 
                         </ol>    
                     </div>
                    </td>
                
               </tr>
                <tr>

                    <td valign="top">
                          <asp:GridView ID="gdvResultados" runat="server" DataKeyNames="ITEMNMBR" 
                                CssClass="gridView"  
                                Width="250px"                     
                                AutoGenerateColumns="False"
                                onprerender="gdvResultados_PreRender"
                                EmptyDataText="No existen registros"                     
                                onrowdatabound="gdvResultados_RowDataBound" 
                                onselectedindexchanged="gdvResultados_SelectedIndexChanged"
                                meta:resourceKey="gvResultados"
                                  >
                                <Columns>
                                    <%--<asp:BoundField DataField="ITEMNMBR" />--%>
                                    <asp:BoundField DataField="ITEMDESC" HeaderText="Resultado" meta:resourceKey="htResultados"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="left"/>                                                                
                                </Columns>
                            </asp:GridView>
                    </td>
                    <td>
                    </td>
                        
                    <td valign="top">
                        <asp:GridView ID="gdvSeleccionados" runat="server"   CssClass="gridView"  Width="250px"  DataKeyNames="ITEMNMBR"          
                                AutoGenerateColumns="False"  
                                OnSelectedIndexChanged="gdvSeleccionados_SelectedIndexChanged"
                                onprerender="gdvSeleccionados_PreRender"
                                EmptyDataText="No existen registros"                     
                                onrowdatabound="gdvSeleccionados_RowDataBound" 
                                meta:resourceKey="gvSeleccionados"
                                 >
                                <Columns>
                                    <%--<asp:BoundField DataField="ITEMNMBR" />--%>
                                    <asp:BoundField DataField="ITEMDESC" HeaderText="Seleccionados" meta:resourceKey="htSeleccionados" ItemStyle-CssClass="right"/>                                                                
                                </Columns>
                            </asp:GridView>
                    </td>
                </tr> 
                <tr>
                    <td>
                        
                    </td>
                    <td></td>
                    <td>
                             
                              <asp:Button ID="btnAddToBoletin" runat="server" Text="Agregar"  meta:resourceKey="btnAgregar" onclick="btnAddToBoletin_Click" /> 
                               
                    </td>
                </tr>
                <tr>
                    <td></td><td></td>
                    <td>
                        <asp:Button ID="btnActivar" runat="server" meta:resourceKey="btnActivar" Text="Activar/Desactivar" onclick="btnActivar_Click" />
                        <asp:Button ID="btnGuardar" runat="server" meta:resourceKey="btnGuardar" Text="Guardar" onclick="btnGuardar_Click" /> 
                    </td>
                </tr>
            </table>
            <div>
    
            <%--Aqui va el grid con los datos que ya estan--%>
             
                    
     <div style="padding-top:30px;">    
                        <asp:GridView ID="gdvBoletin" runat="server"    DataKeyNames="idQuim"   
                        Width="100%" HeaderStyle-Font-Size="8pt" RowStyle-Font-Size="8pt" HeaderStyle-HorizontalAlign="Center"  
                        HeaderStyle-BorderWidth="0.25pt" HeaderStyle-BorderColor="#ADC995" HeaderStyle-BackColor="#F0F5E5" AlternatingRowStyle-BackColor="#D6DFD0"           
                                AutoGenerateColumns="False" 
                                onRowDataBound="gdvBoletin_RowDataBound"                                                     
                                onprerender="gdvBoletin_PreRender"
                                EmptyDataText="No existen registros"
                                OnRowDeleting = "gdvBoletin_RowDeleting"
                                OnSelectedIndexChanged = "gdvBoletin_SelectedIndexChanged" 
                                meta:resourceKey="gvBoletin"
                                >
                            <Columns>
                                <%--<asp:BoundField DataField="idQuim" HeaderText="ID"  />--%>
                                 <asp:BoundField DataField="TipoQuim" HeaderText="Apartado" meta:resourceKey="htApartado"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>   
                                <asp:BoundField DataField="nomQuim" HeaderText="Nombre Comercial" meta:resourceKey="htNombreComercial"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" />
                                <asp:BoundField DataField="IngredienteActivo" HeaderText="Ingrediente Activo" meta:resourceKey="htIngredienteActivo" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                <asp:BoundField DataField="plagas" HeaderText="Plagas que controla"  meta:resourceKey="htPlagasQueControla"
                                     HeaderStyle-Width="300pt" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
<ItemStyle CssClass="wrapped"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Epa180" HeaderText="EPA Ref.#180"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                <asp:BoundField DataField="ToleranciaEpaPpm" HeaderText="Tolerancia EPA. ppm" meta:resourceKey="htTolerancia" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                <asp:BoundField DataField="IntervaloSeguridadCosecha" HeaderText="Intervalo de seguridad a cosecha (días)" meta:resourceKey="htIntervaloDeSeguridad" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                <asp:BoundField DataField="TiempoReentrada" HeaderText="Tiempo de reentrada (hrs)" meta:resourceKey="htTiempoReentrada" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                <asp:BoundField DataField="Dosis" HeaderText="Dosis" meta:resourceKey="htDosis" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>

                                <%-- INDICACIONES --%>
                                <asp:TemplateField HeaderText="Indicaciones" meta:resourceKey="htInidicaciones"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="None"
 ItemStyle-BorderStyle="None"                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="textoIndicaciones" runat="server" NavigateUrl='#' Text='<%# Eval("Indicaciones")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <%-- INDICACIONES --%>	
                                <%--<asp:BoundField DataField="Indicaciones" HeaderText="Indicaciones" HeaderStyle-Width="300pt" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
<ItemStyle></ItemStyle>
                                </asp:BoundField>--%>
                                
                                <asp:BoundField DataField="GrupoQuimico" meta:resourceKey="htGrupoQuimico" HeaderText="Grupo químico"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" />
                                 <asp:BoundField DataField="maxima" meta:resourceKey="htMaximaAplicacion" HeaderText="Máxima aplicación"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                  <asp:BoundField DataField="Abejorro" meta:resourceKey="htCompatibleAbejorros"  HeaderText="Compatible abejorros"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                <asp:BoundField DataField="PersistenciaDias" meta:resourceKey="htPersistencia" HeaderText="Persistencia en Días"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none"/>
                                
                            <asp:TemplateField  ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">    
                                <ItemTemplate>                                         
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="delete" src="../comun/img/delete.png" OnClick="imgDeleteNominee_Click" Width="22px" 
                                    Visible='<%# string.Compare((string)Eval("activo"), "1", false) == 0 ? false : true %>'
                                    />
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   
          

      
</div> 
    </div>
      
    </div> <%--container--%>
    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

