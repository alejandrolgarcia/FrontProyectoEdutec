using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrontProyectoEdutec.Models.Rol;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontProyectoEdutec.Controllers
{
  public class RolesController : Controller
  {
    // GET:
    public ActionResult Index()
    {
      IEnumerable<RolViewModel> roles = null;

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/");

        //HTTP GET
        var responseTask = client.GetAsync("Roles/GetAll");
        responseTask.Wait();

        var result = responseTask.Result;

        if (result.IsSuccessStatusCode)
        {
          var readTask = result.Content.ReadAsAsync<IList<RolViewModel>>();
          readTask.Wait();

          roles = readTask.Result;
        }
        else
        {
          roles = Enumerable.Empty<RolViewModel>();

          ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
        }

      }

      return View(roles);
    }
  }
}
