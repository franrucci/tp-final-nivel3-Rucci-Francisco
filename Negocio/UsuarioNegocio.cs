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

        public int RegistrarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into USERS (email, pass, admin) output inserted.Id values (@email, @pass, 0)"); // Esta consulta agrega y devuelve el Id del usuario insertado. 0 porque un usuario que se registra no sera ADMIN.
                datos.SetearParametro("@email", usuario.Email);
                datos.SetearParametro("@pass", usuario.Password);
                return datos.EjecutarAccionScalar(); // Ejecuto la consulta y devuelvo el Id del usuario insertado.
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
