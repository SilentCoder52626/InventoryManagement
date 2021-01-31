using System.ComponentModel.DataAnnotations;

namespace InventoryLibrary.Entity
{
    public class Customer
    {

        public Customer(string fullName)
        {
            FullName = fullName;
        }
        public void Update(string fullname)
        {
            FullName = fullname;
        }

        [Key]
        [Required]
        public long CusId { get; protected set; }

        [Required(ErrorMessage = "Full name is req.")]
        [Display(Name = "FUll NAME")]
        [StringLength(50)]
        public string FullName { get; protected set; }

        [Display(Name = "Address")]
        [StringLength(150)]
        public string Address { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Please provide your email")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Phone Number:")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

    }
}
