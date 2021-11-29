using FoodOrder.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Services.Interfaces
{
	public interface IOrderPaymentService
	{
		public void CreateOrderPaymant(CreateOrderPaymentDTO model);
	}
}
