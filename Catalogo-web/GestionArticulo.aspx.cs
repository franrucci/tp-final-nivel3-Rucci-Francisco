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
            try
            {
                if (!Seguridad.EsAdmin(Session["usuario"]))
                {
                    Session.Add("error", "Se requieren permisos de ADMIN para acceder a esta sección.");
                    Response.Redirect("Error.aspx", false);
                }
                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulos.DataSource = negocio.ListarArticulos();
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var idArticulo = dgvArticulos.SelectedDataKey.Value.ToString();
                Response.Redirect("DatosArticulo.aspx?id=" + idArticulo, false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MostrarBoton"] = false;
                Response.Redirect("DatosArticulo.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SeleccionarProducto")
                {
                    int indiceFila = Convert.ToInt32(e.CommandArgument);
                    string id = dgvArticulos.DataKeys[indiceFila].Value.ToString();

                    Response.Redirect($"DatosArticulo.aspx?id={id}");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}