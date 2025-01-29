<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Catalogo_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="CSS/Estilos.css" rel="stylesheet" type="text/css" />

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

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="RepeaterArticulos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="custom-repeater-card">
                        <div class="custom-repeater-card-image-container">
                            <asp:Image ImageUrl='<%# Eval("ImagenUrl") != null && !string.IsNullOrEmpty(Eval("ImagenUrl").ToString()) ? Eval("ImagenUrl").ToString() : Negocio.ArticuloNegocio.ImagenError %>' 
                                runat="server"
                                AlternateText="Imagen del producto"
                                onerror="ImagenError(this)"/>
                        </div>
                        <div class="custom-repeater-card-body">
                            <h5 class="custom-repeater-card-title"><%#Eval("Nombre") %></h5>
                            <p class="custom-repeater-card-text"><%#Eval("Descripcion") %></p>
                            <p class="custom-repeater-card-price">
                                <strong>Precio:</strong> $<%# RetornarPrecioConMenosDecimales((decimal)Eval("Precio")) %>
                            </p>
                            <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>">Ver Detalle</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>



</asp:Content>
