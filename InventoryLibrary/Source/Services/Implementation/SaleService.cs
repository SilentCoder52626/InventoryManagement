using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Services.ServiceInterface;
using InventoryLibrary.Source.Dto.CustomerTransaction;
using InventoryLibrary.Source.Entity;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.ServiceInterface;
using System;
using System.Threading.Tasks;

namespace InventoryLibrary.Services.Implementation
{
    public class SaleService : SaleServiceInterface
    {
        private readonly SaleRepositoryInterface _saleRepo;
        private readonly ItemRepositoryInterface _itemRepo;
        private readonly SaleDetailRepositoryInterface _saleDetailRepo;
        private readonly CustomerTransactionServiceInterface _transactionService;
        private readonly CustomerRepositoryInterface _customerRepo;
        public SaleService(SaleRepositoryInterface _saleRepo, SaleDetailRepositoryInterface _saleDetailRepo, ItemRepositoryInterface itemRepo, CustomerTransactionServiceInterface transactionService, CustomerRepositoryInterface customerRepo)
        {
            this._saleRepo = _saleRepo;
            this._saleDetailRepo = _saleDetailRepo;
            _itemRepo = itemRepo;
            _transactionService = transactionService;
            _customerRepo = customerRepo;
        }
        public async Task<Sale> Create(SaleCreateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var Customer = await _customerRepo.GetById(dto.CusId).ConfigureAwait(false);
            var sale = new Sale
            {
                customer = Customer,
                total = dto.total,
                CusId = dto.CusId,
                netTotal = dto.netTotal,
                discount = dto.discount,
                SalesDate = DateTime.Now,
                paidAmount = dto.paidAmount,
                returnAmount = dto.returnAmount,
                dueAmount = Math.Abs(dto.paidAmount - (dto.netTotal + dto.returnAmount))
            };


            await _saleRepo.InsertAsync(sale).ConfigureAwait(false);



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
                await _saleDetailRepo.InsertAsync(SaleDetail).ConfigureAwait(false);
            }
            //Type Sales
            await _transactionService.Create(new CustomerTransactionCreateDto()
            {
                CustomerId = sale.CusId,
                Amount = sale.netTotal,
                ExtraId = sale.SaleId,
                Type = CustomerTransaction.TypeSales
            }).ConfigureAwait(false);
            // Type payment
            if (sale.paidAmount - sale.returnAmount > 0)
            {
                await _transactionService.Create(new CustomerTransactionCreateDto()
                {
                    CustomerId = sale.CusId,
                    Amount = sale.paidAmount - sale.returnAmount,
                    ExtraId = sale.SaleId,
                    Type = CustomerTransaction.TypePayment
                }).ConfigureAwait(false);
            }

            tx.Complete();
            return sale;
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
