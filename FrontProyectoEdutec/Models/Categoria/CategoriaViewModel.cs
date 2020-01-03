using System;
using System.ComponentModel.DataAnnotations;

namespace FrontProyectoEdutec.Models.Categoria
{
  public class CategoriaViewModel
  {
    public int Idcategoria { get; set; }
    [Required]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 64 caracteres y menos de 3 caracteres")]
    public string Nombre { get; set; }
    [StringLength(64, MinimumLength = 3, ErrorMessage = "El nombre no debe tener mas de 64 caracteres y menos de 3 caracteres")]
    public string Descripcion { get; set; }
    public bool Estado { get; set; }
  }

  public enum Estado
  {
    True,
    False
  }
}
