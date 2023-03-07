using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBGROUP_GCC0903.Models
{
    public class CartItem
{
    [Range(1, 200000, ErrorMessage = "The Sale_Price will a positive number between 1 to 200000")]
    public int quantity {set; get;}
    
    public Product product {set; get;}
}
}