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
    public class CategoriaDatos : Conexion
    {
        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_MostrarCategorias";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria categoria = new Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                                Nombre = Convert.ToString(reader["nombre"])
                            };

                            categorias.Add(categoria);
                        }
                    }
                }
            }

            return categorias;
        }

        public void InsertarCategoria(Categoria categoria)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_InsertarCategoria";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditarCategoria(Categoria categoria)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EditarCategoria";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarCategoria(int idCategoria)
        {
            using (SqlConnection conexion = ObtenerConexion())
            {
                conexion.Open();

                string procedimiento = "sp_EliminarCategoria";

                using (SqlCommand cmd = new SqlCommand(procedimiento, conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idCategoria", idCategoria);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
