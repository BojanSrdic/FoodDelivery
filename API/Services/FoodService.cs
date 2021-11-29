using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrder.Services
{
	public class FoodService : IFoodService
	{
		private readonly DatabaseContext context;
		public FoodService(DatabaseContext _context)
		{
			context = _context;
		}

		public void CreateFood(CreateFoodDTO model)
		{
			var food = new Food
			{
				Name = model.Name,
				Price = model.Price
			};

			context.Foods.Add(food);
			context.SaveChanges();
		}

		public Food GetFood(int id)
		{
			if (id == 0)
			{
				throw new Exception("Error input can't be 0!");
			}

			return context.Foods.Find(id);
		}

		public List<Food> GetFoods()
		{
			return context.Foods.ToList();
		}

		public void Delete(int id)
		{
			var foodToDelete = context.Foods.Find(id);
			context.Foods.Remove(foodToDelete);
			context.SaveChanges();
		}
	}
}
