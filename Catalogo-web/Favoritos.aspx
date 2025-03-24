<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Catalogo_web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server" ID="UpdatePanelFavoritos">
        <ContentTemplate>
            <asp:Repeater runat="server" ID="repeaterArticulos">
                <ItemTemplate>

                    <div class="card horizontal-card my-4 shadow-lg">
                        <div class="row g-0 align-items-center">
                            <div class="col-md-4">
                                <asp:Image
                                    ID="ImagenProducto"
                                    runat="server"
                                    AlternateText="Imagen del producto"
                                    CssClass="img-fluid img-proyect"
                                    onerror="ImagenError(this)"
                                    ImageUrl='<%# ObtenerRutaImagen(Eval("ImagenUrl").ToString()) %>' />
                            </div>

                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-category mb-2 text-muted"><span><%#Eval("Categoria.Descripcion")%></span></h5>
                                    <h5 class="card-brand text-uppercase text-secondary"><span><%#Eval("Marca.Descripcion")%></span></h5>
                                    <h5 class="card-title text-primary fw-bold mt-3"><%#Eval("Nombre") %></h5>
                                    <p class="card-price text-success fw-bold fs-5">$<%# RetornarPrecioConMenosDecimales((decimal)Eval("Precio")) %></p>

                                    <div class="d-flex gap-2 mt-3">
                                        <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>" class="btn btn-outline-primary btn-sm">Ver Detalle</a>

                                        <asp:Button
                                            ID="btnFavorito"
                                            runat="server"
                                            CssClass='<%# EsFavorito(Eval("Id")) ? "btn btn-danger btn-sm" : "btn btn-outline-primary btn-sm" %>'
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClick="btnFavorito_Click"
                                            Text="❤ Favorito" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

            <div class="d-flex justify-content-center align-items-center" style="height: 50vh;">
                <asp:Label
                    ID="lblSinFavoritos"
                    runat="server"
                    CssClass="alert alert-warning text-center fw-bold fs-4"
                    Text="No tienes artículos favoritos."
                    Visible="false" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
