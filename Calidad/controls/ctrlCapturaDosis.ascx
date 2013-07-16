<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrlCapturaDosis.ascx.cs" Inherits="controls_ctrlCapturaDosis" %>
<%@ Register Src="~/controls/popUpMessageControl.ascx" TagName="popUpMessageControl" TagPrefix="uc1" %>
<%@ Register Src="~/controls/ctrlRazonesCancelar.ascx" TagName="razonesCancelar" TagPrefix="uc2" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.50401.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Panel ID="pnlCapturaDosisControl" runat="server" CssClass="modalPopup" Style="padding:5px; display:none;" width="500px" >

            <asp:Label runat="server" ID="lbFecha" Text=""></asp:Label>
            <asp:Label runat="server" ID="lbInvernadero" Text=""></asp:Label> 
            
    <%--
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.validate.js" type="text/javascript"></script>--%>
    
    <script type="text/javascript">

        $(function () {
            //*****************validacion de los campos******************************//
            //validacion del formulario (manual sin plugins de validación)
            $("#<%=save.ClientID%>").click(submit_Click);

            function submit_Click() {

                var errors = false;
                $(".dosisInput").each(function () {

                    if (!(this.value.match(/^(\d?\d?\d?\d?\d?\d?)(\.\d{1,4})?$/))) {
                       $("div.alerta span").html('<%=(string)GetLocalResourceObject("jsDecimal")%>');
                        $("#error").show();
                        errors = true;
                        return false;
                    }

                    var min = (Math.round(parseFloat($(this).parent().children(".dosisMinima").val()) * 1000000) / 1000000);
                    var max = (Math.round(parseFloat($(this).parent().children(".dosisMaxima").val()) * 1000000) / 1000000);
                    //errors = true;

                    //error cuando valor menor que cantidad minima o valor mayor que cantidad maxima
                    if ($(this).val() < min || $(this).val() > max) {
                        $("div.alerta span").html('<%=(string)GetLocalResourceObject("jsRango")%>');
                        $("#error").show();
                        errors = true;
                        return false;
                    }


                });

                if (!errors) {
                    return $("#" + formId).valid();
                } else {
                    return false;
                }
            }
            //*****************fin validacion de los campos******************************//

        });

        //boton de cerrar mensaje de error
        function closeErrorMsg() {
            $("div.alerta span").html('');
            $("#error").hide();
        }

        function metodoCancelar()
        {
            $('#<%=btnOKMessageGralControl%>').click();
        }

        function prevEnter(e) {
            if (e.keyCode == 13) {
                return false;
            }
        }
    </script> 


    <%--DE 0 A 7 DATOS--%>
    <asp:GridView ID="gvDosis"  runat="server" AutoGenerateColumns="False" CssClass="gridView2" Width="490px" HeaderStyle-CssClass="wrapped">
        <Columns>
            
            <asp:TemplateField HeaderText="Químico" meta:resourceKey="htQuimico" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="nombreQuimico" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                    <asp:HiddenField ID="idQuimico" Value='<%# Eval("idQuimico") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cantidad Requerida" meta:resourceKey="htCantidadRequerida" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <asp:Label ID="cantidadMinima" runat="server" Text='<%# Eval("cantMinima") %>' /> - <asp:Label ID="cantidaMaxima" runat="server" Text='<%# Eval("cantMaxima") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="unidad1" HeaderText="Unidades" meta:resourceKey="htUnidades" HeaderStyle-Width="60px"/>

            <asp:BoundField DataField="dCapacidad" HeaderText="Litros" meta:resourceKey="htCantidad" HeaderStyle-Width="50px"/>

            <asp:TemplateField HeaderText="Cantidad Pesada" meta:resourceKey="htCantidadPesada" HeaderStyle-Width="50px">
                <ItemTemplate>
                    
                    <asp:TextBox ID="txtInput" runat="server" Text='<%# ( !String.IsNullOrEmpty(Eval("dCanPesada").ToString())) ? Eval("dCanPesada") : Eval("dCanPedida") %>' CssClass="dosisInput cajaChica" onkeypress="return prevEnter(event)"
                        Enabled='<%# !( !String.IsNullOrEmpty(Eval("dFechaCancelado").ToString()) || !String.IsNullOrEmpty(Eval("dFechaPesaje").ToString()) || String.IsNullOrEmpty(Eval("dFechaPlanAplicar").ToString()) ) %>'>
                    </asp:TextBox>
                    
                    <asp:TextBox ID="hiddenMinima" runat="server" Value='<%# Eval("cantMinima") %>' CssClass="dosisMinima" Style="display: none;"></asp:TextBox>
                    <asp:TextBox ID="hiddenMaxima" runat="server" Value='<%# Eval("cantMaxima") %>' CssClass="dosisMaxima" Style="display: none;"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>    
            
    
        <table id="tablaDosis">
            <tr>
                <td>
                    <%--<asp:Button CssClass="button" runat="server" ID="cancelOrden" meta:resourceKey="cancelOrden" OnClick="cancelarOrden" Text="cancelOrden" />--%>
                    <asp:Button CssClass="button" runat="server" ID="Button1" meta:resourceKey="Button1" OnClick="CerrarVentana" Text="Cancelar"  OnClientClick="javascript:metodoCancelar()"/>
                    <asp:Button CssClass="button" runat="server" ID="btnOKMessageGralControl"  meta:resourceKey="btnOKMessageGralControl" Text="Cancelar" style="display: none;" />
                    <asp:Button CssClass="button" runat="server" ID="save" meta:resourceKey="save" OnClick="guardar_dosis" Text="Guardar" />
                    
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

    <%--<asp:Panel ID="panelRazones" runat="server" CssClass="modalPopup" Style="padding:5px; display:none;" width="500px" >
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
                <asp:Button CssClass="button" runat="server" ID="btnRazonesSave" meta:resourceKey="save"  Text="Guardar"  />
                <asp:Button CssClass="button" runat="server" ID="btnRazonesCancelar" meta:resourceKey="Button1"  Text="Cancelar" />
                <asp:Button CssClass="button" runat="server" ID="btnRazonesCancelar2"  meta:resourceKey="btnOKMessageGralControl" Text="Cancelar" style="display: none;" />
            </td>
        </tr>
    </table>

    </asp:Panel>
        <asp:LinkButton runat="server" ID="LinkButton2"  Text=""  Enabled="false"/>
        <asp:ModalPopupExtender  ID="popUpRazones" runat="server" 
            BackgroundCssClass="modalBackground"
            PopupControlID="panelRazones" 
            TargetControlID="lnkHiddenMdlPopupControl" 
            CancelControlID="btnOKMessageGralControl" />--%>
     <%--  </asp:ModalPopupExtender>
        --%>
    <uc1:popUpMessageControl ID="popUpMessageControl1" runat="server" />
    <uc2:razonesCancelar ID="razonesCancelar2" runat="server" />
