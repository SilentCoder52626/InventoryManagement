using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Unit
{
    public class UnitIndexViewModel
    {

        [Required]
        [Display(Name = "Unit ID")]
        public long UnitId { get; set; }

        [Required(ErrorMessage = "Provide an Unit Name")]
        [Display(Name = "Unit Name")]
        public string Name { get; set; }

        public string Status { get; set; }
    }
}
