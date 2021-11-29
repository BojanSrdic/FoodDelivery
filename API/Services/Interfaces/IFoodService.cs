using FoodOrder.Models;
using FoodOrder.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrder.Services
{
	public interface IFoodService
	{
		public Food GetFood(int id);
		public List<Food> GetFoods();
		public void CreateFood(CreateFoodDTO model);
		//public void UpdataFood(CreateFoodDTO model);
		public void Delete(int id);
	}
}
