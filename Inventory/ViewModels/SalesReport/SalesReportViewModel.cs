using System;

namespace Inventory.ViewModels.SalesReport
{
    public class SalesReportViewModel
    {
        public long SalesId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal GrandAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal ReturnAmount { get; set; }

    }
}
