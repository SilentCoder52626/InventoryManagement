using Inventory.ViewModels.SalesReport;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class SalesReportController : Controller
    {
        SaleRepositoryInterface _salesRepo;
        CustomerRepositoryInterface _customerRepo;
        public SalesReportController(SaleRepositoryInterface salesRepo, CustomerRepositoryInterface customerRepo)
        {
            _salesRepo = salesRepo;
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var Sales = await _salesRepo.GetAllAsync();
            var vm = new List<SalesReportViewModel>();
            foreach (var sale in Sales)
            {
                vm.Add(new SalesReportViewModel()
                {
                    Amount = sale.total,
                    GrandAmount = sale.netTotal,
                    DueAmount = sale.dueAmount,
                    Discount = sale.discount,
                    SalesId = sale.SaleId,
                    CustomerName = sale.customer.FullName,
                    PaidAmount = sale.paidAmount,
                    ReturnAmount = sale.returnAmount,
                    TransactionDate = sale.SalesDate
                });
            }
            return View(vm);
        }
    }
}
