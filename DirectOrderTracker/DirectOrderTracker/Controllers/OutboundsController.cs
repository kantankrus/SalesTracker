using DirectOrderTracker.Models.ViewModels;
using DirectOrderTracker.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web.Mvc;

namespace DirectOrderTracker.Controllers
{
    public class OutboundsController : Controller
    {
        private IOutboundRepository outbounds;

        public OutboundsController(IOutboundRepository outboundRepository)
        {
            outbounds = outboundRepository;
        }
        public ActionResult AllOutBounds([DataSourceRequest] DataSourceRequest request, DateTime? fromDate, DateTime? toDate, string commodity)
        {
            var result = outbounds.GetAllSales(commodity);

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditOutBounds([DataSourceRequest] DataSourceRequest request, OutboundSalesViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                outbounds.EditSale(sale);
                outbounds.SaveChanges();
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
    }
}