using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using FrontProyectoEdutec.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace FrontProyectoEdutec.Controllers
{
    public class LoginController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
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
                        "UsuarioActivo",
                        readToken,
                        new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTimeOffset.Now.AddMinutes(15)
                        }
                      );
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact your administrator.");
                    return BadRequest();
                }
            }
            return RedirectToAction("Index", "Categorias");
        }

    }
    //public class LoginController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        return View();

    //    }
    //    public ActionResult Login(LoginViewModel login)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            client.BaseAddress = new Uri("https://localhost:5001/api/");
    //            string token = string.Empty;
    //            // HTTP LOGIN
    //            var loginTask = client.PostAsJsonAsync<LoginViewModel>("Usuarios/Login/", login);
    //            loginTask.Wait();

    //            var result = loginTask.Result;
    //            if (result.IsSuccessStatusCode)
    //            {
    //                var readTask = result.Content.ReadAsStringAsync();
    //                readTask.Wait();
    //                token = readTask.Result;
    //                //return RedirectToAction("Index");
    //                /*
    //                //create a cookie
    //                HttpCookie myCookie = new HttpCookie("myCookie");

    //                //Add key-values in the cookie
    //                myCookie.Values.Add("userid", objUser.id.ToString());

    //                //set cookie expiry date-time. Made it to last for next 12 hours.
    //                myCookie.Expires = DateTime.Now.AddHours(12);

    //                //Most important, write the cookie to client.
    //                Response.Cookies.Add(myCookie);
    //                */
    //            }
    //            else
    //            {
    //                //MOSTRAR ERROR DE LOGIN
    //                ModelState.AddModelError(string.Empty, "Unable to Login. Please contact your administrator.");
    //            }
    //        }
    //        return View(login);
    //    }
    //}
}