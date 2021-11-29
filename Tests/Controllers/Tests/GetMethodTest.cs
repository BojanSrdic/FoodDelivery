using FoodOrder;
using FoodOrder.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Controllers
{
	public class GetMethodTest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;

		public GetMethodTest(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Theory]
		[InlineData("/api/User/1")]
		[InlineData("/api/Order/1")]
		[InlineData("/api/Restaurant/1")]
		public async void Get_ById_ExpectedStatusCode200(string url)
		{
			//Arrange
			var client = _factory.CreateClient();
			//Act
			var response = await client.GetAsync(url);
			//Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}

		[Theory]
		[InlineData("/api/Food/1")]
		public async void Get_FoodById_ExpectedStatusCode200(string url)
		{
			//Arrange
			var client = _factory.CreateClient();
			//Act
			var response = await client.GetAsync(url);

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

		[Theory]
		[InlineData("/api/Food")]
		public async void Get_FoodList_ExpectedStatusCode200(string url)
		{
			//Arrange
			var client = _factory.CreateClient();
			//Act
			var response = await client.GetAsync(url);
			var responseBody = response.Content.ReadAsStringAsync().Result;
			var deserialize = JsonConvert.DeserializeObject<List<Food>>(responseBody);

			//Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
			Assert.Equal("Piletina sa kikirikijem", deserialize[0].Name);
			Assert.Equal(10, deserialize[0].Price);
			Assert.Equal("Teletina sa grilovanim povrcem", deserialize[3].Name);
			Assert.Equal(15, deserialize[3].Price);
			Assert.Equal("Riblja corba", deserialize[6].Name);
			Assert.Equal(25, deserialize[6].Price);
		}

		[Theory]
		[InlineData("/api/User")]
		[InlineData("/api/Order")]
		[InlineData("/api/Restaurant")]
		public async void Get_List_ExpectedStatusCode200(string url)
		{
			//Arrange
			var client = _factory.CreateClient();
			//Act
			var response = await client.GetAsync(url);
			//Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}

		[Theory]
		[InlineData("/api/Food/11")]
		[InlineData("/api/User/11")]
		[InlineData("/api/Restaurant/11")]
		public async void GET_ByIdDoesNotExist_ExpectedStatusCode404(string url)
		{
			//Arrange
			var client = _factory.CreateClient();
			//Act
			var response = await client.GetAsync(url);
			var content = await response.Content.ReadAsStringAsync();

			//Assert
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
			Assert.Equal("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
			Assert.Equal("Food doesn't exist", content);
		}

		[Theory]
		[InlineData("/api/Food/BadRequest")]
		[InlineData("/api/User/BadRequest")]
		[InlineData("/api/Restaurant/BadRequest")]
		public async void GET_ByIdDoesNotExist_ExpectedStatusCode400(string url)
		{
			//Arrange
			var client = _factory.CreateClient();

			//Act
			var response = await client.GetAsync(url);

			//Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}
	}
}
