using System;
using System.ComponentModel.DataAnnotations;

namespace DirectOrderTracker.Models.ViewModels
{
    public class SalesViewModel
    {
        public string CustDesc { get; set; }
        public int CustPOLineID { get; set; }
        [StringLength(25)]
        public string PurchaseOrderNum { get; set; }
        [StringLength(12)]
        public string SalesOrderNum { get; set; }
        [StringLength(12)]
        public string Appt { get; set; }
        [Range(0, 9999999)]
        public int? Qty { get; set; }
        [Range(0, 9999999)]
        public decimal? Price { get; set; }
        [Range(0, 9999999)]
        public decimal? Cost { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LoadDate { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "An arrival date is required to ensure the new sale isn't filtered out accidently. Please enter an estimated arrival date. You can always change this later.")]
        public DateTime? ArrivalDate { get; set; }
        [StringLength(14)]
        public string VendRefNum { get; set; }
        [Range(0, 9999999)]
        public decimal? FreightRate { get; set; }
        public decimal? Margin { get; set; }
        public string Commodity { get; set; }
        public int? VendId { get; set; }
        public short? CarrId { get; set; }
        public int? ProdID { get; set; }
        public short? CustID { get; set; }
        public byte? POLineStatusID { get; set; }
        public byte? AlertReasonID { get; set; }
        [StringLength(500)]
        public string POLineComment { get; set; }
    }
    public class CommodityViewModel
    {
        public string Commodity { get; set; }
        public string CommodityDesc { get; set; } 
    }

    public class SalesPersonVeiewModel
    {
        public string SalesPerson { get; set; }
    }
}