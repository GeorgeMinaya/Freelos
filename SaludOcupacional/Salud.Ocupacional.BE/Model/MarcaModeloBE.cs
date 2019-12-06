using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class MarcaModeloBE
    {
        public class Response{
            public ResponseMarcaModeloBE                response        { get; set; }
            public IEnumerable<ResponseMarcaModeloBE>   lresponse       { get; set; }
        }
        public class ResponseMarcaModeloBE
        {
            public  int     ID		            { get; set; }
            public  string  Brand				{ get; set; }
            public  string  Model				{ get; set; }
            public  bool    Activo              { get; set; }
        }
    }
}
