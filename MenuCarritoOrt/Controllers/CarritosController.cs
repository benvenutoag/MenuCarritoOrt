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

namespace MenuCarritoOrt.Controllers
{
    public class CarritosController : Controller
    {
        private readonly BaseDatos _context;

        public CarritosController(BaseDatos context)
        {
            _context = context;
        }


        // GET: Carritos
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var carritoCompras = _context.Carritos.Include(u => u.Usuario);
            return View(await carritoCompras.ToListAsync());
        }

        // GET: Carritos/CarritoUsuario
        [Authorize(Roles = "USUARIO")]
        public async Task<IActionResult> CarritoUsuario(int idUsuario)
        {
            var carrito = _context.Carritos
                .FirstOrDefaultAsync(n => n.IdUsuario == idUsuario);

            return View(await carrito);
        }


        // GET: Carritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Email");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdProducto,IdUsuario")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                carrito.Productos = new List<Producto>();
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Email", carrito.IdUsuario);
            return View(carrito);
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            return View(carrito);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdProducto,IdUsuario")] Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.Id))
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
            return View(carrito);
        }

        // GET: Carritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.Id == id);
        }

        //public void AgregarAlCarrito(int id, Producto producto)
        //{

        //    var carrito = _context.Carritos.FirstOrDefault(m => m.Id == id);

        //    if (producto != null)
        //    {
        //        carrito.Productos.Add(producto);
        //    }
        //    _context.SaveChanges();

        //}

        // POST: Carritos/Agregar/5
        //public async Task<ActionResult> AgregarCarrito(int IdProducto)
        //{

        //    var idUsuario = int.Parse(User.FindFirst("IdUsuario").Value);

        //    var carrito = await _context.Carritos.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
        //    var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == IdProducto);
        //    List<Producto> ProductosCarrito = new List<Producto>();
        //    var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == idUsuario);


        //    if (usuario.Carrito == null)
        //    {
        //        usuario.Carrito = new Carrito
        //        {
        //            Productos = ProductosCarrito,
        //            Usuario = usuario,
        //            IdUsuario = idUsuario
        //        };
        //    }

        //    if (usuario.Carrito.Productos == null)
        //    {
        //        usuario.Carrito.Productos = ProductosCarrito;
        //    }

        //    var listaFinal = usuario.Carrito.Productos;

        //    if (producto != null)
        //    {
        //        listaFinal.Add(producto);
        //    }

        //    await _context.SaveChangesAsync();

        //    //return RedirectToAction(nameof(Carrito), new { id = carrito.IdUsuario });
        //    return View("CarritoUsuario");

        //}



        // POST : Carritos/AgregarCarrito/5

        public async Task<IActionResult> AgregarCarrito(Producto producto)
        {
            var idUsuario = int.Parse(User.FindFirst("IdUsuario").Value);
            var carrito = await _context.Carritos.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Id == idUsuario);

            if (carrito == null)
            {
                carrito = new Carrito();
                carrito.IdUsuario = idUsuario;
                carrito.Usuario = usuario;
            }

            if (usuario.Carrito == null)
            {
                usuario.Carrito = carrito;
            }

            if (carrito.Productos == null)
            {
                carrito.Productos = new List<Producto>();
            }

            carrito.Productos.Add(producto);


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(CarritoUsuario), new { id = carrito.IdUsuario });
        }

        public async Task<ActionResult> Vaciar(int id)
        {
            var carrito = await _context.Carritos.FirstOrDefaultAsync(m => m.Id == id);
            if (carrito != null && carrito.Productos != null)
            {
                carrito.Productos.Clear();
                return RedirectToAction("Index", "Carrito");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}