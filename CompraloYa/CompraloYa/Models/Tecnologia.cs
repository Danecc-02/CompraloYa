using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CompraloYa.Models
{
    public class Tecnologia
    {
        [Key]
        public int IdTecno { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Código Artículo")]
        public string CodigoTecno { get; set; }

        public int IdTypeTecno { get; set; }

        [ForeignKey("IdTypeTecno")]

        [Display(Name = "Tipo de Tecnología")]
        public TypeTecnologia TypeTecnologia { get; set; }


        [Display(Name = "Detalle")]
        public string DetalleJoya { get; set; }

        [Display(Name = "Existencias")]
        public int Stock { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        public decimal PrecioJoya { get; set; }

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
