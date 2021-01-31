namespace InventoryLibrary.Source.Dto.SaleDetail
{
    public class SaleDetailUpdateDTO
    {
        public long SaleDetailId { get; set; }

        public long SaleId { get; set; }

        public string CustomerName { get; set; }

        public string ItemName { get; set; }

        public int Qty { get; set; }
        public long Total { get; set; }

        public long NetTotal { get; set; }

        public long Discount { get; set; }
    }
}
