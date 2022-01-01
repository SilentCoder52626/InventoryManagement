using InventoryLibrary.AppDbContext;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.DTO.Customer;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Services.ServiceInterface;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace InventoryLibrary.Source.Services
{
    public class CustomerService : CustomerServiceInterface
    {
        private readonly CustomerRepositoryInterface _customerRepo;
        public CustomerService(CustomerRepositoryInterface _customerRepo)
        {
            this._customerRepo = _customerRepo;
        }

        public async Task Create(CustomerCreateDTO dto)
        {
                using var tx = TransactionScopeHelper.GetInstance();
            await ValidateCustomerNumber(dto.PhoneNumber);
                var customer = new Customer(dto.FullName);
                customer.Email = dto.Email;
                customer.Address = dto.Address;
                customer.PhoneNumber = dto.PhoneNumber;
                customer.Gender = dto.Gender;


                await _customerRepo.InsertAsync(customer);

                tx.Complete();
            

        }

        private async Task ValidateCustomerNumber(string phoneNumber, Customer? customer = null)
        {
            var CustomerByNumber = await _customerRepo.GetByNumber(phoneNumber).ConfigureAwait(false);
            if (CustomerByNumber != null && CustomerByNumber != customer)
                throw new Exception("Customer Number already registered.");
            return;
        }

        public async Task Update(CustomerUpdateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var customer = await _customerRepo.GetById(dto.CusId).ConfigureAwait(false);
            await ValidateCustomerNumber(dto.PhoneNumber, customer).ConfigureAwait(false);
            customer.Update(dto.FullName);
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.Gender = dto.Gender;
            customer.Address = dto.Address;

            await _customerRepo.UpdateAsync(customer);

            tx.Complete();

        }


    }
}