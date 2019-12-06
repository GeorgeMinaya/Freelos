using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSaludOcupacional.Models;
using WebSaludOcupacional.Resources;
using WebSaludOcupacional.PCL;
using Salud.Ocupacional.BE;
using System.Web.UI;
using static WebSaludOcupacional.Models.MantenimientoModel;

namespace WebSaludOcupacional.Controllers
{
    public class CondicionController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Condicion
        public async Task<ActionResult> Condiciones()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lCondiciones = await CondicionPCL.ListarCondicions();
                if (lCondiciones.Code != 200) throw new Exception(lCondiciones.Message);

                MantenimientoCondicionModel muModel = new MantenimientoCondicionModel()
                {
                    Usuario = Usuariores.Usuario,
                    lCondicions = lCondiciones.Data.lresponse
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
        public async Task<PartialViewResult> DatosCondicion(int id)
        {
            try
            {
                var datos = await CondicionPCL.BuscarCondicion(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                CondicionBE.ResponseCondicionBE duModel = datos.Data.response;

                return PartialView("_DatosCondicion", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosCondicion(CondicionBE.ResponseCondicionBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new CondicionBE.ResponseCondicionBE
                {
                    ID                  =   duModel.ID
                    , WorkCondition     =   duModel.WorkCondition
                    , Activo            =   true
                };

                var guardar = await CondicionPCL.ModificarCondicion(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente la condición",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar la condición";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Condiciones");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarCondicion()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
                        
            var duModel = new CondicionBE.ResponseCondicionBE
            {
                ID                  =   0
                , WorkCondition     =   ""
                , Activo            =   true
            };

            return PartialView("_RegistrarCondicion", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarCondicion(CondicionBE.ResponseCondicionBE duModel)
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

                    var nuevo = new CondicionBE.ResponseCondicionBE
                    {
                        ID                  =   duModel.ID
                        , WorkCondition     =   duModel.WorkCondition
                        , Activo            =   true
                    };
                    var registrar = await CondicionPCL.RegistrarCondicion(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente la nueva condición",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar la nueva condición";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("Condiciones", "Condicion");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteCondicion(int id)
        {
            try
            {
                var datos = await CondicionPCL.BuscarCondicion(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                CondicionBE.ResponseCondicionBE duModel = datos.Data.response;

                return PartialView("_DeleteCondicion", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCondicion(CondicionBE.ResponseCondicionBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new CondicionBE.ResponseCondicionBE
                {
                    ID          =   duModel.ID,
                    Activo      =   duModel.Activo
                };

                var guardar = await CondicionPCL.ModificarCondicion(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente la condición",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar la condición";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Condiciones");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Condicion


    }
}