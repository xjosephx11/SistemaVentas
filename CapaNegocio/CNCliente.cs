using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNCliente
    {
        private CDCliente objectcdCliente = new CDCliente();

        public List<Cliente> listar()
        {
            //puente para la comunicacion
            return objectcdCliente.listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del cliente\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del cliente\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesaria el correo del cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdCliente.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del cliente\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del cliente\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es necesaria el correo del cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdCliente.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            //puente para la comunicacion para la capa de presentacion
            return objectcdCliente.Eliminar(obj, out Mensaje);
        }
    }
}
