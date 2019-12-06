using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class CondicionBE
    {
        public class Response{
            public ResponseCondicionBE                response        { get; set; }
            public IEnumerable<ResponseCondicionBE>   lresponse       { get; set; }
        }
        public class ResponseCondicionBE
        {
            public  int     ID			        { get; set; }
            public  string  WorkCondition       { get; set; }
            public  bool    Activo              { get; set; }
        }
    }
}
