using FoodDelivery.Models.DTOs;
using FoodDelivery.Services.Interfaces;
using FoodOrder.Data;
using FoodOrder.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodDelivery.Services
{
	public class GeoLocationService : IGeoLocationService
    {
		private readonly DatabaseContext _context;

		public GeoLocationService(DatabaseContext context)
		{
            _context = context;
		}

        public async Task<Coordinates> GetCoordinatesForAddress(string streetNumber, string street, string city, string country)
        {
            var address = CreateAddress(streetNumber, street);
            var adjustedCity = CreateCity(city);

            //Http request za getovanje podataka sa nekog apija ovo je api MICROSOFT BING MAPS
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri("http://dev.virtualearth.net") };
            HttpResponseMessage httpResult = await httpClient.GetAsync(
                String.Format("/REST/v1/Locations/" + country + "/-/" + adjustedCity + "/" + address + "?output=json&key=Agzk8Ng6o8dR2P9kWjL2a7odtZn4i-axKVuP-o4GcurHIZUCNTUy6zDw1qKRd1N8"));
            //DOBIJAS RESPONSE OD APIJA
            var res = await httpResult.Content.ReadAsStringAsync();
            //PARISRAS JSON OBJEKAT KOJI DOBIJES ZA RESPONSE 
            JObject o = JObject.Parse(res);
            //VRSI SE DESERIJALIZACIJA JSON OBJEKTA
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(o.ToString());
            var coordinates = myDeserializedClass.resourceSets.First().resources.Select(p => p.point).Select(c => c.coordinates);
            Coordinates coordinates1 = new Coordinates();
            foreach (var coordinate in coordinates)
            {
                coordinates1.latitude = coordinate[0];
                coordinates1.longitude = coordinate[1];
            }
            return coordinates1;
        }

        private string CreateAddress(string streetNumber, string street)
        {
            return streetNumber + "%20" + street.Replace(" ", "%20");
        }

        private string CreateCity(string city)
        {
            return city.Replace(" ", "%20");
        }

        public double Distance(int UserId, int RestorantId)
        {
            var user = _context.Users.Find(UserId);
            var restaurant = _context.Restaurants.Find(RestorantId);

            var temp1 = Math.Pow(user.Latitude - restaurant.Latitude, 2);
            var temp2 = Math.Pow(user.Longitude - restaurant.Longitude, 2);
            var result = Math.Sqrt(temp1 + temp2);
              
            return result;
        }
    }
}
