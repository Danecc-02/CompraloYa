using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class TypeTecnologia
    {
        [Key]

        [Display(Name = "Tipo Tecnologia")]
        public int IdTypeTecno { get; set; }

        [Display(Name = "Tipo Tecnologia")]
        public string TypeTecnoName { get; set; }

        public IEnumerable<Tecnologia> Tecnologias { get; set; }
    }
}

