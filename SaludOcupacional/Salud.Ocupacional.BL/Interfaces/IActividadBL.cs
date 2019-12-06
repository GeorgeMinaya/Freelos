using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IActividadBL
    {
        Result ListarLogin(int IdUsuario);
        Result Buscar(int IdUsuario);
        Result ModificarActividad(ActividadBE.ResponseActividadBE objUsuarioBE);
        Result RegistrarActividad(ActividadBE.ResponseActividadBE ObjRequestLoginBE);
        
    }
}
