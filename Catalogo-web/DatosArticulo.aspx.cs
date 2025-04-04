﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_web
{
    public partial class DatosArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["MostrarBoton"] != null && (bool)Session["MostrarBoton"] == false)
                    {
                        btnEliminar.Visible = false;
                        Session["MostrarBoton"] = true;
                    }
                    else
                    {
                        btnEliminar.Visible = true;
                    }
                    if (Request.QueryString["id"] != null) // Si hay un ID en la URL, osea estamos MODIFICANDO.
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        Articulo articulo = negocio.ObtenerArticulo(id);

                        articulo.Id = id;

                        CargarDdlMarca(articulo.Marca.Id);
                        CargarDdlCategoria(articulo.Categoria.Id);
                        CargarDatos(articulo);
                    }
                    else
                    {
                        CargarDdlMarca(null); // Si no hay producto, solo carga las marcas
                        CargarDdlCategoria(null); // y categorias
                    }
                }
            }
            catch(Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarDdlMarca(int? marcaSeleccionadaId)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            var listaMarcas = marcaNegocio.ListarMarcas();

            ddlMarca.DataSource = listaMarcas;
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            // Si hay una marca seleccionada la establece en el ddl.
            if (marcaSeleccionadaId.HasValue)
            {
                ddlMarca.SelectedValue = marcaSeleccionadaId.Value.ToString();
            }
        }

        private void CargarDdlCategoria(int? categoriaSeleccionadaId)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            var listaCategorias = categoriaNegocio.ListarCategorias();

            ddlCategoria.DataSource = listaCategorias;
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();

            // Si hay una categora seleccionada la establece en el ddl.
            if (categoriaSeleccionadaId.HasValue)
            {
                ddlCategoria.SelectedValue = categoriaSeleccionadaId.Value.ToString();
            }
        }
        
        private void CargarDatos(Articulo articulo)
        {
            txtCodigo.Text = articulo.Codigo;
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            txtPrecio.Text = articulo.Precio.ToString("F2"); // "F2" asegura 2 decimales.

            if (articulo.ImagenUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                imgArticulo.ImageUrl = articulo.ImagenUrl;
                txtImagenUrlArticulo.Text = articulo.ImagenUrl;
            }
            else
            {
                imgArticulo.ImageUrl = "~/Images/" + articulo.ImagenUrl;
                // Solo borrar el texto si no hay archivo subido
                if (string.IsNullOrEmpty(articulo.ImagenUrl) || chkSubirArchivo.Checked)
                {
                    txtImagenUrlArticulo.Text = "";
                }
                else
                {
                    txtImagenUrlArticulo.Text = articulo.ImagenUrl;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    Session.Add("error", "Hay campos obligatorios que estan vacios.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                Page.Validate();
                if (!Page.IsValid)
                    return;

                ArticuloNegocio negocio = new ArticuloNegocio();

                Articulo articulo = new Articulo();
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;

                articulo.Marca = new Marca();
                articulo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                articulo.Categoria = new Categoria();
                articulo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                // Verifica si se sube un nuevo archivo o se utiliza la URL
                if (chkSubirArchivo.Checked)
                {
                    string nombreImagen = "articulo-" + DateTime.Now.Ticks + ".jpg";
                    string ruta = Server.MapPath("./Images/"); // Es la ruta fisica en la que voy a trabajar
                    txtImagenArchivo.PostedFile.SaveAs(ruta + nombreImagen); // Obtengo lo datos del archivo que selecciono el usuario, le sumo el nombre y lo guardo en la ruta.

                    articulo.ImagenUrl = nombreImagen;
                }
                else if (!string.IsNullOrEmpty(txtImagenUrlArticulo.Text))
                {
                    articulo.ImagenUrl = txtImagenUrlArticulo.Text;
                }

                articulo.Precio = decimal.Parse(txtPrecio.Text);

                if (Request.QueryString["id"] != null)
                {
                    articulo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.ModificarArticulo(articulo);
                }
                else
                {
                    negocio.AgregarArticulo(articulo);
                }

                Response.Redirect("GestionArticulo.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void txtImagenUrlArticulo_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtImagenUrlArticulo.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.EliminarArticulo(int.Parse(Request.QueryString["id"]));
                Response.Redirect("GestionArticulo.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void chkSubirArchivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubirArchivo.Checked)
            {
                divFileUpload.Visible = true;
                divUrlImagen.Visible = false;
            }
            else
            {
                divFileUpload.Visible = false;
                divUrlImagen.Visible = true;
            }
        }
    }
}