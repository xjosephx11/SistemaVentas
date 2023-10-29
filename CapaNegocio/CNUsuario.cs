using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CNUsuario
    {
        private CDUsuario objectcdUsuario = new CDUsuario();

        public List<Usuario> listar() 
        {
           //puente para la comunicacion
            return objectcdUsuario.listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.Contrasenia == "")
            {
                Mensaje += "Es necesaria la contraseña del usuario\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else 
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdUsuario.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }
            if (obj.Documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.Contrasenia == "")
            {
                Mensaje += "Es necesaria la contraseña del usuario\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdUsuario.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            //puente para la comunicacion para la capa de presentacion
            return objectcdUsuario.Eliminar(obj, out Mensaje);
        }
    }
}
