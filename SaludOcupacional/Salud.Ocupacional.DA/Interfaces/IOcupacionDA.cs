using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IOcupacionDA
    {
        OcupacionBE.Response Listar_Ocupacion();
        OcupacionBE.Response BuscarOcupacion(int IdOcupacion); 
        Sp_Delete_OcupacionResult EliminarOcupacion(OcupacionBE.ResponseOcupacionBE objOcupacionBE);
        Sp_Update_OcupacionResult ModificarOcupacion(OcupacionBE.ResponseOcupacionBE objOcupacionBE);
        Sp_Insert_OcupacionResult RegistrarOcupacion(OcupacionBE.ResponseOcupacionBE ObjRequestLoginBE);
    }
}
