using MenuCarritoOrt.Datos;
using MenuCarritoOrt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Controllers
{
    public class HomeController : Controller /*test*/
    {
        private readonly BaseDatos _context;

        public HomeController(BaseDatos context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var usuario = _context
              .Usuarios
              .Where(o => o.Email.ToUpper().Equals(email.ToUpper()) && o.Password.ToUpper().Equals(password.ToUpper()))
              .FirstOrDefault();

            var mailCheck = _context
              .Usuarios
              .Where(o => o.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefault();

            var passCheck = _context
              .Usuarios
              .Where(o => o.Password.ToUpper().Equals(password.ToUpper())).FirstOrDefault();

            bool passEstaBien = passCheck != null;
            

            if (email == "jrr10@gmail.com.ar" && password == "romancito")
            {


                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nombre));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                
                bool mailExiste = mailCheck != null;
                
                if (mailExiste && passEstaBien)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                    identity.AddClaim(new Claim(ClaimTypes.Role, "USUARIO"));

                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                    identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Nombre));

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                    return RedirectToAction("Index", "Categorias");
                } else if (!passEstaBien && mailExiste){
                    return RedirectToAction("Index", "Home");
                }
                else if (!mailExiste)
                {
                    return RedirectToAction("Create", "Usuario");
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
