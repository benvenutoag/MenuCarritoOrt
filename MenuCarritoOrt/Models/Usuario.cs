using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MenuCarritoOrt.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }


        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
