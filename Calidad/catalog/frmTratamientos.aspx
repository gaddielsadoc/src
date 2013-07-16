<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmTratamientos.aspx.cs" Inherits="catalog_frmTratamientos" ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="popUpMessageControl" Src="~/controls/popUpMessageControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <%-- <script type="text/javascript" src="../Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.tablesorter.pager.js"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>--%>

   <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
   <script src="../Scripts/jquery.tablesorter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.tablesorter.pager.js" type="text/javascript"></script>
     <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="../Scripts/ComunJscript.js" type="text/javascript"></script>
   <link href="../CSS/ui-lightness/jquery-ui-1.8.21.custom.css" rel="stylesheet" type="text/css" />   



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

            if (key < 32 || key > 58) {
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
                    '<%= txtNombre.UniqueID %>': { 'required': true }
                },

                messages: {
                    '<%= txtNombre.UniqueID %>': { 'required': 'El Nombre es requerido' }
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

//            $("#<%=btnSave.ClientID%>").click(submit_Click);
//            $("#<%=btnActualizar.ClientID%>").click(submit_Click);

            if ($("#<%=gvTratamiento.ClientID%>").find("tbody").find("tr").size() > 1) {

                $("#<%=gvTratamiento.ClientID%>")
                    .tablesorter({
                        widthFixed: true,
                        widgets: ['zebra'],
                        widgetZebra: { css: ["gridView", "gridViewAlt"] }
                    })
                    .tablesorterPager({ container: $("#pager"), output: '{page} '+'<%= (string)GetGlobalResourceObject("Commun","de")%>'+' {totalPages}' });
            } else {
                $("#pager").hide();
            }
        });
    </script>

    <script type="text/javascript">

        function pageLoad() {
            qumicosLoad();
            registerCombobox();
            
        }

        function registerCombobox() {            
            $("select[name='<%= ddlFiltro.UniqueID %>']").combobox();
        }

        //boton agregar plaga
        function addPlaga() {
            var ddl = $("#<%= ddlFiltro.ClientID %>").clone().html();
            var html = '<tr><td>  ';
            /*Aqui iria codigo de select*/
            html += '<select name="<%= ddlFiltro.UniqueID %>">' + ddl + '</select>';
            html += '</td><td><img alt="Quitar" src="../images/remove-icon.png" class="btnRemoverPlaga"/></td></tr>';
            $('#tblPlagas').append(html);
            registerCombobox();
        }

          
        //boton remover plaga
        $('.btnRemoverPlaga').live('click', function () {
            $(this).parent().parent().remove();
        });

        function qumicosLoad() {
            var plagas = $('#<%=QuimTmp.ClientID %>').val();
            $('#<%=QuimTmp.ClientID %>').val("");
            if (plagas) {
                var itemArray = plagas.split('|');
                $.each(itemArray, function (index, value) {
                    //alert(value);
                    if (value) {
                        var ddl = $("#<%=ddlFiltro.ClientID %>").clone().html();
                        if (index > 0) {
                            var html = '<tr><td>  ';
                            html += '<select name="<%=ddlFiltro.UniqueID %>">' + ddl + '</select>';
                            html += '</td><td><img alt="Quitar" src="../images/remove-icon.png" class="btnRemoverPlaga" /></td></tr>';
                            $('#tblPlagas').append(html);
                        }
                        $('#tblPlagas select:last').val(value);
                    }
                });
                registerCombobox();
            }
        }

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
            <asp:Label ID="lblBienvenido" runat="server" meta:resourceKey="lblBienvenido">Administraci&oacute;n de Tratamientos</asp:Label></h1>
        <table class="index" style="width: 800px; max-width: 800px; min-width: 800px;">
            <tr>
                <td colspan="4" align="left">
                    <h2>
                        <asp:Literal ID="Literal1" runat="server" meta:resourceKey="Literal1"></asp:Literal></h2>
                </td>
            </tr>
            <tr>
                 <td style="width:120px">
                    <asp:Literal ID="ltPlanta" runat="server" meta:resourceKey="ltPlanta">*Planta</asp:Literal> 
                 </td>         
                <td>
                    <asp:DropDownList ID="ddlPlanta" runat="server" AppendDataBoundItems="true" > </asp:DropDownList>   
                </td>

                <td align="right" style="width: 120px;">
                    <asp:Literal ID="ltNombre" runat="server" Text="*Nombre"  meta:resourceKey="ltNombre"></asp:Literal>
                </td>
                <td style="width: 200px;">
                    <asp:TextBox runat="server" ID="txtNombre" MaxLength="255" CssClass="cajaLarga" onkeypress="javascript:return ValidTec( event );"></asp:TextBox>
                </td>
                <td align="right" style="width: 75px;">
                    <asp:Literal ID="ltActivo" runat="server" meta:resourceKey="ltActivo">&iquest;Activo?</asp:Literal>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkActivo" Checked="True" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <table id="tblPlagas">
                        <tr >
                            <td class="margin_top">
                                <asp:DropDownList ID="ddlFiltro" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                            </td>
                            <td>
                                <img id="imgAdd" alt="Agregar" src="../images/add-icon.png" onclick="addPlaga();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:HiddenField runat="server" ID="hdnIdTratamiento" />
                    <asp:HiddenField ID="QuimTmp" runat="server" />

                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnSave_Click" Visible="false" meta:resourceKey="btnActualizar"/>
                    <asp:Button ID="btnSave" runat="server" Text="Guardar Nuevo" OnClick="btnSave_Click" meta:resourceKey="btnSave"/>
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnCancel_Click" meta:resourceKey="btnLimpiar"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click"
                        Visible="false" meta:resourceKey="btnCancel"/>
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
            <asp:GridView ID="gvTratamiento" runat="server" EnableModelValidation="True" AutoGenerateColumns="False"
                OnPreRender="gvTratamiento_PreRender" DataKeyNames="idTratamiento" CssClass="gridView" width="800px"
                EmptyDataText="No existen registros" OnRowDataBound="gvTratamiento_RowDataBound" OnSelectedIndexChanged="gvTratamiento_SelectedIndexChanged" meta:resourceKey="gvTratamiento">
                <Columns>
                    <asp:TemplateField HeaderText="Activo" SortExpression="activo" HeaderStyle-HorizontalAlign="Center" 
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" meta:resourceKey="htActivo">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("bActivo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblActivoGrid" runat="server" Text='<%# (bool)Eval("bActivo")==true?(string)GetLocalResourceObject("Si"):(string)GetLocalResourceObject("No") %>'
                                Enabled="False" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nomPlanta" HeaderText="Planta" SortExpression="nomPlanta" HeaderStyle-Width="300px" meta:resourceKey="htPlanta"/>
                    <asp:BoundField DataField="vNombre" HeaderText="Tratamiento" SortExpression="vNombre" HeaderStyle-Width="550px" meta:resourceKey="htTratamiento">
                        <HeaderStyle Width="463px"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <uc1:popupmessagecontrol ID="popUpMessageControl1" runat="server" />
    </div>
</asp:Content>

