<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmMargenError.aspx.cs" enableEventValidation="false" Inherits="catalog_frmMargenError" ValidateRequest="false"%>
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
            

            //*****************validacion de los campos******************************//
            //validacion del formulario con la clase validate de jquery
            $('#' + formId).validate({
                errorLabelContainer: "div.alerta span",
                wrapper: "li",
                onfocusout: false,
                onsubmit: false,
                onkeyup: false,
                rules: {
                    '<%=txtDesgaste.UniqueID%>': { required: true, number: true },
                    '<%=txtSobrante.UniqueID%>': { required: true, number: true }
                },
                messages: {
                    '<%=txtDesgaste.UniqueID%>': { required: 'El campo desgaste es requerido', number: 'El campo desgaste debe ser numérico' },
                    '<%=txtSobrante.UniqueID%>': { required: 'El campo sobrante es requerido', number: 'El campo sobrante debe ser numérico' }
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
            $("#<%=btnSave.ClientID%>").click(submit_Click);

            function submit_Click() {
                return $("#" + formId).valid();
            }
            //*****************fin validacion de los campos******************************//

        });

        //boton de cerrar mensaje de error
        function closeErrorMsg() {
            $("div.alerta span").html('');
            $("#error").hide();
        }

    </script> 


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
    <asp:Label ID="lblBienvenido" runat="server">M&aacute;rgenes de Error</asp:Label></h1>
    <table class="index">
        <tr>
            <td style="width:120px">Error Desgaste de Boquillas</td>
            <td>
                <asp:TextBox ID="txtDesgaste" runat="server" MaxLength="2" Enabled="False" />
            </td>
            <td>Error Sobrante de Soluci&oacute;n</td>
            <td>
                <asp:TextBox ID="txtSobrante" runat="server" MaxLength="2" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnHabilita" runat="server" Text="Actualizar" onclick="habilitaEdicion"/>
                <asp:Button ID="btnSave" runat="server" Text="Guardar" onclick="Guardar_Actualizar" Visible="False" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Cancelar" onclick="Cancelar_Limpiar" Visible="False" />
            </td>
        </tr>
    </table>
    
    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />

    </div>

</asp:Content>

