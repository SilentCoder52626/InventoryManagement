using InventoryLibrary.AppDbContext;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.DTO.Customer;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Services.ServiceInterface;
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

                var customer = new Customer(dto.FullName);
                customer.Email = dto.Email;
                customer.Address = dto.Address;
                customer.PhoneNumber = dto.PhoneNumber;
                customer.Gender = dto.Gender;


                await _customerRepo.InsertAsync(customer);

                tx.Complete();
            

        }

        public async Task Delete(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var customer = await _customerRepo.GetById(id).ConfigureAwait(true) ?? throw new CustomerNotFoundException();

            await _customerRepo.DeleteAsync(customer).ConfigureAwait(false);

            tx.Complete();
        }

        public async Task Update(CustomerUpdateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var customer = await _customerRepo.GetById(dto.CusId).ConfigureAwait(true);
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