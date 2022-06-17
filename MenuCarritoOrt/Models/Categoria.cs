﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre de Categoría")]
        public string Descripcion { get; set; }

    }
}
