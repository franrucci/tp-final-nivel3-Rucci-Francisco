using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Catalogo_web
{
	public partial class Error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["error"] != null)
            {
                lblError.Text = Session["error"].ToString();
                Session["error"] = null; // Limpia la sesión después de mostrar el error
            }
            else
            {
                lblError.Text = "Ocurrió un error inesperado.";
            }
        }
	}
}