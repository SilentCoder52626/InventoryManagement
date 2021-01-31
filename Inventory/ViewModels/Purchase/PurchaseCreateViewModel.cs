using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Purchase
{
    public class PurchaseCreateViewModel
    {
        [Display(Name = "Purchase ID:-")]
        public long PchId { get; set; }

        [Display(Name = "Supplier ID:-")]
        public long SupId { get; set; }

        [Display(Name = "Item ID:- ")]
        public long ItemId { get; set; }

        [Display(Name = "Total :- ")]
        public long Total { get; set; }

        [Display(Name = "Net Total:-")]
        public long NetTotal { get; set; }

        [Display(Name = "Discount:- ")]
        public long Discount { get; set; }

        [Display(Name = "VAT:- ")]
        public long Vat { get; set; }

    }
}
