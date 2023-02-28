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
        public string pro_name { get; set; }
        public int pro_quantity { get; set; }
        public float import_price { get; set; }
        public float sale_price { get; set; }
        public string? pro_image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string description { get; set; }

        public int cat_id {get;set;}
        [ForeignKey("cat_id")]
        public virtual Category? category { get; set; }
    }
}   