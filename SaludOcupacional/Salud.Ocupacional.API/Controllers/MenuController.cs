using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {
        IMenuBL objMenuBL;

        public MenuController()
        {
            objMenuBL = new MenuBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objMenuBL.Listar());
        }

        [HttpGet]
        [Route("ListarType")]
        public IHttpActionResult ListarType()
        {
            return Ok(objMenuBL.ListarType());
        }
        

       [HttpGet]
        [Route("Buscar/{IdMenu}")]
        public IHttpActionResult Buscar(int IdMenu)
        {
            return Ok(objMenuBL.Buscar(IdMenu));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(MenuBE.ResponseMenuBE objMenuBE)
        {
            return Ok(objMenuBL.ModificarMenu(objMenuBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(MenuBE.ResponseMenuBE objMenuBE)
        {
            return Ok(objMenuBL.RegistrarMenu(objMenuBE));
        }
    }
}
