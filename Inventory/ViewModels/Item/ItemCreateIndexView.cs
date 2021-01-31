using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Item
{
    public class ItemCreateIndexView
    {
        [Required]
        [Display(Name = "Item ID")]
        public long ItemId { get; set; }

        [Required(ErrorMessage = "Provide an Item Name")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Provide a Price")]
        [Display(Name = "Item Price")]
        public long Price { get; set; }
    }
}
