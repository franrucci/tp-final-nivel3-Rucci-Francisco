<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DatosArticulo.aspx.cs" Inherits="Catalogo_web.DatosArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-6">
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Código</label>
                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
                    <asp:RequiredFieldValidator CssClass="validacion" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Campo requerido" Display="Dynamic" />
                    <asp:RegularExpressionValidator CssClass="validacion" runat="server" ControlToValidate="txtCodigo" ErrorMessage="Solo se permiten números y letras sin espacios." ValidationExpression="^[a-zA-Z0-9]+$" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                    <asp:RequiredFieldValidator CssClass="validacion" runat="server" ControlToValidate="txtNombre" ErrorMessage="Campo requerido" Display="Dynamic" />
                    <asp:RegularExpressionValidator CssClass="validacion" runat="server" ControlToValidate="txtNombre" ErrorMessage="Solo se permiten letras y números" ValidationExpression="^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ ]+$" Display="Dynamic" />

                </div>
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
                    <asp:RequiredFieldValidator CssClass="validacion" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="Campo requerido" Display="Dynamic" />
                </div>
                <div class="mb-3">
                    <label for="ddlMarca" class="form-label">Marca</label>
                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="ddlCategoria" class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtPrecio" class="form-label">Precio</label>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
                    <asp:RequiredFieldValidator CssClass="validacion" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Campo requerido" Display="Dynamic" />
                    <asp:RegularExpressionValidator CssClass="validacion" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Solo se permiten números." ValidationExpression="^\d+([,\.]\d{1,2})?$" />
                </div>

                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar cambios" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar artículo" OnClick="btnEliminar_Click" />
                </div>

                <div class="row">
                    <div class="mt-5">
                        <a href="GestionArticulo.aspx" class="btn btn-secondary">Cancelar</a>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:CheckBox ID="chkSubirArchivo" runat="server" AutoPostBack="true" OnCheckedChanged="chkSubirArchivo_CheckedChanged" />
                            <label class="form-check-label" for="chkSubirArchivo">Subir imagen desde archivo</label>
                        </div>

                        <div class="mb-3" id="divUrlImagen" runat="server">
                            <label for="txtImagenUrlArticulo" class="form-label">URL Imagen</label>
                            <asp:TextBox runat="server" ID="txtImagenUrlArticulo" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtImagenUrlArticulo_TextChanged" />
                        </div>

                        <%if (chkSubirArchivo.Checked)
                            {%>
                        <div class="mb-3" id="divFileUpload" runat="server">
                            <label for="fuImagenArticulo" class="form-label">Seleccionar Archivo</label>
                            <input type="file" id="txtImagenArchivo" runat="server" class="form-control" onchange="actualizarImagen()" />
                        </div>
                        <% } %>

                        <asp:Image ImageUrl="https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg"
                            runat="server" ID="imgArticulo" CssClass="img-fluid mb-3" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function actualizarImagen() {
            var inputFile = document.getElementById('<%= txtImagenArchivo.ClientID %>');
            var imgArticulo = document.getElementById('<%= imgArticulo.ClientID %>');

            if (inputFile.files && inputFile.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    imgArticulo.src = e.target.result;
                };
                reader.readAsDataURL(inputFile.files[0]);
            }
        }
    </script>

</asp:Content>
