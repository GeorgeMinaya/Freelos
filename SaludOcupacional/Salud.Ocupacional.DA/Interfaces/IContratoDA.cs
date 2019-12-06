using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IContratoDA
    {
        ContratoBE.Response Listar_Contrato();
        ContratoBE.Response BuscarContrato(int IdContrato);
        Sp_Delete_ContratoResult EliminarContrato(ContratoBE.ResponseContratoBE objContratoBE);
        Sp_Update_ContratoResult ModificarContrato(ContratoBE.ResponseContratoBE objContratoBE);
        Sp_Insert_ContratoResult RegistrarContrato(ContratoBE.ResponseContratoBE ObjRequestLoginBE);
    }
}
