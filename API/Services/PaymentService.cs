using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using FoodOrder.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Services
{
	public class PaymentService : IOrderPaymentService
	{
		private readonly DatabaseContext _context;

		public PaymentService(DatabaseContext context)
		{
			_context = context;
		}

		public void CreateOrderPaymant(CreateOrderPaymentDTO model)
		{
			var payment = TotalPaymentPrice(model);

			var newOrderPayment = new Payment
			{
				Total = payment,
				Orders = _context.Orders.ToList(),

			};

			_context.OrderPayments.Add(newOrderPayment);
			_context.SaveChanges();
		}

		public double TotalPaymentPrice(CreateOrderPaymentDTO model)
		{
			//var price = _context.Sum
			var price = 100;
			return price;
		}

		//public List<Order> GetOrderListById(CreateOrderPaymentDTO model)
		//{
		////	return _context.Orders.GroupBy(model.CustomerId);
		//}

		//public List<Order> GetOrdersByID(CreateOrderPaymentDTO model)
		//{
		//	//U modelu se nalazi customer ID
		//	//return _context.Orders.Find(model.CustomerId);
		//}
	}
}
