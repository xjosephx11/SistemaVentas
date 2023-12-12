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
    public class CNCompra
    {
        private CDCompra objcd_Compra = new CDCompra();

        public int ObtenerCorrelativo()
        {
            //puente para la comunicacion
            return objcd_Compra.ObtenerCorrelativo();
        }

        public bool Registrar(Compra obj,DataTable DetalleCompra, out string Mensaje)
        {

            //puente para la comunicacion para la capa de presentacion
            return objcd_Compra.Registrar(obj, DetalleCompra, out Mensaje);
        }

        public Compra ObtenerCompra(string numero) 
        {
            Compra oCompra = objcd_Compra.ObtenerCompra(numero);

            if (oCompra.IdCompra != 0)
            {
                List<DetalleCompra> oDetalleCompra = objcd_Compra.ObtenerDetalleCompra(oCompra.IdCompra);
                oCompra.oDetalleCompra = oDetalleCompra;  
            }
            return oCompra;
        }
    }
}
