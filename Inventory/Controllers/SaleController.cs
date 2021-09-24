using Inventory.ViewModels.Sale;
using Inventory.ViewModels.SaleDetail;
using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Dto.SaleDetail;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var sales = await _saleRepo.GetQueryable().ToListAsync().ConfigureAwait(true);
                var indexViewModel = new List<SaleIndexViewModel>();

                foreach (var data in sales)
                {
                    var Customer = await _customerRepo.GetById(data.CusId).ConfigureAwait(false);
                    var model = new SaleIndexViewModel()
                    {
                        CusId = data.CusId,
                        SaleId = data.SaleId,
                        CustomerName = Customer?.FullName,
                        netTotal = data.netTotal,
                        discount = data.discount,
                        date = data.SalesDate
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
        [HttpGet]
        public async Task<IActionResult> GetAvailableQty(int id)
        {
            var Item = (await _itemRepo.GetById(id).ConfigureAwait(true));

            decimal qty = 0;
            if (Item != null)
                qty = Item.AvailableQty;

            return Json(qty);
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                var sale = _saleDetailRepo.GetQueryable().Where(a => a.SaleId == id).ToList();
                var data = sale.Select(a => new
                {
                    a.SaleDetailId,
                    a.ItemName,
                    a.Price,
                    a.Qty,
                    a.Total,

                });
                return Json(data);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            var sale = new SaleIndexViewModel
            {
                customers = await _customerRepo.GetAllAsync().ConfigureAwait(true),
                items = (await _itemRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList()
            };


            return View(sale);

        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleCreateViewModel allSales)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sale = new SaleCreateDTO()
                    {
                        CusId = allSales.CusId,
                        discount = allSales.discount,
                        total = allSales.netTotal,
                        netTotal = allSales.netTotal,
                        paidAmount = allSales.paidAmount,
                        returnAmount = allSales.returnAmount
                    };

                    var saleDetails = new List<InventoryLibrary.Source.Dto.SaleDetail.SaleDetailCreateDTO>();
                    foreach (var data in allSales.SalesDetails)
                    {
                        var dto = new SaleDetailCreateDTO
                        {
                            ItemName = data.ItemName,
                            Qty = data.Qty,
                            Total = data.Total,
                            Price = data.Price,
                            ItemId = data.ItemId
                        };


                        saleDetails.Add(dto);
                    }

                    sale.SaleDetails = saleDetails;
                    var Sales = await _saleService.Create(sale).ConfigureAwait(true);

                    _toastNotification.AddSuccessToastMessage("Successfully Created Sale!");
                    return Json(Sales.SaleId);

                }
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            var saleView = new SaleIndexViewModel
            {
                customers = await _customerRepo.GetAllAsync().ConfigureAwait(true),
                items = (await _itemRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList()
            };
            return View(saleView);
        }
        public async Task<IActionResult> Print(long saleId)
        {
            var Sales = await _saleRepo.GetById(saleId);
            var model = new SalesPrintViewModel()
            {

                CusId = Sales.CusId,
                SaleId = Sales.SaleId,
                CustomerName = Sales.customer.FullName,
                netTotal = Sales.netTotal,
                discount = Sales.discount,
                paidAmount = Sales.paidAmount,
                dueAmount = Sales.dueAmount,
                returnAmount = Sales.returnAmount,
                date = Sales.SalesDate,
                total = Sales.total,
                SalesDetails = Sales.SalesDetails.Select(a =>

                    new SaleDetailIndexViewModel()
                    {
                        ItemId = a.ItemId,
                        ItemName = a.ItemName,
                        Price = a.Price,
                        Qty = a.Qty,
                        Total = a.Total
                    }
                ).ToList()
            };
            return View(model);
        }
    }


}
