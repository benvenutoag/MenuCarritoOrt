using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public class Carrito
    {
        [NotMapped]
        public List<Comida> Comidas { get; set; }
        [NotMapped]
        public List<Bebida> Bebidas { get; set; }
        [NotMapped]
        public List<Postre> Postres { get; set; }

        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }
    }
}
