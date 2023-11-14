using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class VentaLogica
    {
        private VentaDatos ventaDatos;

        public VentaLogica()
        {
            ventaDatos = new VentaDatos();
        }

        public List<Venta> ObtenerVentas()
        {
            return ventaDatos.ObtenerVentas();
        }

        public void InsertarVenta(Venta venta)
        {
            ventaDatos.InsertarVenta(venta);
        }
    }

}
