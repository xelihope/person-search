using System.Web.Mvc;

namespace PersonSearch.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}