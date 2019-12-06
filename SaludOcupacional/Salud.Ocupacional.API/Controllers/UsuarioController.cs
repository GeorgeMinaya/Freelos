using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        IUsuarioBL objUsarioBL;

        public UsuarioController()
        {
            objUsarioBL = new UsuarioBL();
        }


        [HttpPost]
        [Route("AutenticarUsuario")]
        public IHttpActionResult AutenticarUsuario(RequestLoginBE objLoginBE)
        {
            return Ok(objUsarioBL.AutenticarLogin(objLoginBE));
        }
        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objUsarioBL.ListarLogin());
        }

        [HttpPost]
        [Route("NotificaUsuario")]
        public IHttpActionResult NotificaUsuario(RequestLoginBE objLoginBE)
        {
            return Ok(objUsarioBL.NotificaUsuario(objLoginBE));
        }

        [HttpGet]
        [Route("Buscar/{IdUsuario}")]
        public IHttpActionResult Buscar(int IdUsuario)
        {
            return Ok(objUsarioBL.Buscar(IdUsuario));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(ResponseLoginBE objUsuarioBE)
        {
            return Ok(objUsarioBL.ModificarUsuario(objUsuarioBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(ResponseLoginBE objUsuarioBE)
        {
            return Ok(objUsarioBL.RegistrarUsuario(objUsuarioBE));
        }
    }
}
