using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryLibrary.Entity
{
    public class Sale
    {
        private readonly ILazyLoader _lazyLoader;



        [Key]
        [Required]
        public long SaleId { get; set; }

        public long CusId { get; set; }


        public DateTime? timestamp { get; set; }

        public decimal vat { get; set; }

        public long total { get; set; }

        public long netTotal { get; set; }

        public long discount { get; set; }

        [ForeignKey("CusId")]
        public virtual Customer Customers { get; set; }

    }
}
