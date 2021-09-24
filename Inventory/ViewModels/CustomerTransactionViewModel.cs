using InventoryLibrary.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Inventory.ViewModels
{
    public class CustomerTransactionViewModel
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public IList<Customer> customers { get; set; } = new List<Customer>();

        public SelectList CustomerSelectList =>
            new SelectList(customers, nameof(Customer.CusId), nameof(Customer.FullName));
        public IList<CustomerTransactionModel> Transactions { get; set; } = new List<CustomerTransactionModel>();
    }
    public class CustomerTransactionModel
    {
        public DateTime TransactionDate { get; set; }
        public long TransactionId { get; set; }
        public string AmountType { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAmount { get; set; }
        public string BalanceType { get; set; }

    }
}
