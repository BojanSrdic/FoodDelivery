using FoodDelivery.Models.Domains;
using FoodDelivery.Services;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using FoodOrder.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class GeoLocationServiceTest
	{
		private readonly DatabaseContext context;

		private readonly Restaurant DobrokNoviSad = new Restaurant { Id = 1, Name = "DobrokSerbia", Latitude =  2, Longitude = 4};
		private readonly User user_Dusan = new User { Id = 1, Name = "Nikola", Country = "Serbia", City = "Novi Sad", Street = "Cara Dusana", StreetNumber = "26", Latitude = 1, Longitude = 3 };


		public GeoLocationServiceTest()
		{
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			context = new DatabaseContext(options);

			context.Database.EnsureCreated();

			Seed(context);
		}

		[Test]
		public void GeoLocationCordinates()
		{
			//Arrange
			var geolocationService = new GeoLocationService(context);

			//Act
			var test = geolocationService.GetCoordinatesForAddress("Serbia", "Novi Sad", "Cara Dusana", "26").Result;

			//Assert
			Assert.AreEqual(45.24398, Math.Round(test.latitude), 5);
			Assert.AreEqual(19.82516, Math.Round(test.longitude), 5);
		}

		[Test]
		public void GeoLocationDistance()
		{
			//Arrange
			var geolocationService = new GeoLocationService(context);

			//Act
			var distance = geolocationService.Distance(1, 1);

			//Assert
			Assert.AreEqual(Math.Sqrt(2), distance);
		}

		private void Seed(DatabaseContext context)
		{
			var restaurants = new[]
			{
				DobrokNoviSad
			};

			var users = new[]
			{
				user_Dusan
			};

			context.Restaurants.AddRange(restaurants);
			context.Users.AddRange(users);
			context.SaveChanges();
		}

	}
}
