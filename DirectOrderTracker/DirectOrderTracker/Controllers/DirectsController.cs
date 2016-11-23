using DirectOrderTracker.Models;
using DirectOrderTracker.Repositories;
using DirectOrderTracker.Models.ViewModels;
using DirectOrderTracker.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using Ninject;
using System.Web.Mvc;
using System.Reflection;
using Ninject.Web.Common;

namespace DirectOrderTracker.Controllers
{
    public class DirectsController : Controller
    {
        private IDirectRepository directs;
        public DirectsController(IDirectRepository directRepository)
        {
            directs = directRepository;
        }

        public ActionResult AllDirects([DataSourceRequest] DataSourceRequest request, DateTime? fromDate, DateTime? toDate, string commodity)
        {
            var result = directs.GetAllSales(commodity);

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditDirects([DataSourceRequest] DataSourceRequest request, DirectSalesViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                directs.EditSale(sale);
                directs.SaveChanges();

                sale.Margin = Calculations.MarginCalc(sale.Qty, sale.UNITPRICE, sale.UNITCOST, sale.POFrghtRate);
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
    }
}