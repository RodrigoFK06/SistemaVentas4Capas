using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class CategoriaLogica
    {
        private CategoriaDatos categoriaDatos;

        public CategoriaLogica()
        {
            categoriaDatos = new CategoriaDatos();
        }

        public List<Categoria> ObtenerCategorias()
        {
            return categoriaDatos.ObtenerCategorias();
        }

        public void InsertarCategoria(Categoria categoria)
        {
            categoriaDatos.InsertarCategoria(categoria);
        }

        public void EditarCategoria(Categoria categoria)
        {
            categoriaDatos.EditarCategoria(categoria);
        }

        public void EliminarCategoria(int idCategoria)
        {
            categoriaDatos.EliminarCategoria(idCategoria);
        }
    }

}
