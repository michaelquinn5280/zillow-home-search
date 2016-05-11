using Newtonsoft.Json;

namespace Zillow.Domain.Entities.Google
{
    /// <summary>
    /// using dynamic for now
    /// todo: serialize into google address object
    /// </summary>
    public class SearchResults
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
    }
}
