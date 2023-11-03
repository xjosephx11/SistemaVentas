using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNCategoria
    {
        private CDCategoria objectcdCategoria = new CDCategoria();

        public List<Categoria> listar()
        {
            //puente para la comunicacion
            return objectcdCategoria.listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion de la categotria\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdCategoria.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesaria la descripcion de la categotria\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                //puente para la comunicacion para la capa de presentacion
                return objectcdCategoria.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Categoria obj, out string Mensaje)
        {
            //puente para la comunicacion para la capa de presentacion
            return objectcdCategoria.Eliminar(obj, out Mensaje);
        }
    }
}
