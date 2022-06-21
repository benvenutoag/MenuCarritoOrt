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
        public List<Producto> Productos { get; set; }

        [NotMapped]
        public List<Producto> ProductosCarrito { get; set; }

        [NotMapped]
        public Usuario Usuario { get; set; }

        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

    }
}
