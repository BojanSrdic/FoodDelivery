using FoodOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.DataAccess.DTOs.Read
{
	public class ReadOrderDto
	{
		public User Customer { get; set; }
		public Food FoodItem { get; set; }
		public int Quontity { get; set; }
		public double OrderPrice { get; set; }
	}
}
