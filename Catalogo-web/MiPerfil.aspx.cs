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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.SesionActiva(Session["usuario"]))
                    {
                        Usuario usuario = (Usuario)Session["usuario"];
                        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                        usuario = usuarioNegocio.ObtenerUsuario(usuario.IdUsuario);
                        CargarDatos(usuario);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CargarDatos(Usuario usuario)
        {
            txtEmail.Text = usuario.Email;
            txtEmail.ReadOnly = true;
            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            if (!string.IsNullOrEmpty(usuario.UrlImagenPerfil))
            {
                imgNuevoPerfil.ImageUrl = "~/Images/" + usuario.UrlImagenPerfil;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;

                if (txtImagen.PostedFile.FileName != "") // Si es distinto de vacio, osea, si hay un archivo (imagen) seleccionado, cargo la imagen.
                {
                    string nombreImagen = "articulo-" + DateTime.Now.Ticks + ".jpg";
                    string ruta = Server.MapPath("./Images/"); // Es la ruta fisica en la que voy a trabajar
                    txtImagen.PostedFile.SaveAs(ruta + nombreImagen); // Obtengo lo datos del archivo que selecciono el usuario, le sumo el nombre y lo guardo en la ruta.
                    usuario.UrlImagenPerfil = nombreImagen; // Guardo el nombre de la imagen en la base de datos
                }

                usuarioNegocio.ActualizarUsuario(usuario);

                // Leer imagen para mostrar en el icono de perfil en el navbar.
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + usuario.UrlImagenPerfil;

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}