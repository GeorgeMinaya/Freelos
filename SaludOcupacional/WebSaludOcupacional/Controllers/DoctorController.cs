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
    public class DoctorController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Doctores
        public async Task<ActionResult> Doctores()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lDoctores = await MantenimientoPCL.ListarDoctores();
                if (lDoctores.Code != 200) throw new Exception(lDoctores.Message);

                MantenimientoDoctorModel muModel = new MantenimientoDoctorModel()
                {
                    Usuario = Usuariores.Usuario,
                    lDoctores = lDoctores.Data.lresponse
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
        public async Task<PartialViewResult> DatosDoctor(int id)
        {
            try
            {
                var datos = await MantenimientoPCL.GetDoctorById(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                DoctorBE.ResponseDoctoreBE duModel = datos.Data.response;

                return PartialView("_DatosDoctor", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosDoctor(DoctorBE.ResponseDoctoreBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new DoctorBE.ResponseDoctoreBE
                {
                    IdDoctor        =   duModel.IdDoctor
                    ,DNIdoctor      =   duModel.DNIdoctor
                    ,Specialism     =   duModel.Specialism
                    ,CMP            =   duModel.CMP
                    ,RENumber       =   duModel.RENumber
                    ,Company        =   duModel.Company
                    ,Activo         =   duModel.Activo
                };

                var guardar = await MantenimientoPCL.ModificarDoctor(modificado);
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
                return RedirectToAction("Doctores");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarDoctor()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var lDoctor = await MantenimientoPCL.ListarDoctores();
            if (lDoctor.Code != 200) throw new Exception(lDoctor.Message);
            if (lDoctor.Data == null) throw new Exception("Error al intentar cargar perfiles");

            var duModel = new DoctorBE.ResponseDoctoreBE
            {
                DNIdoctor = "",
                Specialism = "",
                CMP = "",
                RENumber = "",
                Activo = true,
                Company = 1,
                lEmpresa = lDoctor.Data.lresponse.FirstOrDefault().lEmpresa,
                lUsuario = lDoctor.Data.lresponse.FirstOrDefault().lUsuario
            };

            return PartialView("_RegistrarDoctor", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarDoctor(DoctorBE.ResponseDoctoreBE duModel)
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

                    var nuevo = new DoctorBE.ResponseDoctoreBE
                    {
                        DNIdoctor = duModel.DNIdoctor,
                        Specialism = duModel.Specialism,
                        CMP = duModel.CMP,
                        RENumber = duModel.RENumber,
                        Activo = true,
                        Company = duModel.Company
                    };
                    var registrar = await MantenimientoPCL.RegistrarDoctor(nuevo);
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
                return RedirectToAction("Doctores", "Doctor");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteDoctor(int id)
        {
            try
            {
                var datos = await MantenimientoPCL.GetDoctorById(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                DoctorBE.ResponseDoctoreBE duModel = datos.Data.response;

                return PartialView("_DeleteDoctor", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDoctor(DoctorBE.ResponseDoctoreBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new DoctorBE.ResponseDoctoreBE
                {
                    IdDoctor = duModel.IdDoctor,
                    Activo = duModel.Activo
                };

                var guardar = await MantenimientoPCL.ModificarDoctor(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente al Doctor.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar al Doctor";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Doctores");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Doctores


    }
}