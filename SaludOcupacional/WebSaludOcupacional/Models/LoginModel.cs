using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSaludOcupacional.DTO;

namespace WebSaludOcupacional.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string   NombreLogin     { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string   Contrasena      { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string   Correo          { get; set; }
        public int      Notifica        { get; set; }
    }
}