using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNNegocio
    {
        private CDNegocio objectcdNegocio = new CDNegocio();

        public Negocio ObtenerDatos()
        {
            //puente para la comunicacion
            return objectcdNegocio.ObtenerDatos();
        }

        public bool GuardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del negocio\n";
            }
            if (obj.RUC == "")
            {
                Mensaje += "Es necesario el RUC del negocio\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es necesaria la direccion del negocio\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdNegocio.GuardarDatos(obj, out Mensaje);
            }
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            //puente para la comunicacion
            return objectcdNegocio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            //puente para la comunicacion
            return objectcdNegocio.ActualizarLogo(imagen, out mensaje);
        }
    }
}
