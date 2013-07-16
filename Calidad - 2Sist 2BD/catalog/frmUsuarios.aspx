﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmUsuarios.aspx.cs" Inherits="catalog_frmUsuarios" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc1" TagName="popUpMessageControl" Src="~/controls/popUpMessageControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>
    
    <script type="text/javascript">
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


<script type="text/javascript">

    function submit_Click() {
        return $("#" + formId).valid();
    }

    function closeErrorMsg() {
        $("#error").hide();
    }

    $(function () {

        $("#" + formId).validate({
            onfocusout: false,
            onsubmit: false,
            rules: {
                '<%= txtnombre.UniqueID %>': { 'required': true }              
                , '<%= txtUser.UniqueID %>': { 'required': true }               
            },

            messages: {
                '<%= txtnombre.UniqueID %>': { 'required': 'El Nombre es requerido' }              
                , '<%= txtUser.UniqueID %>': { 'required': 'El Usuario es requerido' }              
            },

            showErrors: function (errorMap, errorList) {
                var html = '';
                for (var i = 0; i < errorList.length; i++) {
                    html += errorList[i].message + '<br />';
                }
                $("div.alerta span").html(html);
            },

            invalidHandler: function (form, v) {
                var errors = v.numberOfInvalids();
                if (errors) {
                    $("#error").show();
                } else {
                    $("#error").hide();
                }
            }
        });

        $("#<%=btnSaveUser.ClientID%>").click(submit_Click);
        $("#<%=btnActualizar.ClientID%>").click(submit_Click);


        
        if ($("#<%=gvUsuarios.ClientID%>").find("tbody").find("tr").size() > 1) {

            $("#<%=gvUsuarios.ClientID%>")
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
    </script>
    
 </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <div class="container">
        <div id="error" class="modalPopup" style="width: 500px; display: none; position: fixed;
            z-index: 9999;">
            <table style="vertical-align: middle; text-align: center; height: 100%; width: 100%;">
                <tbody>
                    <tr>
                        <td style="background: #ccc repeat;">
                            <div id="">
                                <div class="alerta">
                                    <img src="../images/error.png" id="imgMensajeError" alt="" />
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
                </tbody>
            </table>
        </div>
        <h1>
            <asp:Label ID="lblBienvenido" runat="server">Administraci&oacute;n de Usuarios</asp:Label></h1>
        <table class="index" style="width: 800px; max-width: 800px; min-width: 800px;">
            <tr>
                <td colspan="4" align="left">
                    <h2>
                        <asp:Literal ID="Literal1" runat="server" Text="Capture o edite los usuarios"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 200px;">
                    <asp:Literal ID="ltnombre" runat="server" Text="*Nombre"></asp:Literal>
                </td>
                <td style="width: 200px;">
                    <asp:TextBox runat="server" ID="txtnombre" MaxLength="255" onkeypress="javascript:return ValidTec( event );"></asp:TextBox>
                </td>
                <td align="right" style="width: 75px;">
                    <asp:Literal ID="ltActivo" runat="server">&iquest;Activo?</asp:Literal>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCorreoActivo" Checked="True" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 200px;">
                    <asp:Literal ID="ltuser" runat="server" Text="*Usuario"></asp:Literal>
                </td>
                <td style="width: 200px;">
                    <asp:TextBox runat="server" ID="txtUser" MaxLength="255" onkeypress="javascript:return ValidTec( event );"></asp:TextBox>
                </td>
                <%--<td>
                     <asp:Literal ID="ltpass" runat="server" Text="*Contraseña"></asp:Literal>
                </td>
                 <td style="width: 200px;">
                    <asp:TextBox runat="server" ID="txtpass" MaxLength="255" onkeypress="javascript:return ValidTec( event );" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td colspan="2"></td>
            <td>
                     <asp:Literal ID="Literal2" runat="server" Text="*Conf. Contraseña:" Mode="PassThrough"></asp:Literal>
                </td>
                 <td style="width: 200px;">
                    <asp:TextBox runat="server" ID="txtConf" MaxLength="255" onkeypress="javascript:return ValidTec( event );" TextMode="Password"> </asp:TextBox>
                </td>--%>

            </tr>

            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" ID="hdID" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnSaveUser_Click"
                        Visible="false" />
                    <asp:Button ID="btnSaveUser" runat="server" Text="Guardar" OnClick="btnSaveUser_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnCancelUser_Click" />
                    <asp:Button ID="btnCancelUser" runat="server" Text="Cancelar" OnClick="btnCancelUser_Click"
                        Visible="false" />
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
            <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False"
                OnPreRender="gvUsuarios_PreRender" DataKeyNames="idUsuario" CssClass="gridView"
                EmptyDataText="No existen registros"  EnableModelValidation="True"
                OnRowDataBound="gvUsuarios_RowDataBound" onselectedindexchanged="gvUsuarios_SelectedIndexChanged1"  
                >
                <Columns>
                    <asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("activo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("activo")==true?"Sí":"No" %>'
                                Enabled="False" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" HeaderStyle-Width="550px">
                        <HeaderStyle Width="463px"></HeaderStyle>
                    </asp:BoundField>
                     <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" HeaderStyle-Width="80px">
                        <HeaderStyle Width="80px"></HeaderStyle>
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </div>
        <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
    </div>

</asp:Content>

