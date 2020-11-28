using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionMVC.ViewModel
{
    public class FacturaViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public int ClienteId { get; set; }
        public List<ProductoViewModel> Productos { get; set; }

        public ProductoViewModel Producto { get; set; }
    }
}