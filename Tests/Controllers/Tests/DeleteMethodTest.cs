using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using FoodOrder;
using System.Net;

namespace Controllers
{
	public  class DeleteMethodTest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly WebApplicationFactory<Startup> _factory;

		public DeleteMethodTest(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Theory]
		[InlineData("/api/Food/1")]
		[InlineData("/api/Restaurant/1")]
		[InlineData("/api/User/1")]
		public async void Test2(string url)
		{
			//Arrange
			var client = _factory.CreateClient();
			//Act
			var response = await client.DeleteAsync(url);
			//Assert
			response.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}
