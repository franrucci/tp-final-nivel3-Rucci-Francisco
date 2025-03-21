using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Catalogo_web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();
                EmailService emailService = new EmailService();

                usuario.Email = txtEmail.Text;
                usuario.Password = txtPassword.Text;
                usuario.IdUsuario = negocio.RegistrarUsuario(usuario); // Este metodo devuelve el Id del usuario insertado.
                Session.Add("usuario", usuario);

                emailService.ArmarCorreo(usuario.Email, "Equipo de Catalogo Digital", "¡Hola! Te damos la bienvenida a nuestro Catálogo Digital.");
                emailService.EnviarEmail();
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}