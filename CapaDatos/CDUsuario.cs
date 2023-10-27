using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Net.Http.Headers;

namespace CapaDatos
{
    public class CDUsuario
    {
        //agregamos a capaDatos la referencia de capaModelos
        //listar todos los usuarios de la BD
        public List<Usuario> listar()//listamos  la lista de usuarios
        {
            List<Usuario> lista = new List<Usuario> ();
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario, u.Documento,u.Nombre, u.Correo, u.Contrasenia, u.EstaActivo, r.IdRol, r.Descripcion from Usuario u");
                    query.AppendLine("inner join UsuarioRol r on r.IdRol = u.IdRol");
                    SqlCommand cmd = new SqlCommand (query.ToString(), conexion);
                    cmd.CommandType = CommandType.Text;
                    conexion.Open ();
                    using (SqlDataReader dr = cmd.ExecuteReader()) 
                    {
                        
                        while (dr.Read()) 
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["Nombre"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Contrasenia = dr["Contrasenia"].ToString(),
                                EstaActivo = Convert.ToBoolean(dr["EstaActivo"].ToString()),
                                oRol = new UsuarioRol() { IdRol = Convert.ToInt32(dr["IdRol"]), Descripcion = dr["Descripcion"].ToString() }
                            });
                            
                        }
                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Usuario>();
                }
            }
            return lista;

        }

    }
}
