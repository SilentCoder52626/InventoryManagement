using InventoryLibrary.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Sale
{
    public class SaleCreateViewModel
    {
        public long SaleId { get; set; }

        [Display(Name = "Customer Name: -")]
        public long CusId { get; set; }

        [Display(Name = "Item Name:-")]

        public long ItemId { get; set; }

        [Display(Name = "Time & Date:-")]

        public string timestamp { get; set; }

        public long total { get; set; }

        public long netTotal { get; set; }

        public long discount { get; set; }

        public decimal vat { get; set; }
        public IList<Customer> customers { get; set; } = new List<Customer>();

        public InventoryLibrary.Entity.Item item { get; set; }
        public SelectList itemList => new SelectList(items, nameof(item.Id), nameof(item.Name));
        public IList<InventoryLibrary.Entity.Item> items { get; set; } = new List<InventoryLibrary.Entity.Item>();

    }
}
