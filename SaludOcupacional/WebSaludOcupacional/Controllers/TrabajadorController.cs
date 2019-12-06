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
    public class TrabajadorController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Trabajadores
        public async Task<ActionResult> Trabajadores()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lTrabajadores = await TrabajadorPCL.ListarTrabajadors();
                if (lTrabajadores.Code != 200) throw new Exception(lTrabajadores.Message);

                MantenimientoTrabajadorModel muModel = new MantenimientoTrabajadorModel()
                {
                    Usuario = Usuariores.Usuario,
                    lTrabajador = lTrabajadores.Data.lresponse
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
        public async Task<ActionResult> AutoCompleteTrabajador(string term /*our key word*/)
        {
            var lUsuarios = await MantenimientoPCL.ListarUsuarios();
            if (lUsuarios.Code != 200) throw new Exception(lUsuarios.Message);
            int Acc = lUsuarios.Data.lresponse.Count();
            string[] items = new string[Acc];
            int i = 0;
            foreach (ResponseLoginBE g in lUsuarios.Data.lresponse)
            {
                items[i] = string.Format("{0}", g.DNI);
                i = i + 1;
            }
            //we did a fetch of term from items
            var filteredItems = items.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
            //we return a json data
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public async Task<PartialViewResult> DatosTrabajador(int id)
        {
            try
            {
                var Trabajador = await TrabajadorPCL.BuscarTrabajador(id);
                if (Trabajador.Code != 200) throw new Exception(Trabajador.Message);
                var duModel = new TrabajadorBE.ResponseTrabajadorBE
                {
                    IdTrabajador            =   Trabajador.Data.response.IdTrabajador
                    , Birthdate             =   DateTime.Now.ToString("dd-MM-yyyy")
                    , DNIemployee           =   Trabajador.Data.response.DNIemployee
                    , Occupation            =   Trabajador.Data.response.Occupation		
                    , lUsuario              =   Trabajador.Data.response.lUsuario		
                    , lOccupation           =   Trabajador.Data.response.lOccupation
                    , Schedule              =   Trabajador.Data.response.Schedule
                    , lSchedule             =   Trabajador.Data.response.lSchedule
                    , Company               =   Trabajador.Data.response.Company
                    , lCompany              =   Trabajador.Data.response.lCompany
                    , WorkCondition         =   Trabajador.Data.response.WorkCondition
                    , lWorkCondition        =   Trabajador.Data.response.lWorkCondition
                    , RiskFactor            =   Trabajador.Data.response.RiskFactor
                    , lRiskFactor           =   Trabajador.Data.response.lRiskFactor
                    , Name                  =   Trabajador.Data.response.Name
                    , Activo                =   true
                };
                //TrabajadorBE.ResponseTrabajadorBE duModel = Trabajador.Data.response;

                return PartialView("_DatosTrabajador", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosTrabajador(TrabajadorBE.ResponseTrabajadorBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new TrabajadorBE.ResponseTrabajadorBE
                {
                    IdTrabajador            =   duModel.IdTrabajador
                    , Birthdate             =   duModel.Birthdate
                    , DNIemployee           =   duModel.DNIemployee
                    , Occupation            =   duModel.Occupation   		
                    , Schedule              =   duModel.Schedule     		
                    , Company               =   duModel.Company      			
                    , WorkCondition         =   duModel.WorkCondition	
                    , RiskFactor            =   duModel.RiskFactor   		   
                    , Name                  =   duModel.Name         			
                    , Activo                =   true
                };

                var guardar = await TrabajadorPCL.ModificarTrabajador(modificado);
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
                return RedirectToAction("Trabajadores");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarTrabajador()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var Trabajador = await TrabajadorPCL.ListarTrabajadors();
            if (Trabajador.Code != 200) throw new Exception(Trabajador.Message);
            if (Trabajador.Data == null) throw new Exception("Error al intentar cargar perfiles");
            
            var duModel = new TrabajadorBE.ResponseTrabajadorBE
            {
                IdTrabajador            =   0
                , Birthdate             =   DateTime.Now.ToString("dd-MM-yyyy")
                , DNIemployee           =   ""	
                , Occupation            =   0		
                , lUsuario              =   Trabajador.Data.lresponse.FirstOrDefault().lUsuario		
                , lOccupation           =   Trabajador.Data.lresponse.FirstOrDefault().lOccupation		
                , Schedule              =   0		
                , lSchedule             =   Trabajador.Data.lresponse.FirstOrDefault().lSchedule		
                , Company               =   0		
                , lCompany              =   Trabajador.Data.lresponse.FirstOrDefault().lCompany		
                , WorkCondition         =   0
                , lWorkCondition        =   Trabajador.Data.lresponse.FirstOrDefault().lWorkCondition	
                , RiskFactor            =   0		
                , lRiskFactor           =   Trabajador.Data.lresponse.FirstOrDefault().lRiskFactor	   
                , Name                  =   ""			
                , Activo                =   true
            };
            return PartialView("_RegistrarTrabajador", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarTrabajador(TrabajadorBE.ResponseTrabajadorBE duModel)
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

                    var nuevo = new TrabajadorBE.ResponseTrabajadorBE
                    {
                        Birthdate               =   duModel.Birthdate
                        , DNIemployee           =   duModel.DNIemployee
                        , Occupation            =   duModel.Occupation   		
                        , Schedule              =   duModel.Schedule     		
                        , Company               =   duModel.Company      			
                        , WorkCondition         =   duModel.WorkCondition	
                        , RiskFactor            =   duModel.RiskFactor   		   
                        , Name                  =   duModel.Name         			
                        , Activo                =   true
                    };
                    var registrar = await TrabajadorPCL.RegistrarTrabajador(nuevo);
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
                return RedirectToAction("Trabajadores", "Trabajador");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteTrabajador(int id)
        {
            try
            {
                var datos = await TrabajadorPCL.BuscarTrabajador(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                TrabajadorBE.ResponseTrabajadorBE duModel = datos.Data.response;

                return PartialView("_DeleteTrabajador", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTrabajador(TrabajadorBE.ResponseTrabajadorBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new TrabajadorBE.ResponseTrabajadorBE
                {
                    IdTrabajador = duModel.IdTrabajador,
                    Activo = duModel.Activo
                };

                var guardar = await TrabajadorPCL.ModificarTrabajador(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente al Trabajador.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar al Trabajador";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Trabajadores");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Trabajadores


    }
}