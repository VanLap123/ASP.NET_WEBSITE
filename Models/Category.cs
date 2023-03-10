using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBGROUP_GCC0903.Models
{
    public class Category
    {
        [Key]
        public int cat_id { get; set; }
        [StringLength(maximumLength:40,MinimumLength =3,ErrorMessage ="Length must be between 3 to 40")]
        public string cat_name { get; set; }
        public virtual ICollection<Product>? product { get; set; }
    }
}