using Salud.Ocupacional.BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSaludOcupacional.Models;
using WebSaludOcupacional.PCL;
using WebSaludOcupacional.Resources;
using static WebSaludOcupacional.Models.MantenimientoModel;

namespace WebSaludOcupacional.Controllers
{
    public class ArchivoController : Controller
    {
        public static MessageDialog ObjMessage { get; set; }

        #region Archivos
        public async Task<ActionResult> Archivos()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

                //var lArchivoses = await ArchivosPCL.ListarArchivoses();
                //if (lArchivoses.Code != 200) throw new Exception(lArchivoses.Message);

                MantenimientoArchivosModel muModel = new MantenimientoArchivosModel()
                {
                    Usuario = Usuariores.Usuario,
                    //lArchivoses = lArchivoses.Data.lresponse
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
        public async Task<PartialViewResult> DatosArchivo(int id)
        {
            try
            {
                var datos = await ArchivosPCL.BuscarArchivo(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ArchivoBE.ResponseArchivoBE duModel = datos.Data.response;

                return PartialView("_DatosArchivo", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<ActionResult> DatosArchivo(ArchivoBE.ResponseArchivoBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ArchivoBE.ResponseArchivoBE
                {
                };

                var guardar = await ArchivosPCL.ModificarArchivo(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se modificó correctamente el archivo",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar modificar el archivo";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Archivoses");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> RegistrarArchivo()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];

            var datos = await ArchivosPCL.DatosRegistrar();

            if (datos.Code != 200) throw new Exception(datos.Message);
            if (datos.Data == null) throw new Exception("Error al intentar cargar perfiles");
            
            var duModel = new ArchivoBE.ResponseArchivoBE
            {
                LCompany = datos.Data.LCompany,
                LContract = datos.Data.LContract,
                LTrabajador = datos.Data.LTrabajador
            };

            return PartialView("_RegistrarArchivo", duModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegistrarArchivo(ArchivoBE.ResponseArchivoBE duModel)
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
                    byte[] bytesArchivos = new byte[duModel.ArchivoCargado.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(duModel.ArchivoCargado.InputStream))
                    {
                        bytesArchivos = theReader.ReadBytes(duModel.ArchivoCargado.ContentLength);
                    }

                    var login = (BaseModel)Session[Sesiones.UsuarioLogin];

                    var registrar = await ArchivosPCL.RegistrarArchivo(
                        new ArchivoBE.ResponseArchivoBE
                        {
                            IDCompany = duModel.IDCompany,
                            IdContract = duModel.IdContract,
                            IdTrabajador = duModel.IdTrabajador,
                            ArchivoBase64 = Convert.ToBase64String(bytesArchivos),
                            IdUsuarioRegistro = login.Usuario.IdUsuario,
                            FechaRegistro = DateTime.Now
                        });

                    if (registrar.Code != 200) throw new Exception(registrar.Message);

                    //ObjMessage = new MessageDialog()
                    //{
                    //    Title = "Se registro correctamente la actividad",
                    //    Estado = 0,
                    //    Message = registrar.Data.Message
                    //};

                    //if (registrar.Data.Codigo != 0)
                    //{
                    //    ObjMessage.Title = "Error al intentar registrar el nuevo usuario";
                    //    ObjMessage.Estado = registrar.Data.Codigo;
                    //}

                }

                return RedirectToAction("Archivos", "Archivo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PartialViewResult> DeleteArchivo(int id)
        {
            try
            {
                var datos = await ArchivosPCL.BuscarArchivo(id);
                if (datos.Code != 200) throw new Exception(datos.Message);

                ArchivoBE.ResponseArchivoBE duModel = datos.Data.response;

                return PartialView("_DeleteArchivo", duModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteArchivo(ArchivoBE.ResponseArchivoBE duModel)
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
                var modificado = new ArchivoBE.ResponseArchivoBE
                {
                    //IDCompany = duModel.IdArchivo,
                    //Activo = duModel.Activo
                };

                var guardar = await ArchivosPCL.ModificarArchivo(modificado);
                if (guardar.Code != 200) throw new Exception(guardar.Message);
                ModelState.Clear();
                ObjMessage = new MessageDialog()
                {
                    Title = "Se eliminó correctamente el archivo",
                    Estado = 0,
                    Message = guardar.Data.Message
                };
                if (guardar.Data.Codigo != 0)
                {
                    ObjMessage.Title = "Error al intentar eliminar el archivo";
                    ObjMessage.Estado = guardar.Data.Codigo;
                }
                return RedirectToAction("Archivoses");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Archivos


    }
}