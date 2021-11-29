using FoodDelivery.Models.DTOs.Read;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using FoodOrder.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FoodOrder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoodController : ControllerBase
	{
		private readonly IFoodService _foodService;

		public FoodController(IFoodService foodService)
		{
			_foodService = foodService;
		}

		// POST: api/Food
		[HttpPost]
		public IActionResult Create(CreateFoodDTO model)
		{
			_foodService.CreateFood(model);

			return Ok("Food created successfully");
		}

		// GET: api/Food
		[HttpGet]
		public IActionResult GetList()
		{
			return Ok(_foodService.GetFoods());
		}

		// GET: api/Food
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var food = _foodService.GetFood(id);

			if (food == null)
			{
				return NotFound("Food doesn't exist");
			}

			var dto = new ReadFoodDTO() { Name = food.Name, Price = food.Price };

			return Ok(dto);
		}

		//[HttpPut("{id:length(24)}")]
		//public IActionResult Update(string id, Book bookIn)
		//{
		//	var book = _bookService.Get(id);

		//	if (book == null)
		//	{
		//		return NotFound();
		//	}

		//	_bookService.Update(id, bookIn);

		//	return NoContent();
		//}


		// DELETE: api/Food/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var foodToDelete = _foodService.GetFood(id);

			if (foodToDelete == null)
			{
				return NotFound("Food doesn't exist");
			}

			_foodService.Delete(foodToDelete.Id);

			return Ok(new { message = "Food deleted successfully" });
		}
	}
}
