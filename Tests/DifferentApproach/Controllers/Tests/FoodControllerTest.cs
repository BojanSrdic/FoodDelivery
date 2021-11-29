using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Controllers.Tests
{
	public class FoodControllerTest
	{
		[Fact]
		public async void GET_FoodById_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.GetAsync("/api/Food/1");
				response.EnsureSuccessStatusCode();

				var responseBody = response.Content.ReadAsStringAsync().Result;
				JObject o = JObject.Parse(responseBody);
				Food deserialize = JsonConvert.DeserializeObject<Food>(o.ToString());

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
				Assert.Equal("Piletina sa kikirikijem", deserialize.Name);
				Assert.Equal(10, deserialize.Price);
			}
		}
		[Fact]
		public async void GET_FoodByIdDoesNotExist_NotFound()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				var response = await client.GetAsync("/api/Food/22");
				var content = await response.Content.ReadAsStringAsync();

				//Assert
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
				Assert.Equal("Food doesn't exist", content);
			}
		}

		[Fact]
		public async void POST_CreateFood_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var createNewFood = new CreateFoodDTO { Name = "Kikiriki", Price = 40 };
				var response = await client.PostAsync("/api/Food", new StringContent(JsonConvert.SerializeObject(createNewFood), Encoding.UTF8, "application/json"));
				var content = await response.Content.ReadAsStringAsync();
				response.EnsureSuccessStatusCode();

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("Food created successfully", content);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
			}
		}

		[Fact]
		public async void POST_CreateFood_BedRequest()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var createNewFood = new CreateFoodDTO { Price = 40 };
				var response = await client.PostAsync("/api/Food", new StringContent(JsonConvert.SerializeObject(createNewFood), Encoding.UTF8, "application/json"));

				//Assert
				Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			}
		}

		[Fact]
		public async void GET_FoodList_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.GetAsync("/api/Food");
				response.EnsureSuccessStatusCode();

				var responseBody = response.Content.ReadAsStringAsync().Result;
				var deserialize = JsonConvert.DeserializeObject<List<Food>>(responseBody);

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
				Assert.Equal("Piletina sa kikirikijem", deserialize[0].Name);
				Assert.Equal(10, deserialize[0].Price);
				Assert.Equal("Teletina sa grilovanim povrcem", deserialize[3].Name);
				Assert.Equal(15, deserialize[3].Price);
				Assert.Equal("Riblja corba", deserialize[6].Name);
				Assert.Equal(25, deserialize[6].Price);
			}
		}

		[Fact]
		public async void DELETE_Food_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.DeleteAsync("/api/Food/1");
				var content = await response.Content.ReadAsStringAsync();
				response.EnsureSuccessStatusCode();

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
				//Assert.Equal("Food deleted successfully", content);

				//Sta mogu da koristim umesto "Food deleted successfully" ako zelim ba vratim vrednost reurn-a -- return Ok(new { message = "Food deleted successfully" });
			}
		}

		[Fact]
		public async void DELETE_Food_NotFound()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.DeleteAsync("/api/Food/22");
				var content = await response.Content.ReadAsStringAsync();

				//Assert
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
				Assert.Equal("Food doesn't exist", content);
			}
		}

		[Fact]
		public async void GET_NonExistentEndpoint_NotFound()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.GetAsync("/api/WrongEndPoint");

				//Assert
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			}
		}

		[Fact]
		public async void GET_NonExistentEndpoint_BedRequest()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.GetAsync("/api/Food/WrongEndPoint");

				//Assert
				Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
			}
		}

		[Fact]
		public async void POST_NonExistentEndpoint_NotFound()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var createNewFood = new CreateFoodDTO { Name = "Kikiriki", Price = 40 };
				var response = await client.PostAsync("/api/Foods", new StringContent(JsonConvert.SerializeObject(createNewFood), Encoding.UTF8, "application/json"));

				//Assert
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			}
		}
	}
}
