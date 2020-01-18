using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FrontProyectoEdutec.Models.Rol;
//using FrontProyectoEdutec.Models.Rol;
using FrontProyectoEdutec.Models.Usuario;
using FrontProyectoEdutec.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontProyectoEdutec.Controllers
{
    public class UsuariosController : Controller
    {
        // GET:
        public ActionResult Index()
        {
            IEnumerable<UsuarioViewModel> usuarios = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");

                //HTTP GET
                var responseTask = client.GetAsync("Usuarios/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UsuarioViewModel>>();
                    readTask.Wait();

                    usuarios = readTask.Result;
                }
                else
                {
                    usuarios = Enumerable.Empty<UsuarioViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
                }

            }

            return View(usuarios);
        }

        // GET para los Roles
        public ActionResult SelectRoles()
        {
            IEnumerable<SelectViewModel> roles = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");

                //HTTP GET
                var responseTask = client.GetAsync("Roles/Select");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SelectViewModel>>();
                    readTask.Wait();
                    roles = readTask.Result;
                }
                else
                {
                    roles = Enumerable.Empty<SelectViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
                }
            }
            return View(ViewBag.roles);
        }

        // POST USUARIOS

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel usuario)
        {
            using (var cliet = new HttpClient())
            {
                cliet.BaseAddress = new Uri("https://localhost:5001/api/");

                //HTTP POST
                var postTask = cliet.PostAsJsonAsync<CreateViewModel>("Usuarios/Create", usuario);
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
            //ViewBag.Roles;
            return View(usuario);
        }

        // DELETE

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");

                // HTTP DELETE
                var deleteTask = client.DeleteAsync("Usuarios/Delete/" + id.ToString());
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
