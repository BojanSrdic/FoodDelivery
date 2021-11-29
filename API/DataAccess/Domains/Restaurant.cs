using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Domains
{
	public class Restaurant : GeoLocation
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
