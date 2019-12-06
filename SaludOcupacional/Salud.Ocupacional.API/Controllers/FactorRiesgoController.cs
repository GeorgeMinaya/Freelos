using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/FactorRiesgo")]
    public class FactorRiesgoController : ApiController
    {
        IFactorRiesgoBL objFactorRiesgoBL;

        public FactorRiesgoController()
        {
            objFactorRiesgoBL = new FactorRiesgoBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objFactorRiesgoBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdFactorRiesgo}")]
        public IHttpActionResult Buscar(int IdFactorRiesgo)
        {
            return Ok(objFactorRiesgoBL.Buscar(IdFactorRiesgo));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(FactorRiesgoBE.ResponseFactorRiesgoBE objFactorRiesgoBE)
        {
            return Ok(objFactorRiesgoBL.ModificarFactorRiesgo(objFactorRiesgoBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(FactorRiesgoBE.ResponseFactorRiesgoBE objFactorRiesgoBE)
        {
            return Ok(objFactorRiesgoBL.RegistrarFactorRiesgo(objFactorRiesgoBE));
        }
    }
}
