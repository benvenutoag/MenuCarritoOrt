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
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Postre> Postres { get; set; }// se va

        public DbSet<Bebida> Bebidas { get; set; } // se va

        public DbSet<Comida> Comidas { get; set; } // se va

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Carrito> Carritos { get; set; }

        public DbSet<Producto> Productos { get; set; }
    }
}
