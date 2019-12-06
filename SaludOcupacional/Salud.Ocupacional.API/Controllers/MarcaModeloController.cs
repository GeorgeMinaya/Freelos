using Salud.Ocupacional.BE;
using Salud.Ocupacional.BL.Interfaces;
using Salud.Ocupacional.BL.SorceCodes;
using System.Web.Http;

namespace Salud.Ocupacional.API
{
    [RoutePrefix("api/MarcaModelo")]
    public class MarcaModeloController : ApiController
    {
        IMarcaModeloBL objMarcaModeloBL;

        public MarcaModeloController()
        {
            objMarcaModeloBL = new MarcaModeloBL();
        }

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            return Ok(objMarcaModeloBL.ListarLogin());
        }

        [HttpGet]
        [Route("Buscar/{IdMarcaModelo}")]
        public IHttpActionResult Buscar(int IdMarcaModelo)
        {
            return Ok(objMarcaModeloBL.Buscar(IdMarcaModelo));
        }

        [HttpPost]
        [Route("Modificar")]
        public IHttpActionResult Modificar(MarcaModeloBE.ResponseMarcaModeloBE objMarcaModeloBE)
        {
            return Ok(objMarcaModeloBL.ModificarMarcaModelo(objMarcaModeloBE));
        }

        [HttpPost]
        [Route("Registrar")]
        public IHttpActionResult Registrar(MarcaModeloBE.ResponseMarcaModeloBE objMarcaModeloBE)
        {
            return Ok(objMarcaModeloBL.RegistrarMarcaModelo(objMarcaModeloBE));
        }
    }
}
