using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CDVenta
    {
        public int ObtenerCorrelativo()
        {
            int IdCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from Venta");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    IdCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    IdCorrelativo = 0;
                }
            }
            return IdCorrelativo;
        }

        public bool RestarStock(int idproducto, int cantidad) 
        {
            bool respuesta = true;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Producto set Stock = Stock - @cantidad where IdProducto = @idproducto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad",cantidad);
                    cmd.Parameters.AddWithValue("@idproducto",idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta =false;
                }
            }
            return respuesta;
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) 
                {
                    SqlCommand cmd = new SqlCommand("SPRegistrarVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario",obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("TipoDocumento",obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento",obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("DocumentoCliente",obj.DocumentoCLiente);
                    cmd.Parameters.AddWithValue("NombreCliente",obj.NombreCLiente);
                    cmd.Parameters.AddWithValue("MontoPago",obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio",obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleVenta",obj.oDetalleVenta);
                    cmd.Parameters.Add("Resultado",SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje",SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje += ex.Message;
            }
            return Respuesta;
        }

        public bool SumarStock(int idproducto, int cantidad) 
        {
            bool Respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) 
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Producto set Stock = Stock + @cantidad wuere IdProducto = @idproducto");
                    SqlCommand cmd = new SqlCommand(query.ToString(),oconexion);
                    cmd.Parameters.AddWithValue("cantidad", cantidad);
                    cmd.Parameters.AddWithValue("idproducto", idproducto);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    Respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    Respuesta = false;
                }
            }
            return Respuesta;
        }


        public Venta ObtenerVenta(string numero) 
        {
            Venta obj = new Venta();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) 
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVenta,u.Nombre,v.DocumentoCliente,v.NombreCLiente,");
                    query.AppendLine("v.TipoDocumento,v.NumeroDocumento, v.MontoPago,v.MontoCambio,v.MontoTotal,");
                    query.AppendLine("CONVERT(char(10),v.FechaRegistro,103)[FechaRegistro]");
                    query.AppendLine("from Venta v");
                    query.AppendLine("inner join Usuario u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("where v.NumeroDocumento = @numero");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader()) 
                    {
                        while (dr.Read()) 
                        {
                            obj = new Venta()
                            {
                                IdVenta = int.Parse(dr["IdVenta"].ToString()),
                                oUsuario = new Usuario() { NombreCompleto = dr["Nombre"].ToString() },
                                DocumentoCLiente = dr["DocumentoCliente"].ToString(),
                                NombreCLiente = dr["NombreCLiente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch
                {
                    obj = new Venta();
                }
            }
            return obj;
        }

        public List<DetalleVenta> ObtenerDetalleVenta(int idventa) 
        {
            List<DetalleVenta> olista = new List<DetalleVenta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena)) 
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Nombre, dv.PrecioVenta,dv.Cantidad,dv.SubTotal");
                    query.AppendLine("from DetalleVenta dv");
                    query.AppendLine("inner join Producto p on p.IdProducto = dv.IdProducto");
                    query.AppendLine("where dv.IdVenta = 1");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", idventa);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            olista.Add(new DetalleVenta() 
                            {
                                oProducto = new Producto() { Nombre = dr["Nombre"].ToString() },
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString()),
                            });
                        }
                    }
                }
                catch
                {
                    olista = new List<DetalleVenta>();
                }
            }
            return olista;
        }






    }
}
