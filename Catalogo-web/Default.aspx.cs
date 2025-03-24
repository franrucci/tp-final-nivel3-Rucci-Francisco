using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace Catalogo_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArticulos();
            }
        }

        public void CargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.ListarArticulos().Take(6).ToList(); // Toma solo 6 artículos
                Session["listaArticulos"] = listaArticulos;

                if (listaArticulos.Count == 0)
                {
                    lblMensaje.Visible = true; // Muestra el mensaje si no hay artículos
                    RepeaterArticulos.Visible = false; // Oculta el Repeater
                }
                else
                {
                    lblMensaje.Visible = false; // Oculta el mensaje si hay artículos
                    RepeaterArticulos.Visible = true; // Muestra el Repeater
                    RepeaterArticulos.DataSource = listaArticulos;
                    RepeaterArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        // Formatea el precio
        public string RetornarPrecioConMenosDecimales(decimal precio)
        {
            return ArticuloNegocio.RetornarPrecioConMenosDecimales(precio);
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
    }
}