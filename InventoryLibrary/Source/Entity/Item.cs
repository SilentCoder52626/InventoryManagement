using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Entity
{

    public class Item
    {
        public const string StatusActive = "Active";
        public const string StatusInActive = "InActive";

        protected Item()
        {

        }
        public Item(Unit unit, string itemName, decimal rate)
        {
            Status = StatusActive;
            Name = itemName;
            Rate = rate;
            this.Unit = unit;
        }
        public void Update(Unit unit, string itemName, decimal rate)
        {
            this.Unit = unit;
            Name = itemName;
            this.Rate = rate;
        }
        public long UnitId { get; protected set; }
        public virtual Unit Unit { get; protected set; }
        public long Id { get; protected set; }


        public string Status { get; protected set; }

        public string Name { get; protected set; }
        public decimal Rate { get; protected set; }
        public long AvailableQty { get; protected set; }

        public virtual void AddQty(long qty)
        {
            this.AvailableQty += qty;
        }
        public virtual void UpdateRate(decimal rate)
        {
            this.Rate = rate;
        }
        public virtual void DecreaseQty(long qty)
        {
            if (this.AvailableQty < qty)
            {
                throw new Exception($"Not Enough item quantity of {this.Name}.");
            }
            this.AvailableQty -= qty;
        }

        public virtual void Enable()
        {
            Status = StatusActive;
        }
        public virtual void Disable()
        {
            Status = StatusInActive;
        }

        public virtual bool IsActive()
        {
            return Status == StatusActive;
        }

    }
}
