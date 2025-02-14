using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Catalogo_web
{
	public partial class GestionArticulo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            ArticuloNegocio negocio = new ArticuloNegocio();
			dgvArticulos.DataSource = negocio.ListarArticulos();
            dgvArticulos.DataBind();
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idArticulo = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("DatosArticulo.aspx?id=" + idArticulo);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session["MostrarBoton"] = false;
            Response.Redirect("DatosArticulo.aspx");
        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SeleccionarProducto")
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                string id = dgvArticulos.DataKeys[indiceFila].Value.ToString();

                Response.Redirect($"DatosArticulo.aspx?id={id}");
            }
        }
    }
}