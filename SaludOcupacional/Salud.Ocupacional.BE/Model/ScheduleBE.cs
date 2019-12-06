using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class ScheduleBE
    {
        public class Response{
            public ResponseScheduleBE                response        { get; set; }
            public IEnumerable<ResponseScheduleBE>   lresponse       { get; set; }
        }
        public class ResponseScheduleBE
        {
            public  int     ID			        { get; set; }
            public  string  Schedule            { get; set; }
        }
    }
}
