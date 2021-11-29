using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.DataAccess.DTOs.Create
{
	public class CreateOrderDto
	{
		public int UserId { get; set; }
		public int FoodId { get; set; }
		public int Quontity { get; set; }
	}
}
