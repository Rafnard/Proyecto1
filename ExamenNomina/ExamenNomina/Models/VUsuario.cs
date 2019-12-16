using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenNomina.Models
{
        [MetadataType(typeof(VUsuario))]
        public partial class Usuario
        {
            public class VUsuario
            {
                public string Id_Usuario { get; set; }
                [Required]
                [DisplayName("Usuario")]
                public string Nombre { get; set; }
                [Required]
                [DisplayName("Correo")]
                public string Email { get; set; }
                [Required]
                [DisplayName("Contraseña")]
                public string Password { get; set; }
                [Required]
                public string Rol { get; set; }

            }
        }
    }