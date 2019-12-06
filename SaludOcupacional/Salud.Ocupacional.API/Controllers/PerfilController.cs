using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API.Controllers
{
    [RoutePrefix("api/Perfil")]
    public class PerfilController : ApiController
    {
        IPerfilBL objPerfilBL;

        public PerfilController()
        {
            objPerfilBL = new PerfilBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objPerfilBL.Listar());
        }
        /*
        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar([FromBody] PerfilBE objPerfilBE)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            var dataResult = objPerfilBL.Registrar(objPerfilBE, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }

        [HttpGet]
        [Route("Buscar/{IdPerfil}")]
        public IHttpActionResult Listar(int IdPerfil)
        {
            int codeResult = 0;
            string messageResult = string.Empty;

            var dataResult = objPerfilBL.Buscar(IdPerfil, ref codeResult, ref messageResult);

            return Ok(new Result() { Message = messageResult, Data = dataResult, Code = codeResult });
        }
        */
    }
}
