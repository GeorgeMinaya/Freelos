using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaludOcupacional.DTO
{
    public class MenuAppDTO
    {
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public string URL { get; set; }
        public string URLWeb { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
