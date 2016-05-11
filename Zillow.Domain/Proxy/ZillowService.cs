using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Zillow.Domain.Entities.Zillow;

namespace Zillow.Domain.Proxy
{
    /// <summary>
    /// Had some issues getting xsd.exe working with zillow schema
    /// then some more issues getting the generated class working with dnx 5
    /// http://stackoverflow.com/questions/13239726/zillows-searchresults-xsd-and-visual-studios-xsd-command
    /// </summary>
    public class ZillowService
    {
        /// <summary>
        /// Base API Address
        /// </summary>
        private const string ZillowApiBaseAddress = @"http://www.zillow.com/webservice/";

        /// <summary>
        /// Get Search Results from Zillow API
        /// http://www.zillow.com/webservice/GetSearchResults.htm?zws-id=X1-ZWz1dyb53fdhjf_6jziz&address=2114+Bigelow+Ave&citystatezip=Seattle%2C+WA
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>searchresults>Search Results</returns>
        public async Task<searchresults> GetSearchResults(Entities.Address address)
        {
            var requestUri = ParseZillowQuery(address);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.BaseAddress = new Uri(ZillowApiBaseAddress);
            var response = await client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode) return null;

            var results = await response.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(searchresults));
            using (TextReader reader = new StringReader(results))
            {
                return (searchresults) serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Parse Zillow Query String
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private static string ParseZillowQuery(Entities.Address address)
        {
            var zwsId = "X1-ZWz1dyb53fdhjf_6jziz"; //todo: add to config
            var zillowAddress = address.Street.Trim().Replace(' ', '+');
            var zillowCityStateZip = $"{address.City.Trim()}%2C+{address.State.Trim()}+{address.Zip.Trim()}";
            var requestUri = $"GetSearchResults.htm?zws-id={zwsId}&address={zillowAddress}&citystatezip={zillowCityStateZip}";
            return requestUri;
        }
    }
}
