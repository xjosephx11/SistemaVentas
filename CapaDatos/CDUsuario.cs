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
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario> ();
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = "select IdUsuario,Documento,Nombre, Correo, Contrasenia, EstaActivo from Usuario";
                    SqlCommand cmd = new SqlCommand (query, conexion);
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
                                EstaActivo = Convert.ToBoolean(dr["EstaActivo"].ToString())
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
