using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Trabajador")]
    public class TrabajadorController : ApiController
    {
        ITrabajadorBL objTrabajadorBL;

        public TrabajadorController()
        {
            objTrabajadorBL = new TrabajadorBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objTrabajadorBL.Listar());
        }

        [HttpGet]
        [Route("Buscar/{IdTrabajador}")]
        public IHttpActionResult Buscar(int IdTrabajador)
        {
            return Ok(objTrabajadorBL.Buscar(IdTrabajador));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE)
        {
            return Ok(objTrabajadorBL.ModificarTrabajador(objTrabajadorBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(TrabajadorBE.ResponseTrabajadorBE objTrabajadorBE)
        {
            return Ok(objTrabajadorBL.RegistrarTrabajador(objTrabajadorBE));
        }
    }
}
