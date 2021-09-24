namespace InventoryLibrary.Source.Dto.CustomerTransaction
{
    public class CustomerTransactionCreateDto
    {
        public long CustomerId { get; set; }

        public decimal Amount { get; set; }
        public string Type { get; set; }
        public long ExtraId { get; set; }
    }
}
