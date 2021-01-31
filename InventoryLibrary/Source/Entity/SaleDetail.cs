using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryLibrary.Entity
{
    public class SaleDetails
    {

        [Key]
        public long SaleDetailId { get; set; }

        public long SaleId { get; set; }

        public string CustomerName { get; set; }

        public string ItemName { get; set; }

        public int Qty { get; set; }

        public long Total { get; set; }

        public long Price { get; set; }

        [ForeignKey("SaleId")]
        public virtual Sale Sales { get; set; }


    }
}
