using Newtonsoft.Json;

namespace DadJokesAPI.Models
{
    public struct SearchResult<T>
    {
        [JsonProperty(PropertyName = "current_page")]
        public int CurrentPage { get; set; }
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "next_page")]
        public int NextPage { get; set; }
        [JsonProperty(PropertyName = "previous_page")]
        public int PreviousPage { get; set; }
        public T[] Results { get; set; }
        [JsonProperty(PropertyName = "search_term")]
        public string SearchTerm { get; set; }
        [JsonProperty(PropertyName = "total_jokes")]
        public int TotalJokes { get; set; }
        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }
    }
}
