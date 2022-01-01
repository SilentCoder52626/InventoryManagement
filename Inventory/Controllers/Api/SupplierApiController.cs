using Inventory.Repository.Implementation;
using Inventory.ViewModels;
using Inventory.ViewModels.Supplier;
using InventoryLibrary.Dto.Supplier;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Dto.Supplier;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierApiController : ControllerBase
    {
        private readonly SupplierRepositoryInterface _supplierRepo;
        private readonly SupplierServiceInterface _supplierService;

        public SupplierApiController(SupplierRepositoryInterface SupplierRepo, SupplierServiceInterface SupplierService)
        {
            _supplierRepo = SupplierRepo;
            _supplierService = SupplierService;
        }
        [HttpGet("")]
        public async Task<ActionResult> GetAllSuppliersAsync()
        {
            try
            {
                var Suppliers = await _supplierRepo.GetAllAsync();

                return Ok(Suppliers.Select(a => CreateReponseDto(a)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                var Supplier = await _supplierRepo.GetById(id) ?? throw new Exception("Supplier Not Found.");
                return Ok(CreateReponseDto(Supplier));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private object CreateReponseDto(Supplier a)
        {
            return new
            {
                Name = a.Name,
                Address = a.Address,
                Email = a.Email,
                PhoneNumber = a.Phone,
                Status = a.Status,
                Id = a.Id
            };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(SupplierCreateViewModel model)
        {
            try
            {
                var supplier = new SupplierCreateDTO()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                };

                await _supplierService.Create(supplier).ConfigureAwait(true);

                var Supplier = await _supplierRepo.GetByNumber(supplier.Phone) ?? throw new System.Exception("Supplier Not Found.");
                return Ok(CreateReponseDto(Supplier));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Update")]
        public async Task<ActionResult> Update(SupplierUpdateViewModel model)
        {
            try
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

                var Supplier = await _supplierRepo.GetByNumber(supplier.Phone) ?? throw new System.Exception("Supplier Not Found.");
                return Ok(CreateReponseDto(Supplier));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("activate/{id}")]
        public async Task<ActionResult> Activate(long id)
        {
            try
            {
                await _supplierService.Activate(id);
                var Supplier = await _supplierRepo.GetById(id) ?? throw new Exception("Supplier Not Found.");
                return Ok(CreateReponseDto(Supplier));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("deactivate/{id}")]
        public async Task<ActionResult> Deactivate(long id)
        {
            try
            {
                await _supplierService.Deactivate(id);
                var Supplier = await _supplierRepo.GetById(id) ?? throw new Exception("Supplier Not Found.");
                return Ok(CreateReponseDto(Supplier));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
