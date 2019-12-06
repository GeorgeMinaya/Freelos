using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class DoctorBE
    {
        public class Response{
            public ResponseDoctoreBE                response        { get; set; }
            public IEnumerable<ResponseDoctoreBE>   lresponse       { get; set; }
        }
        public class ResponseDoctoreBE
        {
            public int                                          IdDoctor		    { get; set; }
            public string                                       DNIdoctor		    { get; set; }
            public string                                       Specialism	        { get; set; }
            public string                                       CMP			        { get; set; }
            public string                                       RENumber		    { get; set; }
            public int	                                        Company		        { get; set; }
            public bool                                         Activo              { get; set; }
            public IEnumerable<UserBE.ResponseUserBE>           lUsuario            { get; set; }
            public IEnumerable<EmpresaBE.ResponseEmpresaBE>     lEmpresa            { get; set; }
        }
    }
}
