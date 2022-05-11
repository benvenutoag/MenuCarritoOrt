using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public class Carrito
    {
        public List<Producto> Productos { get; set; }

        public double FinalizarCompraPrecio()
        {
            double precioFinal = 0;
            foreach (var item in Productos)
            {
                precioFinal += item.Precio;
            }

            return precioFinal;
        }
    }
}
