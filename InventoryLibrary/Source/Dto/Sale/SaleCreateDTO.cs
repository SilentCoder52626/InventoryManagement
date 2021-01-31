using InventoryLibrary.Source.Dto.SaleDetail;
using System;
using System.Collections.Generic;

namespace InventoryLibrary.Dto.Sale
{
    public class SaleCreateDTO
    {


        public long CusId { get; set; }

        public long ItemId { get; set; }

        public DateTime? timestamp { get; set; }

        public long total { get; set; }

        public long discount { get; set; }
        public long netTotal { get; set; }

        public decimal vat { get; set; }

        public List<SaleDetailCreateDTO> SaleDetails { get; set; }


    }
}
