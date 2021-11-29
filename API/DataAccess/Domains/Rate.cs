using FoodOrder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Domains
{
	public class Rate
	{
		[Key]
		public int Id { get; set; }
		public Food Food { get; set; }
		public User User { get; set; }
		public double Mark { get; set; }
	}
}
