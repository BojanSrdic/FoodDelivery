using FoodDelivery.DataAccess.DTOs.Create;
using FoodDelivery.DataAccess.DTOs.Read;
using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using FoodOrder.Services;
using FoodOrder.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IOrderPaymentService _orderPaymentService;

		private readonly IFoodService _foodService;
		private readonly IUserService _userService;

		public OrderController(IOrderService orderService, IOrderPaymentService orderPaymentService, 
			IFoodService foodService, IUserService userService)
		{
			_orderService = orderService;
			_orderPaymentService = orderPaymentService;
			_foodService = foodService;
			_userService = userService;
		}

		// POST: api/Food
		[HttpPost]
		public IActionResult Create(CreateOrderDto model)
		{
			_orderService.CreateOrder(model);

			return Ok("Food created successfully");
		}

		// GET: api/Order
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var order = _orderService.GetOrder(id);

			if (order == null)
			{
				return NotFound("Order doesn't exist");
			}

			var dto = new ReadOrderDto() 
			{
				Customer = _userService.GetUser(order.UserId),
				FoodItem = _foodService.GetFood(order.FoodId), 
				Quontity = order.Quontity, 
				OrderPrice = order.OrderPrice
			};

			return Ok(dto);
		}

		[HttpGet]
		public List<Order> GetList()
		{
			return _orderService.GetOrders();
		}

		//[HttpPost]
		//[Route("/Payment")]
		//public void Payment(CreateOrderPaymentDTO model)
		//{
		//	_orderPaymentService.CreateOrderPaymant(model);
		//}
	}
}
