using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IGuardiaDA
    {
        GuardiaBE.Response Listar_Guardia();
        GuardiaBE.Response BuscarGuardia(int IdGuardia);
        Sp_Delete_GuardiaResult EliminarGuardia(GuardiaBE.ResponseGuardiaBE objGuardiaBE);
        Sp_Update_GuardiaResult ModificarGuardia(GuardiaBE.ResponseGuardiaBE objGuardiaBE);
        Sp_Insert_GuardiaResult RegistrarGuardia(GuardiaBE.ResponseGuardiaBE ObjRequestLoginBE);
    }
}
