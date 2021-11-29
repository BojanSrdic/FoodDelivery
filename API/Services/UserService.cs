using FoodDelivery.Models.DTOs;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using FoodOrder.Models;
using FoodOrder.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrder.Services
{
	public class UserService : IUserService
	{
		private readonly DatabaseContext _context;
		public readonly IGeoLocationService _geolocationService;

		public UserService(DatabaseContext context, IGeoLocationService geolocationService)
		{
			_context = context;
			_geolocationService = geolocationService;
		}

		public void CreateUser(CreateUserDTO model)
		{
			var GLCordinates = _geolocationService.GetCoordinatesForAddress(model.StreetNumber, model.Street, model.City, model.Country).Result;

			var newUser = new User
			{
				Name = model.Name,
				Country = model.Country,
				City = model.City,
				Street = model.Street,
				StreetNumber = model.StreetNumber,
				Latitude = GLCordinates.latitude,
				Longitude = GLCordinates.longitude
			};

			_context.Users.Add(newUser);
			_context.SaveChanges();
		}

		public User GetUser(int id)
		{
			if (id == 0)
			{
				throw new Exception("Error input can't be 0!");
			}

			var user = _context.Users.Find(id);
			return user;
		}

		public List<User> GetUsers()
		{
			return _context.Users.ToList();
		}

		public void Delete(int id)
		{
			var user = _context.Users.Find(id);
			_context.Users.Remove(user);
			_context.SaveChanges();
		}
	}
}
