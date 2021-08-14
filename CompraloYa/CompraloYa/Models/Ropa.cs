using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace CompraloYa.Models
{
    public class Ropa
    {
        [Key]

        public int IdTypeRopa { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Código Artículo")]
        public string CodigoRopa { get; set; }
        public string Detalle { get; set; }

        [Display(Name = "Existencias")]
        public int Stock { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        public decimal PrecioRopa { get; set; }
        public int IdRopa { get; set; }

        [ForeignKey("IdRopa")]
        [Display(Name = "Tipo de Ropa")]
        public TypeRopa TypeRopa { get; set; }

        public int IdSend { get; set; }

        [ForeignKey("IdSend")]

        [Display(Name = "Tipo de Envío")]
        public TypeSend TypeSend { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [Display(Name = "Imagen del articulo")]

        public IFormFile Img { get; set; }

    }
}
