using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class FavoritoNegocio
    {
        public bool AgregarFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("Insert into Favoritos (IdUser, IdArticulo) Values (@IdUser, @IdArticulo)");
                datos.SetearParametro("@IdUser", idUsuario);
                datos.SetearParametro("@IdArticulo", idArticulo);

                datos.EjecutarAccion();
                return true;
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

        public bool EsFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM Favoritos WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.SetearParametro("@IdUser", idUsuario);
                datos.SetearParametro("@IdArticulo", idArticulo);

                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    return datos.Lector.GetInt32(0) > 0;
                }
                return false;
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

        public bool EliminarFavorito(int idUsuario, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("DELETE FROM Favoritos WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.SetearParametro("@IdUser", idUsuario);
                datos.SetearParametro("@IdArticulo", idArticulo);

                datos.EjecutarAccion();
                return true;
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

        public List<int> ObtenerFavoritos(int idUsuario)
        {
            List<int> favoritos = new List<int>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT IdArticulo FROM Favoritos WHERE IdUser = @IdUser");
                datos.SetearParametro("@IdUser", idUsuario);

                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    favoritos.Add((int)datos.Lector["IdArticulo"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

            return favoritos;
        }


    }
}
