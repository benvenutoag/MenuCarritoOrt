using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public abstract class Producto
    {
        public string Nombre { get; set; }

        public double Precio { get; set; }

        public string Descripcion { get; set; }

        public int Id { get; set; }
    }
}
