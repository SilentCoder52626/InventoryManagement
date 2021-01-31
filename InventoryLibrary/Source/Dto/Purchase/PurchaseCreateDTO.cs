using InventoryLibrary.Dto.PurchaseDetail;
using System.Collections.Generic;

namespace InventoryLibrary.Dto.Purchase
{

    public class PurchaseCreateDTO
    {

        public long PurchaseId { get; set; }
        public long SupplierId { get; set; }
        public long ItemId { get; set; }

        public decimal Total { get; set; }

        public decimal GrandTotal { get; set; }

        public string? Remarks { get; set; }

        public decimal Discount { get; set; }

        public decimal Vat { get; set; }

        public List<PurchaseDetailCreateDTO> PurchaseDetails { get; set; }

    }
}
