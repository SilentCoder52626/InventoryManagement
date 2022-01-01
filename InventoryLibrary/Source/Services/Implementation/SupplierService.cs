using InventoryLibrary.Dto.Supplier;
using InventoryLibrary.Entity;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Dto.Supplier;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using System;
using System.Threading.Tasks;


namespace InventoryLibrary.Services.Implementation
{
    public class SupplierService : SupplierServiceInterface
    {
        private readonly SupplierRepositoryInterface _supplierRepo;
        public SupplierService(SupplierRepositoryInterface _supplierRepo)
        {
            this._supplierRepo = _supplierRepo;
        }

        public async Task Activate(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var supplier = await _supplierRepo.GetById(id).ConfigureAwait(false) ?? throw new System.Exception("Supplier Not Found.");

            supplier.Enable();

            await _supplierRepo.UpdateAsync(supplier).ConfigureAwait(false);

            tx.Complete();
        }

        public async Task Create(SupplierCreateDTO dto)
        {
           
            using var tx = TransactionScopeHelper.GetInstance();
            await ValidateSupplierNumber(dto.Phone);
            var supplier = new Supplier(dto.Name, dto.Address, dto.Email, dto.Phone);

            await _supplierRepo.InsertAsync(supplier).ConfigureAwait(false);

            tx.Complete();
            
        }
        private async Task ValidateSupplierNumber(string phoneNumber, Supplier? supplier= null)
        {
            var CustomerByNumber = await _supplierRepo.GetByNumber(phoneNumber).ConfigureAwait(false);
            if (CustomerByNumber != null && CustomerByNumber != supplier)
                throw new Exception("Supplier Number already registered.");
            return;
        }

        public async Task Deactivate(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var supplier = await _supplierRepo.GetById(id).ConfigureAwait(false) ?? throw new System.Exception("Supplier Not Found.");

            supplier.Disable();
            
            await _supplierRepo.UpdateAsync(supplier).ConfigureAwait(false);

            tx.Complete();
        }


        public async Task Update(SupplierUpdateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var supplier = await _supplierRepo.GetById(dto.Id).ConfigureAwait(false) ?? throw new System.Exception("Supplier Not Found.");
            await ValidateSupplierNumber(dto.Phone, supplier);
            supplier.Update(dto.Name, dto.Address, dto.Email, dto.Phone);

            await _supplierRepo.UpdateAsync(supplier);

            tx.Complete();
        }
    }
}
