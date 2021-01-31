using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Sale
{
    public class SaleUpdateViewModel
    {
        public long SaleId { get; set; }

        [Display(Name = "Customer Name: -")]
        public long CusId { get; set; }

        [Display(Name = "Item Name:-")]
        public long ItemId { get; set; }

        [Display(Name = "Total:- ")]
        [Required]
        public long total { get; set; }

        public long discount { get; set; }

        public long netTotal { get; set; }

        [Display(Name = "Time & Date:-")]
        public DateTime? timestamp { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal vat { get; set; }
    }
}
