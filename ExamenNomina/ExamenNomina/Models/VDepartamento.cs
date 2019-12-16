using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenNomina.Models
{
    [MetadataType(typeof(VDepartamento))]
    public partial class Departamento
    {
        public class VDepartamento
        {

            public int IdDepartamento { get; set; }
            [Required]
            [DisplayName("Departamento")]
            public string NomDepartamento { get; set; }

        }
    }
}