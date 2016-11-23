using DirectOrderTracker.Models;
using DirectOrderTracker.Models.ViewModels;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DirectOrderTracker.Services
{
    public class SalesService : ISaleService
    {
        private OrdersEntities entities;
        public SalesService(OrdersEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<string> GetAllSalesPeople()
        {
            return entities.CustPOes.Where(s=>s.Salesperson != null).Select(s => s.Salesperson).Distinct();
        }

        public IEnumerable<CommodityViewModel> GetAllCommodities()
        {
            return entities.Products.Select(c => new CommodityViewModel {
                Commodity = c.Commodity,
                CommodityDesc = c.Commodity
            }).Distinct().OrderBy(x=>x.Commodity).ToList();
        }

        public IEnumerable<VendorViewModel> GetAllVendors()
        {
            return entities.Vendors.Select(v => new VendorViewModel { VendId = v.VendId, VendDesc = v.VendDesc }).OrderBy(v => v.VendDesc).ToList();
        }

        public IEnumerable<Carrier> GetAllCarriers()
        {
            return entities.Carriers.OrderBy(v => v.CarrDesc).ToList();
        }

        public IEnumerable<POStatu> GetAllStatuses()
        {
            return entities.POStatus.OrderBy(v => v.POStatusDesc).ToList();
        }

    }
}