
namespace InventoryLibrary.Source.DTO.Customer
{
    public class CustomerCreateDTO
    {
        public long CusId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
    }
}
