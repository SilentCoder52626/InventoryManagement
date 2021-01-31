using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryLibrary.Entity
{

    public class Item
    {
        public const string StatusActive = "Active";
        public const string StatusInActive = "DeActivate";

        protected Item()
        {

        }
        public Item(string itemName)
        {
            Status = StatusActive;
            Name = itemName;
        }
        public void Update(string itemName)
        {
            Name = itemName;
        }


        public long Id { get; protected set; }


        public string Status { get; protected set; }

        public string Name { get; protected set; }


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
