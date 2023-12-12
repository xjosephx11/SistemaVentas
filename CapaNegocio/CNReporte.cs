using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNReporte
    {
        private CDReporte objcd_reporte = new CDReporte();

        public List<ReporteCompra> Compra(string fechainicio, string fechafin, int idproveedor) 
        {
            return objcd_reporte.Compra(fechainicio,fechafin,idproveedor);
        }

        public List<ReporteVenta> Venta(string fechainicio, string fechafin) 
        {
            return objcd_reporte.Venta(fechainicio,fechafin);
        }
    }
}
