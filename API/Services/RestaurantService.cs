using FoodDelivery.Models.Domains;
using FoodDelivery.Models.DTOs;
using FoodDelivery.Models.DTOs.Create;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
	public class RestaurantService : IRestaurantService
	{
		private readonly DatabaseContext _context;
		public readonly IGeoLocationService _geolocationService;

		public RestaurantService(DatabaseContext context, IGeoLocationService geolocationService)
		{
			_context = context;
			_geolocationService = geolocationService;
		}

		public void CreateRestaurant(CreateRestaurantDTO model)
		{
			var GLCordinates = _geolocationService.GetCoordinatesForAddress(model.StreetNumber, model.Street,
					model.City, model.Country).Result;

			var newRestaurant = new Restaurant
			{
				Name = model.Name,
				Country = model.Country,
				City = model.City,
				Street = model.Street,
				StreetNumber = model.StreetNumber,
				Latitude = GLCordinates.latitude,
				Longitude = GLCordinates.longitude
			};

			_context.Restaurants.Add(newRestaurant);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var restaurantToDelete = _context.Restaurants.Find(id);
			_context.Restaurants.Remove(restaurantToDelete);
			_context.SaveChangesAsync();
		}

		public Restaurant GetRestaurant(int id)
		{
			if(id == 0)
			{
				throw new Exception("Error input can't be 0!");
			}
			
			return _context.Restaurants.Find(id);
		}

		public List<Restaurant> GetRestaurants()
		{
			return _context.Restaurants.ToList();
		}
	}
}
