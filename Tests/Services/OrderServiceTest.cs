using FoodDelivery.DataAccess.DTOs.Create;
using FoodDelivery.Services;
using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using FoodOrder.Services;
using FoodOrder.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class OrderServiceTest : IDisposable
	{
		private OrderService orderService;
		private FoodService foodService;
		private UserService userService;
		private readonly GeoLocationService geolocationService;
		private readonly DatabaseContext context;

		private readonly Food Avocado = new Food { Id = 1, Name = "Avokado", Price = 10};
		private readonly Food Chocolate = new Food { Id = 2, Name = "Cokolada", Price = 10 };

		private readonly User user_Nikola = new User { Id = 1, Name = "Nikola", Country = "Serbia", City = "Zrenjanin", Street = "Zabaljska", StreetNumber = "38" };
		private readonly User user_Toni = new User { Id = 2, Name = "Toni", Country = "Croatia", City = "Makarska", Street = "Biokovska", StreetNumber = "3" };

		private readonly Order FirstOrder = new Order { Id = 1, UserId = 1, FoodId = 1, Quontity = 3, OrderPrice = 30 };
		private readonly Order SecondOrder = new Order { Id = 2, UserId = 2, FoodId = 2, Quontity = 2, OrderPrice = 20 };

		private readonly List<Food> AllFoods = new List<Food>();
		private readonly List<User> AllUsers = new List<User>();
		private readonly List<Order> AllOrders = new List<Order>();

		public OrderServiceTest()
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
			orderService = new OrderService(context);
			foodService = new FoodService(context);
			userService = new UserService(context, geolocationService);
			AllOrders.Add(FirstOrder);
			AllOrders.Add(SecondOrder);

			AllFoods.Add(Chocolate);
			AllFoods.Add(Avocado);

			AllUsers.Add(user_Nikola);
			AllUsers.Add(user_Toni);

		}

		[Test]
		public void Create_NewOrer_ReturnSameOrder()
		{
			int foodId = 1;
			int userId = 1;
			int quontity = 11;

			//Arrange
			var createOrder = new CreateOrderDto { FoodId = foodId, UserId = userId, Quontity = quontity };

			//Act
			orderService.CreateOrder(createOrder);
			var find = orderService.GetOrder(3);
			AllOrders.Add(find);

			var user = userService.GetUser(userId);
			var food = foodService.GetFood(foodId);

			//Assert
			Assert.AreEqual(quontity*food.Price, find.OrderPrice);
			Assert.AreEqual(food.Id, find.FoodId);
			Assert.AreEqual(user.Id, find.UserId);
			Assert.AreEqual("Nikola", user.Name);
			Assert.AreEqual("Avokado", food.Name);
		}

		[Test]
		public void Get_OrderPrice()
		{
			//Act
			var createOrderDto = new CreateOrderDto { UserId = 1, FoodId = 1, Quontity = 7 };
			var orderPrice = orderService.GetOrderPrice(createOrderDto);

			//Assert
			Assert.AreEqual(70, orderPrice);
		}

		[Test]
		public void Get_OrderdById_CheckOrderProperties()
		{
			//Act
			var find = orderService.GetOrder(1);

			//Assert
			Assert.AreEqual(FirstOrder.FoodId, find.FoodId);
			Assert.AreEqual(FirstOrder.UserId, find.UserId);
			Assert.AreEqual(FirstOrder.Quontity, find.Quontity);
			Assert.AreEqual(FirstOrder.OrderPrice, find.OrderPrice);
		}

		[Test]
		public void Get_ListOfOrderss()
		{
			//Act
			var listOfOrders = orderService.GetOrders();

			//Assert
			Assert.That(listOfOrders, Has.All.Matches<Order>(f => IsInExpected(f, AllOrders)));
		}

		private void Seed(DatabaseContext context)
		{
			var orders = new[]
			{
				FirstOrder,
				SecondOrder,
			};
			var foods = new[]
			{
				Avocado,
				Chocolate
			};
			var users = new[]
			{
				user_Nikola,
				user_Toni
			};

			context.Orders.AddRange(orders);
			context.Users.AddRange(users);
			context.Foods.AddRange(foods);
			context.SaveChanges();
		}

		private static bool IsInExpected(Order item, IEnumerable<Order> expected)
		{
			var matchedItem = expected.FirstOrDefault(f =>
				f.Id == item.Id &&
				f.FoodId == item.FoodId &&
				f.UserId == item.UserId &&
				f.Quontity == item.Quontity
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
