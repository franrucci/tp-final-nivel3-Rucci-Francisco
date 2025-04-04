﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Catalogo_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/Estilos.css" rel="stylesheet" type="text/css" />
    <script src="Js/JavaScript.js"></script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="card text-center custom-card">
            <div class="card-body">
                <h5 class="card-title custom-card-title">Bienvenido/a a nuestra tienda virtual</h5>
                <p class="card-text custom-card-text">Explora nuestros productos y disfruta de las mejores ofertas.</p>
            </div>
        </div>
    </div>

    <hr />
    <asp:UpdatePanel runat="server" ID="UpdatePanelFavoritos">
        <ContentTemplate>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <asp:Repeater ID="RepeaterArticulos" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="custom-repeater-card">
                                <div class="custom-repeater-card-image-container">
                                    <asp:Image
                                        runat="server"
                                        AlternateText="Imagen del producto"
                                        onerror="ImagenError(this)"
                                        ImageUrl='<%# ObtenerRutaImagen(Eval("ImagenUrl").ToString()) %>' />
                                </div>
                                <div class="custom-repeater-card-body">
                                    <h5 class="custom-repeater-card-title"><%#Eval("Nombre") %></h5>
                                    <p class="custom-repeater-card-text"><%#Eval("Descripcion") %></p>
                                    <p class="custom-repeater-card-price">
                                        <strong>Precio:</strong> $<%# RetornarPrecioConMenosDecimales((decimal)Eval("Precio")) %>
                                    </p>
                                    <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>" class="btn btn-primary btn-sm rounded-pill">
                                        <i class="bi bi-eye"></i>Ver Detalle
                                    </a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-warning text-center" Text="No hay artículos cargados." Visible="false"></asp:Label>

</asp:Content>
