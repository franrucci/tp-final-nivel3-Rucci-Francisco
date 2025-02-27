using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_web
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        private int idArticulo;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.Count == 0 || !int.TryParse(Request.QueryString["id"], out idArticulo))
                {
                    Session.Add("error", "Error");
                    Response.Redirect("Error.aspx", false);
                }
                CargarProducto();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        public void CargarProducto()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo;

            articulo = articuloNegocio.ObtenerArticulo(idArticulo);

            lblTituloArticulo.Text = $"{articulo.Marca.Descripcion} {articulo.Nombre}";
            imgArticulo.ImageUrl = ArticuloNegocio.RetornarImagenValida(articulo.ImagenUrl);
            lblNombreArticulo.Text = articulo.Nombre;
            lblDescripcionArticulo.Text = articulo.Descripcion;
            lblMarcaArticulo.Text = articulo.Marca.Descripcion;
            lblCategoriaArticulo.Text = articulo.Categoria.Descripcion;
            lblPrecioArticulo.Text = "$" + ArticuloNegocio.RetornarPrecioConMenosDecimales(articulo.Precio);
        }
    }
}