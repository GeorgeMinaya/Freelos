using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IContratoBL
    {
        Result ListarLogin();
        Result Buscar(int IdUsuario);
        Result ModificarContrato(ContratoBE.ResponseContratoBE objUsuarioBE);
        Result RegistrarContrato(ContratoBE.ResponseContratoBE ObjRequestLoginBE);
        
    }
}
