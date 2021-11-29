using FoodOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.DTOs.Read
{
	public class ReadUserDTO
	{
		public string Name { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
