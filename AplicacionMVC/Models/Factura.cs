using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicacionMVC.Models
{
    [Table("Facturas")]
    public class Factura
    {
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public int PersonaId { get; set; }
        public virtual Persona Persona { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}