using Inventory.ViewModels;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.DTO.Customer;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerServiceInterface _customerService;
        private readonly IToastNotification _toastNotification;
        private readonly CustomerRepositoryInterface _customerRepo;

        public CustomerController(IToastNotification toastNotification,
                                    CustomerServiceInterface _customerService,
                                    CustomerRepositoryInterface _customerRepo,
                                    SaleServiceInterface _saleService)
        {
            _toastNotification = toastNotification;
            this._customerService = _customerService;
            this._customerRepo = _customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var Customer = await _customerRepo.GetQueryable().ToListAsync();
                var indexViewModel = new List<CustomerIndexViewModel>();

                foreach (var data in Customer)
                {

                    var model = new CustomerIndexViewModel
                    {
                        CusId = data.CusId,
                        PhoneNumber = data.PhoneNumber,
                        FullName = data.FullName,
                        Address = data.Address,
                        Email = data.Email,
                        Gender = data.Gender
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var customer = new CustomerCreateDTO
                    {
                        PhoneNumber = model.PhoneNumber,
                        FullName = model.FullName,
                        Email = model.Email,
                        Gender = model.Gender,
                        Address = model.Address
                    };


                    await _customerService.Create(customer).ConfigureAwait(true);
                    _toastNotification.AddSuccessToastMessage("Created:- " + customer.FullName);

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Update(long id)
        {

            try
            {
                var customer = await _customerRepo.GetById(id).ConfigureAwait(true) ?? throw new CustomerNotFoundException("Customer has not been found of that number!");

                var model = new CustomerUpdateViewModel
                {
                    CusId = customer.CusId,
                    FullName = customer.FullName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Gender = customer.Gender,
                    Address = customer.Address
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("Index");

            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateViewModel model)
        {
            try
            {
                var customer = new CustomerUpdateDTO
                {
                    CusId = model.CusId,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    Email = model.Email
                };


                await _customerService.Update(customer).ConfigureAwait(true);

                _toastNotification.AddSuccessToastMessage("Updated to :- " + customer.FullName);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            return RedirectToAction("Index");
        }

    }
}
