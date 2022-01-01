using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels
{
    public class CustomerCreateViewModel
    {
        [Required]
        public long CusId { get; set; }

        [Required(ErrorMessage = "Full name is req.")]
        [Display(Name = "Full Name:")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Display(Name = "Address:")]
        [StringLength(150)]
        public string? Address { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Please provide your email")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is a Required Field!")]
        [Phone(ErrorMessage = "Phone Number is not Valid!")]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }
    }
}
