using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBGROUP_GCC0903.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cus_id { get; set; }
        [Required(ErrorMessage ="Invalid Name!")]
        // [StringLength(50)]
        //[Range(0,10)]//from 0->10
        // [DataType(DataType)]
        public string cus_name { get; set; }
        [Required]
        public DateTime cus_birthday { get; set; }
        [Required]
        public string cus_gender { get; set; }
        [Required]
        public string cus_address { get; set; }
    }
}