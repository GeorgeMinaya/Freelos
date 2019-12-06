using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class MenuBE
    {
        public class Response{
            public ResponseMenuBE                response        { get; set; }
            public IEnumerable<ResponseMenuBE>   lresponse       { get; set; }
        }
        public class ResponseMenuBE
        {
            public int                                          IdMenu          { get; set; }
            public int                                          TypeUser        { get; set; }
            public string                                       Nombre          { get; set; }
            public string                                       Icono           { get; set; }
            public string                                       URL             { get; set; }
            public bool                                         Activo          { get; set; }
            public IEnumerable<MenuTypeBE.ResponseMenuTypeBE>   lTipoMenu       { get; set; }
        }
    }
}
