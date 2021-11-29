using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Interfaces
{
	public interface IGeoLocationService
	{
		public Task<Coordinates> GetCoordinatesForAddress(string streetNumber, string street, string city, string country);
	}
}
