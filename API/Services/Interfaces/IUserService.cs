using FoodDelivery.Models.DTOs;
using FoodOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Services.Interfaces
{
	public interface IUserService
	{
		public User GetUser(int id);
		public List<User> GetUsers();
		public void CreateUser(CreateUserDTO model);

		//public void UpdataFood(CreateUserDTO model);
		public void Delete(int id);
	}
}
