using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using Negocio;
using Dominio;

namespace Catalogo_web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://simg.nicepng.com/png/small/202-2022264_usuario-annimo-usuario-annimo-user-icon-png-transparent.png";
            if (!(Page is Login || Page is Default || Page is DetalleArticulo || Page is ListadoArticulo || Page is Error || Page is Registro))
            {
                if (!Seguridad.SesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            if (Seguridad.SesionActiva(Session["usuario"]))
            {
                Usuario usuario = (Usuario)Session["usuario"];
                if (!string.IsNullOrEmpty(usuario.UrlImagenPerfil)) //Valido si UrlImagenPerfil NO es nulo o vacio.
                {
                    imgAvatar.ImageUrl = "~/Images/" + usuario.UrlImagenPerfil;
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