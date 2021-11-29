using FoodDelivery.DataAccess.DTOs.Create;
using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrder.Services.Interfaces
{
	public class OrderService : IOrderService
	{
		private readonly DatabaseContext _context;

		public OrderService(DatabaseContext context)
		{
			_context = context;
		}

		public void CreateOrder(CreateOrderDto model)
		{
			var orderPrice = GetOrderPrice(model);

			var newOrder = new Order
			{
				OrderPrice = orderPrice,
				Quontity = model.Quontity,

				FoodId = model.FoodId,
				UserId = model.UserId,
			};
			
			_context.Orders.Add(newOrder);
			_context.SaveChanges();
		}

		public List<Order> GetOrders()
		{
			return _context.Orders.ToList();
		}

		public Order GetOrder(int id)
		{
			if (id == 0)
			{
				throw new Exception("Error input can't be 0!");
			}

			return _context.Orders.Find(id);
		}

		public double GetOrderPrice(CreateOrderDto model)
		{
			var selectedFood = _context.Foods.Find(model.FoodId).Price;
			//var selectedFood = _foodService.GetFood(model.FoodId).Price;
			var price = (double)model.Quontity;

			return price * selectedFood;
		}

		//All Customer Orders
		//public List<Order> CustomerOrderList(UserOrdersDTO model)
		//{
		//	return GetOrders().FindAll(a => a.Customer.Id == model.CustomerId);
		//}

	}
}
