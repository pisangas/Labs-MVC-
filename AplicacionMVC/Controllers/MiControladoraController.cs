using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionMVC.Controllers
{
    public class MiControladoraController : Controller
    {
        // GET: MiControladora
        public ActionResult Index()
        {
            return View();            
        }

        public ActionResult Saludo(string nombre)
        {
            ViewBag.Mensaje = "Hola " + nombre + "!";
            return View();
        }
    }
}