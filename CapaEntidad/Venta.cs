using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal PrecioTotal { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
    }
}
