using Salud.Ocupacional.BE;
using Salud.Ocupacional.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IMarcaModeloDA
    {
        MarcaModeloBE.Response Listar_MarcaModelo();
        MarcaModeloBE.Response BuscarMarcaModelo(int IdMarcaModelo);
        Sp_Delete_MarcaModeloResult EliminarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE objMarcaModeloBE);
        Sp_Update_MarcaModeloResult ModificarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE objMarcaModeloBE);
        Sp_Insert_MarcaModeloResult RegistrarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE ObjRequestLoginBE);
    }
}
