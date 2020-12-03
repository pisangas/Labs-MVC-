using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Autenticacion.Models;
using MVC_Autenticacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Autenticacion.Controllers
{
    public class UsuariosController : Controller
    {
        private UserManager<ApplicationUser> gestorUsuarios = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Usuarios
        public ActionResult Index()
        {
            return View(gestorUsuarios.Users.Select(u => new UserViewModel { Email = u.Email, Nombre = u.UserName, UserId = u.Id}));
        }

        public ActionResult Roles(string id)
        {
            UserViewModel userViewModel = CargarInformacion(id);
            return View(userViewModel);
        }

        public ActionResult AgregarRol(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userViewModel = new UserViewModel
            {
                UserId = user.Id,
                Nombre = user.UserName,
                Email = user.Email
            };

            ViewBag.RolId = new SelectList(db.Roles.OrderBy(r => r.Name), "Id", "Name");

            return View(userViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AgregarRol(UserViewModel userViewModel)
        {
            var rolId = Request["RolId"];

            if (string.IsNullOrEmpty(rolId))
            {
                ViewBag.RolId = new SelectList(db.Roles.OrderBy(r => r.Name), "Id", "Name");
                return View(userViewModel);
            }

            var rol = db.Roles.Find(rolId);

            if (!gestorUsuarios.IsInRole(userViewModel.UserId, rol.Name))
            {
                gestorUsuarios.AddToRole(userViewModel.UserId, rol.Name);
            }
            userViewModel = CargarInformacion(userViewModel.UserId);
            return View("Roles", userViewModel);
        }

        private UserViewModel CargarInformacion(string userId)
        {
            var usuario = db.Users.Find(userId);

            if (usuario == null) return null;

            UserViewModel userViewModel = new UserViewModel
            {
                UserId = usuario.Id,
                Nombre = usuario.UserName,
                Email = usuario.Email,
                Roles = new List<RoleViewModel>()
            };

            foreach (var item in usuario.Roles)
            {
                var rol = db.Roles.Find(item.RoleId);
                var roleViewModel = new RoleViewModel
                {
                    RoleId = item.RoleId,
                    Nombre = rol.Name
                };

                userViewModel.Roles.Add(roleViewModel);
            }

            return userViewModel;
        }

    }
}