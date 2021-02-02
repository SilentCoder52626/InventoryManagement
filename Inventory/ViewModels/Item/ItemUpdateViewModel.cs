using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.ViewModels.Item
{
    public class ItemUpdateViewModel
    {
        [Required]
        [Display(Name = "Item ID")]
        public long ItemId { get; set; }
        [Required]
        [Display(Name = "Unit ID")]
        public long UnitId { get; set; }

        [Required(ErrorMessage = "Provide an Item Name")]
        [Display(Name = "Item Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Provide an Item Rate")]
        [Display(Name = "Item Rate")]
        public decimal Rate { get; set; }

        public IList<InventoryLibrary.Source.Entity.Unit> Units = new List<InventoryLibrary.Source.Entity.Unit>();

        public SelectList UnitSelectList => new SelectList(Units, nameof(InventoryLibrary.Source.Entity.Unit.Id),nameof(InventoryLibrary.Source.Entity.Unit.Name));

    }
}
