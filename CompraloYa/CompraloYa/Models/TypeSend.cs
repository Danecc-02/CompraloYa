using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class TypeSend
    {
        [Key]
        public int IdSend { get; set; }

        [Display(Name ="Tipo de Envio")]
        public string TypeSendName { get; set; }


        public IEnumerable<Joyeria> Joyerias { get; set; }
        public IEnumerable<Tecnologia> Tecnologias { get; set; }
    }
}
