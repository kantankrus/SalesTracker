using DirectOrderTracker.Models;
using DirectOrderTracker.Models.ViewModels;
using System.Collections.Generic;

namespace DirectOrderTracker.Services
{
    public interface ISaleService
    {
        IEnumerable<string> GetAllSalesPeople();
        IEnumerable<CommodityViewModel> GetAllCommodities();
        IEnumerable<VendorViewModel> GetAllVendors();
        IEnumerable<Carrier> GetAllCarriers();
        IEnumerable<POStatu> GetAllStatuses();
    }
}
