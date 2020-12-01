using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Autenticacion.ViewModels
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public string Nombre { get; set; }
    }
}