using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zillow.Domain.Proxy
{
    public class GoogleService
    {
        /// <summary>
        /// Base API address
        /// </summary>
        private const string MapsApiBaseAddress = @"http://maps.googleapis.com/maps/api/";

        /// <summary>
        /// Get Formatted Address using Google API
        /// </summary>
        /// <param name="search">Search String</param>
        /// <returns>Returns null if we can't derive a valid address from our search, otherwise we return Address object</returns>
        public async Task<Entities.Address> GetFormattedAddress(string search)
        {
            var formattedSearch = search.Trim().Replace(" ", "+");
            var requestUri = $"geocode/json?address={formattedSearch}&sensor=false";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(MapsApiBaseAddress);
            var response = await client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode) return null;
            var results = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(results);
            if (result.status == @"ZERO_RESULTS") return null;
            var address = ParseAddressResults(result.results[0].formatted_address.ToString().Split(','));

            return address;
        }

        /// <summary>
        /// this should really be done by using types returned from google and not the formatted address string
        /// </summary>
        /// <param name="addressComponents">Array of Address Results</param>
        /// <returns>Entities.Address</returns>
        private static Entities.Address ParseAddressResults(string[] addressComponents)
        {
            if (addressComponents.Length != 4) return null;
            var stateZipComponentes = addressComponents[2].Trim().Split(' ');
            return new Entities.Address
            {
                Street = addressComponents[0],
                City = addressComponents[1],
                Country = addressComponents[3],
                State = stateZipComponentes[0],
                Zip = stateZipComponentes[1]
            };
        }
    }
}
