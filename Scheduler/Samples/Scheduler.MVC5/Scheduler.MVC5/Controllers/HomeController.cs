using System.Web.Mvc;

namespace Scheduler.MVC5.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}