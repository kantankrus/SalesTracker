using DirectOrderTracker.Models.ViewModels;
using System.Collections.Generic;

namespace DirectOrderTracker.Services
{
    public interface IDirectRepository
    {
       IEnumerable<DirectSalesViewModel> GetAllSales(string commodity);
       void EditSale(DirectSalesViewModel sale);
       void SaveChanges();
    }
}