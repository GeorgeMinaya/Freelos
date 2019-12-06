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
    public class MarcaModeloController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Marca
        public async Task<ActionResult> MarcasModelos()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lMarcasModelos = await MarcasModelosPCL.ListarMarcasModelos();
                if (lMarcasModelos.Code != 200) throw new Exception(lMarcasModelos.Message);

                MantenimientoMarcasModelosModel muModel = new MantenimientoMarcasModelosModel()
                {
                    Usuario = Usuariores.Usuario,
                    lMarcaModelo = lMarcasModelos.Data.lresponse
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
        public async Task<PartialViewResult> DatosMarcaModelo(int id)
        {
            try
            {
                var datos = await MarcasModelosPCL.BuscarMarcasModelos(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                MarcaModeloBE.ResponseMarcaModeloBE duModel = datos.Data.response;

                return PartialView("_DatosMarcaModelo", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new MarcaModeloBE.ResponseMarcaModeloBE
                {
                    ID          =   duModel.ID
                    , Brand     =   duModel.Brand
                    , Model     =   duModel.Model
                    , Activo    =   true
                };

                var guardar = await MarcasModelosPCL.ModificarMarcasModelos(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente la Marca & Modelo",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar la Marca & Modelo";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("MarcasModelos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarMarcaModelo()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var duModel = new MarcaModeloBE.ResponseMarcaModeloBE
            {
                ID          =   0
                , Brand     =   ""
                , Model     =   ""
                , Activo    =   true
            };

            return PartialView("_RegistrarMarcaModelo", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE duModel)
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

                    var nuevo = new MarcaModeloBE.ResponseMarcaModeloBE
                    {
                        ID          =   0
                        , Brand     =   duModel.Brand
                        , Model     =   duModel.Model
                        , Activo    =   true
                    };
                    var registrar = await MarcasModelosPCL.RegistrarMarcasModelos(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente la Marca & Modelo",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar la Marca & Modelo";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("MarcasModelos", "MarcaModelo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteMarcaModelo(int id)
        {
            try
            {
                var datos = await MarcasModelosPCL.BuscarMarcasModelos(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                MarcaModeloBE.ResponseMarcaModeloBE duModel = datos.Data.response;

                return PartialView("_DeleteMarcaModelo", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMarcaModelo(MarcaModeloBE.ResponseMarcaModeloBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new MarcaModeloBE.ResponseMarcaModeloBE
                {
                    ID      = duModel.ID,
                    Activo  = duModel.Activo
                };

                var guardar = await MarcasModelosPCL.ModificarMarcasModelos(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente la Marca & Modelo",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar la Marca & Modelo";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("MarcasModelos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Marca


    }
}