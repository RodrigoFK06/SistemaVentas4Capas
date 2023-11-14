using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Tipo { get; set; }
        public int Sueldo { get; set; }
        public string Direccion { get; set; }
    }
}
