using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public int FoodId { get; set; }
		public int Quontity { get; set; }
		public double OrderPrice { get; set; }
	}
}
