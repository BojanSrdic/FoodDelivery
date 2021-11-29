using FoodDelivery.Services;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using FoodOrder.Services;
using FoodOrder.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;

namespace FoodOrder
{
	public class Startup
	{
		private readonly IWebHostEnvironment _env;

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			_env = env;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			//if (_env.IsDevelopment())
			//{
			//Console.WriteLine("--> Using InMemoryDatabase in Development mode");
			services.AddDbContext<DatabaseContext>(option => option.UseInMemoryDatabase("InMemory"));

			services.AddControllers();
			//}
			//else if (_env.IsEnvironment("Testing"))
			//{
			//Console.WriteLine("--> Using InMemoryDatabase for Testing ");
			//services.AddDbContext<DatabaseContext>(option => option.UseInMemoryDatabase("InMemory" + Guid.NewGuid().ToString()), ServiceLifetime.Singleton);

			//services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
			//}

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodOrder", Version = "v1" });
			});

			services.AddScoped<IFoodService, FoodService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IOrderPaymentService, PaymentService>();
			services.AddScoped<IRestaurantService, RestaurantService>();
			services.AddScoped<IGeoLocationService, GeoLocationService>();

			services.AddAutoMapper(this.GetType().Assembly);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment() || env.IsEnvironment("Testing"))
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodOrder v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			InMemoryTestData.AddTestDataInMemory(app);
		}
	}
}
