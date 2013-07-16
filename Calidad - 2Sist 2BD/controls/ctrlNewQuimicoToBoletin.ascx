<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlNewQuimicoToBoletin.ascx.cs" Inherits="controls_ctrlNewQuimicoToBoletin" %>

<table >
        <tr >
            <td >
                <table >
                    <tr>
                        <td colspan="7" align ="center">
                            <div class="category">
                                <span>AGREGAR UN NUEVO QU&Iacute;MICO</span>
                            </div>
                            <hr />            
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <%--Nombre--%>
                        <td align="left">
                            <span>Nombre: </span>
                        </td>
                        <td align="left" colspan="2" >
                             <asp:DropDownList ID="ddlNombre" runat="server"> </asp:DropDownList>
                        </td>
                        <td rowspan="7" colspan="4" align="center">
                            <img src="../comun/img/quim1.jpg" style="height: 128px; width: 127px"  />
                        </td>
                    </tr>
                    <tr>
                        <%--Tipo:--%>
                        <td align="left">
                            <span>Tipo: </span>
                        </td>
                        <td align="left" colspan="2">
                           <asp:DropDownList ID="ddlTipo" runat="server"> </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <%--I. Activo:--%>
                        <td align="left">
                            <span>Ingrediente Activo: </span>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtIActivo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <%--Grupo quimico:--%>
                        <td align="left">
                            <span>Grupo químico: </span>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtGrupo" runat="server"></asp:TextBox>
                        </td>
                    
                    </tr>
                    <tr>
                        <%--EPA Ref #180:--%>
                        <td align="left">
                            <span >EPA Ref #180: </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt180" runat="server" Width="35px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>   
                        <%--Tolerancia EPA ppm:--%>
                        <td align="left" >
                            <span>Tol. EPA ppm: </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtppm" runat="server" Width="35px" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <%--Persistencia--%>
                        <td align="left">
                            <span>Persitencia en días: </span>
                        </td> 
                        <td align="left">
                            <asp:TextBox ID="txtPersistencia" runat="server" Width="35px"></asp:TextBox>
                        </td>  
                     </tr>  
                    <tr>
                        <%--Intervalo Seguridad a Cocecha:--%>
                         <td align="left" >
                            <span>Intervalo Seguridad Cocecha en  d&iacute;as: </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCocecha" runat="server" Width="35px" ></asp:TextBox> 
                        </td>                        
                    </tr>                     
                    <tr>
                         <%--Tiempo Reentrada:--%>
                        <td align="left">
                            <span>T. Reentrada en horas: </span>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtReentrada" runat="server" Width="35px"></asp:TextBox>
                        </td>                        
                    </tr>
                    <tr >
                         <%--Dosis / Unidad--%>
                        <td align="left">
                            <span>Dosis Inferior: </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDosisInf" runat="server" Width="35px"></asp:TextBox>
                      </td>
                       <td align="left">
                            Superior:<asp:TextBox ID="txtDosisSup" runat="server" Width="35px"></asp:TextBox>
                      </td>
                      <td></td>
                      <td>                      
                            <asp:DropDownList ID="ddlDosisNumerador" runat="server"></asp:DropDownList>
                      </td>
                      <td> <b> / </b></td>
                      <td colspan="2">                            
                            <asp:DropDownList ID="ddlDosisDenominador" runat="server" ></asp:DropDownList>
                       </td>  
                    </tr>
                     <tr>
                          <%--Maxima aplicacion--%>
                        <td align="left">
                            <span>M&aacute;xima Aplicaci&oacute;n: </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMax" runat="server" Width="35px"></asp:TextBox> 
                        </td>
                        <td>                      
                            <asp:DropDownList ID="ddlMaximaNumerador" runat="server" ></asp:DropDownList>
                        </td>
                        <td > <b> / </b></td>
                    
                        <td align="left">
                            <asp:DropDownList ID="ddlMaximaDenominador" runat="server" ></asp:DropDownList>
                        </td>                                                
                        <td colspan="2"> <b> por </b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMaximaPeriodo" runat="server" ></asp:DropDownList>
                        </td>
                                                 
                     </tr>
                      <%--Abejorros: --%>
                     <tr>
                         <td align="left" >
                            <span>Compatibilidad abejorros: </span>
                        </td>  
                        <td align="left" colspan="2" >
                            <asp:DropDownList ID="ddlAbejorros" runat="server"></asp:DropDownList>
                        </td> 
                     </tr>
                      <%--Plagas que controla: --%>
                     <tr>
                         <td align="left" colspan="3">
                            <span>Plagas que controla: </span>
                        </td>                    
                     </tr>   
                     <tr>
                        <td align="left" colspan="4">
                            <asp:DropDownList ID="ddlPlaga" runat="server"></asp:DropDownList>
                        </td> 
                     </tr>
                
                     <%--Indicaciones--%>
                     <tr>
                        <td align="left" colspan="3">
                            <span>Indicaciones: </span>
                        </td>  
                     </tr>
                      <tr>
                        <td align="center" colspan="7">
                            <asp:TextBox ID="txtIndicaciones" runat="server" Height="150px" 
                                TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td> 
                     </tr>      
               </table>
            </td>
         </tr>
         <%--Botones--%>
         <tr>
            
                <td align="left">
                    <asp:Button ID="btnSavenewQuimico" runat="server" CssClass="buttonHigh" 
                        Text="Guardar" Width="80px" onclick="btnSavenewQuimico_Click"  />
                    <asp:Button ID="btnCancelnewQuimico" runat="server" CssClass="button" Text="Cancelar" Width="85px" />
                </td>
            </tr>
    </table>