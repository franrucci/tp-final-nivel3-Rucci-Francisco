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

        protected void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {

            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x =>
                x.Nombre.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()) ||
                (x.Marca != null && x.Marca.Descripcion.ToUpper().Contains(txtFiltroRapido.Text.ToUpper())) ||
                (x.Categoria != null && x.Categoria.Descripcion != null && x.Categoria.Descripcion.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()))
            );
            RepeaterArticulos.DataSource = listaFiltrada;
            RepeaterArticulos.DataBind();
        }

        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            // Verifica si hay una lista en la sesión
            if (Session["listaArticulos"] != null)
            {
                List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];

                // Filtra por Nombre, Marca o Categoría
                List<Articulo> listaFiltrada = lista.FindAll(x =>
                    x.Nombre.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()) ||
                    (x.Marca != null && x.Marca.Descripcion.ToUpper().Contains(txtFiltroRapido.Text.ToUpper())) ||
                    (x.Categoria != null && x.Categoria.Descripcion != null && x.Categoria.Descripcion.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()))
                );

                // Asigna la lista filtrada al Repeater
                RepeaterArticulos.DataSource = listaFiltrada;
                RepeaterArticulos.DataBind();
            }
        }
    }
}