using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class FiltroBE
    {
        public class Response{
            public ResponseFiltroBE                 response        { get; set; }
            public IEnumerable<ResponseFiltroBE>    lresponse       { get; set; }
        }
        public class ResponseFiltroBE
        {
            public int      IDFilter        { get; set; }
            public string   Filter          { get; set; }
            public  bool    Activo          { get; set; }
        }
    }
}
