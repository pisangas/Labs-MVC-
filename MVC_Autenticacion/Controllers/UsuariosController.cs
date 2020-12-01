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
            var usuario = db.Users.Find(id);

            if (usuario == null) return null;

            UserViewModel userViewModel = new UserViewModel
            {
                UserId = usuario.Id,
                Nombre = usuario.UserName,
                Email = usuario.Email,
                Roles= new List<RoleViewModel>()
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




            return View();
        }

    }
}