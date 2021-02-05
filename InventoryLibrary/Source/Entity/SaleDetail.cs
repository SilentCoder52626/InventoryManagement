using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Entity
{
    public class SaleDetails
    {
        public long SaleDetailId { get; set; }

        public long SaleId { get; set; }

        public string ItemName { get; set; }
        public long ItemId { get; set; }

        public int Qty { get; set; }

        public decimal Total { get; set; }

        public long Price { get; set; }

        public virtual Sale Sales { get; set; }


    }
}
