using Inventory.ViewModels;
using InventoryLibrary.AppDbContext;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.DTO.Customer;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Testdbcontext _context;
        private readonly CustomerServiceInterface _customerService;
        private readonly IToastNotification _toastNotification;
        private readonly CustomerRepositoryInterface _customerRepo;

        public CustomerController(Testdbcontext context,
                                    IToastNotification toastNotification,
                                    CustomerServiceInterface _customerService,
                                    CustomerRepositoryInterface _customerRepo,
                                    SaleServiceInterface _saleService)
        {
            _context = context;
            _toastNotification = toastNotification;
            this._customerService = _customerService;
            this._customerRepo = _customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var Customer = (await _customerRepo.GetQuerable().ConfigureAwait(true)).ToList();
                var indexViewModel = new List<CustomerIndexViewModel>();

                foreach (var data in Customer)
                {

                    var model = new CustomerIndexViewModel();

                    model.CusId = data.CusId;
                    model.PhoneNumber = data.PhoneNumber;
                    model.FullName = data.FullName;
                    model.Address = data.Address;
                    model.Email = data.Email;
                    model.Gender = data.Gender;

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

                    var customer = new CustomerCreateDTO();

                    customer.PhoneNumber = model.PhoneNumber;
                    customer.FullName = model.FullName;
                    customer.Email = model.Email;
                    customer.Gender = model.Gender;
                    customer.Address = model.Address;

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

                var model = new CustomerUpdateViewModel();

                model.CusId = customer.CusId;
                model.FullName = customer.FullName;
                model.Email = customer.Email;
                model.PhoneNumber = customer.PhoneNumber;
                model.Gender = customer.Gender;
                model.Address = customer.Address;
                return View(model);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }
            //var customer = _context.Customers.Where(a => a.CusId == id).SingleOrDefault();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Update(CustomerUpdateViewModel model)
        {
            try
            {
                var customer = new CustomerUpdateDTO();

                customer.CusId = model.CusId;
                customer.FullName = model.FullName;
                customer.Address = model.Address;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Gender = model.Gender;
                customer.Email = model.Email;

                await _customerService.Update(customer).ConfigureAwait(true);

                _toastNotification.AddSuccessToastMessage("Updated to :- " + customer.FullName);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {
            //var cus = _context.customers.Find(id);

            await _customerService.Delete(id).ConfigureAwait(true);
            return RedirectToAction("Index");
        }

    }
}
