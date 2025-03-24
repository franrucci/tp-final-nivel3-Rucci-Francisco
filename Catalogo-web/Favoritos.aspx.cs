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
    public partial class Favoritos : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarArticulos();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        public string ObtenerRutaImagen(string imagenUrl)
        {
            if (string.IsNullOrEmpty(imagenUrl))
            {
                return ArticuloNegocio.ImagenError;
            }

            if (imagenUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                return imagenUrl;
            }

            return "~/Images/" + imagenUrl;
        }

        public string RetornarPrecioConMenosDecimales(decimal precio) // Formatea el precio
        {
            return ArticuloNegocio.RetornarPrecioConMenosDecimales(precio);
        }

        public void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.ListarArticulos();
            Session.Add("listaArticulos", listaArticulos);

            // Verifica si el usuario ha marcado algún artículo como favorito
            if (Session["usuario"] != null)
            {
                Usuario usuarioActual = (Usuario)Session["usuario"];
                FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
                List<int> favoritosUsuario = favoritoNegocio.ObtenerFavoritos(usuarioActual.IdUsuario);  // Lista de IDs de los artículos favoritos

                // Filtra los artículos para que solo se muestren los favoritos
                listaArticulos = listaArticulos.Where(articulo => favoritosUsuario.Contains(articulo.Id)).ToList();
            }

            // Verifica si hay favoritos y muestra el mensaje en caso contrario
            lblSinFavoritos.Visible = listaArticulos.Count == 0;

            // Asigna la lista filtrada de artículos favoritos al Repeater
            repeaterArticulos.DataSource = listaArticulos;
            repeaterArticulos.DataBind();
        }

        protected bool EsFavorito(object idArticulo)
        {
            if (ViewState["Favoritos"] != null)
            {
                List<int> favoritos = (List<int>)ViewState["Favoritos"];
                return favoritos.Contains(Convert.ToInt32(idArticulo));
            }
            return false;
        }

        protected void btnFavorito_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            try
            {
                Button btn = (Button)sender;
                int idArticulo = int.Parse(btn.CommandArgument);
                Usuario usuarioActual = (Usuario)Session["usuario"];


                if (favoritoNegocio.EsFavorito(usuarioActual.IdUsuario, idArticulo))
                {
                    favoritoNegocio.EliminarFavorito(usuarioActual.IdUsuario, idArticulo);
                }
                else
                {
                    favoritoNegocio.AgregarFavorito(usuarioActual.IdUsuario, idArticulo);
                }

                // Recarga la lista para reflejar cambios
                CargarArticulos();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}