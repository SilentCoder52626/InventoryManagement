using System;

namespace InventoryLibrary.Entity
{
    public class CustomerTransaction
    {
        public const string TypePayment = "PAYMENT";
        public const string TypeSales = "SALES";
        public const string TypeDebit = "DEBIT";
        public const string TypeCredit = "CREDIT";

        protected CustomerTransaction()
        {

        }
        public CustomerTransaction(Customer customer, decimal amount, string type, long extraId)
        {
            Customer = customer;
            CustomerId = customer.CusId;
            Amount = amount;
            Type = type;
            TransactionDate = DateTime.Now;
            if (type == TypeSales)
            {
                AmountType = TypeDebit;
            }
            else
            {
                AmountType = TypeCredit;
            }
            ExtraId = extraId;
        }
        public virtual long Id { get; set; }
        public virtual long CustomerId
        {
            get; set;
        }

        public virtual Customer Customer { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string Type { get; set; }
        public virtual string AmountType { get; set; }
        // Sales => Sales Id 
        // If Seperate Payment => Payment Id :TODO:
        public virtual long ExtraId { get; set; }

    }
}
