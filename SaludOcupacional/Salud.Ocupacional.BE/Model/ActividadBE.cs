using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salud.Ocupacional.BE
{
    public class ActividadBE
    {
        public class Response{
            public ResponseActividadBE                response        { get; set; }
            public IEnumerable<ResponseActividadBE>   lresponse       { get; set; }
        }
        public class ResponseActividadBE
        {
            public int      IDactivity      { get; set; }
            public int      IDCompany       { get; set; }
            public IEnumerable<EmpresaBE.ResponseEmpresaBE> lCompany { get; set; }
            public string   Date            { get; set; }
            public int      Contract        { get; set; }
            public IEnumerable<ContratoBE.ResponseContratoBE> lContract { get; set; }
            public string   StartHour       { get; set; }
            public string   FinishHour      { get; set; }
            public TimeSpan StartHour1       { get; set; }
            public TimeSpan FinishHour1      { get; set; }
            public int      Respirator      { get; set; }
            public IEnumerable<MarcaModeloBE.ResponseMarcaModeloBE> lRespirator { get; set; }
            public int      Filter          { get; set; }
            public IEnumerable<FiltroBE.ResponseFiltroBE> lFilter { get; set; }
            public int      Quantity        { get; set; }
            public string   Supervisor      { get; set; }
            public string   Description     { get; set; }
            public  bool    Activo          { get; set; }
        }
    }
}
