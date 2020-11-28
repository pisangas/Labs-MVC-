using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicacionMVC.Models
{
    [Table("Personas")]
    public class Persona
    {
        public int PersonaId { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres ", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres ", MinimumLength = 3)]
        public string Apellido { get; set; }

        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres ", MinimumLength = 3)]
        public string Telefono { get; set; }

        [StringLength(120, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres ", MinimumLength = 3)]
        public string Direccion { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres ", MinimumLength = 3)]
        [Required(ErrorMessage = "Debe ingresar el campo {0}")]
        public string Documento { get; set; }
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }

        public string NombreCompleto
        {
            get
            {
                return $"{Nombre} {Apellido}";
            }
        }
    }
}