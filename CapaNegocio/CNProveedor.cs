using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNProveedor
    {

        private CDProveedor objectcdProveedor = new CDProveedor();

        public List<Proveedor> listar()
        {
            //puente para la comunicacion
            return objectcdProveedor.listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del proveedor\n";
            }
            if (obj.RasonSocial == "")
            {
                Mensaje += "Es necesario la razon social del proveedor\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesaria el correo del proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdProveedor.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del proveedor\n";
            }
            if (obj.RasonSocial == "")
            {
                Mensaje += "Es necesario la razon social del proveedor\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesaria el correo del proveedor\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdProveedor.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            //puente para la comunicacion para la capa de presentacion
            return objectcdProveedor.Eliminar(obj, out Mensaje);
        }
    }
}
