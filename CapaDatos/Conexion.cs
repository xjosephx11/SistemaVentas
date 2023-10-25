using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        //esta clase se encarga de enviar a las demas clases la cadena de
        //conexion que tenemos en nuestra appconfig
        //agregamos una referencia (systemconfiguracion, en capadatos)
        public static string cadena = ConfigurationManager.ConnectionStrings["CadenaConexion"].ToString();

    }
}
