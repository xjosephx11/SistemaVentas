using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDPermiso
    {
        public List<Permiso> listar(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();
            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdRol, p.MenuDescripcion from Permiso p");
                    query.AppendLine("inner join UsuarioRol r on r.IdRol = p.IdRol");
                    query.AppendLine("inner join Usuario u on u.IdRol = r.IdRol");
                    query.AppendLine("where u.IdUsuario = @IdUsuario");
                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", idusuario);
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                oRol = new UsuarioRol() { IdRol = Convert.ToInt32(dr["IdRol"]) },
                                NombreMenu = dr["MenuDescripcion"].ToString()
                            });

                        }
                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Permiso>();
                }
            }
            return lista;

        }
    }
}
