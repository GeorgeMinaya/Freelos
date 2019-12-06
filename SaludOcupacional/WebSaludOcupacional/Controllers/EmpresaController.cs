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
    public class EmpresaController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Empresa

        public async Task<ActionResult> Empresas()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var UsuarioLogin = (BaseModel)Session[Sesiones.UsuarioLogin];

                var lEmpresas = await EmpresaPCL.ListarEmpresas();
                if (lEmpresas.Code != 200) throw new Exception(lEmpresas.Message);

                MantenimientoEmpresaModel muModel = new MantenimientoEmpresaModel()
                {
                    Usuario = UsuarioLogin.Usuario,
                    lEmpresas = lEmpresas.Data.lresponse
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
        public async Task<PartialViewResult> DatosEmpresa(int id)
        {
            try
            {
                var datos = await EmpresaPCL.BuscarEmpresa(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                EmpresaBE.ResponseEmpresaBE duModel = datos.Data.response;

                return PartialView("_DatosEmpresa", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DatosEmpresa(EmpresaBE.ResponseEmpresaBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Empresares = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new EmpresaBE.ResponseEmpresaBE
                {
                    IDCompany               =   duModel.IDCompany		
                    , RUC                   =   duModel.RUC				
                    , Name                  =   duModel.Name			
                    , Tradename             =   duModel.Tradename		
                    , Address               =   duModel.Address			
                    , Contactname           =   duModel.Contactname		
                    , ContactLastname       =   duModel.ContactLastname	
                    , ContactLastname2      =   duModel.ContactLastname2
                    , Cellphone             =   duModel.Cellphone		
                    , Email                 =   duModel.Email			
                    , Activo                =   duModel.Activo
                };
                var guardar = await EmpresaPCL.ModificarEmpresa(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();

                ObjMessage = new MessageDialog()
                {
                    Title   =   "Se modificó correctamente la empresa",
                    Estado  =   0,
                    Message =   guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title    =   "Error al intentar modificar la empresa";
                    ObjMessage.Estado   =   guardar.Data.Codigo;
                }
                return RedirectToAction("Empresas");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> RegistrarEmpresa()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            
            var duModel = new EmpresaBE.ResponseEmpresaBE
            {
                RUC                     =   null
                , Name                  =   ""
                , Tradename             =   ""
                , Address               =   ""
                , Contactname           =   ""
                , ContactLastname       =   ""
                , ContactLastname2      =   ""
                , Cellphone             =   ""
                , Email                 =   ""
                , Activo                =   true
            };

            return PartialView("_RegistrarEmpresa", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarEmpresa(EmpresaBE.ResponseEmpresaBE duModel)
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
                    var Empresares = (BaseModel)Session[Sesiones.UsuarioLogin];
                    
                    var nuevo = new EmpresaBE.ResponseEmpresaBE
                    {
                        IDCompany               =   duModel.IDCompany		
                        , RUC                   =   duModel.RUC				
                        , Name                  =   duModel.Name			
                        , Tradename             =   duModel.Tradename		
                        , Address               =   duModel.Address			
                        , Contactname           =   duModel.Contactname		
                        , ContactLastname       =   duModel.ContactLastname	
                        , ContactLastname2      =   duModel.ContactLastname2
                        , Cellphone             =   duModel.Cellphone		
                        , Email                 =   duModel.Email			
                        , Activo                =   duModel.Activo
                    };
                    var registrar = await EmpresaPCL.RegistrarEmpresa(nuevo);
                    if (registrar.Code != 200) throw new Exception(registrar.Message);
                    ObjMessage = new MessageDialog() {
                        Title = "Se registro correctamente la nueva empresa",
                        Estado = 0,
                        Message = registrar.Data.Message
                    };
                    if (registrar.Data.Codigo != 0) { 
                        ObjMessage.Title = "Error al intentar registrar la nueva empresa";
                        ObjMessage.Estado = registrar.Data.Codigo;
                    }
                }

                return RedirectToAction("Empresas", "Empresa");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteEmpresa(int id)
        {
            try
            {
                var datos = await EmpresaPCL.BuscarEmpresa(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                EmpresaBE.ResponseEmpresaBE duModel = datos.Data.response;

                return PartialView("_DeleteEmpresa", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteEmpresa(EmpresaBE.ResponseEmpresaBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var modificado = new EmpresaBE.ResponseEmpresaBE
                {
                    IDCompany   =   duModel.IDCompany,
                    Activo      =   duModel.Activo
                };

                var guardar = await EmpresaPCL.ModificarEmpresa(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente la empresa",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar la empresa";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Empresas");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Empresas

    }
}