<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlLoginLdap.ascx.cs" Inherits="controls_ctrlLoginLdap" %>

      <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="alert" ></asp:Label>
<table class="index2" align="center" runat="server" id="tblLogin">
<tr><td colspan="3">
 <h1 style="width:350px; max-width:350px; min-width:350px;" >
     <asp:Literal ID="ltBienvenido" runat="server" meta:resourceKey="ltBienvenido"></asp:Literal>
 
 </h1>
<h1 style="width:350px; max-width:350px; min-width:350px;">
<%--<asp:Label ID="ltWelcome" runat="server" meta:resourceKey="ltWelcome" Text=""></asp:Label>--%>
</h1></td>
</tr>
    <tr>
        <td style="text-align:right; width:200px;">
            <asp:Literal ID="Literal1" runat="server" meta:resourceKey="Literal1"></asp:Literal>
            
        </td>
        <td align="left" style="width:150px;">
            <asp:TextBox ID="txtUsername" runat="server" CssClass="controls" Width="150px"></asp:TextBox>
            
        </td>
        <td align="left" style="width:150px;">
        <asp:RequiredFieldValidator meta:resourceKey="validator1" ID="rfvUsername" runat="server" ErrorMessage="*Campo requerido" ForeColor="#ff6600" ControlToValidate="txtUsername" ></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="text-align:right;">
            <asp:Literal ID="Literal2" runat="server" meta:resourceKey="Literal2"></asp:Literal>
          
        </td>
        <td align="left"> 
            <asp:TextBox ID="txtPassword" runat="server" CssClass="controls" TextMode="Password"  Text="3p7sGC" Width="150px"></asp:TextBox>
            
        </td>
        <%--<td align="left"><asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*Campo requerido" ForeColor="#ff6600" ControlToValidate="txtPassword" ></asp:RequiredFieldValidator> </td>--%>
    </tr>
          
    <tr>

        <td colspan="3">
            <asp:LinkButton ID="lnkLogin" runat="server"  CssClass="save" 
                onclick="lnkLogin_Click" meta:resourceKey="lnkLogin"></asp:LinkButton>
        </td>
    </tr>

</table>

<%--<table  id="tblConections" class="index2" align="center" runat="server">
<tr><td colspan="3">
 <h1 style="width:350px; max-width:350px; min-width:350px;">
     <asp:Literal ID="Literal3" runat="server" meta:resourceKey="Literal3"></asp:Literal>


 </h1>
<span style="width:350px; max-width:350px; min-width:350px;"><asp:Label ID="lblInfoCon" runat="server" meta:resourceKey="ltWelcome" Text=""></asp:Label></span></td>
</tr>
    <tr>
        
        <td align="center">
            
            
            <asp:LinkButton ID="lbUsa" runat="server" OnClick="lbUsa_Click" meta:resourceKey="lbUsa">
             
            </asp:LinkButton>
            
            
        </td>
        
    </tr>
    <tr>
        
        <td align="center"> 
            
            <asp:LinkButton ID="lbMex" runat="server" OnClick="lbMex_Click" meta:resourceKey="lbMex"></asp:LinkButton>
        </td>
        <%--<td align="left"><asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*Campo requerido" ForeColor="#ff6600" ControlToValidate="txtPassword" ></asp:RequiredFieldValidator> </td>--%>
  <%--  </tr>
          
    <tr>

        <td colspan="3">
            <asp:LinkButton ID="lbSalir" runat="server"  CssClass="save" 
                onclick="lbSalir_Click" meta:resourceKey="lbSalir"></asp:LinkButton>
        </td>
    </tr>

</table>--%>