using FoodDelivery.Models.DTOs;
using FoodDelivery.Services;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
	public class UserServiceTest : IDisposable
	{
		private readonly DatabaseContext context;
		private UserService userService;
		private IGeoLocationService geolocationService;

		private readonly User user_Nikola = new User { Id = 1, Name = "Nikola", Country = "Serbia", City = "Zrenjanin", Street = "Zabaljska", StreetNumber = "38" };
		private readonly User user_Toni = new User { Id = 2, Name = "Toni", Country = "Croatia", City = "Makarska", Street = "Biokovska", StreetNumber = "3" };

		private readonly List<User> AllUsers = new List<User>();

		public UserServiceTest()
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
			geolocationService = new GeoLocationService(context);
			userService = new UserService(context, geolocationService);
			AllUsers.Add(user_Nikola);
			AllUsers.Add(user_Toni);
		}

		[Test]
		public void Create_NewUser_ReturnSameFood()
		{
			//Arrange
			var createUserDto = new CreateUserDTO { Name = "Dusan", Country = "Serbia", City = "Novi Sad", Street = "Cara Dusana", StreetNumber = "26" };

			//Act
			userService.CreateUser(createUserDto);
			var test = userService.GetUser(3);
			AllUsers.Add(test);

			//Assert
			Assert.AreEqual("Dusan", test.Name);
			Assert.AreEqual("Serbia", test.Country);
			Assert.AreEqual("Novi Sad", test.City);
			Assert.AreEqual("Cara Dusana", test.Street);
			Assert.AreEqual("26", test.StreetNumber);
			//Assert.AreEqual("26", test.Latitude);
			//Assert.AreEqual("26", test.Longitude);
		}

		[Test]
		public void Get_UserById_CheckUserProperties()
		{
			//Act
			var test = userService.GetUser(1);

			//Assert
			Assert.AreEqual(user_Nikola.Name, test.Name);
			Assert.AreEqual(user_Nikola.Country, test.Country);
			Assert.AreEqual(user_Nikola.City, test.City);
			Assert.AreEqual(user_Nikola.Street, test.Street);
			Assert.AreEqual(user_Nikola.StreetNumber, test.StreetNumber);
			Assert.AreEqual(user_Nikola.Latitude, test.Latitude);
			Assert.AreEqual(user_Nikola.Longitude, test.Longitude);
		}

		[Test]
		public void Get_FoodById_0_ThrowsException()
		{
			try
			{
				//Act
				var test = userService.GetUser(0);
			}
			catch (Exception ex)
			{
				//Assert
				Assert.AreEqual("Error input can't be 0!", ex.Message);
			}
		}

		[Test]
		public void Get_ListOfUser()
		{
			//Act
			var listOfUsers = userService.GetUsers();

			//Assert
			Assert.That(listOfUsers, Has.All.Matches<User>(f => IsInExpected(f, AllUsers)));
		}

		[Test]
		public void Delete_UserById_ReturnNull()
		{
			int id = 2;

			//Act
			userService.Delete(id);
			var findFood = userService.GetUser(id);
			var listOfFoods = userService.GetUsers();

			//Assert
			Assert.IsNull(findFood);
			Assert.That(listOfFoods, Has.All.Matches<User>(f => IsInExpected(f, AllUsers)));
		}

		private void Seed(DatabaseContext context)
		{
			var users = new[]
			{
				user_Nikola,
				user_Toni
			};

			context.Users.AddRange(users);
			context.SaveChanges();
		}

		private static bool IsInExpected(User item, IEnumerable<User> expected)
		{
			var matchedItem = expected.FirstOrDefault(f =>
				f.Id == item.Id &&
				f.Name == item.Name &&
				f.Country == item.Country &&
				f.City == item.City &&
				f.Street == item.Street &&
				f.StreetNumber ==item.StreetNumber
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
