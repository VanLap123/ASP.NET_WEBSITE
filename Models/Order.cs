using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBGROUP_GCC0903.Models
{
   public class Order
	{
		[Key]
		public int order_id { get; set; }
		public string cus_name { get; set; }
		public string deliveryLocal { get; set; }
		public string cus_phone { get; set; }
		[DataType(DataType.Date)]
		public DateTime OrderDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime DeliveryDate { get; set; }
		public virtual ICollection<Customer>? Customers { get; set; }
	
	}
}