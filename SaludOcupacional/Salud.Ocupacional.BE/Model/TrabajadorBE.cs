using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class TrabajadorBE
    {
        public class Response{
            public ResponseTrabajadorBE                response        { get; set; }
            public IEnumerable<ResponseTrabajadorBE>   lresponse       { get; set; }
        }
        public class ResponseTrabajadorBE
        {
            public  int                                             IdTrabajador	    { get; set; }
            public  string                                          DNIemployee		    { get; set; }
            public  string                                          Birthdate		    { get; set; }
            public  int                                             Occupation		    { get; set; }
            public  IEnumerable<OcupacionBE.ResponseOcupacionBE>    lOccupation		    { get; set; }
            public  int                                             Schedule		    { get; set; }
            public  IEnumerable<ScheduleBE.ResponseScheduleBE>      lSchedule		    { get; set; }
            public  int                                             Company			    { get; set; }
            public  IEnumerable<EmpresaBE.ResponseEmpresaBE>        lCompany		    { get; set; }
            public  int                                             WorkCondition	    { get; set; }
            public  IEnumerable<CondicionBE.ResponseCondicionBE>    lWorkCondition	    { get; set; }
            public  string                                          DateEntry		    { get; set; }
            public  int                                             RiskFactor		    { get; set; }
            public  IEnumerable<UserBE.ResponseUserBE>              lUsuario            { get; set; }
            public  IEnumerable<RiskFactorBE.ResponseRiskFactorBE>  lRiskFactor	        { get; set; }
            public  string                                          Name			    { get; set; }
            public  bool                                            Activo              { get; set; }
        }
    }
}
