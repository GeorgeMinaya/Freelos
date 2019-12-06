using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IEmpresaBL
    {
        Result Listar();
        Result Buscar(int IdUsuario);
        Result ModificarEmpresa(EmpresaBE.ResponseEmpresaBE objUsuarioBE);
        Result RegistrarEmpresa(EmpresaBE.ResponseEmpresaBE ObjRequestLoginBE);
        
    }
}
