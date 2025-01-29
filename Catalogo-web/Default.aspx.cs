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
            CargarArticulos();

        }

        public void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.ListarArticulos();
            Session.Add("listaArticulos", listaArticulos);

            RepeaterArticulos.DataSource = listaArticulos;
            RepeaterArticulos.DataBind();
        }


        // Formatea el precio
        public string RetornarPrecioConMenosDecimales(decimal precio)
        {
            return ArticuloNegocio.RetornarPrecioConMenosDecimales(precio);
        }
    }
}