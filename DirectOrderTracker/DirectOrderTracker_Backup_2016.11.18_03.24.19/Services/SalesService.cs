using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DirectOrderTracker.Models;
using DirectOrderTracker.Models.ViewModels;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using System.Globalization;

namespace DirectOrderTracker.Services
{
    public class SalesService
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
        public IList<DirectSalesViewModel> GetDiretSales(DateTime? fromDate, DateTime? toDate, string commodity)
        {

            if (commodity != "All")
            {
               var Results = entities.CustPOes
                            .Where(x => x.WAREHOUSE == "~D")
                            .OrderByDescending(x => x.ModifyDate)
                             .Select(s => new DirectSalesViewModel
                             {
                                 CustPOID = s.CustPOID,
                                 CustDesc = s.Customer.CustDesc,
                                 POBillToRef = s.POBillToRef,
                                 Qty = s.Qty.HasValue ? s.Qty.Value : default(int),
                                 UNITPRICE = s.UNITPRICE.HasValue ? s.UNITPRICE.Value : default(decimal),
                                 UNITCOST = s.UNITCOST.HasValue ? s.UNITCOST.Value : default(decimal),
                                 SalesOrder = s.SalesOrder,
                                 PPROREFDATE = s.PPROREFDATE,
                                 CustCode = s.Customer.CustCode,
                                 POComment = s.POComment,
                                 Commodity = s.Product.Commodity,
                                 ProdDesc = s.Product.ProdDesc,
                                 SalesPerson = s.Salesperson,
                                 ApptNum = s.ApptNum,
                                 VendId = s.VendId.HasValue ? s.VendId.Value : default(int),
                                 VendorPO = s.VendorPO,
                                 CarrId = s.CarrId.HasValue ? s.CarrId.Value : default(int),
                                 POFrghtRate = s.POFrghtRate,
                                 ArrivalDate = s.ArrivalDate,
                                 POStatusID = s.POStatusID.HasValue ? s.POStatusID.Value : default(byte),
                                 Margin = ((s.Qty * s.UNITPRICE) - (s.Qty * s.UNITCOST)) - s.POFrghtRate
                             })
                            //.Where(x => x.ArrivalDate >= fromDate && x.ArrivalDate <= toDate)
                            .Where(x => x.Commodity == commodity)
                            .ToList();
                foreach (var result in Results)
                {
                    result.LoadDate = DateTime.ParseExact(result.PPROREFDATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                return Results;
            }
            else
            {
                  var   Results = entities.CustPOes
                               .Where(x => x.WAREHOUSE == "~D")
                               .OrderByDescending(x => x.ModifyDate)
                                .Select(s => new DirectSalesViewModel
                                {
                                    CustPOID = s.CustPOID,
                                    CustDesc = s.Customer.CustDesc,
                                    POBillToRef = s.POBillToRef,
                                    Qty = s.Qty.HasValue ? s.Qty.Value : default(int),
                                    UNITPRICE = s.UNITPRICE.HasValue ? s.UNITPRICE.Value : default(decimal),
                                    UNITCOST = s.UNITCOST.HasValue ? s.UNITCOST.Value : default(decimal),
                                    SalesOrder = s.SalesOrder,
                                    PPROREFDATE = s.PPROREFDATE,
                                    CustCode = s.Customer.CustCode,
                                    POComment = s.POComment,
                                    Commodity = s.Product.Commodity,
                                    ProdDesc = s.Product.ProdDesc,
                                    SalesPerson = s.Salesperson,
                                    ApptNum = s.ApptNum,
                                    VendId = s.VendId.HasValue ? s.VendId.Value : default(int),
                                    VendorPO = s.VendorPO,
                                    CarrId = s.CarrId.HasValue ? s.CarrId.Value : default(int),
                                    POFrghtRate = s.POFrghtRate,
                                    ArrivalDate = s.ArrivalDate,
                                    POStatusID = s.POStatusID.HasValue ? s.POStatusID.Value : default(byte),
                                    Margin = ((s.Qty * s.UNITPRICE) - (s.Qty * s.UNITCOST)) - s.POFrghtRate
                                })
                                .ToList();
                foreach (var result in Results)
                {
                    result.LoadDate = DateTime.ParseExact(result.PPROREFDATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                return Results;
            }
            //Process the results and turn the string PPROREFDATE into a valid DateTime type.
                  
        }

        public void EditDirectSale(DirectSalesViewModel sale)
        {
            var existingSale = entities.CustPOes.Find(sale.CustPOID);
            existingSale.POComment = sale.POComment;
            existingSale.ApptNum = sale.ApptNum;
            existingSale.VendorPO = sale.VendorPO;
            existingSale.CarrId = sale.CarrId;
            existingSale.POFrghtRate = sale.POFrghtRate;
            existingSale.ArrivalDate = sale.ArrivalDate;
            existingSale.POStatusID = sale.POStatusID;
            //Vendor Drop Down
            if (sale.VendId != null & sale.VendId >0)
            {
                existingSale.VendId = sale.VendId;
            }
            else
            {
                existingSale.VendId = 1;
            }
            //Carrier Drop Down
            if (sale.CarrId != null & sale.CarrId > 0)
            {
                existingSale.CarrId = sale.CarrId;
            }
            else
            {
                existingSale.CarrId = 1;
            }

            entities.Entry(existingSale).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public IList<OutboundSalesViewModel> GetOutBoundSales(DateTime? fromDate, DateTime? toDate, string commodity)
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

        public void EditOuboundSale(OutboundSalesViewModel sale)
        {
            var existingSale = entities.CustPOes.Find(sale.CustPOID);
            existingSale.POComment = sale.POComment;

            entities.Entry(existingSale).State = EntityState.Modified;
            entities.SaveChanges();
        }
        public IList<InboundSalesViewModel> GetInboundSales(DateTime? fromDate, DateTime? toDate, string commodity)
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
                                CostAlt = s.CostAlt.HasValue ? s.Cost.Value : default(decimal),
                                PPROREFDATE = s.PPROREFDATE,
                                CarrierDesc = s.CarrierDesc,
                                LineFrghtRate = s.LineFrghtRate.HasValue ? s.Cost.Value : default(decimal),
                                VendPurchPONum= s.VendPurchPONum,
                                POLineComment = s.POLineComment,
                                Commodity = s.Product.Commodity,
                                LoadNumber = s.LoadNumber,
                                ProdDesc = s.Product.ProdDesc,
                                TYPE = s.TYPE,
                                Buyer = s.Buyer
                            })
                        .ToList();
        }

        public void EditInboundSale(InboundSalesViewModel sale)
        {
            var existingSale = entities.CustPOLines.Find(sale.CustPOLineID);
            existingSale.POLineComment = sale.POLineComment;

            entities.Entry(existingSale).State = EntityState.Modified;
            entities.SaveChanges();
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