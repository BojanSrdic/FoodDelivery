using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.DTOs.Create
{
	public class CreateRestaurantDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Country { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string Street { get; set; }

		[Required]
		public string StreetNumber { get; set; }
	}
}
