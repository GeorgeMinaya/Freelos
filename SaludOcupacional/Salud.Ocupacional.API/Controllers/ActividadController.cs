using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Actividad")]
    public class ActividadController : ApiController
    {
        IActividadBL objActividadBL;

        public ActividadController()
        {
            objActividadBL = new ActividadBL();
        }

        [HttpGet]
        [Route("Listar/{IdUsuario}")]
        public IHttpActionResult Listar(int IdUsuario)
        {
            return Ok(objActividadBL.ListarLogin(IdUsuario));
        }

        [HttpGet]
        [Route("Buscar/{IdActividad}")]
        public IHttpActionResult Buscar(int IdActividad)
        {
            return Ok(objActividadBL.Buscar(IdActividad));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(ActividadBE.ResponseActividadBE objActividadBE)
        {
            return Ok(objActividadBL.ModificarActividad(objActividadBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(ActividadBE.ResponseActividadBE objActividadBE)
        {
            return Ok(objActividadBL.RegistrarActividad(objActividadBE));
        }
    }
}
