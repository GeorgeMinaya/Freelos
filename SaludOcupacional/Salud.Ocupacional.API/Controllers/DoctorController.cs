using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Doctor")]
    public class DoctorController : ApiController
    {
        IDoctorBL objDoctorBL;

        public DoctorController()
        {
            objDoctorBL = new DoctorBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objDoctorBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdDoctor}")]
        public IHttpActionResult Buscar(int IdDoctor)
        {
            return Ok(objDoctorBL.Buscar(IdDoctor));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(DoctorBE.ResponseDoctoreBE objDoctorBE)
        {
            return Ok(objDoctorBL.ModificarDoctor(objDoctorBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(DoctorBE.ResponseDoctoreBE objDoctorBE)
        {
            return Ok(objDoctorBL.RegistrarDoctor(objDoctorBE));
        }
    }
}
