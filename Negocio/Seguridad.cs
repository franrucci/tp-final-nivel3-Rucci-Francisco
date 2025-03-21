using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool SesionActiva(object user) // Metodo para validar si la sesion esta activa. Osea, si hay un usuario logueado.
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario != null && usuario.IdUsuario != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool EsAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            return usuario != null?  usuario.Admin : false; // si el usuario es distinto de null, devuelvo el valor de la propiedad Admin, sino, devuelvo false.
        }
    }
}
