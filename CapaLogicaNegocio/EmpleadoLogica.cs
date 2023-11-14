using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogicaNegocio
{
    public class EmpleadoLogica
    {
        private EmpleadoDatos empleadoDatos;

        public EmpleadoLogica()
        {
            empleadoDatos = new EmpleadoDatos();
        }

        public List<Empleado> ObtenerEmpleados()
        {
            return empleadoDatos.ObtenerEmpleados();
        }

        public void InsertarEmpleado(Empleado empleado)
        {
            empleadoDatos.InsertarEmpleado(empleado);
        }

        public void EditarEmpleado(Empleado empleado)
        {
            empleadoDatos.EditarEmpleado(empleado);
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            empleadoDatos.EliminarEmpleado(idEmpleado);
        }
    }

}
