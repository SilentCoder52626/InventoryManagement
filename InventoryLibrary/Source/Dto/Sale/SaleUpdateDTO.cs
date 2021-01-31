namespace InventoryLibrary.Dto.Sale
{
    public class SaleUpdateDTO
    {

        public long SaleId { get; set; }

        public long CusId { get; set; }

        public long ItemId { get; set; }

        public string timestamp { get; set; }

        public long total { get; set; }

        public long discount { get; set; }
        public decimal vat { get; set; }

    }
}
