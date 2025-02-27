<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="GestionArticulo.aspx.cs" Inherits="Catalogo_web.GestionArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Gestión de Artículos</h2>
        <div class="table-responsive">
            <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" AutoGenerateColumns="False" CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="Codigo" HeaderText="Código" HeaderStyle-CssClass="table-dark" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="table-dark" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="table-dark" />
                    <asp:BoundField DataField="Marca.Descripcion" HeaderText="Marca" HeaderStyle-CssClass="table-dark" />
                    <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" HeaderStyle-CssClass="table-dark" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" HeaderStyle-CssClass="table-dark" />
                    <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Seleccionar"
                        HeaderText="Acción"
                        ItemStyle-CssClass="text-center"
                        ControlStyle-CssClass="btn btn-primary btn-sm rounded-pill"
                        HeaderStyle-CssClass="table-dark" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="mt-3">
            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success btn-lg" Text="Nuevo artículo" OnClick="btnAgregar_Click" />
        </div>
    </div>
</asp:Content>
