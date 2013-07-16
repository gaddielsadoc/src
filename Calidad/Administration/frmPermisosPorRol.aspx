<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmPermisosPorRol.aspx.cs" Inherits="Administration_frmPermisosPorRol" %>

<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="asp" Namespace="NewControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">

        function btnSeleccionarTodos_handler() {
            $('[type=checkbox]').each(function () {
                this.checked = true;
            });
            return false;
        }

        function btnQuitarSeleccion_handler() {
            $('[type=checkbox]').each(function () {
                this.checked = false;
            });
            return false;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>
            <asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido"></asp:Label></h1>
        <table class="index">
            <tr>
                <td align="left" colspan="4">
                    <h2>
                        <asp:Literal ID="Literal1" runat="server" meta:resourceKey="Literal1"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                <td style="width:200px;" align="right">
                    <asp:Literal ID="ltRol" runat="server" meta:resourceKey="ltRol"></asp:Literal>
                </td>
                <td style="width:200px;" align="left">
                    <asp:DropDownList ID="ddlRol" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlRol_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Literal ID="ltModulo" runat="server" meta:resourceKey="ltModulo">Modulo:</asp:Literal>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlModulo" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlModulo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    <asp:Button ID="btnGuardar" runat="server"  OnClick="btnGuardar_Click" meta:resourceKey="btnGuardar"/>
                    <input type="button" id="btnQuitarSeleccion"  value="<%=GetLocalResourceObject("btnQuitarSeleccion")%>"   onclick="javascript:btnQuitarSeleccion_handler();"/>
                    <input type="button" id="btnSeleccionarTodos"  value="<%=GetLocalResourceObject("btnSeleccionarTodos")%>"  onclick="javascript:btnSeleccionarTodos_handler();"/>
                </td>
            </tr>
        </table>
        <div class="grid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
               
                    <asp:CheckBoxListWithAttributes ID="listaSecciones" runat="server" CssClass="permisos gridView" 
                        ondatabound="listaSecciones_DataBound">
                    </asp:CheckBoxListWithAttributes>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlModulo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlRol" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
    </div>
</asp:Content>
