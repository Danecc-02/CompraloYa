using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class TypeRopa
    {
        [Key]

        [Display(Name = "Tipo de Ropa")]
        public int IdTypeRopa { get; set; }

        [Display(Name = "Clase de Ropa")]
        public string TypeRopaName { get; set; }

        public IEnumerable<Ropa> Ropas { get; set; }
    }
}
