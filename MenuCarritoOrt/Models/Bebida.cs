using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public class Bebida
    {
        public string Nombre { get; set; }

        public double Precio { get; set; }

        public string Descripcion { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
