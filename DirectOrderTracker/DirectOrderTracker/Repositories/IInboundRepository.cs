using DirectOrderTracker.Models.ViewModels;
using System.Collections.Generic;

namespace DirectOrderTracker.Repositories
{
    public interface IInboundRepository
    {
        IEnumerable<InboundSalesViewModel> GetAllSales(string commodity);
        void EditSale(InboundSalesViewModel sale);
        void SaveChanges();
    }
}
