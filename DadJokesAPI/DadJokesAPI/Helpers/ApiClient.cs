using System.Net.Http;
using System.Threading.Tasks;
using DadJokesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

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

        public async Task<T> Get<T>()
        {
            var response = await Client.GetStringAsync(ApiUrl);
            return JsonConvert.DeserializeObject<T>(response);
		}

        public async Task<List<T>> Search<T>(string query)
        {
            var results = new List<T>();
            var page = 1;

            do
            {
			    var searchUrl = ApiUrl + "/search";
			    var queryParams = new Dictionary<string, string>();
			    var queryArray = new List<string>();

			    // Check if query was supplied
			    if (query != null) 
			    {
					queryParams["term"] = query;
			    }
			    queryParams["page"] = $"{page}";

			    foreach(KeyValuePair<string, string> entry in queryParams)
			    {
					queryArray.Add($"{entry.Key}={entry.Value}");
			    }

			    searchUrl += $"?{string.Join("&", queryArray)}";

                var response = await Client.GetStringAsync(searchUrl);
                var searchResult = JsonConvert.DeserializeObject<SearchResult<T>>(response);

                if (searchResult.Results.Any()) 
		        {
                    results.AddRange(searchResult.Results.ToList());
				}

                if (results.Count() >= 30) 
		        {
                    return results.Take(30).ToList();
				}

                page = (searchResult.NextPage != searchResult.CurrentPage) ? searchResult.NextPage : 1;
            } while (page > 1);
            return results;
		}
    }
}
