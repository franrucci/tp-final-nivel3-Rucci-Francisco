<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Catalogo_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-center mt-5">
        <div class="card shadow-lg p-5 w-50 w-md-75 w-sm-100">
            <div class="card-body">
                <h2 class="text-center mb-4">Registrarse</h2>
                <asp:Panel runat="server" DefaultButton="btnRegistrarse">
                    <div class="mb-3">
                        <label class="form-label fw-bold">Email</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" Placeholder="Ingrese su email" />
                        <%--                        <asp:RequiredFieldValidator CssClass="validacion" runat="server" ControlToValidate="txtEmail" ErrorMessage="Campo requerido" Display="Dynamic" />
                        <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Formato de E-mail incorrecto" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" runat="server" Display="Dynamic" />--%>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-bold">Contraseña</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" Placeholder="Ingrese su contraseña" />
                        <%--                        <asp:RequiredFieldValidator CssClass="validacion" runat="server" ControlToValidate="txtPassword" ErrorMessage="Campo requerido" />--%>
                    </div>

                    <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold mt-2" Visible="false"></asp:Label>

                    <div class="d-grid gap-3 mt-3">
                        <asp:Button Text="Ingresar" CssClass="btn btn-primary btn-lg" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" runat="server" />
                        <a href="/" class="btn btn-outline-secondary btn-lg">Cancelar</a>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
