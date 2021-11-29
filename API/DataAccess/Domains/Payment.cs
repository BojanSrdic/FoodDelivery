using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Models
{
	public class Payment
	{
		public int Id { get; set; }
		public double Total { get; set; }
		public User CustomerId { get; set; }
		public List<Order> Orders { get; set; }
	}
}
