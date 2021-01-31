using InventoryLibrary.AppDbContext;
using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Entity;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace InventoryLibrary.Services.Implementation
{
    public class SaleService : SaleServiceInterface
    {
        private readonly SaleRepositoryInterface _saleRepo;
        private readonly SaleDetailRepositoryInterface _saleDetailRepo;
        public SaleService(SaleRepositoryInterface _saleRepo, SaleDetailRepositoryInterface _saleDetailRepo)
        {
            this._saleRepo = _saleRepo;
            this._saleDetailRepo = _saleDetailRepo;
        }
        public async Task Create(SaleCreateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var sale = new Sale();

            sale.vat = dto.vat;
            sale.total = dto.total;
            sale.CusId = dto.CusId;
            sale.netTotal = dto.netTotal;
            sale.discount = dto.discount;
            sale.timestamp = dto.timestamp;

            await _saleRepo.InsertAsync(sale).ConfigureAwait(true);



            foreach (var SaleData in dto.SaleDetails)
            {
                var SaleDetail = new SaleDetails();

                SaleDetail.Qty = SaleData.Qty;
                SaleDetail.Total = SaleData.Total;
                SaleDetail.Price = SaleData.Price;
                SaleDetail.SaleId = sale.SaleId;
                SaleDetail.ItemName = SaleData.ItemName;
                SaleDetail.CustomerName = SaleData.CustomerName;

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
