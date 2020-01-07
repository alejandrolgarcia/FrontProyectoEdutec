using System;
using FrontProyectoEdutec.Models.Usuario;
using FrontProyectoEdutec.Models.Rol;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FrontProyectoEdutec.Models.ViewModels
{
  public class CreateUsuariosViewModel
  {
    public CreateViewModel Usuario { get; set; }

    public SelectList SelecViewModel { get; set; }

    public CreateUsuariosViewModel(CreateViewModel usuario, List<SelectViewModel> selectViewModel )
    {
      this.SelecViewModel = new SelectList(selectViewModel, "Idrol", "Nombre");
    }

    public CreateUsuariosViewModel() { }
  }
}
