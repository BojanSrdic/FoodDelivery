using AutoMapper;
using FoodDelivery.Models.DTOs;
using FoodDelivery.Models.DTOs.Read;
using FoodOrder.Models;

namespace FoodDelivery.Settings.AutoMapper
{
	public class AutoMapper : Profile
	{
		public AutoMapper()
		{
			// Source -> Target
			CreateMap<User, ReadUserDTO>();
			//CreateMap<CreateUserDTO, User>();
		}
	}
}
