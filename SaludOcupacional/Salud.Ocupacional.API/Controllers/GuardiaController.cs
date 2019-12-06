using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Guardia")]
    public class GuardiaController : ApiController
    {
        IGuardiaBL objGuardiaBL;

        public GuardiaController()
        {
            objGuardiaBL = new GuardiaBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objGuardiaBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdGuardia}")]
        public IHttpActionResult Buscar(int IdGuardia)
        {
            return Ok(objGuardiaBL.Buscar(IdGuardia));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(GuardiaBE.ResponseGuardiaBE objGuardiaBE)
        {
            return Ok(objGuardiaBL.ModificarGuardia(objGuardiaBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(GuardiaBE.ResponseGuardiaBE objGuardiaBE)
        {
            return Ok(objGuardiaBL.RegistrarGuardia(objGuardiaBE));
        }
    }
}
