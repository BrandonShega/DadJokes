using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DadJokesAPI.Helpers
{
    public class ApiClient
    {
        private static readonly string ApiUrl = "https://www.icanhazdadjoke.com";
        private static readonly HttpClient Client = new HttpClient();

        public ApiClient()
        { 
            //Add required headers for request, should be adding as default?
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("User-Agent", "My Library (https://www.github.com/brandonshega/dadjokes)");
		}

        public async Task<ActionResult<T>> Get<T>()
        {
            var response = await Client.GetStringAsync(ApiUrl);
            return JsonConvert.DeserializeObject<T>(response);
		}
    }
}
