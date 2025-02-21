using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool ValidarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("Select Id, email, pass, admin from USERS where email = @email and pass = @pass");
                datos.SetearParametro("@email", usuario.Email);
                datos.SetearParametro("@pass", usuario.Password);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["Id"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
