using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IMenuBL
    {
        Result Listar();
        Result ListarType();
        Result Buscar(int IdUsuario);
        Result ModificarMenu(MenuBE.ResponseMenuBE objUsuarioBE);
        Result RegistrarMenu(MenuBE.ResponseMenuBE ObjRequestLoginBE);
        
    }
}
