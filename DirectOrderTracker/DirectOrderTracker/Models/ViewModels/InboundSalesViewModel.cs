using System.ComponentModel.DataAnnotations;

namespace DirectOrderTracker.Models.ViewModels
{
    public class InboundSalesViewModel
    {
        public int CustPOLineID { get; set; }
        [Display(Name = "Comment")]
        public string POLineComment { get; set; }
        public decimal Qty { get; set; }
        [Display(Name = "Product")]
        public string ProdDesc { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Arrival Date")]
        public string PPROREFDATE { get; set; }
        [Display(Name = "FH Ref #")]
        public string FreshouseRefNum { get; set; }
        [Display(Name = "Vendor")]
        public string VendDesc { get; set; }
        [Display(Name = "FOB")]
        public decimal Cost { get; set; }
        [Display(Name = "Dlvrd Cost")]
        public decimal CostAlt { get; set; }
        [Display(Name = "Carrier")]
        public string CarrierDesc { get; set; }
        [Display(Name = "Rate")]
        public decimal LineFrghtRate { get; set; }
        [Display(Name = "Load #")]
        public string LoadNumber { get; set; }
        [Display(Name = "Vendor Ref#")]
        public string VendPurchPONum { get; set; }
        public string Commodity { get; set; }
        public string Buyer { get; set; }
        [Display(Name = "Type")]
        public string TYPE { get; set; }
    }
}