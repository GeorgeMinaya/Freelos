using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaludOcupacional.DTO
{
    public class UsuarioLoginAppDTO
    {
        public class Autenticar
        {
            public int      IdCentro    { get; set; }
            public string   Nombre      { get; set; }
            public string   Contrasena  { get; set; }
        }
        public UsuarioLoginAppDTO()
        {
            this.Menu = new List<MenuAppDTO>();
        }

        public int IdUsuario { get; set; }
        public string NombreLogin { get; set; }
        public string NombreCompleto { get; set; }
        public int IdCentro { get; set; }
        public string CodigoCentro { get; set; }
        public string NombreCentro { get; set; }
        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; }        
        public List<MenuAppDTO> Menu { get; set; }
        public string Contrasena { get; set; }
    }
}
