using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public UsuarioRol oRol { get; set; }
        public bool EstaActivo { get; set; }
        public string FechaRegistro { get; set; }
    }
}
