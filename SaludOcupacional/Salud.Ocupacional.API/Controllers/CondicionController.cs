using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Condicion")]
    public class CondicionController : ApiController
    {
        ICondicionBL objCondicionBL;

        public CondicionController()
        {
            objCondicionBL = new CondicionBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objCondicionBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdCondicion}")]
        public IHttpActionResult Buscar(int IdCondicion)
        {
            return Ok(objCondicionBL.Buscar(IdCondicion));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(CondicionBE.ResponseCondicionBE objCondicionBE)
        {
            return Ok(objCondicionBL.ModificarCondicion(objCondicionBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(CondicionBE.ResponseCondicionBE objCondicionBE)
        {
            return Ok(objCondicionBL.RegistrarCondicion(objCondicionBE));
        }
    }
}
