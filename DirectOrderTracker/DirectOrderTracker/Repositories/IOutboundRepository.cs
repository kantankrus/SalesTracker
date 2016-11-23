using DirectOrderTracker.Models.ViewModels;
using System.Collections.Generic;

namespace DirectOrderTracker.Repositories
{
    public interface IOutboundRepository
    {
        IEnumerable<OutboundSalesViewModel> GetAllSales(string commodity);
        void EditSale(OutboundSalesViewModel sale);
        void SaveChanges();
    }
}
