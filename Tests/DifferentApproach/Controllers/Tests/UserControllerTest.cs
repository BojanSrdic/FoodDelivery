using FoodDelivery.Models.DTOs;
using FoodOrder.Models.DTOs;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Controllers.Tests
{
	public class UserControllerTest
	{
		[Fact]
		public async void GET_FoodById_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.GetAsync("/api/User/1");
				response.EnsureSuccessStatusCode();

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
			}
		}

		[Fact]
		public async void GET_FoodByIdDoesNotExist_NotFound()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				var response = await client.GetAsync("/api/User/22");
				var content = await response.Content.ReadAsStringAsync();

				//Assert
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
				Assert.Equal("Food doesn't exist", content);
			}
		}
		[Fact]
		public async void POST_CreateUser_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var createNewUser = new CreateUserDTO { Name = "Aaaa", Country = "Serbia", City = "Novi Sad", Street = "Cara Dusana", StreetNumber = "10" };
				var response = await client.PostAsync("/api/Food", new StringContent(JsonConvert.SerializeObject(createNewUser), Encoding.UTF8, "application/json"));
				var content = await response.Content.ReadAsStringAsync();
				//response.EnsureSuccessStatusCode();

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("Food created successfully", content);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
			}
		}

		[Fact]
		public async void POST_CreateUser_BadRequest()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var createNewUser = new CreateUserDTO { Street = "Cara Dusana", StreetNumber = "10" };
				var response = await client.PostAsync("/api/User", new StringContent(JsonConvert.SerializeObject(createNewUser), Encoding.UTF8, "application/json"));

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
				var response = await client.GetAsync("/api/User");
				response.EnsureSuccessStatusCode();

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
			}
		}

		[Fact]
		public async void DELETE_User_StatusCodeOK()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.DeleteAsync("/api/User/1");
				response.EnsureSuccessStatusCode();

				//Assert
				Assert.Equal(HttpStatusCode.OK, response.StatusCode);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
			}
		}

		[Fact]
		public async void DELETE_User_NotFound()
		{
			//Arrange
			using (var client = new TestClientProvider().Client)
			{
				//Act
				var response = await client.DeleteAsync("/api/User/22");
				var content = await response.Content.ReadAsStringAsync();

				//Assert
				Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
				Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
				Assert.Equal("Food doesn't exist", content);
			}
		}
	}
}
