using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class RiskFactorBE
    {
        public class Response{
            public ResponseRiskFactorBE response                        { get; set; }
            public IEnumerable<ResponseRiskFactorBE>   lresponse        { get; set; }
        }
        public class ResponseRiskFactorBE
        {
            public  int     ID			        { get; set; }
            public  string  RiskFactor          { get; set; }
        }
    }
}
