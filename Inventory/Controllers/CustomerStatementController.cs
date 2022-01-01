using Inventory.ViewModels;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class CustomerStatementController : Controller
    {
        CustomerTansactionRepositoryInterface _transactionRepo;
        CustomerRepositoryInterface _customerRepo;

        public CustomerStatementController(CustomerTansactionRepositoryInterface transactionRepo, CustomerRepositoryInterface customerRepo)
        {
            _transactionRepo = transactionRepo;
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Index(long customerId = 0)
        {

            CustomerTransactionViewModel vm = new CustomerTransactionViewModel
            {
                CustomerId = customerId,
                customers = await _customerRepo.GetAllAsync()
            };
            if (customerId > 0)
            {
                var Customer = await _customerRepo.GetById(customerId).ConfigureAwait(false);
                vm.CustomerName = Customer.FullName;

                var Transactions = await _transactionRepo.GetAllTransactionOfCustomer(customerId).ConfigureAwait(false);
                foreach (var t in Transactions)
                {
                    var tt = new CustomerTransactionModel()
                    {
                        TransactionDate = t.TransactionDate,
                        Amount = t.Amount,
                        AmountType = t.AmountType,
                        TransactionId = t.Id,
                        Type = t.Type,
                        BalanceAmount = 0,
                        BalanceType = ""
                    };

                    vm.Transactions.Add(tt);
                }
                var CustomerStatementModelsWithBalance = new List<CustomerTransactionModel>();
                for (int i = 0; i < vm.Transactions.Count; i++)
                {
                    var CurrentData = vm.Transactions[i];
                    var CurrentIndex = i;
                    int PreviousIndex = CurrentIndex - 1;
                    decimal PreviousBalance = 0;

                    var StatementModel = new CustomerTransactionModel()
                    {
                        TransactionId = CurrentData.TransactionId,
                        Amount = CurrentData.Amount,
                        TransactionDate = CurrentData.TransactionDate,
                        Type = CurrentData.Type,
                        AmountType = CurrentData.AmountType
                    };
                    if (i > 0)
                    {
                        PreviousBalance = CustomerStatementModelsWithBalance[PreviousIndex].BalanceAmount;
                    }

                    var BalanceAmount = StatementModel.AmountType == CustomerTransaction.TypeDebit
                        ? PreviousBalance + StatementModel.Amount
                        : PreviousBalance - StatementModel.Amount;
                    StatementModel.BalanceType = BalanceAmount < 0 ? "" : "(Due)";
                    StatementModel.BalanceAmount = Math.Abs(BalanceAmount);

                    CustomerStatementModelsWithBalance.Add(StatementModel);

                }
                vm.Transactions = CustomerStatementModelsWithBalance;

            }
            return View(vm);
        }
    }
}
