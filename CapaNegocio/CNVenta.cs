using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNVenta
    {
        private CDVenta objcd_Venta = new CDVenta();

        public bool RestarStock(int idproducto, int cantidad) 
        {
            return objcd_Venta.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad) 
        {
            return objcd_Venta.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            //puente para la comunicacion
            return objcd_Venta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {

            //puente para la comunicacion para la capa de presentacion
            return objcd_Venta.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public Venta ObtenerVenta(string numero) 
        {
            Venta oventa = objcd_Venta.ObtenerVenta(numero);
            if (oventa.IdVenta != 0)
            {
                List<DetalleVenta> oDetalleVenta = objcd_Venta.ObtenerDetalleVenta(oventa.IdVenta);
                oventa.oDetalleVenta = oDetalleVenta;
            }
            return oventa;
        }
    }
}
