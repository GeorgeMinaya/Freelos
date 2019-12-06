using ACL.Community.Response;
using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IPerfilBL
    {
        Result Listar();
        PerfilBE Registrar(PerfilBE objPerfilBE, ref int code, ref string message);
        PerfilBE Buscar(int IdPerfil, ref int code, ref string message);
    }
}
