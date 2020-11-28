using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionMVC.ViewModel
{
    public class CorreoViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Destinatario { get; set; }
        public string Asunto { get; set; }

        [DataType(DataType.MultilineText)]
        public string Cuerpo { get; set; }
    }
}