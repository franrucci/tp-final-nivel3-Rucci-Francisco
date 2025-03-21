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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // --------------------------------------------------------------------------------
                // VALIDACIONES EXTRA DE CAMPOS VACIOS EN EL FORMULARIO DE LOGIN
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    Session.Add("Error", "Los campos Email y Contraseña estan vacios.");
                    Response.Redirect("Error.aspx", false);
                    return; // Detengo la ejecucion del metodo, no sigue con las siguientes lineas.
                }
                //--------------------------------------------------------------------------------

                Page.Validate(); // Valido los controles del formulario.
                if (!Page.IsValid) 
                    return; // Si no se cumple la validacion de los controles del fomrulario, se detiene la ejecucion del metodo, no sigue con las siguientes lineas.

                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.Email = txtEmail.Text;
                usuario.Password = txtPassword.Text;

                if (negocio.ValidarUsuario(usuario)) // Si este metodo devuelve TRUE, es porque existe el usuario en la base de datos.
                {
                    Session.Add("usuario", usuario); // Guardo el usuario en la sesion.
                    Response.Redirect("Default.aspx", false); // Redirecciono a la pagina principal Default.aspx.
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos."; // Si el usuario no existe en la base de datos, muestro un mensaje de error.
                    lblError.Visible = true; // Hago visible el mensaje de error.
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}