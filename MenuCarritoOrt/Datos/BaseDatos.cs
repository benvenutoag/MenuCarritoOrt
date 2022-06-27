using MenuCarritoOrt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Datos
{
    public class BaseDatos : DbContext
    {
        public BaseDatos(DbContextOptions opciones) : base(opciones)
        {

        }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Carrito> Carrito { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<CarritoItem> CarritoItem { get; set; }
    }
}
