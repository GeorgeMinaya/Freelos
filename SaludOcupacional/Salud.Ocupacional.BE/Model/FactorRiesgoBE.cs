using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class FactorRiesgoBE
    {
        public class Response{
            public ResponseFactorRiesgoBE                response        { get; set; }
            public IEnumerable<ResponseFactorRiesgoBE>   lresponse       { get; set; }
        }
        public class ResponseFactorRiesgoBE
        {
            public int      ID                  { get; set; }
            public string   RiskFactor          { get; set; }
            public bool     Activo              { get; set; }
        }
    }
}
