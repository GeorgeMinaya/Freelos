using ACL.MegaCentro.BE;
using ACL.MegaCentro.BL.Interfaces;
using ACL.MegaCentro.BL.SorceCodes;
using Lindley.General.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ACL.MegaCentro.API.Controllers
{
    [RoutePrefix("api/Impresora")]
    public class ImpresoraController : ApiController
    {
        IImpresoraBL objImpresoraBL;

        public ImpresoraController() {
            objImpresoraBL = new ImpresoraBL();
        }

        [HttpGet]
        [Route("Listar/{IdCentro}")]
        public IHttpActionResult Listar(int IdCentro) {
            int codeResult = 0;
            string messageResult = string.Empty;

            var dataResult = objImpresoraBL.Listar(IdCentro, ref codeResult, ref messageResult);
            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar([FromBody] ImpresoraBE objImpresoraBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            var dataResult = objImpresoraBL.Registrar(objImpresoraBE, ref codeResult, ref messageResult);
            objImpresoraBE.IdImpresora = Convert.ToInt32(dataResult);
            return Ok(new Result() { Message = messageResult, Data = objImpresoraBE, Code = codeResult });
        }
    }
}
