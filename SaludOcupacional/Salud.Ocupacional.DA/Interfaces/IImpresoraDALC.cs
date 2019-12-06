using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.MegaCentro.DALC.Interfaces
{
    public interface IImpresoraDALC
    {
        int Registrar(ImpresoraBE objImpresoraBE);
        int Actualizar(ImpresoraBE objImpresoraBE);
        IEnumerable<ImpresoraBE> Listar(int IdCentro);
    }
}
