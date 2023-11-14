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
    public class VentaDatos : Conexion
    {
        public List<Venta> ObtenerVentas()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_MostrarVentas";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Venta venta = new Venta
                            {
                                IdVenta = Convert.ToInt32(reader["idVenta"]),
                                Cantidad = Convert.ToInt32(reader["cantidad"]),
                                FechaVenta = Convert.ToDateTime(reader["fechaVenta"]),
                                PrecioTotal = Convert.ToDecimal(reader["precioTotal"]),
                                IdCliente = Convert.ToInt32(reader["idCliente"]),
                                IdProducto = Convert.ToInt32(reader["idProducto"])
                            };

                            ventas.Add(venta);
                        }
                    }
                }
            }

            return ventas;
        }
        public void InsertarVenta(Venta venta)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_InsertarVenta";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                    cmd.Parameters.AddWithValue("@fechaVenta", venta.FechaVenta);
                    cmd.Parameters.AddWithValue("@precioTotal", venta.PrecioTotal);
                    cmd.Parameters.AddWithValue("@idCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("@idProducto", venta.IdProducto);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }

}
