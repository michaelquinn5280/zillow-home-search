using System;
using Microsoft.AspNet.Mvc;
using Zillow.Domain.Proxy;

namespace Zillow.Mvc.Controllers.API
{
    /// <summary>
    /// Home Search Controller
    /// GET: /api/search/searchCriteria
    /// </summary>
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [HttpGet("{searchCriteria}")]
        public IActionResult Search(string searchCriteria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchCriteria)) return HttpBadRequest("invalid search criteria");

                var address = new GoogleService().GetFormattedAddress(searchCriteria).Result;
                if (address == null) return HttpBadRequest("address not found");

                var response = new ZillowService().GetSearchResults(address).Result;
                return response.message.code != "0" ? Ok(response.message) : Ok(response.response);

            }
            catch (Exception ex)
            {
                //todo: log ex and put this try catch logic for exception handing in mvc filter
                return new HttpStatusCodeResult(500);
            }
        }
    }
}
