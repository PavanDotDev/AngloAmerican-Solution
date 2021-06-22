using System.Net.Http;
using Newtonsoft.Json;

namespace AngloAmerican.Account.Services
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient httpClient;

        /* TODO
            - Improve the usage of HttpClient in GetAddress method
         */
        public AddressService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public string GetAddress()
        {
            // var http = new HttpClient();
            var response = httpClient.GetAsync("https://randomuser.me/api/?nat=gb");
            var content = response.Result.Content;
            var adr = content.ReadAsStringAsync().Result;

            var address = GetCityAndPostCode(adr);

            return address;
        }

        private string GetCityAndPostCode(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
            dynamic city = jsonObject.results[0].location.city;
            dynamic postcode = jsonObject.results[0].location.postcode;

            var address = $"{city.ToString()} {postcode.ToString()}";

            return address;
        }
    }

    public interface IAddressService
    {
        string GetAddress();
    }
}
