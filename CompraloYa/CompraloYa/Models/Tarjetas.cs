using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class Tarjetas
    {
        [Key]
        public int VCCtarjeta { get; set; }
        public string NumeroTarjeta { get; set; }

        public string FechaExpiracion { get; set; }

        
    }
}
