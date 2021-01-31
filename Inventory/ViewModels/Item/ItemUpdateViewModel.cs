using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Item
{
    public class ItemUpdateViewModel
    {
        [Required]
        [Display(Name = "Item ID")]
        public long ItemId { get; set; }

        [Required(ErrorMessage = "Provide an Item Name")]
        [Display(Name = "Item Name")]
        public string Name { get; set; }


    }
}
