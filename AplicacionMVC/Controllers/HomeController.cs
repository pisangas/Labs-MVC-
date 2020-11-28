using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Prueba()
        {
            ViewBag.Saludo = "Mensaje de prueba";

            return View();
        }

        

        public ActionResult AJAXAntiforgery()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXAntiforgery(string nombre)
        {
            return Json(new { saludo = "Hola " + nombre});
        }

    }
}