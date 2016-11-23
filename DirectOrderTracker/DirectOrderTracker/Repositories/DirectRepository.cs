using DirectOrderTracker.Models;
using DirectOrderTracker.Models.ViewModels;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;

namespace DirectOrderTracker.Services
{
    public class DirectRepository : IDirectRepository
    {
        private OrdersEntities entities;
        public DirectRepository(OrdersEntities entities)
        {
            this.entities = entities;
        }
        public IEnumerable<DirectSalesViewModel> GetAllSales(string commodity)
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
                              .ToList();
                foreach (var result in Results)
                {
                    result.LoadDate = DateTime.ParseExact(result.PPROREFDATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                return Results;
            }
            //Process the results and turn the string PPROREFDATE into a valid DateTime type.
        }

        public void EditSale(DirectSalesViewModel sale)
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
            if (sale.VendId != null & sale.VendId > 0)
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
        }

        public void SaveChanges()
        {
            entities.SaveChanges();
        }
    }
}