using Microsoft.AspNet.Mvc;

namespace Zillow.Mvc.Controllers
{
    public class HomeSearchController : Controller
    {
        /// <summary>
        /// GET: /homesearch/
        /// </summary>
        /// <returns>View for search</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
