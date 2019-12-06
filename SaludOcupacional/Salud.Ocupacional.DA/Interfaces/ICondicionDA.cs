using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface ICondicionDA
    {
        CondicionBE.Response Listar_Condicion();
        CondicionBE.Response BuscarCondicion(int IdCondicion);
        Sp_Delete_CondicionResult EliminarCondicion(CondicionBE.ResponseCondicionBE objCondicionBE);
        Sp_Update_CondicionResult ModificarCondicion(CondicionBE.ResponseCondicionBE objCondicionBE);
        Sp_Insert_CondicionResult RegistrarCondicion(CondicionBE.ResponseCondicionBE ObjRequestLoginBE);
    }
}
