using FoodDelivery.Models.Domains;
using FoodDelivery.Models.DTOs;
using FoodDelivery.Models.DTOs.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services.Interfaces
{
	public interface IRestaurantService
	{
		public List<Restaurant> GetRestaurants();
		public Restaurant GetRestaurant(int id);
		public void CreateRestaurant(CreateRestaurantDTO model);
		public void Delete(int id);
	}
}
