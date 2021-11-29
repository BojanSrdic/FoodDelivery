using FoodDelivery.Models.Domains;
using FoodDelivery.Models.DTOs;
using FoodDelivery.Models.DTOs.Create;
using FoodDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RestaurantController : ControllerBase
	{
		private readonly IRestaurantService _restaurantService;
		public readonly IGeoLocationService _geolocationService;

		public RestaurantController(IRestaurantService restaurantService, IGeoLocationService geolocationService)
		{
			_restaurantService = restaurantService;
			_geolocationService = geolocationService;
		}

		// POST: api/Restaurant
		[HttpPost]
		public IActionResult Create(CreateRestaurantDTO model)
		{
			_restaurantService.CreateRestaurant(model);

			return Ok("Food created successfully");
		}

		// GET: api/Restaurant
		[HttpGet]
		public IActionResult GetRestaurantList()
		{
			return Ok(_restaurantService.GetRestaurants());
		}

		// GET: api/Restaurant
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var food = _restaurantService.GetRestaurant(id);

			if (food == null)
			{
				return NotFound("Food doesn't exist");
			}

			//var dto = new ReadFoodDTO() { Name = food.Name, Price = food.Price };

			return Ok(food);
		}

		// DELETE: api/Food/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var restaurantToDelete = _restaurantService.GetRestaurant(id);

			if (restaurantToDelete == null)
			{
				return NotFound("Food doesn't exist");
			}

			_restaurantService.Delete(restaurantToDelete.Id);

			return Ok(new { message = "Food deleted successfully" });
		}
	}
}
