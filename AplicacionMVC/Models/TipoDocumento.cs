using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicacionMVC.Models
{
    [Table("TiposDocumento")]
    public class TipoDocumento
    {
        public int TipoDocumentoId { get; set; }
        
        [Display(Name = "Descripción")]
        [Required (ErrorMessage = "Debe ingresar el campo {0}")]
        public string Descripcion { get; set; }

        public virtual ICollection<Persona> personas { get; set; }
    }
}