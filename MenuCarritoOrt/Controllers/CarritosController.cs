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
            var carritoCompras = _context.Carritos;
            return View(await carritoCompras.ToListAsync());
        }

        // GET: Carritos/CarritoUsuario
        [Authorize(Roles = "USUARIO")]
        public async Task<IActionResult> CarritoUsuario(int id)
        {
            int idusuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var carrito = _context.Carritos
                .FirstOrDefaultAsync(n => n.IdUsuario == idusuario);

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

        //public async Task<ActionResult> AgregarAlCarrito(int id, Producto producto)
        //{

            // var carrito = _context.Carritos.FirstOrDefault(m => m.Id == id);

            //// if (producto != null)
            // {
            //    carrito.Productos.Add(producto);
            // }
            //_context.SaveChanges();

          //  return (View("Productos"));
        

    //}
            

    

        // POST: Carritos/AgregarCarrito/5
        
        public async Task<ActionResult> AgregarCarrito(int IdProducto)
        {
            var idUsuario = int.Parse(User.FindFirst("IdUsuario").Value);

            var carrito = await _context.Carritos.FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.IdProducto == IdProducto);
            List<Producto> ProductosCarrito = new List<Producto>();

            if (producto != null)
            {
                ProductosCarrito.Add(producto);
            }

            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Carrito), new { id = carrito.IdUsuario });
            return View("CarritoUsuario");

        }

        //public void RemoverProducto(int id, int idProducto)
        //{
        //    var carrito = _context.Carritos.FirstOrDefault(m => m.Id == id);
        //    var producto = _context.Productos.FirstOrDefault(m => m.IdProducto == idProducto);
        //    if (producto != null)
        //    {
        //        carrito.Productos.Remove(producto);
        //    }
        //    _context.SaveChanges();
        //}

        //public async Task<ActionResult> VaciarCarrito(int id)
        //{
        //    var carrito = _context.Carritos.FirstOrDefault(m => m.Id == id);
        //    if (carrito != null)
        //    {
        //        carrito.Productos.Clear();
        //        return RedirectToAction("Index", "Carrito")
        //    }
        //    else
        //    {
        //        throw new SystemException("No se pudo vaciar el carrito");
        //    }
        //}

        //// POST: Carritos/Cerrar/5
        //[HttpPost, ActionName("Cerrar")]
        //[ValidateAntiForgeryToken]
        //public double Cerrar(int id)
        //{
        //    Carrito carr = _context.Carritos.FirstOrDefault(m => m.Id == id);
        //    var productos = carr.Productos;
        //    double total = 0;
        //    foreach(Producto producto in productos){
        //        total += producto.Precio;
        //    }

        //    return total;

        //}

    }
}