using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionMVC.Areas.ModuloInventarios.Controllers
{
    public class InventariosController : Controller
    {
        // GET: ModuloInventarios/Inventarios
        public ActionResult Index()
        {
            return View();
        }
    }
}