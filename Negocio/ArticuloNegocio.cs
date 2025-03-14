using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Net.Http;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public const string ImagenError = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";

        public List<Articulo> ListarArticulos()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Tipo, A.IdMarca, A.IdCategoria, ImagenUrl, Precio From ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdCategoria = C.Id And A.IdMarca = M.Id");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Tipo"];

                    listaArticulos.Add(articulo);
                }
                return listaArticulos;
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

        public List<Articulo> FiltrarArticulos(string campo, string criterio, string filtro)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Tipo, A.IdMarca, A.IdCategoria, ImagenUrl, Precio " +
                                  "From ARTICULOS A, CATEGORIAS C, MARCAS M " +
                                  "Where A.IdCategoria = C.Id And A.IdMarca = M.Id And ";

                if (campo == "Categoría")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + filtro + "' ";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + filtro + "%' ";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "' ";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%' ";
                            break;
                    }
                }
                else // para "Precio"
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consulta += "Precio = " + filtro + " ";
                            break;
                        case "Mayor a":
                            consulta += "Precio > " + filtro + " ";
                            break;
                        default:
                            consulta += "Precio < " + filtro + " ";
                            break;
                    }
                }

                datos.SetearConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Tipo"];

                    listaArticulos.Add(articulo);
                }
                return listaArticulos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void AgregarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (" + "'" + articulo.Codigo + "', " + "'" + articulo.Nombre + "', " + "'" + articulo.Descripcion + "', " + articulo.Marca.Id + ", " + articulo.Categoria.Id + ", " + "'" + articulo.ImagenUrl + "', " + articulo.Precio + ")");
                datos.SetearParametro("@idMarca", articulo.Marca.Id);
                datos.SetearParametro("@idCategoria", articulo.Categoria.Id);
                datos.SetearParametro("@imagenUrl", articulo.ImagenUrl);
                datos.EjecutarAccion();
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

        public void ModificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio Where Id = @id");
                datos.SetearParametro("@codigo", articulo.Codigo);
                datos.SetearParametro("@nombre", articulo.Nombre);
                datos.SetearParametro("@descripcion", articulo.Descripcion);
                datos.SetearParametro("@idMarca", articulo.Marca.Id);
                datos.SetearParametro("@idCategoria", articulo.Categoria.Id);
                datos.SetearParametro("@imagenUrl", articulo.ImagenUrl);
                datos.SetearParametro("@precio", articulo.Precio);
                datos.SetearParametro("@id", articulo.Id);

                datos.EjecutarAccion();
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

        public void EliminarArticulo(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearConsulta("delete from ARTICULOS where id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string RetornarPrecioConMenosDecimales(decimal precio)
        {
            decimal precioTruncado = Math.Floor(precio * 100) / 100;
            return precioTruncado.ToString("F2");
        }

        public Articulo ObtenerArticulo(int id)
        {
            Articulo articulo = new Articulo();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Tipo, A.IdMarca, A.IdCategoria, ImagenUrl, Precio From ARTICULOS A, CATEGORIAS C, MARCAS M Where A.IdCategoria = C.Id And A.IdMarca = M.Id And A.Id = @id");
                datos.SetearParametro("@id", id);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];
                    articulo.Descripcion = (string)datos.Lector["Descripcion"];
                    articulo.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Tipo"];
                }
                return articulo;
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

        public static string RetornarImagenValida(string rutaImagen)
        {
            if (string.IsNullOrEmpty(rutaImagen))
            {
                return ImagenError;
            }

            // Primero, si es una URL, la retornamos directamente sin validar la extensión
            if (rutaImagen.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                rutaImagen.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return rutaImagen;
            }

            // Luego, validamos extensiones para imágenes locales
            string[] extensionesValidas = { ".jpg", ".jpeg", ".png" };
            if (!extensionesValidas.Any(ext => rutaImagen.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            {
                return ImagenError;
            }

            return "~/Images/" + rutaImagen;
        }
    }
}