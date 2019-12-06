using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface ITrabajadorDA
    {
        TrabajadorBE.Response Listar_Trabajador();
        TrabajadorBE.Response BuscarTrabajador(int IdTrabajador); 
        Sp_Delete_TrabajadorResult EliminarTrabajador(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE);
        Sp_Update_TrabajadorResult ModificarTrabajador(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE);
        Sp_Insert_TrabajadorResult RegistrarTrabajador(TrabajadorBE.ResponseTrabajadorBE ObjRequestLoginBE);
    }
}
