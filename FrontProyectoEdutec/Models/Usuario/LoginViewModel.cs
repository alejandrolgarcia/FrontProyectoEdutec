using System;
using System.ComponentModel.DataAnnotations;

namespace FrontProyectoEdutec.Models.Usuario
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
