using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class ProductoLogica
    {
        private ProductoDatos productoDatos;

        public ProductoLogica()
        {
            productoDatos = new ProductoDatos();
        }

        public List<Producto> ObtenerProductos()
        {
            return productoDatos.ObtenerProductos();
        }

        public void InsertarProducto(Producto producto)
        {
            productoDatos.InsertarProducto(producto);
        }

        public void EditarProducto(Producto producto)
        {
            productoDatos.EditarProducto(producto);
        }

        public void EliminarProducto(int idProducto)
        {
            productoDatos.EliminarProducto(idProducto);
        }
    }

}
