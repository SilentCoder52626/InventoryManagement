using Inventory.ViewModels.Sale;
using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Dto.SaleDetail;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class SaleController : Controller
    {
        private readonly SaleRepositoryInterface _saleRepo;
        private readonly IToastNotification _toastNotification;
        private readonly CustomerRepositoryInterface _customerRepo;
        private readonly ItemRepositoryInterface _itemRepo;
        private readonly SaleServiceInterface _saleService;
        private readonly SaleDetailRepositoryInterface _saleDetailRepo;

        public SaleController(SaleRepositoryInterface _saleRepo,
                              IToastNotification _toastNotification,
                              CustomerRepositoryInterface customerRepo,
                              ItemRepositoryInterface _itemRepo,
                              SaleServiceInterface _saleService,
                              SaleDetailRepositoryInterface _saleDetailRepo)
        {
            this._saleRepo = _saleRepo;
            _customerRepo = customerRepo;
            this._itemRepo = _itemRepo;
            this._toastNotification = _toastNotification;
            this._saleService = _saleService;
            this._saleDetailRepo = _saleDetailRepo;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var sales = (await _saleRepo.GetQueryable().ConfigureAwait(true)).ToList();
                var indexViewModel = new List<SaleIndexViewModel>();

                foreach (var data in sales)
                {
                    var model = new SaleIndexViewModel()
                    {
                        vat = data.vat,
                        CusId = data.CusId,
                        SaleId = data.SaleId,
                        CustomerName = data.Customers.FullName,
                        netTotal = data.netTotal,
                        discount = data.discount
                    };



                    indexViewModel.Add(model);
                }

                return View(indexViewModel);

            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetPrice(int id)
        {
            var Item = (await _itemRepo.GetById(id).ConfigureAwait(true));

            decimal price = 0;
            if (Item != null)
                price = Item.Rate;

            return Json(price);
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            var sale = (await _saleDetailRepo.GetQueryable()).Where(a => a.SaleId == id).ToList();
            var data = sale.Select(a => new
            {
                a.SaleDetailId,
                a.ItemName,
                a.Price,
                a.Qty,
                a.Total,

            });
            return Json(sale);
        }

        public async Task<IActionResult> Create()
        {
            var sale = new SaleIndexViewModel();

            sale.customers = await _customerRepo.GetAllAsync().ConfigureAwait(true);
            sale.items = await _itemRepo.GetAllAsync().ConfigureAwait(true);

            return View(sale);

        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleIndexViewModel allSales)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sale = new SaleCreateDTO()
                    {
                        CusId = allSales.CusId,
                        ItemId = allSales.ItemId,
                        discount = allSales.discount,
                        vat = allSales.vat,
                        total = allSales.total,
                        netTotal = allSales.netTotal,
                    };

                    var SaleDetails = new List<InventoryLibrary.Source.Dto.SaleDetail.SaleDetailCreateDTO>();
                    foreach (var data in allSales.SalesDetails)
                    {
                        var dto = new SaleDetailCreateDTO();

                        dto.CustomerName = data.CustomerName;
                        dto.ItemName = data.ItemName;
                        dto.Qty = data.Qty;
                        dto.Total = data.Total;
                        dto.Price = data.Price;

                        SaleDetails.Add(dto);
                    }

                    sale.SaleDetails = SaleDetails;
                    await _saleService.Create(sale).ConfigureAwait(true);

                    _toastNotification.AddSuccessToastMessage("Sucessfully Created Sale!");
                    return Json(sale);

                }
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            allSales.customers = await _customerRepo.GetAllAsync().ConfigureAwait(true);
            allSales.items = await _itemRepo.GetAllAsync().ConfigureAwait(true);

            return View(allSales);
        }
    }


}
