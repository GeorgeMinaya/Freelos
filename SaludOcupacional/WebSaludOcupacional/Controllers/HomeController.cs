using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSaludOcupacional.Models;
using WebSaludOcupacional.PCL;
using System.Threading.Tasks;
using WebSaludOcupacional.DTO;
using WebSaludOcupacional.Resources;
using Salud.Ocupacional.BE;

namespace WebSaludOcupacional.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            var Usuariores = (BaseModel)Session[Sesiones.UsuarioLogin];
            var model = new BaseModel()
            {
                Usuario = Usuariores.Usuario
            };
            return View(model);
        }

        public ActionResult About()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session[Sesiones.UsuarioLogin] == null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}