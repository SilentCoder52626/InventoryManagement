namespace InventoryLibrary.Source.Dto.SaleDetail
{
    public class SaleDetailCreateDTO
    {

        public long SaleId { get; set; }

        public string ItemName { get; set; }
        public long ItemId { get; set; }

        public int Qty { get; set; }

        public decimal Total { get; set; }

        public long Price { get; set; }
    }
}
