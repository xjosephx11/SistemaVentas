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
    public class CDCategoria
    {
        public List<Categoria> listar()//listamos  la lista de usuarios
        {
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdCategoria, Descripcion, EstaActivo from Categoria");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                EstaActivo = Convert.ToBoolean(dr["EstaActivo"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                    lista = new List<Categoria>();
                }
            }
            return lista;

        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            int idcategoriagenerado = 0;
            Mensaje = string.Empty;

            try
            {
                //conexion a la base de datos
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCATEGORIA", oconexion);//procedimiento almacenado
                    //datps del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", obj.EstaActivo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();//abrimos la coneccion
                    cmd.ExecuteNonQuery();//lo ejecutamos
                    //obtenemos el valor de estas 2 variables desde el procedimiento almacenado y los agregamos en las variables
                    idcategoriagenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idcategoriagenerado = 0;
                Mensaje = ex.Message;
            }

            return idcategoriagenerado;
        }


        public bool Editar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                //conexion a la base de datos
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARCATEGORIA", oconexion);//procedimiento almacenado
                    //datos del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Estado", obj.EstaActivo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();//abrimos la coneccion
                    cmd.ExecuteNonQuery();//lo ejecutamos
                    //obtenemos el valor de estas 2 variables desde el procedimiento almacenado y los agregamos en las variables
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }


        public bool Eliminar(Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                //conexion a la base de datos
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARCATEGORIA", oconexion);//procedimiento almacenado
                    //datos del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();//abrimos la coneccion
                    cmd.ExecuteNonQuery();//lo ejecutamos
                    //obtenemos el valor de estas 2 variables desde el procedimiento almacenado y los agregamos en las variables
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}
