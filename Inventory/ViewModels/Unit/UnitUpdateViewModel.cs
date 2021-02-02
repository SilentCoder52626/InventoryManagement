using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Unit
{
    public class UnitUpdateViewModel
    {
        [Required]
        [Display(Name = "Unit ID")]
        public long UnitId { get; set; }

        [Required(ErrorMessage = "Provide an Unit Name")]
        [Display(Name = "Unit Name")]
        public string Name { get; set; }


    }
}
