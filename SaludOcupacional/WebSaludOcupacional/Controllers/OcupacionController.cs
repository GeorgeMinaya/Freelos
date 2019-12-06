using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSaludOcupacional.DTO;
using WebSaludOcupacional.Models;
using WebSaludOcupacional.Resources;
using WebSaludOcupacional.PCL;
using Salud.Ocupacional.BE;
using System.Web.UI;
using static WebSaludOcupacional.Models.MantenimientoModel;

namespace WebSaludOcupacional.Controllers
{
    public class OcupacionController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Ocupacion
        public async Task<ActionResult> Ocupaciones()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lOcupaciones = await OcupacionPCL.ListarOcupaciones();
                if (lOcupaciones.Code != 200) throw new Exception(lOcupaciones.Message);

                MantenimientoOcupacionModel muModel = new MantenimientoOcupacionModel()
                {
                    Usuario = Usuariores.Usuario,
                    lOcupacion = lOcupaciones.Data.lresponse
                };
                ViewBag.Message = ObjMessage;
                ObjMessage = null;
                return View(muModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public async Task<PartialViewResult> DatosOcupacion(int id)
        {
            try
            {
                var datos = await OcupacionPCL.BuscarOcupacion(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                OcupacionBE.ResponseOcupacionBE duModel = datos.Data.response;

                return PartialView("_DatosOcupacion", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosOcupacion(OcupacionBE.ResponseOcupacionBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new OcupacionBE.ResponseOcupacionBE
                {
                    ID              = duModel.ID
                    , Occupation    = duModel.Occupation
                    , Activo        = true
                };

                var guardar = await OcupacionPCL.ModificarOcupacion(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente la ocupación",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar la ocupación";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Ocupaciones");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarOcupacion()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var duModel = new OcupacionBE.ResponseOcupacionBE
            {
                ID              = 0
                , Occupation    = ""
                , Activo        = true
            };

            return PartialView("_RegistrarOcupacion", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarOcupacion(OcupacionBE.ResponseOcupacionBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                    var nuevo = new OcupacionBE.ResponseOcupacionBE
                    {
                        ID              = 0
                        , Occupation    = duModel.Occupation
                        , Activo        = true
                    };
                    var registrar = await OcupacionPCL.RegistrarOcupacion(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente la ocupación",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar la nuevo ocupación";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("Ocupaciones", "Ocupacion");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteOcupacion(int id)
        {
            try
            {
                var datos = await OcupacionPCL.BuscarOcupacion(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                OcupacionBE.ResponseOcupacionBE duModel = datos.Data.response;

                return PartialView("_DeleteOcupacion", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOcupacion(OcupacionBE.ResponseOcupacionBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new OcupacionBE.ResponseOcupacionBE
                {
                    ID      = duModel.ID,
                    Activo  = duModel.Activo
                };

                var guardar = await OcupacionPCL.ModificarOcupacion(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente la ocupación",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar la ocupación";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Ocupaciones");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Ocupacion


    }
}