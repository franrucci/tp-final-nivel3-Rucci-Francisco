<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoArticulo.aspx.cs" Inherits="Catalogo_web.ListadoArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel runat="server" ID="UpdatePanelFiltros">
        <ContentTemplate>
            <% if (Page is Catalogo_web.ListadoArticulo)
                { %>
            <div id="filtros-container" class="container mt-3 p-3 border rounded shadow">
                <!-- Búsqueda por nombre, marca o descripcion -->
                <div class="mb-3 d-flex align-items-center">
                    <asp:TextBox ID="txtFiltroRapido" CssClass="form-control form-control-sm me-2" placeholder="Buscar..." runat="server" AutoPostBack="true" OnTextChanged="txtFiltroRapido_TextChanged"></asp:TextBox>
                    <button type="button" runat="server" onserverclick="BuscarButton_ServerClick" id="BuscarButton"
                        class="btn btn-primary btn-sm">
                        <i class="bi bi-search"></i>Buscar
                    </button>
                </div>


                <% } %>
            </div>

            <%-- <% } %>--%>
        </ContentTemplate>
    </asp:UpdatePanel>







    <asp:UpdatePanel runat="server" ID="UpdatePanelArticulos">
        <ContentTemplate>

            <asp:Repeater runat="server" ID="RepeaterArticulos">
                <ItemTemplate>

                    <div class="card horizontal-card my-4 shadow-lg">
                        <div class="row g-0 align-items-center">
                            <div class="col-md-4">
                                <asp:Image
                                    ID="ImagenProducto"
                                    ImageUrl='<%# Eval("ImagenUrl") != null && !string.IsNullOrEmpty(Eval("ImagenUrl").ToString()) ? Eval("ImagenUrl").ToString() : Negocio.ArticuloNegocio.ImagenError %>'
                                    CssClass="img-fluid img-proyect"
                                    AlternateText="Imagen del producto"
                                    onerror="ImagenError(this)"
                                    runat="server" />
                            </div>

                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-category mb-2 text-muted"><span><%#Eval("Categoria.Descripcion")%></span></h5>
                                    <h5 class="card-brand text-uppercase text-secondary"><span><%#Eval("Marca.Descripcion")%></span></h5>
                                    <h5 class="card-title text-primary fw-bold mt-3"><%#Eval("Nombre") %></h5>
                                    <p class="card-price text-success fw-bold fs-5">$<%# RetornarPrecioConMenosDecimales((decimal)Eval("Precio")) %></p>
                                    <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>" class="btn btn-outline-primary mt-3">Ver Detalle</a>
                                </div>
                            </div>
                        </div>
                    </div>


                </ItemTemplate>
            </asp:Repeater>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
