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
    public class ClienteDatos : Conexion
    {
        public List<Cliente> ObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_MostrarClientes";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(reader["idCliente"]),
                                Nombres = Convert.ToString(reader["nombres"]),
                                Apellidos = Convert.ToString(reader["apellidos"]),
                                Dni = Convert.ToString(reader["dni"]),
                                Correo = Convert.ToString(reader["correo"]),
                                Direccion = Convert.ToString(reader["direccion"])
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }

        public void InsertarCliente(Cliente cliente)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_InsertarCliente";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@dni", cliente.Dni);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarCliente(Cliente cliente)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EditarCliente";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                    cmd.Parameters.AddWithValue("@dni", cliente.Dni);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarCliente(int idCliente)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EliminarCliente";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
