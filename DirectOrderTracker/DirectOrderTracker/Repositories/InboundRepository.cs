using DirectOrderTracker.Models;
using DirectOrderTracker.Models.ViewModels;
using DirectOrderTracker.Repositories;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DirectOrderTracker.Services
{
    public class InboundRepository : IInboundRepository
     {
        private OrdersEntities entities;
        public InboundRepository(OrdersEntities entities)
        {
            this.entities = entities;
        }
        public IEnumerable<InboundSalesViewModel> GetAllSales(string commodity)
        {
            return entities.CustPOLines
                           .Where(x => x.WAREHOUSE != "~D")
                           .OrderByDescending(x => x.ModifyDate)
                            .Select(s => new InboundSalesViewModel
                            {
                                CustPOLineID = s.CustPOLineID,
                                VendDesc = s.Vendor.VendDesc,
                                FreshouseRefNum = s.FreshouseRefNum,
                                Qty = s.Qty.HasValue ? s.Qty.Value : default(decimal),
                                Cost = s.Cost.HasValue ? s.Cost.Value : default(decimal),
                                CostAlt = default(decimal),
                                PPROREFDATE = s.PPROREFDATE,
                                CarrierDesc = s.CarrierDesc,
                                LineFrghtRate = s.LineFrghtRate.HasValue ? s.Cost.Value : default(decimal),
                                VendPurchPONum = s.VendPurchPONum,
                                POLineComment = s.POLineComment,
                                Commodity = s.Product.Commodity,
                                LoadNumber = s.LoadNumber,
                                ProdDesc = s.Product.ProdDesc,
                                TYPE = s.TYPE,
                                Buyer = s.Buyer
                            })
                        .ToList();
        }

        public void EditSale(InboundSalesViewModel sale) {
            var existingSale = entities.CustPOLines.Find(sale.CustPOLineID);
            existingSale.POLineComment = sale.POLineComment;
            entities.Entry(existingSale).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            entities.SaveChanges();
        }

    }
}