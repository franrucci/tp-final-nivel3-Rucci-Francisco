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
    public partial class ListadoArticulo : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarArticulos();

        }

        public void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session.Add("listaArticulos", negocio.ListarArticulos());
            RepeaterArticulos.DataSource = Session["listaArticulos"];
            RepeaterArticulos.DataBind();
        }

        // Formatea el precio
        public string RetornarPrecioConMenosDecimales(decimal precio)
        {
            return ArticuloNegocio.RetornarPrecioConMenosDecimales(precio);
        }
    }
}