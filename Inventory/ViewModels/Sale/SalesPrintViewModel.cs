using Inventory.ViewModels.SaleDetail;
using System;
using System.Collections.Generic;

namespace Inventory.ViewModels.Sale
{
    public class SalesPrintViewModel
    {
        public long SaleId { get; set; }

        public long CusId { get; set; }

        public string? CustomerName { get; set; }

        public long ItemId { get; set; }

        public string? ItemName { get; set; }

        public long total { get; set; }

        public long discount { get; set; }

        public long netTotal { get; set; }
        public long paidAmount { get; set; }

        public long returnAmount { get; set; }
        public long dueAmount { get; set; }


        public DateTime date { get; set; }

        public List<SaleDetailIndexViewModel> SalesDetails { get; set; } = new List<SaleDetailIndexViewModel>();
    }
}
