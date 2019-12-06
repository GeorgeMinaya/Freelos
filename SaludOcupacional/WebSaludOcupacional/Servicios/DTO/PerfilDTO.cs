using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSaludOcupacional.DTO
{
    public class PerfilDTO
    {
        public int IdPerfil { get; set; }
        public string Nombre { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public bool Activo { get; set; }
        public List<MenuBE> Opciones { get; set; }
    }
}
