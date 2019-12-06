using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class MenuTypeBE
    {
        public class Response{
            public ResponseMenuTypeBE               response        { get; set; }
            public IEnumerable<ResponseMenuTypeBE>  lresponse       { get; set; }
        }
        public class ResponseMenuTypeBE
        {
            public int      Id              { get; set; }
            public string   UserType        { get; set; }
        }
    }
}
