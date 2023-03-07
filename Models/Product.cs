using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBGROUP_GCC0903.Models
{
    public class Product
    {
        [Key]
        public int pro_id { get; set; }
        [Required]
        [StringLength(maximumLength:25,MinimumLength =3,ErrorMessage ="Length must be between 3 to 25")]
        public string pro_name { get; set; }
        [Range(1, 200000, ErrorMessage = "The Quantity will a positive number between 1 to 200000")]
        public int pro_quantity { get; set; }
        [Range(1, 200000, ErrorMessage = "The Import Price will a positive number between 1 to 200000 ")]
        public float import_price { get; set; }
        [Range(1, 200000, ErrorMessage = "The Sale_Price will a positive number between 1 to 200000")]
        public float sale_price { get; set; }

        public string? pro_image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [StringLength(maximumLength:50,MinimumLength =1,ErrorMessage ="Length must be between 1 to 50")]
        public string description { get; set; }

        public int cat_id {get;set;}
        [ForeignKey("cat_id")]
        public virtual Category? category { get; set; }
    }
}   