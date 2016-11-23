using System.ComponentModel.DataAnnotations;

namespace DirectOrderTracker.Models.ViewModels
{
    public class OutboundSalesViewModel
    {
        public byte? POStatusID { get; set; }
        public int CustPOID { get; set;}
        public short? CustBillToID { get; set; }
        public string Appt { get; set;}
        public string CustDesc { get; set;}
        [Display(Name = "Customer PO")]
        public string POBillToRef { get; set; }
        [Display(Name = "FH S/O")]
        public string SalesOrder { get; set; }
        [Display(Name = "Ship Date")]
        public string PPROREFDATE { get; set; }
        public string Lot { get; set; }
        public string Sublot { get; set; }
        [Display(Name = "Qty")]
        public decimal Qty { get; set; }
        [Display(Name = "Product")]
        public string ProdDesc { get; set; }
        [Display(Name = "Price")]
        public decimal UNITPRICE { get; set; }
        [Display(Name = "Cost")]
        public decimal UNITCOST { get; set; }
        [Display(Name = "Type")]
        public string TYPE { get; set; }
        [Display(Name = "Commodity")]
        public string MainCommodity { get; set; }
        [Display(Name = "Warehouse")]
        public string WAREHOUSE { get; set; }
        public string Salesperson { get; set; }
        [Display(Name = "Product #")]
        public int ProdNum { get; set; }
        [Display(Name = "Comment")]
        public string POComment { get; set; } 


    }
}