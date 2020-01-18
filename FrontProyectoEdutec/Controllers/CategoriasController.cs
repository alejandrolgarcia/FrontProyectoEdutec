using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FrontProyectoEdutec.Models.Categoria;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontProyectoEdutec.Controllers
{
  public class CategoriasController : Controller
  {
    //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJsZ0BnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbmlzdHJhZG9yIiwiSWR1c3VhcmlvIjoiMSIsIlJvbCI6IkFkbWluaXN0cmFkb3IiLCJOb21icmUiOiJMdWlzIiwiZXhwIjoxNTc4ODg1OTc2LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo1MDAxLyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEvIn0.soK1hSigM0DWe3VtjF2mQxUsvOG_7tYpZbP82I-cJTo";
    // GET:
    public ActionResult Index()
    {
      IEnumerable<CategoriaViewModel> categorias = null;

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/");
        // Enviar el token de autorizacion en el Header
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //HTTP GET
        var responseTask = client.GetAsync("Categorias/GetAll");
        responseTask.Wait();

        var result = responseTask.Result;

        if (result.IsSuccessStatusCode)
        {
          var readTask = result.Content.ReadAsAsync<IList<CategoriaViewModel>>();
          readTask.Wait();

          categorias = readTask.Result;
        }
        else
        {
          categorias = Enumerable.Empty<CategoriaViewModel>();

          ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
        }

      }

      return View(categorias);
    }

    // POST

    public ActionResult create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(CategoriaViewModel categoria)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/");

        //HTTP POST
        var postTask = client.PostAsJsonAsync<CategoriaViewModel>("Categorias/Create", categoria);
        postTask.Wait();

        var result = postTask.Result;

        if (result.IsSuccessStatusCode)
        {
          return Redirect("Index");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
        }
      }
      return View(categoria);
    }

    // EDIT

    public ActionResult Edit(int id)
    {
      CategoriaViewModel categoria = null;

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/");

        // HTTP GET para traer la categoria
        var responseTask = client.GetAsync("Categorias/GetItem/" + id.ToString());
        responseTask.Wait();

        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {
          var readTask = result.Content.ReadAsAsync<CategoriaViewModel>();
          readTask.Wait();

          categoria = readTask.Result;
        }
      }
      return View(categoria);
    }

    [HttpPost]
    public ActionResult Edit(CategoriaViewModel categoria)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/Categorias/");

        // HTTP PUT
        var putTask = client.PutAsJsonAsync<CategoriaViewModel>("Update/", categoria);
        putTask.Wait();

        var result = putTask.Result;
        if (result.IsSuccessStatusCode)
        {
          return RedirectToAction("Index");
        }
      }

      return View(categoria);

    }

    // DELETE

    public ActionResult Delete(int id)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:5001/api/");

        // HTTP DELETE
        var deleteTask = client.DeleteAsync("Categorias/Delete/" + id.ToString());
        deleteTask.Wait();

        var result = deleteTask.Result;
        if (result.IsSuccessStatusCode)
        {
          return RedirectToAction("Index");
        }
      }

      return RedirectToAction("Index");
    }
  }
}
