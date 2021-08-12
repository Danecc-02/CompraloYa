using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class Joyeria
    {
        [Key]
        public int IdJoyeria { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string CodigoJoya { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreJoya { get; set; }
        public string DetalleJoya { get; set; }
        public  int Stock { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        public decimal PrecioJoya { get; set; }

        public int IdSend { get; set; }

        [ForeignKey("IdSend")]

        public TypeSend TypeSend { get; set; }
    }
}
