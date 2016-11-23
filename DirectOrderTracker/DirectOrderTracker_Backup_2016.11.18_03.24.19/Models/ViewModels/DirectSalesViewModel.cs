using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace DirectOrderTracker.Models.ViewModels
{
    public class DirectSalesViewModel
    {
        public int CustPOID { get; set; }
        [Display(Name = "Comment")]
        public string POComment { get; set; }
        [Display(Name = "Customer")]
        public string CustDesc { get; set; }
        [Display(Name = "Cust Code")]
        public string CustCode { get; set; }
        [Display(Name = "Customer PO#")]
        public string POBillToRef { get; set; }
        [Display(Name = "FH SO#")]
        public string SalesOrder { get; set; }
        [Display(Name = "Load Date")]
        [DataType(DataType.Date)]
        public string PPROREFDATE { get; set; }
        public decimal? Qty { get; set; }
        [Display(Name = "Product")]
        public string ProdDesc { get; set; }
        [Display(Name = "Price")]
        public decimal? UNITPRICE { get; set; }
        [Display(Name = "Cost")]
        public decimal? UNITCOST { get; set; }
        public string Commodity { get; set; }
        public string SalesPerson { get; set; }
        [StringLength(12)]
        public string ApptNum { get; set; }
        [Display(Name = "Arrival Date")]
        [DataType(DataType.Date)]
        public DateTime? ArrivalDate { get; set; }
        [Display(Name = "Load Date")]
        [DataType(DataType.Date)]
        public DateTime? LoadDate { get; set; }
        public int? VendId { get; set; }
        public string VendorPO { get; set; }
        [Display(Name = "Carrier")]
        public int? CarrId { get; set; }
        [Display(Name = "Rate")]
        [Range(0, 9999999)]
        public decimal? POFrghtRate { get; set; }
        public decimal? Margin { get; set; }
        [Display(Name = "Status")]
        public byte? POStatusID { get; set; }

    }
}