using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Salud.Ocupacional.BE
{
    public class ArchivoBE
    {
        public class Response{
            public ResponseArchivoBE                response        { get; set; }
            public IEnumerable<ResponseArchivoBE>   lresponse       { get; set; }
        }
        public class ResponseArchivoBE
        {


            public int IDCompany { get; set; }
            public IEnumerable<EmpresaBE.ResponseEmpresaBE> LCompany { get; set; }
            public int IdContract { get; set; }
            public IEnumerable<ContratoBE.ResponseContratoBE> LContract { get; set; }
            public int IdTrabajador { get; set; }
            public IEnumerable<TrabajadorBE.ResponseTrabajadorBE> LTrabajador { get; set; }
            public DateTime FechaRegistro { get; set; }
            public int IdUsuarioRegistro { get; set; }
            public string ArchivoBase64 { get; set; }
            public HttpPostedFileBase ArchivoCargado { get; set; }

            public string UrlArchivo { get; set; }

            //public  string  RUC					{ get; set; }
            //public  string  Name				{ get; set; }
            //public  string  Tradename			{ get; set; }
            //public  string  Address				{ get; set; }
            //public  string  Contactname			{ get; set; }
            //public  string  ContactLastname		{ get; set; }
            //public  string  ContactLastname2	{ get; set; }
            //public  string  Cellphone			{ get; set; }
            //public  string  Email				{ get; set; }
            //public  bool    Activo              { get; set; }
        }
    }
}
