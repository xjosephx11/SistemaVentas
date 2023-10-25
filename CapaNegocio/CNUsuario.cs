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
           
            return objectcdUsuario.listar();
        }
    }
}
