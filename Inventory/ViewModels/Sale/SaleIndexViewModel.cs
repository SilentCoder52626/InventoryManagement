using Inventory.ViewModels.SaleDetail;
using InventoryLibrary.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Sale
{
    public class SaleIndexViewModel
    {
        public long SaleId { get; set; }

        [Display(Name = "Customer Name: -")]
        public long CusId { get; set; }

        public string? CustomerName { get; set; }

        [Display(Name = "Item Name:-")]
        public long ItemId { get; set; }

        public string? ItemName { get; set; }

        [Display(Name = "Total:- ")]
        [Required]
        public long total { get; set; }

        public long discount { get; set; }

        public long netTotal { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal vat { get; set; }

        public List<SaleDetailIndexViewModel> SalesDetails { get; set; }
        public IList<Customer> customers { get; set; } = new List<Customer>();

        public SelectList CustomerSelectList =>
            new SelectList(customers, nameof(Customer.CusId), nameof(Customer.FullName));
        public InventoryLibrary.Entity.Item? item { get; set; }
        public SelectList itemList => new SelectList(items, nameof(item.Id), nameof(item.Name));
        public IList<InventoryLibrary.Entity.Item> items { get; set; } = new List<InventoryLibrary.Entity.Item>();

    }
}
