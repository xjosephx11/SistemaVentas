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
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuario, u.Documento,u.Nombre, u.Correo, u.Contrasenia, u.EstaActivo, r.IdRol, r.Descripcion from Usuario u");
                    query.AppendLine("inner join UsuarioRol r on r.IdRol = u.IdRol");
                    SqlCommand cmd = new SqlCommand (query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open ();
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

        public int Registrar(Usuario obj, out string Mensaje) 
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                //conexion a la base de datos
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) 
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARUSUARIO", oconexion);//procedimiento almacenado
                    //datps del procedimiento almacenado
                    cmd.Parameters.AddWithValue("Documento",obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.EstaActivo);

                    cmd.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();//abrimos la coneccion
                    cmd.ExecuteNonQuery();//lo ejecutamos
                    //obtenemos el valor de estas 2 variables desde el procedimiento almacenado y los agregamos en las variables
                    idusuariogenerado = Convert.ToInt32(cmd.Parameters["IdUsuarioResultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = ex.Message;
            }

            return idusuariogenerado;
        }


        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                //conexion a la base de datos
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARRUSUARIO", oconexion);//procedimiento almacenado
                    //datos del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("Estado", obj.EstaActivo);

                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();//abrimos la coneccion
                    cmd.ExecuteNonQuery();//lo ejecutamos
                    //obtenemos el valor de estas 2 variables desde el procedimiento almacenado y los agregamos en las variables
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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


        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                //conexion a la base de datos
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ELIMINARUSUARIO", oconexion);//procedimiento almacenado
                    //datos del procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);

                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();//abrimos la coneccion
                    cmd.ExecuteNonQuery();//lo ejecutamos
                    //obtenemos el valor de estas 2 variables desde el procedimiento almacenado y los agregamos en las variables
                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
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
