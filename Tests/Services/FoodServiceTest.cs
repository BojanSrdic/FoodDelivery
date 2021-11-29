using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using FoodOrder.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
	public class FoodServiceTest : IDisposable
	{
		private FoodService foodService;
		private readonly DatabaseContext context;

		private readonly Food Peanut = new Food { Id = 1, Name = "Kikiriki" };
		private readonly Food Avocado = new Food { Id = 2, Name = "Avokado" };
		private readonly Food Fish = new Food { Id = 3, Name = "Riba" };
		private readonly Food Chocolate = new Food { Id = 4, Name = "Cokolada" };

		private readonly List<Food> AllFood = new List<Food>();

		public FoodServiceTest()
		{
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			context = new DatabaseContext(options);

			context.Database.EnsureCreated();

			Seed(context);
		}

		[SetUp]
		public void Setup()
		{
			foodService = new FoodService(context);
			AllFood.Add(Peanut);
			AllFood.Add(Avocado);
			AllFood.Add(Fish);
			AllFood.Add(Chocolate);
		}

		[Test]
		public void Create_NewFood_ReturnSameFood()
		{
			//Arrange
			var createFoodDTO = new CreateFoodDTO { Name = "Med", Price = 15 };

			//Act
			foodService.CreateFood(createFoodDTO);
			var findFood = foodService.GetFood(5);
			AllFood.Add(findFood);

			//Assert
			Assert.AreEqual("Med", findFood.Name);
			Assert.AreEqual(15, findFood.Price);
			Assert.AreNotEqual("Kikiriki", findFood.Name);
		}

		[Test]
		public void Get_FoodById_CheckFoodProperties()
		{
			//Act
			var findFood = foodService.GetFood(1);

			//Assert
			Assert.AreEqual(Peanut.Name, findFood.Name);
			Assert.AreEqual(Peanut.Price, findFood.Price);
		}

		[Test]
		public void Get_FoodById_0_ThrowsException()
		{
			try
			{
				//Act
				var test = foodService.GetFood(0);
			}
			catch (Exception ex)
			{
				//Assert
				Assert.AreEqual("Error input can't be 0!", ex.Message);
			}
		}

		[Test]
		public void Get_ListOfFoods()
		{
			//Act
			var listOfFoods = foodService.GetFoods();

			//Assert
			Assert.That(listOfFoods, Has.All.Matches<Food>(f => IsInExpected(f, AllFood)));
		}

		[Test]
		public void Delete_FoodById_ReturnNull()
		{
			//Act
			foodService.Delete(3);
			var findFood = foodService.GetFood(3);
			var listOfFoods = foodService.GetFoods();

			//Assert
			Assert.IsNull(findFood);
			Assert.That(listOfFoods, Has.All.Matches<Food>(f => IsInExpected(f, AllFood)));
		}

		private void Seed(DatabaseContext context)
		{
			var foods = new[]
			{
				Peanut,
				Avocado,
				Fish,
				Chocolate
			};

			context.Foods.AddRange(foods);
			context.SaveChanges();
		}

		private static bool IsInExpected(Food item, IEnumerable<Food> expected)
		{
			var matchedItem = expected.FirstOrDefault(f =>
				f.Id == item.Id &&
				f.Name == item.Name &&
				f.Price == item.Price
			);

			return matchedItem != null;
		}

		public void Dispose()
		{
			context.Database.EnsureDeleted();
			context.Dispose();
		}
	}
}
