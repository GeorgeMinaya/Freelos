using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaludOcupacional.DTO
{
    public class UsuarioDTO
    {
        public UsuarioDTO()
        {
            this.Centros = new List<CentroDTO>();
        }

        public int IdUsuario { get; set; }
        public string NombreLogin { get; set; }
        public string NombreCompleto { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public bool Activo { get; set; }
        public List<CentroDTO> Centros { get; set; }
    }
}
