using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WEBGROUP_GCC0903.Models
{
    	public class OrderDetail
	{
		[Key]
		[Column(Order = 1)]
		public int order_id { get; set; }
		[Key]
		[Column(Order = 2)]
		[Range(1, 200000, ErrorMessage = "The Quantity will a positive number between 1 to 200000")]
		public int quantity { get; set; }
		public int pro_id { get; set; }
		[ForeignKey("order_id")]
		public virtual Order? Order { get; set; }
		[ForeignKey("pro_id")]
		public virtual Product? Product { get; set;}

	}
}