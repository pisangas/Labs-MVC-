using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Autenticacion.Models;
using MVC_Autenticacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Autenticacion.Controllers
{
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> gestorRoles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        // GET: Roles
        public ActionResult Index()
        {
            return View(gestorRoles.Roles.Select(r => new RoleViewModel { RoleId = r.Id, Nombre = r.Name}));
        }

        // GET: Roles
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!gestorRoles.RoleExists(roleViewModel.Nombre))
                {
                    gestorRoles.Create(new IdentityRole(roleViewModel.Nombre));
                }
                return RedirectToAction("Index");
            }           
            
            return View(roleViewModel);
        }
    }
}