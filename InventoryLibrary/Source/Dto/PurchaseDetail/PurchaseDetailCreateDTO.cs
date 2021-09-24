namespace InventoryLibrary.Dto.PurchaseDetail
{
    public class PurchaseDetailCreateDTO
    {
        public long PchDetailId { get; set; }
        public long PchId { get; set; }
        public long ItemId { get; set; }
        public long Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal SalesRate { get; set; }

    }
}
