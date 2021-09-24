using InventoryLibrary.Source.Entity;
using System;
using System.Collections.Generic;

namespace InventoryLibrary.Entity
{
    public class Purchase
    {
        protected Purchase()
        {

        }

        public Purchase(Supplier suppliers, decimal discount)
        {
            Suppliers = suppliers;
            Discount = discount;
            PurchaseDateTime = DateTime.Now;
        }

        public long Id { get; protected set; }

        public decimal Total { get; protected set; }

        public decimal GrandTotal { get; protected set; }

        public decimal Discount { get; protected set; }

        public DateTime PurchaseDateTime { get; set; }

        public long SupplierId { get; set; }
        public virtual Supplier Suppliers { get; set; }

        public virtual void AddPurchaseDetails(Item item, long qty, decimal rate, decimal salesRate)
        {
            var amount = qty * rate;
            PurchaseDetails.Add(new PurchaseDetail(this, item, qty, rate, salesRate));
            Calculate(amount);
        }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

        public void Calculate(decimal amount)
        {
            Total += amount;
            GrandTotal = Total - Discount;
        }

    }
}
