using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryLibrary.Entity
{
    public class Supplier
    {
        private const string StatusActive = "Active";
        private const string StatusInActive = "InActive";

        public Supplier(string name, string address, string email, string phone)
        {
            Status = StatusActive;
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }   

        public void Update(string name, string address, string email, string phone)
        {
            Name = name;
            Address = address;
            Email = email;
            Phone = phone;
        }

        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Address { get; protected set; }


        public string Email { get; protected set; }

        public string Phone { get; protected set; }

        public string Status { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

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
