using InventoryLibrary.Dto.Purchase;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services.Implementation
{
    public class PurchaseService : PurchaseServiceInterface
    {
        private readonly PurchaseRepositoryInterface _purchaseRepo;
        private readonly SupplierRepositoryInterface _supplierRepo;
        private readonly ItemRepositoryInterface _itemRepo;

        public PurchaseService( 
            PurchaseRepositoryInterface _purchaseRepo, 
            SupplierRepositoryInterface _supplierRepo,
            ItemRepositoryInterface _itemRepo
            )
        {
            this._purchaseRepo = _purchaseRepo;
            this._supplierRepo = _supplierRepo;
            this._itemRepo = _itemRepo;
        }
        public async Task Create(PurchaseCreateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var supplier = await _supplierRepo.GetById(dto.SupplierId).ConfigureAwait(false) ?? throw new Exception("Supplier not found!!!");


            var purchase = new Purchase(supplier, dto.Total, dto.GrandTotal, dto.Discount);

            foreach (var data in dto.PurchaseDetails)
            {
                var item = await _itemRepo.GetById(data.ItemId).ConfigureAwait(false) ?? throw new Exception("Item not found!");

                purchase.AddPurchaseDetails(item, data.Qty, data.Rate);
                item.AddQty(data.Qty);
                item.UpdateRate(data.Rate);
                await _itemRepo.UpdateAsync(item).ConfigureAwait(false);
            }

            await _purchaseRepo.InsertAsync(purchase).ConfigureAwait(false);



            tx.Complete();

        }


    }
}
