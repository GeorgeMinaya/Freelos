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
    public class GuardiaController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Guardia
        public async Task<ActionResult> Guardias()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lGuardias = await GuardiaPCL.ListarGuardias();
                if (lGuardias.Code != 200) throw new Exception(lGuardias.Message);

                MantenimientoGuardiaModel muModel = new MantenimientoGuardiaModel()
                {
                    Usuario = Usuariores.Usuario,
                    lGuardia = lGuardias.Data.lresponse
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
        public async Task<PartialViewResult> DatosGuardia(int id)
        {
            try
            {
                var datos = await GuardiaPCL.BuscarGuardia(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                GuardiaBE.ResponseGuardiaBE duModel = datos.Data.response;

                return PartialView("_DatosGuardia", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosGuardia(GuardiaBE.ResponseGuardiaBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new GuardiaBE.ResponseGuardiaBE
                {
                    ID          =   duModel.ID
                    , Schedule  =   duModel.Schedule
                    , Activo    =   true
                };  

                var guardar = await GuardiaPCL.ModificarGuardia(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente el usuario",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar el usuario";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Guardias");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarGuardia()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var duModel = new GuardiaBE.ResponseGuardiaBE
            {
                ID          =   0
                , Schedule  =   ""
                , Activo    =   true
            };

            return PartialView("_RegistrarGuardia", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarGuardia(GuardiaBE.ResponseGuardiaBE duModel)
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

                    var nuevo = new GuardiaBE.ResponseGuardiaBE
                    {
                        ID          =   0
                        , Schedule  =   duModel.Schedule
                        , Activo    =   true
                    };
                    var registrar = await GuardiaPCL.RegistrarGuardia(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente la nueva guardia",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar la nueva guardia";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("Guardias", "Guardia");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteGuardia(int id)
        {
            try
            {
                var datos = await GuardiaPCL.BuscarGuardia(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                GuardiaBE.ResponseGuardiaBE duModel = datos.Data.response;

                return PartialView("_DeleteGuardia", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteGuardia(GuardiaBE.ResponseGuardiaBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new GuardiaBE.ResponseGuardiaBE
                {
                    ID      =   duModel.ID,
                    Activo  =   duModel.Activo
                };

                var guardar = await GuardiaPCL.ModificarGuardia(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente la guardia",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar la guardia";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Guardias");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Guardia


    }
}