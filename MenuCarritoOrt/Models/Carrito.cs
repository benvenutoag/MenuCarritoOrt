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
        //[NotMapped]
        //public List<Comida> Comidas { get; set; }
        //[NotMapped]
        //public List<Bebida> Bebidas { get; set; }
        //[NotMapped]
        //public List<Postre> Postres { get; set; }
        [NotMapped]
        public List<Producto> Productos { get; set; }

        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }

        //public double DevolverTotal()
        //{
        //    double total = 0;
        //    if (Comidas.Count > 0 && Bebidas.Count > 0 && Postres.Count > 0)
        //    {
        //        foreach (var item in Comidas)
        //        {
        //            total += item.Precio;
        //        }
        //        foreach (var item in Bebidas)
        //        {
        //            total += item.Precio;
        //        }
        //        foreach (var item in Postres)
        //        {
        //            total += item.Precio;
        //        }
        //    }

        //    return total;
        //}


    }
}
