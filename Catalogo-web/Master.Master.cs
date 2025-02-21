using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using Negocio;

namespace Catalogo_web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page is Login || Page is Default || Page is DetalleArticulo || Page is ListadoArticulo || Page is Error))
            {
                if (!Seguridad.SesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }
}