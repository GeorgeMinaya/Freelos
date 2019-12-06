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
    public class MenuController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Menus
        public async Task<ActionResult> Menus()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lMenus = await MenuPCL.ListarMenus();
                if (lMenus.Code != 200) throw new Exception(lMenus.Message);

                MantenimientoMenuModel muModel = new MantenimientoMenuModel()
                {
                    Usuario = Usuariores.Usuario,
                    lMenus = lMenus.Data.lresponse
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
        public async Task<PartialViewResult> DatosMenu(int id)
        {
            try
            {
                var datos = await MenuPCL.GetMenuById(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                MenuBE.ResponseMenuBE duModel = datos.Data.response;

                return PartialView("_DatosMenu", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosMenu(MenuBE.ResponseMenuBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new MenuBE.ResponseMenuBE
                {
                    IdMenu          =   duModel.IdMenu
                    , TypeUser      =   duModel.TypeUser 
                    , Nombre        =   duModel.Nombre   
                    , Icono         =   duModel.Icono    
                    , URL           =   duModel.URL      
                    , Activo        =   duModel.Activo   
                };

                var guardar = await MenuPCL.ModificarMenu(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente el usuario.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar el usuario";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Menus");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarMenu()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var Menus = await MenuPCL.ListarTypeMenus();
            if (Menus.Code != 200) throw new Exception(Menus.Message);
            if (Menus.Data == null) throw new Exception("Error al intentar cargar perfiles");

            var duModel = new MenuBE.ResponseMenuBE
            {
                TypeUser        =   0
                , Nombre        =   ""
                , Icono         =   ""
                , URL           =   ""
                , Activo        =   true
                , lTipoMenu     =   Menus.Data.lresponse
            };

            return PartialView("_RegistrarMenu", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarMenu(MenuBE.ResponseMenuBE duModel)
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

                    var nuevo = new MenuBE.ResponseMenuBE
                    {
                        TypeUser        =   duModel.TypeUser 
                        , Nombre        =   duModel.Nombre   
                        , Icono         =   duModel.Icono    
                        , URL           =   duModel.URL      
                        , Activo        =   duModel.Activo   
                        , lTipoMenu     =   duModel.lTipoMenu
                    };
                    var registrar = await MenuPCL.RegistrarMenu(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente el usuario.",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar el nuevo usuario";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("Menus", "Menu");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteMenu(int id)
        {
            try
            {
                var datos = await MenuPCL.GetMenuById(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                MenuBE.ResponseMenuBE duModel = datos.Data.response;

                return PartialView("_DeleteMenu", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMenu(MenuBE.ResponseMenuBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new MenuBE.ResponseMenuBE
                {
                    IdMenu = duModel.IdMenu,
                    Activo = duModel.Activo
                };

                var guardar = await MenuPCL.ModificarMenu(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente al Menu.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar al Menu";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Menus");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Menus
    }
}