using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicacionMVC.Models
{
    [Table("DetalleFactura")]
    public class DetalleFactura
    {
        public int DetalleFacturaId { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public int Cantidad { get; set; }

        public int ProductoId { get; set; }
        public virtual Producto Producto { get; set; }

        public virtual Factura Factura { get; set; }

    }
}