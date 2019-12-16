using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenNomina.Models
{
    [MetadataType(typeof(VEmpleado))]
    public partial class Empleado
    {
        public class VEmpleado
        {
            public int IdEmpleado { get; set; }
            [Required]
            [DisplayName("Departamento")]
            public int IdDepartamento { get; set; }
            [Required]
            [DisplayName("Nombre de empleado")]
            public string NomEmpleado { get; set; }
            public Nullable<bool> Activo { get; set; }
            [Required]
            [DisplayName("Sueldo/Dia")]
            public double Sueldo { get; set; }
        }
    }
}