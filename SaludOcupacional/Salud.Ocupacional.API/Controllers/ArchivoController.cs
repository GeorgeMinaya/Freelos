using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Salud.Ocupacional.API.Controllers
{
    [RoutePrefix("api/Archivo")]
    public class ArchivoController : ApiController
    {
        readonly IArchivoBL ObjArchivoBL; 

        public ArchivoController()
        {
            ObjArchivoBL = new ArchivoBL();
        }

        [HttpGet]
        [Route("DatosRegistrar")]
        public IHttpActionResult DatosRegistrar()
        {
            return Ok(ObjArchivoBL.DatosRegistrar());
        }

        [HttpPost]
        [Route("RegistrarArchivo")]
        public IHttpActionResult RegistrarArchivo(ArchivoBE.ResponseArchivoBE archivoBE)
        {
            return Ok(ObjArchivoBL.RegistrarArchivo(archivoBE));
        }
    }
}
