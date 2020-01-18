using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using FrontProyectoEdutec.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontProyectoEdutec.Controllers
{
  public class LoginController : Controller
  {
    // GET: /<controller>/
    public IActionResult Index()
    {
      return View();
    }

    public ActionResult login()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Login(LoginViewModel model)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/");
        var postTask = client.PostAsJsonAsync<LoginViewModel>("Usuarios/Login", model);

        postTask.Wait();

        var result = postTask.Result;

        if (result.IsSuccessStatusCode)
        {
          var readToken = result.Content.ToString();

          Response.Cookies.Append(
              "LLave",
              readToken,
              new CookieOptions() { Path = "/"}
            );
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
          return BadRequest();
        }
      }
      return View();
    }
    
  }
}
