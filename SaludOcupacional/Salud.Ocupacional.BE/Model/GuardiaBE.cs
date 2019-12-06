using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class GuardiaBE
    {
        public class Response{
            public ResponseGuardiaBE                response        { get; set; }
            public IEnumerable<ResponseGuardiaBE>   lresponse       { get; set; }
        }
        public class ResponseGuardiaBE
        {
            public  int     ID      			{ get; set; }
            public  string  Schedule			{ get; set; }
            public  bool    Activo              { get; set; }
        }
    }
}
