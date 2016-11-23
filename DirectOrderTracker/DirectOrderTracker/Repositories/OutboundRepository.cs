using DirectOrderTracker.Models;
using DirectOrderTracker.Repositories;
using DirectOrderTracker.Models.ViewModels;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DirectOrderTracker.Services
{
    public class OutboundRepository : IOutboundRepository
    {
        private OrdersEntities entities;
        public OutboundRepository(OrdersEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<OutboundSalesViewModel> GetAllSales(string commmodity)
        {
            return entities.CustPOes
                               .Where(x => x.WAREHOUSE != "~D")
                               .OrderByDescending(x => x.ModifyDate)
                                .Select(s => new OutboundSalesViewModel
                                {
                                    CustPOID = s.CustPOID,
                                    CustDesc = s.Customer.CustDesc,
                                    POBillToRef = s.POBillToRef,
                                    Qty = s.Qty.HasValue ? s.Qty.Value : default(int),
                                    UNITPRICE = s.UNITPRICE.HasValue ? s.UNITPRICE.Value : default(decimal),
                                    SalesOrder = s.SalesOrder,
                                    PPROREFDATE = s.PPROREFDATE,
                                    UNITCOST = s.UNITCOST.HasValue ? s.UNITCOST.Value : default(decimal),
                                    CustBillToID = s.CustBillToID,
                                    POStatusID = s.POStatusID,
                                    POComment = s.POComment,
                                    MainCommodity = s.Product.Commodity,
                                    Lot = s.LOT + s.SUBLOT,
                                    ProdDesc = s.Product.ProdDesc,
                                    TYPE = s.TYPE,
                                    WAREHOUSE = s.WAREHOUSE,
                                    Salesperson = s.Salesperson
                                })
                            .ToList();
        }

        public void EditSale(OutboundSalesViewModel sale)
        {
            var existingSale = entities.CustPOes.Find(sale.CustPOID);
            existingSale.POComment = sale.POComment;

            entities.Entry(existingSale).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            entities.SaveChanges();
        }
    }
}