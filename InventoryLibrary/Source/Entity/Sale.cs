using InventoryLibrary.Entity;
using System;
using System.Collections.Generic;

namespace InventoryLibrary.Source.Entity
{
    public class Sale
    {
        public virtual long SaleId { get; set; }

        public virtual long CusId { get; set; }
        public virtual Customer customer { get; set; }

        public virtual DateTime SalesDate { get; set; }

        public virtual long total { get; set; }

        public virtual long netTotal { get; set; }

        public virtual long discount { get; set; }
        public virtual long paidAmount { get; set; }
        public virtual long returnAmount { get; set; }
        public virtual long dueAmount { get; set; }


        public virtual ICollection<SaleDetails> SalesDetails { get; set; } = new List<SaleDetails>();

    }
}
