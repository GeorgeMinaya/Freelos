using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface ITrabajadorBL
    {
        Result Listar();
        Result Buscar(int IdUsuario);
        Result ModificarTrabajador(TrabajadorBE.ResponseTrabajadorBE objUsuarioBE);
        Result RegistrarTrabajador(TrabajadorBE.ResponseTrabajadorBE ObjRequestLoginBE);
        
    }
}
