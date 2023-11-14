using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class EmpleadoDatos : Conexion
    {
        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();

            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_MostrarEmpleados";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado empleado = new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                                Nombres = Convert.ToString(reader["nombres"]),
                                Apellidos = Convert.ToString(reader["apellidos"]),
                                Dni = Convert.ToString(reader["dni"]),
                                Tipo = Convert.ToString(reader["tipo"]),
                                Sueldo = Convert.ToInt32(reader["sueldo"]),
                                Direccion = Convert.ToString(reader["direccion"])
                            };

                            empleados.Add(empleado);
                        }
                    }
                }
            }

            return empleados;
        }

        public void InsertarEmpleado(Empleado empleado)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_InsertarEmpleado";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombres", empleado.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
                    cmd.Parameters.AddWithValue("@dni", empleado.Dni);
                    cmd.Parameters.AddWithValue("@tipo", empleado.Tipo);
                    cmd.Parameters.AddWithValue("@sueldo", empleado.Sueldo);
                    cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarEmpleado(Empleado empleado)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EditarEmpleado";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idEmpleado", empleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("@nombres", empleado.Nombres);
                    cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
                    cmd.Parameters.AddWithValue("@dni", empleado.Dni);
                    cmd.Parameters.AddWithValue("@tipo", empleado.Tipo);
                    cmd.Parameters.AddWithValue("@sueldo", empleado.Sueldo);
                    cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EliminarEmpleado";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
