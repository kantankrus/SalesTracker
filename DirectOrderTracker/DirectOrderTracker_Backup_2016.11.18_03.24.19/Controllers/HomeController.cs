using DirectOrderTracker.Models;
using DirectOrderTracker.Services;
using System.Web.Mvc;

namespace DirectOrderTracker.Controllers
{
    public class HomeController : Controller
    {
        private SalesService salesService;
        public HomeController()
        {
            salesService = new SalesService(new OrdersEntities());
           
        }
        public ActionResult Index()
        {
            ViewData["vendors"] = salesService.GetAllVendors();
            ViewData["carriers"] = salesService.GetAllCarriers();
            ViewData["statuses"] = salesService.GetAllStatuses();
            return View("Direct");
        }
        public ActionResult Direct()
        {
            ViewData["vendors"] = salesService.GetAllVendors();
            ViewData["carriers"] = salesService.GetAllCarriers();
            ViewData["statuses"] = salesService.GetAllStatuses();
            return View();
        }
        public ActionResult Inbound()
        {

            return View();
        }
        public ActionResult Outbound()
        {
          
            return View();
        }

    }
}
