using Inventory.Repository.Implementation;
using Inventory.ViewModels;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.DTO.Customer;
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
    [Route("api/customers")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly CustomerRepositoryInterface _customerRepo;
        private readonly CustomerServiceInterface _customerService;
        private readonly CustomerTansactionRepositoryInterface _transactionRepo;

        public CustomerApiController(CustomerRepositoryInterface customerRepo, CustomerServiceInterface customerService, CustomerTansactionRepositoryInterface transactionRepo)
        {
            _customerRepo = customerRepo;
            _customerService = customerService;
            _transactionRepo = transactionRepo;
        }
        [HttpGet("")]
        public async Task<ActionResult> GetAllCustomersAsync()
        {
            try
            {
                var Customers = await _customerRepo.GetAllAsync();

                return Ok(Customers.Select(a => CreateReponseDto(a)).ToList());
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
                var Customer = await _customerRepo.GetById(id) ?? throw new CustomerNotFoundException();
                return Ok(CreateReponseDto(Customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private object CreateReponseDto(Customer a)
        {
            return new
            {
                Name = a.FullName,
                Address = a.Address,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,
                Gender = a.Gender,
                Id = a.CusId
            };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CustomerCreateViewModel model)
        {
            try
            {
                var CustomerDto = new CustomerCreateDTO
                {
                    PhoneNumber = model.PhoneNumber,
                    FullName = model.FullName,
                    Email = model.Email,
                    Gender = model.Gender,
                    Address = model.Address
                };


                await _customerService.Create(CustomerDto).ConfigureAwait(true);
                var Customer = await _customerRepo.GetByNumber(CustomerDto.PhoneNumber) ?? throw new CustomerNotFoundException();
                return Ok(CreateReponseDto(Customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Update")]
        public async Task<ActionResult> Update(CustomerUpdateViewModel model)
        {
            try
            {
                var CustomerDto = new CustomerUpdateDTO
                {
                    CusId = model.CusId,
                    FullName = model.FullName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    Email = model.Email
                };

                await _customerService.Update(CustomerDto).ConfigureAwait(true);
                var Customer = await _customerRepo.GetByNumber(CustomerDto.PhoneNumber) ?? throw new CustomerNotFoundException();
                return Ok(CreateReponseDto(Customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Statement/{id}")]
        public async Task<ActionResult> GetStatementAsync(long id)
        {
            try
            {
                var Customer = await _customerRepo.GetById(id).ConfigureAwait(false) ?? throw new CustomerNotFoundException();
                var CustomerName = Customer.FullName;
                var vm = new List<CustomerTransactionModel>();
                var Transactions = await _transactionRepo.GetAllTransactionOfCustomer(id);
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

                    vm.Add(tt);
                }
                var CustomerStatementModelsWithBalance = new List<CustomerTransactionModel>();
                for (int i = 0; i < vm.Count; i++)
                {
                    var CurrentData = vm[i];
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
                vm = CustomerStatementModelsWithBalance;

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
