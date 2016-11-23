using DirectOrderTracker.Models;
using DirectOrderTracker.Models.ViewModels;
using DirectOrderTracker.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Web.Mvc;

namespace DirectOrderTracker.Controllers
{
    public partial class GridController : Controller
    {
        private SalesService salesService;
        public GridController()
        {
            salesService = new SalesService(new OrdersEntities());
        }
        /*
         * 
         *OUT-BOUND SALES
         * 
         */
        public ActionResult AllOutBounds([DataSourceRequest] DataSourceRequest request, DateTime? fromDate, DateTime? toDate, string commodity)
        {
            var result = salesService.GetOutBoundSales(fromDate, toDate, commodity);
            
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditOutBounds([DataSourceRequest] DataSourceRequest request, OutboundSalesViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                salesService.EditOuboundSale(sale);
               // sale.Margin = Calculations.MarginCalc(sale.Qty, sale.Price, sale.Cost);
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
        /*
         * 
         *IN-BOUND SALES
         * 
         */
        public ActionResult AllInBounds([DataSourceRequest] DataSourceRequest request, DateTime? fromDate, DateTime? toDate, string commodity)
        {
            var result = salesService.GetInboundSales(fromDate, toDate, commodity);

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditInBounds([DataSourceRequest] DataSourceRequest request, InboundSalesViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                salesService.EditInboundSale(sale);
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
        /*
         * 
         *DIRECT SALES
         * 
         */
        public ActionResult AllDirects([DataSourceRequest] DataSourceRequest request, DateTime? fromDate, DateTime? toDate, string commodity)
        {
            var result = salesService.GetDiretSales(fromDate, toDate, commodity);

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditDirects([DataSourceRequest] DataSourceRequest request, DirectSalesViewModel sale)
        {
            if (sale != null && ModelState.IsValid)
            {
                salesService.EditDirectSale(sale);
                sale.Margin = Calculations.MarginCalc(sale.Qty, sale.UNITPRICE, sale.UNITCOST, sale.POFrghtRate);
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }



        public ActionResult GetSalesPeople()
        {
            return Json(salesService.GetAllSalesPeople(), JsonRequestBehavior.AllowGet);
        }

        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteSale([DataSourceRequest] DataSourceRequest request, SalesViewModel sale)
        {
            if (sale != null)
            {
                salesService.DeleteSale(sale);
            }
            return Json(new[] { sale }.ToDataSourceResult(request, ModelState));
        }
        */
        public ActionResult GetAllCommodities()
        {
            return Json(salesService.GetAllCommodities(), JsonRequestBehavior.AllowGet);
        }
    }
}
