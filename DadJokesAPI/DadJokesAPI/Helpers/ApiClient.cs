using System.Net.Http;
using System.Threading.Tasks;
using DadJokesAPI.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace DadJokesAPI.Helpers
{
    public class ApiClient
    {
        private static readonly string ApiUrl = "https://www.icanhazdadjoke.com";
        // Create only one instance of client per documentation
        private static readonly HttpClient Client = new HttpClient();

        public ApiClient()
        { 
            //Add required headers for request, should be adding as default?
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("User-Agent", "My Library (https://www.github.com/brandonshega/dadjokes)");
		}

        public async Task<T> Get<T>()
        {
            var response = await Client.GetStringAsync(ApiUrl);
            return JsonConvert.DeserializeObject<T>(response);
		}

        public async Task<List<T>> Search<T>(string query)
        {
            var results = new List<T>();

		    var searchUrl = ApiUrl + "/search";
		    var queryParams = new Dictionary<string, string>();
		    var queryArray = new List<string>();

		    // Check if query was supplied
		    if (query != null) 
		    {
				queryParams["term"] = query;
		    }
		    queryParams["limit"] = "30";

			// Loop over each query pair and create query string
		    foreach(KeyValuePair<string, string> entry in queryParams)
		    {
				queryArray.Add($"{entry.Key}={entry.Value}");
		    }

		    searchUrl += $"?{string.Join("&", queryArray)}";

			var response = await Client.GetStringAsync(searchUrl);
			var searchResult = JsonConvert.DeserializeObject<SearchResult<T>>(response);

			if (searchResult.Results.Any()) 
			{
				results = searchResult.Results.ToList();
			} 
			return results;
		}
    }
}
