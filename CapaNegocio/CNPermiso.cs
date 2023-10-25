using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNPermiso
    {
        private CDPermiso objectcdPermiso = new CDPermiso();

        public List<Permiso> listar(int idusuario)
        {
            return objectcdPermiso.listar(idusuario);
        }
    }
}
