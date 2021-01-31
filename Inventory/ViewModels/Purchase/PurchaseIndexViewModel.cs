using Inventory.ViewModels.PurchaseDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Purchase
{
    public class PurchaseIndexViewModel
    {
        [Display(Name = "Purchase ID:-")]
        public long Id { get; set; }

        [Display(Name = "Supplier:")]
        public long SupplierId { get; set; }

        [Display(Name = "Supplier Name:-")]
        public string? SupplierName { get; set; }

        [Display(Name = "Item: ")]
        public long ItemId { get; set; }

        [Display(Name = "Item Name:- ")]
        public string? ItemName { get; set; }

        [Display(Name = "Total :- ")]
        public decimal Total { get; set; }

        [Display(Name = "Grand Total:-")]
        public decimal GrandTotal { get; set; }

        [Display(Name = "Discount:- ")]
        public decimal Discount { get; set; }

        [Display(Name = "VAT:- ")]
        public decimal Vat { get; set; }

        [Display(Name = "Date/Time")]
        public DateTime PurchaseDateTime { get; set; }

        [Display(Name = "Remarks:- ")]
        public string? Remarks { get; set; }
        public List<PurchaseDetailCreateViewModel>? PurchaseDetails { get; set; }

        public IList<InventoryLibrary.Entity.Supplier>? Suppliers { get; set; }

        public IList<InventoryLibrary.Entity.Item>? Items { get; set; }

    }
}
