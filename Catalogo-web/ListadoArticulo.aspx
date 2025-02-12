<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListadoArticulo.aspx.cs" Inherits="Catalogo_web.ListadoArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:UpdatePanel runat="server" ID="UpdatePanelFiltros">
        <ContentTemplate>
            <% if (Page is Catalogo_web.ListadoArticulo)
                { %>
            <div id="filtros-container" class="container mt-3 p-3 border rounded shadow">
                <!-- Filtro rapido por nombre, marca o descripcion -->
                <div class="mb-3 d-flex align-items-center">
                    <asp:TextBox ID="txtFiltroRapido" CssClass="form-control form-control-sm me-2" placeholder="Buscar..." runat="server" AutoPostBack="true" OnTextChanged="txtFiltroRapido_TextChanged"></asp:TextBox>
                    <button type="button" runat="server" onserverclick="BuscarButton_ServerClick" id="BuscarButton"
                        class="btn btn-primary btn-sm">
                        <i class="bi bi-search"></i>Buscar
                    </button>
                </div>


                <!-- Filtro avanzado -->
                <div class="form-check mb-2">
                    <asp:CheckBox ID="chkFiltroAvanzado" CssClass="" OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" AutoPostBack="true" runat="server" />
                    <label class="form-check-label">
                        <i class="bi bi-funnel-fill"></i>Filtro avanzado
                    </label>
                </div>
                <% } %>

                <% if (chkFiltroAvanzado.Checked)
                    { %>
                <div class="row">

                    <!-- Ordenar por -->
                    <div class="col-md-6 mb-2">
                        <asp:Label Text="Ordenar por:" class="form-label" runat="server" />
                        <asp:DropDownList ID="ddlOrdenarTipo" CssClass="form-select form-select-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenarTipo_SelectedIndexChanged">
                            <asp:ListItem Text="Categoría" />
                            <asp:ListItem Text="Marca" />
                            <asp:ListItem Text="Precio" />
                        </asp:DropDownList>
                    </div>

                    <!-- Criterio -->
                    <div class="col-md-6 mb-2">
                        <asp:Label Text="Criterio" class="form-label" runat="server" />
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-select form-select-sm"></asp:DropDownList>
                    </div>


                    <!-- Filtro -->
                    <div class="col-md-6 mb-2">
                        <asp:Label Text="Filtro" runat="server" />
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control form-control-sm" />
                    </div>


                    <!-- Botones de Buscar y limpiar -->
                    <div class="d-flex justify-content-end mt-3">
                        <asp:Button
                            runat="server"
                            OnClick="btnBuscar_Click"
                            ID="btnBuscar"
                            CssClass="btn btn-success btn-sm mx-1"
                            Text="Buscar"
                            CausesValidation="false"
                            UseSubmitBehavior="false" />

                        <asp:Button
                            runat="server"
                            OnClick="btnLimpiar_Click"
                            ID="btnLimpiar"
                            CssClass="btn btn-danger btn-sm"
                            Text="Limpiar"
                            CausesValidation="false"
                            UseSubmitBehavior="false" />
                    </div>

                    <% } %>
                </div>

                <asp:Repeater runat="server" ID="repeaterArticulos">
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
