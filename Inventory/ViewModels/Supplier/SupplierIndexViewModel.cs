using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.Supplier
{
    public class SupplierIndexViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Full Name: ")]
        public string Name { get; set; }

        [Display(Name = "Address: ")]
        public string? Address { get; set; }

        [Display(Name = "Email: ")]
        public string? Email { get; set; }

        [Display(Name="Status:-")]
        public string Status { get; set; }

        [Display(Name = "Phone: ")]
        public string Phone { get; set; }

    }
}
