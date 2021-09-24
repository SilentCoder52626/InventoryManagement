namespace Inventory.ViewModels.PurchaseDetail
{
    public class PurchaseDetailCreateViewModel
    {
        public long Id { get; set; }
        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public long Qty { get; set; }
        public long Amount { get; set; }
        public long Rate { get; set; }
        public long SalesRate { get; set; }

    }
}
