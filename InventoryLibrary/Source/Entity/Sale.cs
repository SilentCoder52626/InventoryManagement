using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryLibrary.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InventoryLibrary.Source.Entity
{
    public class Sale
    {
        public long SaleId { get; set; }

        public long CusId { get; set; }

        public DateTime SalesDate { get; set; }

        public long total { get; set; }

        public long netTotal { get; set; }

        public long discount { get; set; }
        
        public virtual ICollection<SaleDetails> SalesDetails { get; set; } = new List<SaleDetails>();

    }
}
