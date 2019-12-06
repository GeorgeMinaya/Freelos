using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class UsuarioBE
    {
        public int IdUsuario { get; set; }
        public string NombreLogin { get; set; }
        public string NombreCompleto { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public bool Activo { get; set; }
    }
    public class UsuarioLoginAppBE
    {
        public int IdUsuario { get; set; }
        public string NombreLogin { get; set; }
        public string NombreCompleto { get; set; }
        public int IdCentro { get; set; }
        public string CodigoCentro { get; set; }
        public string NombreCentro { get; set; }
        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; }
        public string Contrasena { get; set; }
    }
}
