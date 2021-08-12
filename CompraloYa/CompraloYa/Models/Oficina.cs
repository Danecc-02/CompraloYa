using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class Oficina
    {
        [Key]

        public int IdOficina { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Codigo Articulo")]
        public string CodigoOficina { get; set; }

        [Display(Name = "Nombre Producto")]
        public string Nombre { get; set; }

        [Display(Name = "Detalles")]
        public string DetalleOficina { get; set; }

        [Display(Name = "Existencias")]
        public int Stock { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        public decimal PrecioOficina { get; set; }
        public int IdSend { get; set; }

        [ForeignKey("IdSend")]

        public TypeSend TypeSend { get; set; }
    }
}
