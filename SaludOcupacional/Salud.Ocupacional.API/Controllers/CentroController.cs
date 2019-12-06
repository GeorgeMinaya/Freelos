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
    [RoutePrefix("api/Centro")]
    public class CentroController : ApiController
    {
        ICentroBL objCentroBL;

        public CentroController()
        {
            objCentroBL = new CentroBL();
        }

        [HttpGet]
        [Route("List")]
        public IHttpActionResult List()
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            var dataResult = objCentroBL.List(ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }
    }
}
