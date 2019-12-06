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
    public class ContratoController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Contratos
        public async Task<ActionResult> Contratos()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lContratoses = await ContratosPCL.ListarContratos();
                if (lContratoses.Code != 200) throw new Exception(lContratoses.Message);

                MantenimientoContratosModel muModel = new MantenimientoContratosModel()
                {
                    Usuario     =   Usuariores.Usuario,
                    lContratos  =   lContratoses.Data.lresponse
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
        public async Task<PartialViewResult> DatosContrato(int id)
        {
            try
            {
                var datos = await ContratosPCL.BuscarContrato(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ContratoBE.ResponseContratoBE duModel = datos.Data.response;

                return PartialView("_DatosContrato", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosContrato(ContratoBE.ResponseContratoBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ContratoBE.ResponseContratoBE
                {
                    IDContract      =   duModel.IDContract
                    ,DraegerUser    =	Usuariores.Usuario.DNI
		            ,Amount		    =	duModel.Amount		
		            ,Company	    =	duModel.Company		
		            ,InitialDate    =	duModel.InitialDate
		            ,FinalDate	    =	duModel.FinalDate	
		            ,Quantity	    =	duModel.Quantity		
		            ,Dascription    =	duModel.Dascription	
		            ,Celular        =	duModel.Celular
		            ,Email          =	duModel.Email	
                    ,Activo = true
                };

                var guardar = await ContratosPCL.ModificarContrato(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente el contrato.",
                    Estado = 0,
                    Message = "Se modificó correctamente el contrato. " + duModel.Dascription.ToUpper()
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar el contrato";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Contratos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarContrato()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

            var Contrato = await ContratosPCL.ListarContratos();
            if (Contrato.Code != 200) throw new Exception(Contrato.Message);
            if (Contrato.Data == null) throw new Exception("Error al intentar cargar perfiles");
            
            var duModel = new ContratoBE.ResponseContratoBE
            {
                IDContract      =   0
                ,DraegerUser    =   Usuariores.Usuario.DNI
                ,Amount         =   ""
                ,Company        =   0
                ,lCompany       =   Contrato.Data.lresponse.FirstOrDefault().lCompany		
                ,lUsuario       =   Contrato.Data.lresponse.FirstOrDefault().lUsuario		
                ,InitialDate    =   ""
                ,FinalDate      =   ""
                ,Dascription    =   ""
		        ,Email          =	""
                ,Activo         =   true
            };

            return PartialView("_RegistrarContrato", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarContrato(ContratoBE.ResponseContratoBE duModel)
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

                    var nuevo = new ContratoBE.ResponseContratoBE
                    {
                        IDContract      =   duModel.IDContract
                        ,DraegerUser    =	Usuariores.Usuario.DNI
		                ,Amount		    =	duModel.Amount		
		                ,Company	    =	duModel.Company		
		                ,InitialDate    =	duModel.InitialDate
		                ,FinalDate	    =	duModel.FinalDate	
		                ,Quantity	    =	duModel.Quantity		
		                ,Dascription    =	duModel.Dascription	
		                ,Celular        =	duModel.Celular
		                ,Email          =	duModel.Email	
                        ,Activo = true
                    };
                    var registrar = await ContratosPCL.RegistrarContrato(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog()
                    {
                        Title = "Se registro correctamente el contrato.",
                        Estado = 0,
                        Message = "Se registro correctamente el contrato. " + duModel.Dascription.ToUpper() 
                    };
                    if (registrar.Data.Codigo != 0)
                    {
                        ObjMessage.Title = "Error al intentar registrar el nuevo contrato";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }
                return RedirectToAction("Contratos", "Contrato");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteContrato(int id)
        {
            try
            {
                var datos = await ContratosPCL.BuscarContrato(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ContratoBE.ResponseContratoBE duModel = datos.Data.response;

                return PartialView("_DeleteContrato", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteContrato(ContratoBE.ResponseContratoBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ContratoBE.ResponseContratoBE
                {
                    IDContract = duModel.IDContract,
                    Activo = duModel.Activo
                };

                var guardar = await ContratosPCL.ModificarContrato(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente al Contrato.",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar al Contrato";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Contratos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Contratos


    }
}