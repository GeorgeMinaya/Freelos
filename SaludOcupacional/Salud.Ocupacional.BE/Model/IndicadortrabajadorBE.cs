using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class IndicadorTrabajadorBE
    {
        public class Response{
            public ResponseIndicadorTrabajadorBE                response        { get; set; }
            public IEnumerable<ResponseIndicadorTrabajadorBE>   lresponse       { get; set; }
        }
        public class ResponseIndicadorTrabajadorBE
        {
            public  int     IDCompany			{ get; set; }
            public  string  RUC					{ get; set; }
            public  string  Name				{ get; set; }
            public  string  Tradename			{ get; set; }
            public  string  Address				{ get; set; }
            public  string  Contactname			{ get; set; }
            public  string  ContactLastname		{ get; set; }
            public  string  ContactLastname2	{ get; set; }
            public  string  Cellphone			{ get; set; }
            public  string  Email				{ get; set; }
            public  bool    Activo              { get; set; }
        }
    }
}
