using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.DA.Interfaces
{
    public interface IPerfilDA
    {
        IEnumerable<PerfilBE> Listar();
        int Registrar(PerfilBE objPerfilBE);
        int Actualizar(PerfilBE objPerfilBE);
        PerfilBE Buscar(int IdPerfil);
    }
}
