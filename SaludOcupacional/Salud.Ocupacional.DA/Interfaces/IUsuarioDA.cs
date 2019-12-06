using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IUsuarioDA
    {
        Response Usuario_Autenticar(RequestLoginBE ObjRequestLoginBE);
        Response Listar_Usuario();
        int NotificaUsuario(RequestLoginBE objLoginBE);
        Response BuscarUsuario(int IdUsuario); 
        Sp_Delete_UsuerResult EliminarUsuario(ResponseLoginBE objUsuarioBE);
        Sp_Update_UsuerResult ModificarUsuario(ResponseLoginBE objUsuarioBE);
        Sp_Insert_UsuerResult RegistrarUsuario(ResponseLoginBE ObjRequestLoginBE);
        

    }
}
