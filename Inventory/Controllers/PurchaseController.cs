using Inventory.ViewModels.Purchase;
using InventoryLibrary.Dto.Purchase;
using InventoryLibrary.Dto.PurchaseDetail;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly SupplierRepositoryInterface _supplierInterface;
        private readonly ItemRepositoryInterface _itemRepo;
        private readonly IToastNotification _toastNotification;
        private readonly PurchaseServiceInterface _purchaseService;
        private readonly PurchaseRepositoryInterface _purchaseRepo;
        private readonly PurchaseDetailRepositoryInterface _purchaseDetailRepo;
        public PurchaseController(SupplierRepositoryInterface _supplierInterface,
                                  ItemRepositoryInterface _itemRepo,
                                  IToastNotification _toastNotification,
                                  PurchaseServiceInterface _purchaseService,
                                  PurchaseRepositoryInterface _purchaseRepo,
                                  PurchaseDetailRepositoryInterface _purchaseDetailRepo)
        {
            this._supplierInterface = _supplierInterface;
            this._itemRepo = _itemRepo;
            this._toastNotification = _toastNotification;
            this._purchaseService = _purchaseService;
            this._purchaseRepo = _purchaseRepo;
            this._purchaseDetailRepo = _purchaseDetailRepo;
        }
        public async Task<IActionResult> Index()
        {

            var purchases = (await _purchaseRepo.GetQueryable().ConfigureAwait(true)).ToList();

            var indexViewModel = new List<PurchaseIndexViewModel>();

            foreach (var data in purchases)
            {
                var purchase = new PurchaseIndexViewModel()
                {
                    Id = data.Id,
                    Discount = data.Discount,
                    Total = data.Total,
                    GrandTotal = data.GrandTotal,
                    SupplierName = data.Suppliers.Name,
                    SupplierId = data.SupplierId,
                    Date = data.PurchaseDateTime
                };

                indexViewModel.Add(purchase);
            }
            return View(indexViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                var purchaseDetail = (await _purchaseDetailRepo.GetQueryable().ConfigureAwait(true)).Where(a => a.PurchaseId == id).ToList();
                var data = purchaseDetail.Select(a => new
                {
                    a.Id,
                    a.Items.Name,
                    a.Amount,
                    a.Qty,
                    a.Rate
                });
                return Json(data);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var purchase = new PurchaseIndexViewModel
            {
                Suppliers = await _supplierInterface.GetAllAsync().ConfigureAwait(true),
                Items = (await _itemRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList()
            };


            return View(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseIndexViewModel allPurchases)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var purchase = new PurchaseCreateDTO()
                    {
                        SupplierId = allPurchases.SupplierId,
                        Total = allPurchases.Total,
                        GrandTotal = allPurchases.GrandTotal,
                        Discount = allPurchases.Discount,
                        Remarks = allPurchases.Remarks
                    };

                    var purchaseDetails = new List<PurchaseDetailCreateDTO>();

                    foreach (var data in allPurchases.PurchaseDetails)
                    {
                        var dto = new PurchaseDetailCreateDTO
                        {
                            ItemId = data.ItemId, Rate = data.Rate, Qty = data.Qty, Amount = data.Amount
                        };


                        purchaseDetails.Add(dto);
                    }
                    purchase.PurchaseDetails = purchaseDetails;
                    await _purchaseService.Create(purchase).ConfigureAwait(true);
                    return Json(purchase);
                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Error in creating sale!");

            }

            allPurchases.Suppliers = await _supplierInterface.GetAllAsync().ConfigureAwait(true);
            allPurchases.Items = (await _itemRepo.GetAllAsync().ConfigureAwait(true)).Where(a => a.IsActive()).ToList();

            return View(allPurchases);

        }
    }
}
