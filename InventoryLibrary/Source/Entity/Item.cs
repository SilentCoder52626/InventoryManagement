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
