using DirectOrderTracker.Models;
using DirectOrderTracker.Services;
using System.Web.Mvc;

namespace DirectOrderTracker.Controllers
{
    public partial class GridController : Controller
    {
        private ISaleService salesService;

        public GridController(ISaleService saleService)
        {
            this.salesService = saleService;
        }

        public ActionResult GetSalesPeople()
        {
            return Json(salesService.GetAllSalesPeople(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCommodities()
        {
            return Json(salesService.GetAllCommodities(), JsonRequestBehavior.AllowGet);
        }
    }
}
