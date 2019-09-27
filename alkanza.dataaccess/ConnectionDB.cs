using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace alkanza.dataaccess
{
    public static class ConnectionDB
    {
        /// <summary>
        /// Returna la cadena de conexion
        /// </summary>
        /// <returns>Cadena de Conexion</returns>
        public static string CadenaConexion()
        {
            string sCadenaConexion = string.Empty;
            sCadenaConexion = ConfigurationManager.AppSettings["connectionString"].ToString();
            return sCadenaConexion;
        }
    }
}
