using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicacionMVC.Models
{
    [Table("Productos")]
    public class Producto
    {
        public int ProductoId { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres ", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        [Display (Name = "Descripción")]
        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public decimal Precio { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }


    }
}