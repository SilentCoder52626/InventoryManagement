using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.Dto.CustomerTransaction;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.ServiceInterface;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.Implementation
{
    public class CustomerTransactionService : CustomerTransactionServiceInterface
    {
        private readonly CustomerRepositoryInterface _customerRepo;
        private readonly CustomerTansactionRepositoryInterface _transactionRepo;

        public CustomerTransactionService(CustomerRepositoryInterface customerRepo, CustomerTansactionRepositoryInterface transactionRepo)
        {
            _customerRepo = customerRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task Create(CustomerTransactionCreateDto dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var customer = await _customerRepo.GetById(dto.CustomerId).ConfigureAwait(false) ?? throw new CustomerNotFoundException();
            CustomerTransaction Transaction = new CustomerTransaction(customer, dto.Amount, dto.Type, dto.ExtraId);
            await _transactionRepo.InsertAsync(Transaction).ConfigureAwait(false);
            tx.Complete();
        }
    }
}
