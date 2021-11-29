using FoodDelivery.DataAccess.DTOs.Create;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Services.Interfaces
{
	public interface IOrderService
	{
		public void CreateOrder(CreateOrderDto model);
		public Order GetOrder(int id);
		public List<Order> GetOrders();
		//public List<Order> CustomerOrderList(UserOrdersDTO model);
	}
}
