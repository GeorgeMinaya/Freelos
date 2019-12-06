using Salud.Ocupacional.BE;
using ACL.Community.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BL.Interfaces
{
    public interface IMarcaModeloBL
    {
        Result ListarLogin();
        Result Buscar(int IdUsuario);
        Result ModificarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE objUsuarioBE);
        Result RegistrarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE ObjRequestLoginBE);
        
    }
}
