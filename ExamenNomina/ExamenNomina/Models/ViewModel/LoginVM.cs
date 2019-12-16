using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ExamenNomina.Models.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "Correo electronico")]
        [Required]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "El campo correo electronico debe ser valido")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}