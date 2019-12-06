using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class ContratoBE
    {
        public class Response{
            public ResponseContratoBE                response        { get; set; }
            public IEnumerable<ResponseContratoBE>   lresponse       { get; set; }
        }        
        public class ResponseContratoBE
        {
            public int                                          IDContract      { get; set; }
            public string                                       DraegerUser     { get; set; }
            public string                                       Amount          { get; set; }
            public int                                          Company         { get; set; }
            public IEnumerable<EmpresaBE.ResponseEmpresaBE>     lCompany        { get; set; }
            public IEnumerable<UserBE.ResponseUserBE>           lUsuario        { get; set; }
            public string                                       InitialDate     { get; set; }
            public string                                       FinalDate       { get; set; }
            public string                                       Quantity        { get; set; }
            public string                                       Dascription     { get; set; }
            public string                                       Celular         { get; set; }
            public string                                       Email           { get; set; }
            public bool                                         Activo          { get; set; }
        }
    }
}
