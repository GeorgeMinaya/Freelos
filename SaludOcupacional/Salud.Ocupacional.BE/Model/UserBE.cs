using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class UserBE
    {
        public class Response
        {
            public ResponseUserBE               response        { get; set; }
            public IEnumerable<ResponseUserBE>  lresponse       { get; set; }
        }
        public class ResponseUserBE
        {
            public  int     IdUsuario    			{ get; set; }
            public  string  DNI      				{ get; set; }
            public  string  Name     				{ get; set; }
        }
    }
}
