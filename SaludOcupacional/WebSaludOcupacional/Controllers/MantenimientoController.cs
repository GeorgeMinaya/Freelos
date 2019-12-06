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
using static WebSaludOcupacional.Models.MantenimientoModel;
using Salud.Ocupacional.BE;
using System.Web.UI;

namespace WebSaludOcupacional.Controllers
{
    public class MantenimientoController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Perfiles Usuario
        public async Task<ActionResult> PerfilUsuario()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                ViewBag.Message = ObjMessage;
                ObjMessage = null;

                var datos = await MantenimientoPCL.GetUsuarioById(Usuariores.Usuario.IdUsuario);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ResponseLoginBE duModel = datos.Data.response;
                MantenimientoUsuarioModel muModel = new MantenimientoUsuarioModel();
                muModel.Usuario = datos.Data.response;
                return View(muModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PerfilUsuario(MantenimientoUsuarioModel duModel)
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
                    IdUsuario = duModel.Usuario.IdUsuario,
                    DNI = duModel.Usuario.DNI,
                    Name = duModel.Usuario.Name,
                    LastName1 = duModel.Usuario.LastName1,
                    LastName2 = duModel.Usuario.LastName2,
                    Type = duModel.Usuario.Type,
                    Mobile = "1",
                    Password = duModel.Usuario.Password,
                    Activo = duModel.Usuario.Activo,
                    Email = duModel.Usuario.Email
                };
                var guardar = await MantenimientoPCL.ModificarUsuario(modificado);
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
                return RedirectToAction(duModel.Usuario.Mobile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Perfiles Usuario

        #region Contraseña Usuario
        public async Task<ActionResult> ContraseñaUsuario()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                ViewBag.Message = ObjMessage;
                ObjMessage = null;

                var datos = await MantenimientoPCL.GetUsuarioById(Usuariores.Usuario.IdUsuario);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ResponseLoginBE duModel = datos.Data.response;
                MantenimientoUsuarioModel muModel = new MantenimientoUsuarioModel();
                muModel.Usuario = datos.Data.response;
                return View(muModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ContraseñaUsuario(MantenimientoUsuarioModel duModel)
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
                    IdUsuario = duModel.Usuario.IdUsuario,
                    DNI = duModel.Usuario.DNI,
                    Name = duModel.Usuario.Name,
                    LastName1 = duModel.Usuario.LastName1,
                    LastName2 = duModel.Usuario.LastName2,
                    Type = duModel.Usuario.Type,
                    Mobile = "1",
                    Password = duModel.Usuario.Password,
                    Activo = duModel.Usuario.Activo,
                    Email = duModel.Usuario.Email
                };
                var guardar = await MantenimientoPCL.ModificarUsuario(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();

                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente la contraseña.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar la contraseña.";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction(duModel.Usuario.Mobile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Perfiles Usuario

    }
}