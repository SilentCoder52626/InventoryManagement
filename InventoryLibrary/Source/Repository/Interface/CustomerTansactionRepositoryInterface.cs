using InventoryLibrary.Base;
using InventoryLibrary.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Repository.Interface
{
    public interface CustomerTansactionRepositoryInterface : BaseRepositoryInterface<CustomerTransaction>
    {
        Task<List<CustomerTransaction>> GetAllTransactionOfCustomer(long customerId);
    }
}
