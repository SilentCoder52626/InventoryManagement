using System;
using System.Collections.Generic;
using System.Text;
using InventoryLibrary.Entity;

namespace InventoryLibrary.Source.Entity
{
    public class Unit
    {
        public const string StatusActive = "Active";
        public const string StatusInActive = "InActive";

        protected Unit()
        {

        }
        public Unit(string name)
        {
            this.Name = name;
            Enable();
        }
        public long Id { get; protected set; }
        public string Name { get; protected set; }
        public string Status { get; protected set; }
        public virtual ICollection<Item> Items { get; protected set; } = new List<Item>();

        public virtual void Update(string name)
        {
            this.Name = name;
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
