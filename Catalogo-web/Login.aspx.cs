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
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    Session.Add("Error", "Los campos Email y Contraseña estan vacios.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                Page.Validate();
                if (!Page.IsValid) 
                    return;

                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();
                usuario.Email = txtEmail.Text;
                usuario.Password = txtPassword.Text;
                if (negocio.ValidarUsuario(usuario)) // Si se logueo correctamente
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                    lblError.Visible = true;
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