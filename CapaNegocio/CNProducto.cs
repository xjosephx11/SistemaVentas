using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNProducto
    {
        private CDProducto objectcdProducto = new CDProducto();

        public List<Producto> listar()
        {
            //puente para la comunicacion
            return objectcdProducto.listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el codigo del producto\n";
            }
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion del producto\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdProducto.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario el codigo del producto\n";
            }
            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion del producto\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdProducto.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            //puente para la comunicacion para la capa de presentacion
            return objectcdProducto.Eliminar(obj, out Mensaje);
        }
    }
}
