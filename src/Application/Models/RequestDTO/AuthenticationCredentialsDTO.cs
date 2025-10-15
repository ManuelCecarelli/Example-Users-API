using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.RequestDTO
{
    public class AuthenticationCredentialsDTO
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}
