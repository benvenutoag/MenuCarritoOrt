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

        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

        public int Cantidad { get; set; }

        public virtual Producto Producto { get; set; }




        


    }
}
