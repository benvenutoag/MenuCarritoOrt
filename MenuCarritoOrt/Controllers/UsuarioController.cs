using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuCarritoOrt.Datos;
using MenuCarritoOrt.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MenuCarritoOrt.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BaseDatos _context;

        public UsuarioController(BaseDatos context)
        {
            _context = context;
        }

        // GET: Usuario
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuario.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> NoAutorizado()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);

                if (User.IsInRole("USUARIO"))
                {
                    var carrito = new Carrito
                    {
                        IdUsuario = usuario.Id,
                        Productos = new List<Producto>(),
                        Usuario = usuario
                    };
                    _context.Carritos.Add(carrito);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,Password")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        // GET: Usuario/Ingresar/5

        [AllowAnonymous]
        public IActionResult Ingresar(string returnUrl)
        {
            TempData["UrlIngreso"] = returnUrl;
            return View();
        }


        // POST: Usuario/Ingresar/5
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Ingresar(string email, string password)
        {

            var usuario = _context
              .Usuarios
              .Where(o => o.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefault();

            if (usuario != null)
            {
                if (password.SequenceEqual(usuario.Password))
                {
                    if (email == "jrr10@gmail.com.ar" && password == "romancito")
                    {
                        ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                        identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                        identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Email));

                        identity.AddClaim(new Claim("IdUsuario", usuario.Id.ToString()));
                        identity.AddClaim(new Claim("NombreUsuario", usuario.Nombre.ToString()));


                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("IndexAdmin", "Home");
                    }
                    else
                    {
                        ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre));

                        identity.AddClaim(new Claim(ClaimTypes.Role, "USUARIO"));

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

                        identity.AddClaim(new Claim(ClaimTypes.GivenName, usuario.Email));

                        identity.AddClaim(new Claim("IdUsuario", usuario.Id.ToString()));
                        identity.AddClaim(new Claim("NombreUsuario", usuario.Nombre.ToString()));


                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("IndexIngreso", "Home");

                    }
                }
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

    }

}
