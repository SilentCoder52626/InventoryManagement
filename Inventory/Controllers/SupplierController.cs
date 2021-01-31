using Inventory.ViewModels.Supplier;
using InventoryLibrary.Dto.Supplier;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Dto.Supplier;
using InventoryLibrary.Source.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{

    public class SupplierController : Controller
    {
        private readonly SupplierRepositoryInterface _supplierRepo;
        private readonly IToastNotification _toastNotification;
        private readonly SupplierServiceInterface _supplierService;
        public SupplierController(SupplierRepositoryInterface _supplierRepo,
                                  IToastNotification _toastNotification,
                                  SupplierServiceInterface _supplierService)
        {
            this._supplierRepo = _supplierRepo;
            this._toastNotification = _toastNotification;
            this._supplierService = _supplierService;
        }

        public async Task<IActionResult> Activate(long id)
        {
            try
            {
                await _supplierService.Activate(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var supplier = new SupplierCreateDTO()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                    };

                    await _supplierService.Create(supplier).ConfigureAwait(true);

                    _toastNotification.AddSuccessToastMessage("Supplier Name: - " + model.Name);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }


        public async Task<IActionResult> Deactivate(long id)
        {
            try
            {
                await _supplierService.Deactivate(id);
                _toastNotification.AddSuccessToastMessage("Succesfully Deactivated!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View();
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var Suppliers = (await _supplierRepo.GetQuerable().ConfigureAwait(true)).ToList();

                var indexViewModel = new List<SupplierIndexViewModel>();

                foreach (var data in Suppliers)
                {
                    var supplier = new SupplierIndexViewModel()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Phone = data.Phone,
                        Email = data.Email,
                        Address = data.Address,
                        Status = data.Status

                    };

                    indexViewModel.Add(supplier);
                }

                return View(indexViewModel);
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            return View();
        }

       

        public async Task<IActionResult> Update(long id)
        {
            try
            {
                var supplier = await _supplierRepo.GetById(id).ConfigureAwait(true);

                var model = new SupplierUpdateViewModel()
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Phone = supplier.Phone,
                    Address = supplier.Address,
                    Email = supplier.Email
                };

                return View(model);
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupplierUpdateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var supplier = new SupplierUpdateDTO()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Phone = model.Phone,
                        Address = model.Address,
                        Email = model.Email,
                    };

                    await _supplierService.Update(supplier).ConfigureAwait(true);
                    _toastNotification.AddInfoToastMessage("Updated to:- " + model.Name);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _supplierService.Delete(id).ConfigureAwait(true);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View();
        }

    }
}
