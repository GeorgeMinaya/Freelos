using ACL.MegaCentro.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.MegaCentro.DALC.Interfaces
{
    public interface ICentroDALC
    {
        IEnumerable<CentroBE> List();
    }
}
