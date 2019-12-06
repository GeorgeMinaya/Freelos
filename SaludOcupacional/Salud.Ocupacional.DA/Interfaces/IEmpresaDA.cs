using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IEmpresaDA
    {
        EmpresaBE.Response Listar_Empresa();
        EmpresaBE.Response BuscarEmpresa(int IdEmpresa); 
        Sp_Delete_EmpresaResult EliminarEmpresa(EmpresaBE.ResponseEmpresaBE objEmpresaBE);
        Sp_Update_EmpresaResult ModificarEmpresa(EmpresaBE.ResponseEmpresaBE objEmpresaBE);
        Sp_Insert_EmpresaResult RegistrarEmpresa(EmpresaBE.ResponseEmpresaBE ObjRequestLoginBE);
    }
}
