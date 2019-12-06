using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using WebSaludOcupacional.Models;
using WebSaludOcupacional.PCL;
using WebSaludOcupacional.Resources;
using Salud.Ocupacional.BE;

namespace WebSaludOcupacional.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public async Task<ActionResult> Index()
        {
            Session.Clear();
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel model)
        {
            try
            {
                var usuario = model.NombreLogin;
                var contrasena = model.Contrasena;
                RequestLoginBE ObjLogin = new RequestLoginBE() {
                    DNI = usuario,
                    Password = contrasena
                };
                var autenticar = await UsuarioPCL.Login(ObjLogin);
                if (autenticar.Code != 200) throw new Exception(autenticar.Message);

                var sesion = new BaseModel();
                sesion.Usuario = autenticar.Data.response;

                Session[Sesiones.UsuarioLogin] = sesion;
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }            
        }

        public ActionResult Password()=> View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Password(LoginModel model)
        {
            try
            {
                RequestLoginBE ObjLogin = new RequestLoginBE()
                {
                    Email = model.Correo
                };
                var notificar = await UsuarioPCL.NotificaLogin(ObjLogin);
                if (notificar.Code != 200) throw new Exception(notificar.Message);
                ViewBag.Notifica = notificar.Data;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}