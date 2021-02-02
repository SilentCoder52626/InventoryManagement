using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Item
{
    public class ItemIndexViewModel
    {

        [Required]
        [Display(Name = "Item ID")]
        public long ItemId { get; set; }

        [Required(ErrorMessage = "Provide an Item Name")]
        [Display(Name = "Item Name")]
        public string Name { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }
        [Required(ErrorMessage = "Provide an Item Rate")]
        [Display(Name = "Item Rate")]
        public decimal Rate { get; set; }

        public string Status { get; set; }

    }
}
