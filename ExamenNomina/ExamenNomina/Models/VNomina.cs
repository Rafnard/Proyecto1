using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenNomina.Models
{
    [MetadataType(typeof(VNomina))]
    public partial class Nomina
    {
        
        public class VNomina
        {
            public int IdNomina { get; set; }
            [Required]
            [DisplayName("Nombre de Empleado")]
            public int IdEmpleado { get; set; }
            [Required]
            
            public string Fecha { get; set; }
          
            [Required]
            [DisplayName("Dias Trabajados")]
            public int Dias { get; set; }
            [Required]
            [DisplayName("Sueldo Quincenal")]
            public double SueldoQuincenal { get; set; }


        }
    }
}