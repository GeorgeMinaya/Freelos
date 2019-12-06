using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Contrato")]
    public class ContratoController : ApiController
    {
        IContratoBL objContratoBL;

        public ContratoController()
        {
            objContratoBL = new ContratoBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objContratoBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdContrato}")]
        public IHttpActionResult Buscar(int IdContrato)
        {
            return Ok(objContratoBL.Buscar(IdContrato));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(ContratoBE.ResponseContratoBE objContratoBE)
        {
            return Ok(objContratoBL.ModificarContrato(objContratoBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(ContratoBE.ResponseContratoBE objContratoBE)
        {
            return Ok(objContratoBL.RegistrarContrato(objContratoBE));
        }
    }
}
