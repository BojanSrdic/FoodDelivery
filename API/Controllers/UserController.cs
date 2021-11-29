using AutoMapper;
using FoodDelivery.Models.DTOs;
using FoodDelivery.Models.DTOs.Read;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		public readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UserController(IMapper mapper, IUserService userService)
		{
			_mapper = mapper;
			_userService = userService;
		}

		// POST: api/User
		[HttpPost]
		public IActionResult Create(CreateUserDTO model)
		{
			_userService.CreateUser(model);

			return Ok("Food created successfully");
		}

		// GET: api/User
		[HttpGet]
		public IActionResult GetList()
		{
			// Vratiti DTO
			return Ok(_userService.GetUsers());
		}

		// GET: api/User/1
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var user = _userService.GetUser(id);

			if (user == null)
			{
				return NotFound("Food doesn't exist");
			}

			var readUser = _mapper.Map<ReadUserDTO>(user);

			return Ok(readUser);
		}

		// DELETE: api/User/1
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var foodToDelete = _userService.GetUser(id);

			if (foodToDelete == null)
			{
				return NotFound("Food doesn't exist");
			}

			_userService.Delete(foodToDelete.Id);

			return Ok("Food deleted successfully");
		}
	}
}

//https://nominatim.openstreetmap.org/ui/reverse.html
