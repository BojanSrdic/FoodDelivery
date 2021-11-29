using FoodDelivery.Models.Domains;
using FoodOrder.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FoodOrder.Data
{
	public class InMemoryTestData
	{ 
		public static void AddTestDataInMemory(IApplicationBuilder app)
		{
			var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetService<DatabaseContext>();
			SeedData(context);			
		}

		private static void SeedData(DatabaseContext context)
		{
			var food = new[]
			{
				new Food {  Name = "Piletina sa kikirikijem", Price = 10 },
				new Food { Id = 2, Name = "Piletina sa grilovanim povrcem", Price = 10 },
				new Food { Id = 3, Name = "Teletina sa kikirikijem", Price = 15 },
				new Food { Id = 4, Name = "Teletina sa grilovanim povrcem", Price = 15 },
				new Food { Id = 5, Name = "Teleca corba", Price = 20 },
				new Food { Id = 6, Name = "Pileca supa", Price = 20 },
				new Food { Id = 7, Name = "Riblja corba", Price = 25 },
			};

			var customer = new[]
			{
				new User { Id = 1, Name = "Bojan" }
			};

			var order = new[]
			{
				new Order { Id = 1, Quontity = 5, UserId = 1, FoodId = 1},
				new Order { Id = 2, Quontity = 3, UserId = 1, FoodId = 1}
			};

			var restaurant = new[]
			{
				new Restaurant { Id = 1 },
				new Restaurant { Id = 2 }
			};

			context.Foods.AddRange(food);
			context.Users.AddRange(customer);
			context.Orders.AddRange(order);
			context.Restaurants.AddRange(restaurant);
			context.SaveChanges();
		}
	}
}
