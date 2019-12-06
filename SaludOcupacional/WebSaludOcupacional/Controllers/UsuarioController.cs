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
    public class UsuarioController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }
        
        #region Usuarios

        public async Task<ActionResult> Usuarios()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lUsuarios = await MantenimientoPCL.ListarUsuarios();
                if (lUsuarios.Code != 200) throw new Exception(lUsuarios.Message);

                MantenimientoUsuarioModel muModel = new MantenimientoUsuarioModel()
                {
                    Usuario = Usuariores.Usuario,
                    lUsuarios = lUsuarios.Data.lresponse
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
        public async Task<PartialViewResult> DatosUsuario(int id)
        {
            try
            {
                var datos = await MantenimientoPCL.GetUsuarioById(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ResponseLoginBE duModel = datos.Data.response;

                return PartialView("_DatosUsuario", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DatosUsuario(ResponseLoginBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ResponseLoginBE
                {
                   IdUsuario           = duModel.IdUsuario,
                   DNI                 = duModel.DNI,
                   Name                = duModel.Name,
                   LastName1           = duModel.LastName1,
                   LastName2           = duModel.LastName2,
                   Type                = duModel.Type,
                   Mobile              = "1",   
                   Password            = duModel.Password,
                   Activo              = duModel.Activo,
                   Email               = duModel.Email
                };
                var guardar = await MantenimientoPCL.ModificarUsuario(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();

                ObjMessage = new MessageDialog()
                {
                    Title   =   "Se modificó correctamente el usuario.",
                    Estado  =   0,
                    Message =   guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title    =   "Error al intentar modificar el usuario";
                    ObjMessage.Estado   =   guardar.Data.Codigo;
                }
                return RedirectToAction(duModel.Mobile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> RegistrarUsuario()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            
            var perfiles = await PerfilPCL.ListarPerfiles();
            if (perfiles.Code != 200) throw new Exception(perfiles.Message);
            if (perfiles.Data == null) throw new Exception("Error al intentar cargar perfiles");

            List<PerfilBE> lPerfiles = new List<PerfilBE>();
            lPerfiles.Add(new PerfilBE { IdPerfil = 0, Nombre = "--- Seleccione ---" });
            lPerfiles.AddRange(perfiles.Data);
            
            var duModel = new ResponseLoginBE
            {
                DNI                 = "",
                Name                = "",
                LastName1           = "",
                LastName2           = "",
                Type                = 1,
                Mobile              = "",
                Password            = "",
                Email               = "",
                perfilList          = lPerfiles.ToList()
            };

            return PartialView("_RegistrarUsuario", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarUsuario(ResponseLoginBE duModel)
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
                    
                    var nuevo = new ResponseLoginBE
                    {
                       IdUsuario           = duModel.IdUsuario,
                       DNI                 = duModel.DNI,
                       Name                = duModel.Name,
                       LastName1           = duModel.LastName1,
                       LastName2           = duModel.LastName2,
                       Type                = duModel.Type,
                       Mobile              = "1",
                       Password            = duModel.Password,
                       Email               = duModel.Email
                    };
                    var registrar = await MantenimientoPCL.RegistrarUsuario(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog() {
                        Title = "Se registro correctamente el usuario.",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0) { 
                        ObjMessage.Title = "Error al intentar registrar el nuevo usuario";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }

                return RedirectToAction("Usuarios", "Usuario");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteUsuario(int id)
        {
            try
            {
                var perfiles = await PerfilPCL.ListarPerfiles();
                if (perfiles.Code != 200) throw new Exception(perfiles.Message);
                if (perfiles.Data == null) throw new Exception("Error al intentar cargar perfiles");

                List<PerfilBE> lPerfiles = new List<PerfilBE>();
                lPerfiles.AddRange(perfiles.Data);

                var datos = await MantenimientoPCL.GetUsuarioById(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ResponseLoginBE duModel = datos.Data.response;

                return PartialView("_DeleteUsuario", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUsuario(ResponseLoginBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ResponseLoginBE
                {
                    IdUsuario = duModel.IdUsuario,
                    Activo = duModel.Activo
                };

                var guardar = await MantenimientoPCL.ModificarUsuario(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente el usuario.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar el usuario";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Usuarios");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}