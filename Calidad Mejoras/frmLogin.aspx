<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="frmLogin.aspx.cs" Inherits="frmLogin" %>
<%@ Register Src="controls/ctrlLoginLdap.ascx" TagName="ctrlLoginLdap" TagPrefix="uc2"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Calidad - NatureSweet </title>    
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="comun/css/comun.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" language="javascript">


        
    </script>
</head>
<body style="height:100%; min-height:100%; float:none; position:relative;">    <form runat="server" id="form1" style="height:100%; min-height:100%; float:none; position:relative;">
<div id="wrapper" style="height:100%; min-height:100%; padding-bottom:100px;">


    <div class="header">
<div class="img">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/comun/img/naturesweet.png" Width="145px" Height="145px" />
 
</div>

 <div class="search">
           <table cellspacing="0" cellpadding="0"><tr><td align="right">
           </td><td colspan="2">&nbsp;</td>
          <td align="right">
                 <%--<asp:LinkButton ID="LinkButton2" runat="server" meta:resourceKey="Espanol"
                        onclick="LinkButton2_Click" CausesValidation="false"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" meta:resourceKey="Ingles" 
                        onclick="LinkButton3_Click" CausesValidation="false"></asp:LinkButton>--%>
                <asp:DropDownList Visible="false" ID="ddlLocale" runat="server" CausesValidation="False" style="display: none">
                 <%--   <asp:ListItem Text="Español" Value="es-MX"></asp:ListItem>
                    <asp:ListItem Text="Ingles" Value="en-US"></asp:ListItem>--%>
                </asp:DropDownList>
                </td>
                </tr>
            
            <tr>
            <td> &nbsp; </td>
            <td>    </td>
            <td colspan="2" align="right" style="font-size:11px;  padding-top:2px;">
                <asp:Literal ID="Literal1" runat="server" meta:resourceKey="Literal1"></asp:Literal> 
           <a target="_blank" class="facebook" data-ga-track="facebook,header,en" href="http://facebook.com/naturesweet"><asp:Image ID="Image2" runat="server" BorderStyle="none" ImageUrl="~/comun/img/facebook.png"/></a>
           <a target="_blank" class="twitter" data-ga-track="twitter,header,en" href="https://twitter.com/nstomatoes"><asp:Image ID="Image3" runat="server" BorderStyle="none" ImageUrl="~/comun/img/twitter.png"/></a>
           <a target="_blank" class="youtube" href="http://youtube.com/naturesweettomatoes"><asp:Image ID="Image4" runat="server" BorderStyle="none" ImageUrl="~/comun/img/youtube.png"/></a>
           </td></tr>
           </table>
        </div>

		

</div>

    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td align="center" style="white-space:nowrap;">
           
               <uc2:ctrlLoginLdap ID="ctrlLoginLdap1" runat="server" />
            </td>
        </tr>
    </table>
    <img id="loginLoading" src="images/loading.gif" alt="" height="30" width="30" style="display: none;" />
     

    <%--<script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest)

        function beginRequest(sender, args) {
            var pamns = document.getElementById('loginLoading');
            if (pamns) {
                pamns.style.display = 'block';
            }
        }

        function endRequest(sender, args) {
            var pamns = document.getElementById('loginLoading');
            if (pamns) 
            {
                pamns.style.display = 'none';
            }
        }         
    </script>--%>
 
 <div style="position:fixed; width:100%; bottom:0;">
   <div class="disclaimer" style="position:absolute;">
                                    El contenido de esta pagina de datos es confidencial y se entiende dirigido y para
                                    uso exclusivo de los proveedores de NatureSweet por lo que no podrá distribuirse
                                    y/o difundirse por ningún medio sin la previa autorización del emisor original.
                                    Si usted no es el destinatario, se le prohíbe su utilización total o parcial para
                                    cualquier fin.
                                    <br />
                                    <br />
                                    The content of this data transmission is confidential and is intended to be delivered
                                    only to the addressees. Therefore, it shall not be distributed and/or disclosed
                                    through any means without the authorization of the original sender. If you are not
                                    the addressee, you are forbidden from using it, either totally or partially, for
                                    any purpose
                                    </div>
        <div class="footer" style="position:absolute;">
   <span></span> <asp:Image ID="Image5" ImageUrl="~/comun/img/footer-food.png" runat="server"/> </div>  </div> </div>  </form>
</body>  
</html>
