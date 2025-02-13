<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="Catalogo_web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel runat="server" CssClass="container mt-5">
        <div class="card shadow rounded-3 p-4">

            <div class="row mb-3">
                <div class="col-12 text-center">
                    <h2 class="fw-bold">
                        <asp:Label runat="server" ID="lblTituloArticulo" />
                    </h2>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6 text-center">
                    <asp:Image runat="server" CssClass="img-thumbnail" ID="imgArticulo"
                        AlternateText="Imagen del producto" OnError="ErrorCargaImagenProducto(this)" />
                </div>

                <div class="col-md-6">
                    <asp:Table runat="server" CssClass="table">
                        <asp:TableHeaderRow CssClass="table-dark">
                            <asp:TableHeaderCell ColumnSpan="2">Detalles del Producto</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell><strong>Producto</strong></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblNombreArticulo" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><strong>Descripción</strong></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblDescripcionArticulo" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><strong>Marca</strong></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblMarcaArticulo" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><strong>Categoría</strong></asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblCategoriaArticulo" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><strong>Precio</strong></asp:TableCell>
                            <asp:TableCell CssClass="text-success fs-5 fw-bold">
                                <asp:Label runat="server" ID="lblPrecioArticulo" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
