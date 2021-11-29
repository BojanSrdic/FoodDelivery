using FoodDelivery.Models.DTOs;
using FoodOrder;
using FoodOrder.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Controllers
{
	public class CreateMethodTest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;

		public CreateMethodTest(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Theory]
		[InlineData("/api/Food")]
		public async void POST_CreateFood_StatusCodeOK(string url)
		{
			//Arrange
			var client = _factory.CreateClient();

			//Act
			var createNewFood = new CreateFoodDTO { Name = "Kikiriki", Price = 40 };
			var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(createNewFood), Encoding.UTF8, "application/json"));
			
			var content = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();

			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("Food created successfully", content);
			Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}


		[Theory]
		[InlineData("/api/User")]
		public async void POST_CreateUser_StatusCodeOK(string url)
		{
			//Arrange
			var client = _factory.CreateClient();

			//Act
			var createNewuser = new CreateUserDTO { Name = "Nikola", Country = "Serbia", Street = "Sekspirova", City = "Novi Sad", StreetNumber = "12" };
			var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(createNewuser), Encoding.UTF8, "application/json"));

			var content = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();

			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("Food created successfully", content);
			Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}
	}
}
