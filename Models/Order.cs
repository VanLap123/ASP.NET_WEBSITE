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
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string country { get; set; }
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string cus_first_name { get; set; }
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string cus_last_name { get; set; }
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string cus_address { get; set; }
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string cus_city { get; set; }
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string cus_phone { get; set; }
		[EmailAddress(ErrorMessage ="Please enter a valid email")]
		[StringLength(maximumLength:25,MinimumLength =1,ErrorMessage ="Length must be between 1 to 25")]
		public string cus_email { get; set; }

		[DataType(DataType.Date)]
		public DateTime OrderDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime DeliveryDate { get; set; }
	
	}
}