using Microsoft.AspNetCore.Http;
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

        [Display(Name = "Código Artículo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string CodigoJoya { get; set; }

        [Display(Name = "Nombre Artículo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreJoya { get; set; }

        [Display(Name = "Detalle")]
        public string DetalleJoya { get; set; }

        [Display(Name = "Existencia")]
        public  int Stock { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Precio")]
        public decimal PrecioJoya { get; set; }

        public int IdSend { get; set; }

        [ForeignKey("IdSend")]
        [Display(Name = "Tipo de Envío")]
        public TypeSend TypeSend { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [NotMapped]
        [Display(Name = "Imagen del articulo")]

        public IFormFile Img { get; set; }
    }
}
