using System;
using System.ComponentModel.DataAnnotations;

namespace FrontProyectoEdutec.Models.Usuario
{
  public class LoginViewModel
  {
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
  }
}
