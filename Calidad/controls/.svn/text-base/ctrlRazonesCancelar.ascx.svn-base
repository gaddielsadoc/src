<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlRazonesCancelar.ascx.cs" Inherits="controls_ctrlRazonesCancelar" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.50401.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Panel ID="pnlCapturaDosisControl" runat="server" CssClass="modalPopup" Style="padding:5px; display:none;" width="500px" >

    <table class="index3" align="center" style="min-width:400px; margin:5px;">
        <tr>
            <td colspan="2" style="text-align:right; background:#ffa05f;" >
                <h4><asp:Label runat="server" ID="lblBienvenida"  meta:resourceKey="lblBienvenida"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label runat="server" ID="lbRazon"  meta:resourceKey="lbRazon"></asp:Label>
            </td>
            <td>
               <asp:DropDownList ID="ddlRazones" runat="server" Width="200px" DataTextField="vRazon" DataValueField="idRazonCambio">   </asp:DropDownList> 
            </td>
        </tr>
        <tr>
            <td colspan="2" class="floatnone">           
                <asp:Button CssClass="button" runat="server" ID="save" meta:resourceKey="save"  Text="Guardar" OnClick="save_OnClick" />
                <asp:Button CssClass="button" runat="server" ID="Button1" meta:resourceKey="Button1"  Text="Cancelar" OnClick="cancelar2_OnClick" />
                <asp:Button CssClass="button" runat="server" ID="btnOKMessageGralControl"  meta:resourceKey="btnOKMessageGralControl" Text="Cancelar" style="display: none;" />
            </td>
        </tr>
    </table>

    </asp:Panel>
        <asp:LinkButton runat="server" ID="lnkHiddenMdlPopupControl"  Text=""  Enabled="false"/>
        <asp:ModalPopupExtender ID="mdlPopupMessageGralControl" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="pnlCapturaDosisControl" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnOKMessageGralControl">
        </asp:ModalPopupExtender>
        
    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
