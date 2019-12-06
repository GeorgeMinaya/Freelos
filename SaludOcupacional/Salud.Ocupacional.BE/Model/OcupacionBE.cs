using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class OcupacionBE
    {
        public class Response{
            public ResponseOcupacionBE                response        { get; set; }
            public IEnumerable<ResponseOcupacionBE>   lresponse       { get; set; }
        }
        public class ResponseOcupacionBE
        {
            public  int     ID      			{ get; set; }
            public  string  Occupation			{ get; set; }
            public  bool    Activo              { get; set; }
        }
    }
}
