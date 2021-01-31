using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Supplier
{
    public class SupplierUpdateViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Full Name: ")]
        [Required(ErrorMessage = "Please Provide a Full Name")]
        public string Name { get; set; }

        [Display(Name = "Address: ")]
        public string? Address { get; set; }

        [Display(Name = "Email: ")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string? Email { get; set; }

        [Display(Name = "Phone: ")]
        [Required(ErrorMessage = "Please Provide a Phone number")]
        [Phone(ErrorMessage = "Phone number is not valid!")]
        public string Phone { get; set; }
    }
}
