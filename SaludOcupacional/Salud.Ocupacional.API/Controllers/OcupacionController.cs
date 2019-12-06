using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Ocupacion")]
    public class OcupacionController : ApiController
    {
        IOcupacionBL objOcupacionBL;

        public OcupacionController()
        {
            objOcupacionBL = new OcupacionBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objOcupacionBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdOcupacion}")]
        public IHttpActionResult Buscar(int IdOcupacion)
        {
            return Ok(objOcupacionBL.Buscar(IdOcupacion));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(OcupacionBE.ResponseOcupacionBE objOcupacionBE)
        {
            return Ok(objOcupacionBL.ModificarOcupacion(objOcupacionBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(OcupacionBE.ResponseOcupacionBE objOcupacionBE)
        {
            return Ok(objOcupacionBL.RegistrarOcupacion(objOcupacionBE));
        }
    }
}
