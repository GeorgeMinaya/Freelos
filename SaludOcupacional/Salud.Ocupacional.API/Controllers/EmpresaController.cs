using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/Empresa")]
    public class EmpresaController : ApiController
    {
        IEmpresaBL objEmpresaBL;

        public EmpresaController()
        {
            objEmpresaBL = new EmpresaBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objEmpresaBL.Listar());
        }

        [HttpGet]
        [Route("Buscar/{IdEmpresa}")]
        public IHttpActionResult Buscar(int IdEmpresa)
        {
            return Ok(objEmpresaBL.Buscar(IdEmpresa));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(EmpresaBE.ResponseEmpresaBE objEmpresaBE)
        {
            return Ok(objEmpresaBL.ModificarEmpresa(objEmpresaBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(EmpresaBE.ResponseEmpresaBE objEmpresaBE)
        {
            return Ok(objEmpresaBL.RegistrarEmpresa(objEmpresaBE));
        }
    }
}
