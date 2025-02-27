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
            try
            {
                if (!IsPostBack)
                {
                    CargarArticulos();
                    CargarDdl();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        public void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session.Add("listaArticulos", negocio.ListarArticulos());
            repeaterArticulos.DataSource = Session["listaArticulos"];
            repeaterArticulos.DataBind();
        }

        
        public string RetornarPrecioConMenosDecimales(decimal precio) // Formatea el precio
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
            repeaterArticulos.DataSource = listaFiltrada;
            repeaterArticulos.DataBind();
        }

        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (Session["listaArticulos"] != null)
                {
                    List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];

                    // Filtro por Nombre, Marca o Categoría
                    List<Articulo> listaFiltrada = lista.FindAll(x =>
                        x.Nombre.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()) ||
                        (x.Marca != null && x.Marca.Descripcion.ToUpper().Contains(txtFiltroRapido.Text.ToUpper())) ||
                        (x.Categoria != null && x.Categoria.Descripcion != null && x.Categoria.Descripcion.ToUpper().Contains(txtFiltroRapido.Text.ToUpper()))
                    );

                    repeaterArticulos.DataSource = listaFiltrada;
                    repeaterArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            txtFiltroRapido.Enabled = !chkFiltroAvanzado.Checked;
        }

        protected void ddlOrdenarTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDdl();
        }

        private void CargarDdl()
        {
            ddlCriterio.Items.Clear(); // Para que no acumule las cosas cargadas.
            if ((ddlOrdenarTipo.SelectedItem.ToString() == "Categoría") || (ddlOrdenarTipo.SelectedItem.ToString() == "Marca"))
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                repeaterArticulos.DataSource = negocio.FiltrarArticulos(ddlOrdenarTipo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltro.Text);
                repeaterArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtFiltro.Text = "";
                ddlOrdenarTipo.SelectedIndex = ddlOrdenarTipo.Items.IndexOf(ddlOrdenarTipo.Items.FindByText("Categoría"));
                ddlCriterio.SelectedIndex = ddlCriterio.Items.IndexOf(ddlCriterio.Items.FindByText("Contiene"));
                CargarArticulos();
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
    }
}