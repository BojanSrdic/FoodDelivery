using FoodDelivery.Models.Domains;
using FoodDelivery.Models.DTOs.Create;
using FoodDelivery.Services;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class RestaurantServiceTest : IDisposable
	{
		private readonly DatabaseContext context;
		public RestaurantService service;
		public IGeoLocationService geoLocationService;

		private readonly Restaurant restaurant_1 = new Restaurant { Id = 1, Name = "DobrokSerbia", Country = "Serbia", City = "Zrenjanin", Street = "Zabaljska", StreetNumber = "38" };
		private readonly Restaurant restaurant_2 = new Restaurant { Id = 2, Name = "DobrokCroatia", Country = "Croatia", City = "Makarska", Street = "Biokovska", StreetNumber = "3" };

		private readonly List<Restaurant> AllRestaurants = new List<Restaurant>();

		public RestaurantServiceTest()
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
			geoLocationService = new GeoLocationService(context);
			service = new RestaurantService(context, geoLocationService);
			AllRestaurants.Add(restaurant_1);
			AllRestaurants.Add(restaurant_2);
		}

		[Test]
		public void Create_NewRestaurant_ReturnSameFood()
		{
			//Arrange
			var createRestaurantDto = new CreateRestaurantDTO { Name = "Dusan", Country = "Serbia", City = "Novi Sad", Street = "Cara Dusana", StreetNumber = "26" };
			
			//Act
			service.CreateRestaurant(createRestaurantDto);
			var addTestData = service.GetRestaurant(3);
			AllRestaurants.Add(addTestData);

			//Assert
			Assert.AreEqual("Dusan", addTestData.Name);
			Assert.AreEqual("Serbia", addTestData.Country);
			Assert.AreEqual("Novi Sad", addTestData.City);
			Assert.AreEqual("Cara Dusana", addTestData.Street);
			Assert.AreEqual("26", addTestData.StreetNumber);
			Assert.AreEqual(45.24398, Math.Round(addTestData.Latitude), 5);
			Assert.AreEqual(19.82516, Math.Round(addTestData.Longitude), 5);
		}

		[Test]
		public void Get_RestaurantById_CheckRestaurantProperties()
		{
			//Act
			var test = service.GetRestaurant(1);

			//Assert
			Assert.AreEqual(restaurant_1.Name, test.Name);
			Assert.AreEqual(restaurant_1.Country, test.Country);
			Assert.AreEqual(restaurant_1.City, test.City);
			Assert.AreEqual(restaurant_1.Street, test.Street);
			Assert.AreEqual(restaurant_1.StreetNumber, test.StreetNumber);
			Assert.AreEqual(restaurant_1.Latitude, test.Latitude);
			Assert.AreEqual(restaurant_1.Longitude, test.Longitude);
		}

		[Test]
		public void Get_RestaurantById_0_ThrowsException()
		{
			try
			{
				//Act
				var test = service.GetRestaurant(0);
			}
			catch (Exception ex)
			{
				//Assert
				Assert.AreEqual("Error input can't be 0!", ex.Message);
			}
		}

		[Test]
		public void Get_ListOfRestaurants()
		{
			//Act
			var listOfRestaurants = service.GetRestaurants();

			//Assert
			Assert.AreEqual(2, listOfRestaurants.Count);
			Assert.That(listOfRestaurants, Has.All.Matches<Restaurant>(f => IsInExpected(f, AllRestaurants)));
		}

		[Test]
		public void Delete_RestaurantById_ReturnNull()
		{
			int id = 2;

			//Act
			service.Delete(id);
			var findFood = service.GetRestaurant(id);
			var listOfFoods = service.GetRestaurants();

			//Assert
			Assert.IsNull(findFood);
			Assert.That(listOfFoods, Has.All.Matches<Restaurant>(f => IsInExpected(f, AllRestaurants)));
		}

		private void Seed(DatabaseContext context)
		{
			var restaurants = new[]
			{
				restaurant_1,
				restaurant_2
			};

			context.Restaurants.AddRange(restaurants);
			context.SaveChanges();
		}

		private static bool IsInExpected(Restaurant item, IEnumerable<Restaurant> expected)
		{
			var matchedItem = expected.FirstOrDefault(f =>
				f.Id == item.Id &&
				f.Name == item.Name &&
				f.Country == item.Country &&
				f.City == item.City &&
				f.Street == item.Street &&
				f.StreetNumber == item.StreetNumber
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
