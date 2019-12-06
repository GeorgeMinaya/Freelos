using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IOcupacionBL
    {
        Result ListarLogin();
        Result Buscar(int IdUsuario);
        Result ModificarOcupacion(OcupacionBE.ResponseOcupacionBE objUsuarioBE);
        Result RegistrarOcupacion(OcupacionBE.ResponseOcupacionBE ObjRequestLoginBE);
        
    }
}
