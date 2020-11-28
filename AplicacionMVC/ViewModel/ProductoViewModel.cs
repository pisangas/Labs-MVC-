using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionMVC.ViewModel
{
    public class ProductoViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar el Producto")]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad")]
        public int Cantidad { get; set; } = 1;

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal SubTotal
        {
            get
            {
                return Precio * Cantidad;
            }
        }

    }
}