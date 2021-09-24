using Inventory.ViewModels.SaleDetail;
using System.Collections.Generic;

namespace Inventory.ViewModels.Sale
{
    public class SaleCreateViewModel
    {

        public long CusId { get; set; }

        public long netTotal { get; set; }

        public long discount { get; set; }
        public long paidAmount { get; set; }
        public long returnAmount { get; set; }

        public List<SaleDetailIndexViewModel> SalesDetails { get; set; } = new List<SaleDetailIndexViewModel>();


    }
}
