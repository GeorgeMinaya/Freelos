using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IMenuDA
    {
        MenuBE.Response Listar_Menu();
        MenuTypeBE.Response Listar_Type_Menu();
        MenuBE.Response BuscarMenu(int IdMenu); 
        Sp_Delete_MenuResult EliminarMenu(MenuBE.ResponseMenuBE objMenuBE);
        Sp_Update_MenuResult ModificarMenu(MenuBE.ResponseMenuBE objMenuBE);
        Sp_Insert_MenuResult RegistrarMenu(MenuBE.ResponseMenuBE ObjRequestLoginBE);
    }
}
