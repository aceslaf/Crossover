using System.Web.Mvc;

namespace CrossoverTask.Web.Controllers
{

    public class HomeController : BaseController
    {
       
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}