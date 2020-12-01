using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Autenticacion.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Nombre { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RoleViewModel rol { get; set; }

        public List<RoleViewModel> Roles { get; set; }


    }
}