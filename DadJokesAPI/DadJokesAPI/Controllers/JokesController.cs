using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DadJokesAPI.Helpers;
using DadJokesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DadJokesAPI.Controllers
{
    [Route("api/jokes")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private static readonly ApiClient apiClient = new ApiClient();

        [HttpGet]
        public async Task<DadJoke> GetJoke()
        {
            return await apiClient.Get<DadJoke>();
        }

        [HttpGet("{query}")]
        public async Task<Dictionary<string, List<DadJoke>>> GetJoke(string query)
        {
            var results = await apiClient.Search<DadJoke>(query);
            var highlightedResults = results.Select(joke =>
            {
                var text = joke.Joke;
                var replacedText = Regex.Replace(text, $"({query})", "<$1>");
                return new DadJoke() { Id = joke.Id, Joke = replacedText };
            });

            var shortJokes = highlightedResults.Where(joke =>
            {
                return joke.Joke.Split(" ").Length < 10;
            }).ToList();
            var mediumJokes = highlightedResults.Where(joke =>
            {
                var length = joke.Joke.Split(" ").Length;
                return length >= 10 && length < 20;
            }).ToList();
            var longJokes = highlightedResults.Where(joke =>
            {
                return joke.Joke.Split(" ").Length >= 20;
            }).ToList();

            var jokes = new Dictionary<string, List<DadJoke>>();
            jokes["short"] = shortJokes;
            jokes["medium"] = mediumJokes;
            jokes["long"] = longJokes;
            return jokes;
		}
    }
}