using InventoryLibrary.Entity;

namespace InventoryLibrary.Source.Entity
{

    public class PurchaseDetail
    {
        protected PurchaseDetail()
        {

        }
        public PurchaseDetail(Purchase purchase, Item item, long qty, decimal rate, decimal salesRate)
        {
            Purchase = purchase;
            Items = item;
            Qty = qty;
            Amount = qty * rate;
            Rate = rate;
            SalesRate = salesRate;
        }

        public long Id { get; protected set; }

        public long Qty { get; protected set; }

        public decimal Amount { get; set; }

        public decimal Rate { get; set; }
        public decimal SalesRate { get; set; }


        public long PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

        public long ItemId { get; set; }
        public virtual Item Items { get; set; }
    }
}
