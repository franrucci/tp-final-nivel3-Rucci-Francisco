<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Catalogo_web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-center mt-5">
        <div class="card shadow-lg p-5 w-50 w-md-75 w-sm-100">
            <div class="card-body">
                <h2 class="text-center mb-4">Iniciar Sesión</h2>

                <div class="mb-3">
                    <label class="form-label fw-bold">Email</label>
                    <asp:TextBox runat="server" CssClass="form-control" REQUIRED ID="txtEmail" Placeholder="Ingrese su email" />
                </div>

                <div class="mb-3">
                    <label class="form-label fw-bold">Contraseña</label>
                    <asp:TextBox runat="server" CssClass="form-control" REQUIRED ID="txtPassword" TextMode="Password" Placeholder="Ingrese su contraseña" />
                </div>

                <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold mt-2" Visible="false"></asp:Label>

                <div class="d-grid gap-3 mt-3">
                    <asp:Button Text="Ingresar" CssClass="btn btn-primary btn-lg" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />
                    <a href="/" class="btn btn-outline-secondary btn-lg">Cancelar</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
