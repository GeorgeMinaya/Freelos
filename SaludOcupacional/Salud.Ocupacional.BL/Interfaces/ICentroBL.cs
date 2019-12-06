using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.MegaCentro.BL.Interfaces
{
    public interface ICentroBL
    {
        IEnumerable<CentroBE> List(ref int code, ref string message);
    }
}
