using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IActividadDA
    {
        ActividadBE.Response Listar_Actividad(int IdUsuario);
        ActividadBE.Response BuscarActividad(int IdActividad);
        Sp_Delete_ActividadResult EliminarActividad(ActividadBE.ResponseActividadBE objActividadBE);
        Sp_Update_ActividadResult ModificarActividad(ActividadBE.ResponseActividadBE objActividadBE);
        Sp_Insert_ActividadResult RegistrarActividad(ActividadBE.ResponseActividadBE ObjRequestLoginBE);
    }
}
