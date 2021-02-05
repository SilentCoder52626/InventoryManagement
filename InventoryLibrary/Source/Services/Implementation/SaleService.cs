using InventoryLibrary.AppDbContext;
using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Entity;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using System;
using System.Threading.Tasks;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.Entity;

namespace InventoryLibrary.Services.Implementation
{
    public class SaleService : SaleServiceInterface
    {
        private readonly SaleRepositoryInterface _saleRepo;
        private readonly ItemRepositoryInterface _itemRepo;
        private readonly SaleDetailRepositoryInterface _saleDetailRepo;
        public SaleService(SaleRepositoryInterface _saleRepo, SaleDetailRepositoryInterface _saleDetailRepo, ItemRepositoryInterface itemRepo)
        {
            this._saleRepo = _saleRepo;
            this._saleDetailRepo = _saleDetailRepo;
            _itemRepo = itemRepo;
        }
        public async Task Create(SaleCreateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var sale = new Sale
            {
                total = dto.total,
                CusId = dto.CusId,
                netTotal = dto.netTotal,
                discount = dto.discount,
                SalesDate = DateTime.Now
            };


            await _saleRepo.InsertAsync(sale).ConfigureAwait(true);



            foreach (var SaleData in dto.SaleDetails)
            {
                var SaleDetail = new SaleDetails
                {
                    Qty = SaleData.Qty,
                    Total = SaleData.Total,
                    Price = SaleData.Price,
                    SaleId = sale.SaleId,
                    ItemName = SaleData.ItemName
                };
                var item = await _itemRepo.GetById(SaleData.ItemId);
                item.DecreaseQty(SaleData.Qty);
                await _itemRepo.UpdateAsync(item);
                await _saleDetailRepo.InsertAsync(SaleDetail).ConfigureAwait(true);
            }

            tx.Complete();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();

        }

        public Task Update(SaleUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
