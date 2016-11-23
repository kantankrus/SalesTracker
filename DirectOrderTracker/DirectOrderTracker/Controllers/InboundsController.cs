using DirectOrderTracker.Models.ViewModels;
using DirectOrderTracker.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web.Mvc;

namespace DirectOrderTracker.Controllers
{
    public class InboundsController : Controller
    {
        //Repository in injected into controller constructor. NinjectWebCommon.cs contains the configuration for this.
        private IInboundRepository inbounds;
        public InboundsController(IInboundRepository inboundRepository)
        {
            inbounds = inboundRepository;
        }
        public ActionResult AllInBounds([DataSourceRequest] DataSourceRequest request, DateTime? fromDate, DateTime? toDate, string commodity)
        {
            var result = inbounds.GetAllSales(commodity);

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditInBounds([DataSourceRequest] DataSourceRequest request, InboundSalesViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                inbounds.EditSale(sale);
                inbounds.SaveChanges();
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
    }
}