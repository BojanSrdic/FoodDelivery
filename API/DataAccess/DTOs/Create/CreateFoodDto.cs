using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Models.DTOs
{
	public class CreateFoodDTO
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public double Price { get; set; }
	}
}
