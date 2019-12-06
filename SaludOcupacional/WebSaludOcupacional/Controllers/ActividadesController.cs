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
    public class ActividadController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Actividad
        public async Task<ActionResult> Actividades()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var lActividades = await ActividadPCL.ListarActividades(Usuariores.Usuario.IdUsuario);
                if (lActividades.Code != 200) throw new Exception(lActividades.Message);

                MantenimientoActividadModel muModel = new MantenimientoActividadModel()
                {
                    Usuario     =   Usuariores.Usuario,
                    lActividad  =   lActividades.Data.lresponse
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
        public async Task<PartialViewResult> DatosActividad(int id)
        {
            try
            {
                var datos = await ActividadPCL.BuscarActividad(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ActividadBE.ResponseActividadBE duModel = datos.Data.response;

                return PartialView("_DatosActividad", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosActividad(ActividadBE.ResponseActividadBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ActividadBE.ResponseActividadBE
                {
                    IDactivity          =   duModel.IDactivity
                    , IDCompany         =   duModel.IDCompany  
                    , Date              =   duModel.Date       
                    , Contract          =   duModel.Contract   
                    , StartHour         =   duModel.StartHour  
                    , FinishHour        =   duModel.FinishHour 
                    , Respirator        =   duModel.Respirator 
                    , Filter            =   duModel.Filter      
                    , Quantity          =   duModel.Quantity   
                    , Supervisor        =   duModel.Supervisor 
                    , Description       =   duModel.Description
                    , Activo            =   true
                };

                var guardar = await ActividadPCL.ModificarActividad(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente la actividad",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar la actividad";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Actividades");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarActividad()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
            var Actividades = await ActividadPCL.ListarActividades(Usuariores.Usuario.IdUsuario);
            if (Actividades.Code != 200) throw new Exception(Actividades.Message);
            if (Actividades.Data == null) throw new Exception("Error al intentar cargar perfiles");
            
            var duModel = new ActividadBE.ResponseActividadBE
            {
                IDactivity          =   0
                , IDCompany         =   0  
                , lCompany          =   Actividades.Data.lresponse.FirstOrDefault().lCompany		
                , Date              =   ""
                , Contract          =   0  
                , lContract         =   Actividades.Data.lresponse.FirstOrDefault().lContract
                , StartHour         =   ""
                , FinishHour        =   ""
                , Respirator        =   0
                , lRespirator       =   Actividades.Data.lresponse.FirstOrDefault().lRespirator
                , Filter            =   0
                , lFilter           =   Actividades.Data.lresponse.FirstOrDefault().lFilter
                , Supervisor        =   ""
                , Description       =   ""
                , Activo            =   true
                
            };
            return PartialView("_RegistrarActividad", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarActividad(ActividadBE.ResponseActividadBE duModel)
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

                    var nuevo = new ActividadBE.ResponseActividadBE
                    {
                        IDactivity          =   0
                        , IDCompany         =   duModel.IDCompany  
                        , Date              =   Convert.ToString(duModel.Date)
                        , Contract          =   duModel.Contract   
                        , StartHour         =   Convert.ToString(duModel.StartHour)
                        , FinishHour        =   Convert.ToString(duModel.FinishHour)
                        , Respirator        =   duModel.Respirator 
                        , Filter            =   duModel.Filter  
                        , Quantity          =   duModel.Quantity   
                        , Supervisor        =   duModel.Supervisor 
                        , Description       =   duModel.Description
                        , Activo            =   true
                    };
                    var registrar = await ActividadPCL.RegistrarActividad(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente la actividad",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar el nuevo usuario";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("Actividades", "Actividad");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteActividad(int id)
        {
            try
            {
                var datos = await ActividadPCL.BuscarActividad(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ActividadBE.ResponseActividadBE duModel = datos.Data.response;

                return PartialView("_DeleteActividad", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteActividad(ActividadBE.ResponseActividadBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ActividadBE.ResponseActividadBE
                {
                    IDactivity          =   duModel.IDactivity
                    , Activo            =   false
                };

                var guardar = await ActividadPCL.ModificarActividad(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente la actividad",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar al Actividad";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Actividades");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Actividad


    }
}