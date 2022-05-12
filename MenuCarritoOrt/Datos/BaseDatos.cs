﻿using MenuCarritoOrt.Models;
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

        public DbSet<Postre> Postres { get; set; }

        public DbSet<Bebida> Bebidas { get; set; }

        public DbSet<Comida> Comidas { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

    }
}
