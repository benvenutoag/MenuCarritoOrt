using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public class Producto
    {
        public Guid ProductoId { get; set; }


        public string Nombre { get; set; }

        public string Descripcion { get; set; }


        public float PrecioVigente { get; set; }


        public string Foto { get; set; } = "default.png";

    }
}
