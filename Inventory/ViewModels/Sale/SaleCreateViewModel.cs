using InventoryLibrary.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Inventory.ViewModels.SaleDetail;

namespace Inventory.ViewModels.Sale
{
    public class SaleCreateViewModel
    {

        public long CusId { get; set; }

        public long netTotal { get; set; }

        public long discount { get; set; }
        
        public List<SaleDetailIndexViewModel> SalesDetails { get; set; } = new List<SaleDetailIndexViewModel>();
        

    }
}
