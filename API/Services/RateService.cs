using FoodDelivery.Models.Domains;
using FoodDelivery.Models.DTOs;
using FoodOrder.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodDelivery.Services
{
	public interface IRateService
	{
		public void CreateRate(CreateRateDTO model);
		public void GetRate();
		public List<Rate> GetRateList();
	}

	public class RateService : IRateService
	{
		private readonly DatabaseContext _context;

		public RateService(DatabaseContext context)
		{
			_context = context;
		}
		public void CreateRate(CreateRateDTO model)
		{
			var rate = new Rate	{ Mark = model.Mark };

			_context.Rates.Add(rate);
			_context.SaveChanges();
		}

		public void GetRate()
		{
			throw new NotImplementedException();
		}

		public List<Rate> GetRateList()
		{
			return _context.Rates.ToList();
		}

		public double CalculateRate(CheckFoodRateDTO model)
		{
			var rateList = GetRateList().FindAll(r => r.Food.Name == model.Name).Select(r => r.Mark);
			var sumOfMarks = rateList.Sum();
			var numberOfMarks = GetRateList().FindAll(r => r.Food.Name == model.Name).Count();

			var average = sumOfMarks / numberOfMarks;
			return average;


			//var averageRate = GetRateList().FindAll(r => r.Food.Name == model.Name).Select(r => r.Mark);
			//var a = rateList.Average();
		}
	}
}