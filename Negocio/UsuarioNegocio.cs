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
                datos.SetearConsulta("Select Id, email, pass, admin, urlImagenPerfil from USERS where email = @email and pass = @pass");
                datos.SetearParametro("@email", usuario.Email);
                datos.SetearParametro("@pass", usuario.Password);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["Id"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    if(!(datos.Lector["urlImagenPerfil"] is DBNull)) // Si no es NULL la cadena de la imagen del usuario en la BD, obtengo esa cadena
                    {
                        usuario.UrlImagenPerfil = (string)datos.Lector["urlImagenPerfil"];
                    }
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

        public void ActualizarUsuario(Usuario usuario)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            try
            {
                accesoDatos.SetearConsulta("update USERS set nombre = @nombre, apellido = @apellido, urlImagenPerfil = @imagen Where Id = @id");
                accesoDatos.SetearParametro("@nombre", usuario.Nombre);
                accesoDatos.SetearParametro("@apellido", usuario.Apellido);
                accesoDatos.SetearParametro("@imagen", usuario.UrlImagenPerfil != null ? usuario.UrlImagenPerfil : "");
                accesoDatos.SetearParametro("@id", usuario.IdUsuario);

                accesoDatos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                accesoDatos.CerrarConexion();
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            Usuario usuario = new Usuario();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("Select Id, Email, Nombre, apellido, urlImagenPerfil From USERS Where Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["Id"];
                    usuario.Email = (string)datos.Lector["Email"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil = (string)datos.Lector["urlImagenPerfil"];
                }
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
