using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class ClienteLogica
    {
        private ClienteDatos clienteDatos;

        public ClienteLogica()
        {
            clienteDatos = new ClienteDatos();
        }

        public List<Cliente> ObtenerClientes()
        {
            return clienteDatos.ObtenerClientes();
        }

        public void InsertarCliente(Cliente cliente)
        {
            clienteDatos.InsertarCliente(cliente);
        }

        public void EditarCliente(Cliente cliente)
        {
            clienteDatos.EditarCliente(cliente);
        }

        public void EliminarCliente(int idCliente)
        {
            clienteDatos.EliminarCliente(idCliente);
        }
    }

}
