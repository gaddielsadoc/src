<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BoletinPublicado.aspx.cs" Inherits="pages_BoletinPublicado" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../Scripts/overlib_mini.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container">

        <%--Tutilo--%>
            <h1 style="width:800px; min-width:800px; display:table; margin-left:auto; margin-right:auto; max-width:800px;"> 
               <asp:Literal runat="server" ID="ltTitulo" meta:resourceKey="ltTitulo">Bolet&iacute;n Publicado</asp:Literal>                
            </h1> 
            <%--Tabla de boletin -cabecera--%>
            <table class="index">
                <tr>
                    <td><asp:Literal ID="ltPlanta" meta:resourceKey="ltPlanta" runat="server">Planta:</asp:Literal></td>
                    <td><asp:DropDownList runat="server" ID="ddlPlanta" AutoPostBack="true" 
                            onselectedindexchanged="ddlPlanta_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><asp:Literal ID="ltNombre" meta:resourceKey="ltNombre" runat="server">*Nombre:</asp:Literal></td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="300" CssClass="cajaLarga" Enabled="false"></asp:TextBox>
                    </td>               
                       <td >
                        <asp:Button ID="btbExport" runat="server" meta:resourceKey="xport" Text="Exportar PDF" onclick="btbExport_Click" Visible="False"/>
                    </td>
                    
                    <td >
                        <asp:Button ID="bntEnviar" meta:resourceKey="btnEnviar" runat="server" Text="Enviar Mail" 
                            onclick="bntEnviar_Click"  />
                    </td>
                </tr>  
              
             </table>

             <br />
             <br />
            

      <div>
    
            <%--Aqui va el grid con los datos que ya estan--%>
             
                    
          
           <asp:GridView ID="gdvBoletin" runat="server"
                onRowDataBound="gdvBoletin_RowDataBound"                                                     
                onprerender="gdvBoletin_PreRender"
                DataKeyNames="idQuim"   
                Width="100%" 
                HeaderStyle-Font-Size="8pt" 
                HeaderStyle-HorizontalAlign="Center"   
                HeaderStyle-BorderColor="#ADC995"                 
                HeaderStyle-BorderWidth="0.25pt"
                HeaderStyle-BackColor="#F0F5E5" 
                RowStyle-Font-Size="8pt" 
                AlternatingRowStyle-BackColor="#D6DFD0"           
                AutoGenerateColumns="False"                                
                EmptyDataText="No existen registros" 
                meta:resourceKey="gvBoletin"
                EnableModelValidation="True" >
                <AlternatingRowStyle BackColor="#D6DFD0"></AlternatingRowStyle>
                 <Columns>
                    <%--<asp:BoundField DataField="idQuim" HeaderText="ID"  />--%>
                        <asp:BoundField DataField="TipoQuim" HeaderText="Apartado" meta:resourceKey="htApartado"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">   
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="nomQuim" HeaderText="Nombre Comercial"  meta:resourceKey="htNombreComercial" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="IngredienteActivo" meta:resourceKey="htIngredienteActivo" HeaderText="Ingrediente Activo" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="plagas" meta:resourceKey="htPlagasQueControla" HeaderText="Plagas que controla" HeaderStyle-Width="300pt" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="300pt"></HeaderStyle>
                                <ItemStyle CssClass="wrapped"></ItemStyle>
                         </asp:BoundField>
                         <asp:BoundField DataField="Epa180" HeaderText="EPA Ref.#180"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="ToleranciaEpaPpm" meta:resourceKey="htTolerancia" HeaderText="Tolerancia EPA. ppm" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="IntervaloSeguridadCosecha" meta:resourceKey="htIntervaloDeSeguridad" HeaderText="Intervalo de seguridad a cosecha (días)" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="TiempoReentrada" meta:resourceKey="htTiempoReentrada" HeaderText="Tiempo de reentrada (hrs)" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Dosis" meta:resourceKey="htDosis" HeaderText="Dosis" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                         </asp:BoundField>
                         <%-- INDICACIONES --%>
                        <asp:TemplateField HeaderText="Indicaciones" meta:resourceKey="htIndicaciones"  HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="None" ItemStyle-BorderStyle="None"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                            <ItemTemplate>
                                <asp:HyperLink ID="textoIndicaciones" runat="server" NavigateUrl='#' Text='<%# Eval("Indicaciones")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GrupoQuimico" meta:resourceKey="htGrupoQuimico" HeaderText="Grupo químico"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="maxima" meta:resourceKey="htMaximaAplicacion" HeaderText="Máxima aplicación"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Abejorro" meta:resourceKey="htCompatibleAbejorros" HeaderText="Compatible abejorros" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PersistenciaDias" meta:resourceKey="htPersistencia" HeaderText="Persistencia en Días" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>                               
                            </ItemTemplate>

<HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>
                        
                        <asp:TemplateField  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" ></asp:HyperLink>                              
                            </ItemTemplate>

<HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>
                        
                        <asp:TemplateField  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" ></asp:HyperLink>                                
                            </ItemTemplate>

<HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:TemplateField>    
                               
                       <%-- <asp:TemplateField  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFichaTecnica" runat="server" CommandName="Select" Text="Ficha Técnica" OnClick="lnkFichaTecnica_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        
                        <asp:TemplateField  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkHSeguridad" runat="server" CommandName="Select" Text="H. Seguridad" OnClick="lnkHSeguridad_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        
                        <asp:TemplateField  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCofepris" runat="server" CommandName="Select" Text="Reg. COFEPRIS" OnClick="lnkCofepris_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>                            
                </Columns>
                <HeaderStyle HorizontalAlign="Center" BackColor="#F0F5E5" BorderColor="#ADC995" BorderWidth="0.25pt" Font-Size="8pt"></HeaderStyle>
                <RowStyle Font-Size="8pt"></RowStyle>
        </asp:GridView>
                   
          
          <asp:GridView ID="GridViewMail" runat="server"
                onprerender="gdvBoletin_PreRender"
                DataKeyNames="idQuim"   
                Width="100%" 
                HeaderStyle-Font-Size="8pt" 
                HeaderStyle-HorizontalAlign="Center"   
                HeaderStyle-BorderColor="#ADC995"                 
                HeaderStyle-BorderWidth="0.25pt"
                HeaderStyle-BackColor="#F0F5E5" 
                RowStyle-Font-Size="8pt" 
                AlternatingRowStyle-BackColor="#D6DFD0"           
                AutoGenerateColumns="False"                                
                EmptyDataText="No existen registros" 
                EnableModelValidation="True"
                Visible="false"
                >
                <AlternatingRowStyle BackColor="#D6DFD0"></AlternatingRowStyle>
                 <Columns>
                    <%--<asp:BoundField DataField="idQuim" HeaderText="ID"  />--%>
                        <asp:BoundField DataField="TipoQuim" meta:resourceKey="htApartado" HeaderText="Apartado"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">   
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="nomQuim" meta:resourceKey="htNombreComercial" HeaderText="Nombre Comercial" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="IngredienteActivo" meta:resourceKey="htIngredienteActivo" HeaderText="Ingrediente Activo" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="plagas" HeaderText="Plagas que controla" meta:resourceKey="htPlagasQueControla" HeaderStyle-Width="300pt" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="300pt"></HeaderStyle>
                                <ItemStyle CssClass="wrapped"></ItemStyle>
                         </asp:BoundField>
                         <asp:BoundField DataField="Epa180" HeaderText="EPA Ref.#180"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="ToleranciaEpaPpm" HeaderText="Tolerancia EPA. ppm" meta:resourceKey="htTolerancia" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="IntervaloSeguridadCosecha" HeaderText="Intervalo de seguridad a cosecha (días)" meta:resourceKey="htIntervaloDeSeguridad" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="TiempoReentrada" HeaderText="Tiempo de reentrada (hrs)" meta:resourceKey="htTiempoReentrada" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Dosis" HeaderText="Dosis" meta:resourceKey="htDosis" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                         </asp:BoundField>
                       <asp:BoundField DataField="Indicaciones" HeaderText="Indicaciones" meta:resourceKey="htIndicaciones" HeaderStyle-Width="300pt" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="300pt"></HeaderStyle>
                                <ItemStyle></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="GrupoQuimico" HeaderText="Grupo químico" meta:resourceKey="htGrupoQuimico"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none" >
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="maxima" HeaderText="Máxima aplicación" meta:resourceKey="htMaximaAplicacion"  HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Abejorro" HeaderText="Compatible abejorros" meta:resourceKey="htCompatibleAbejorros" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PersistenciaDias" HeaderText="Persistencia en Días" meta:resourceKey="htPersistencia" HeaderStyle-Width="150pt" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderStyle="none" ItemStyle-BorderStyle="none">
                                <HeaderStyle BorderStyle="None" Width="150pt"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None"></ItemStyle>
                        </asp:BoundField>
                                            
                </Columns>
                <HeaderStyle HorizontalAlign="Center" BackColor="#F0F5E5" BorderColor="#ADC995" BorderWidth="0.25pt" Font-Size="8pt"></HeaderStyle>
                <RowStyle Font-Size="8pt"></RowStyle>
        </asp:GridView>
                   
       

    </div>

 </div> 

 <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

</asp:Content>

