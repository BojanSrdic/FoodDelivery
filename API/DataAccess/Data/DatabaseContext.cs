using FoodDelivery.Models.Domains;
using FoodOrder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Data
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
		{

		}
		public DbSet<User> Users { get; set; }
		public DbSet<Food> Foods { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Payment> OrderPayments { get; set; }
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<Rate> Rates { get; set; }

	}
}
