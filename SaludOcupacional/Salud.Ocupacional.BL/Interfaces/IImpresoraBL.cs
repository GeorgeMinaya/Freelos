using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.MegaCentro.BL.Interfaces
{
    public interface IImpresoraBL
    {
        int? Registrar(ImpresoraBE objImpresoraBE, ref int code, ref string message);
        IEnumerable<ImpresoraBE> Listar(int IdCentro, ref int code, ref string message);
    }
}
