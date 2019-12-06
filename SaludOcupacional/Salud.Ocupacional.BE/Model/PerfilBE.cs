using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class PerfilBE
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<MenuBE> Opciones { get; set; }
    }
}
