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
    public class ProductoDatos : Conexion
    {
        public List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_MostrarProductos";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Producto producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                Nombre = Convert.ToString(reader["nombre"]),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                IdCategoria = Convert.ToInt32(reader["idCategoria"])
                            };

                            productos.Add(producto);
                        }
                    }
                }
            }

            return productos;
        }

        public void InsertarProducto(Producto producto)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_InsertarProducto";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@idCategoria", producto.IdCategoria);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarProducto(Producto producto)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EditarProducto";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@idCategoria", producto.IdCategoria);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarProducto(int idProducto)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EliminarProducto";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
