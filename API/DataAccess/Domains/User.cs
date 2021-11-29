using FoodDelivery.Models.Domains;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Models
{
	public class User : GeoLocation
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
