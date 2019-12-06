using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IUsuarioBL
    {
        Result AutenticarLogin(RequestLoginBE ObjRequestLoginBE);
        Result ListarLogin();
        Result NotificaUsuario(RequestLoginBE objLoginBE);
        Result Buscar(int IdUsuario);
        Result ModificarUsuario(ResponseLoginBE objUsuarioBE);
        Result RegistrarUsuario(ResponseLoginBE ObjRequestLoginBE);
        
    }
}
