using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IFactorRiesgoDA
    {
        FactorRiesgoBE.Response Listar_FactorRiesgo();
        FactorRiesgoBE.Response BuscarFactorRiesgo(int IdFactorRiesgo); 
        Sp_Delete_FactorRiesgoResult EliminarFactorRiesgo(FactorRiesgoBE.ResponseFactorRiesgoBE objFactorRiesgoBE);
        Sp_Update_FactorRiesgoResult ModificarFactorRiesgo(FactorRiesgoBE.ResponseFactorRiesgoBE objFactorRiesgoBE);
        Sp_Insert_FactorRiesgoResult RegistrarFactorRiesgo(FactorRiesgoBE.ResponseFactorRiesgoBE ObjRequestLoginBE);
    }
}
