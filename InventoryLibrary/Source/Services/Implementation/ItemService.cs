using InventoryLibrary.AppDbContext;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.DTO.Item;
using InventoryLibrary.Source.Extension;
using InventoryLibrary.Source.Services.ServiceInterface;
using System.Threading.Tasks;

namespace InventoryLibrary.Source.Services
{
    public class ItemService : ItemServiceInterface
    {
        private readonly ItemRepositoryInterface _itemRepo;

        public ItemService( ItemRepositoryInterface _itemRepo)
        {
            this._itemRepo = _itemRepo;
        }

        public async Task Activate(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var item = await _itemRepo.GetById(id).ConfigureAwait(false);

            item.Enable();

            await _itemRepo.UpdateAsync(item).ConfigureAwait(false);


            tx.Complete();

        }

        public async Task Create(ItemCreateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            
            var item = new Item(dto.Unit ,dto.ItemName,dto.Price);
            await _itemRepo.InsertAsync(item);
            
            tx.Complete();

        }

        public async Task Deactivate(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var item = await _itemRepo.GetById(id).ConfigureAwait(false);
            item.Disable();
            await _itemRepo.UpdateAsync(item).ConfigureAwait(false);

            tx.Complete();
        }



        public async Task Delete(long id)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var item = await _itemRepo.GetById(id).ConfigureAwait(false);

            await _itemRepo.DeleteAsync(item);

            tx.Complete();

        }

        public async Task Update(ItemUpdateDTO dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var item = await _itemRepo.GetById(dto.ItemId).ConfigureAwait(false) ?? throw new ItemNotFoundException();
            item.Update(dto.Unit, dto.Name, dto.Price);
            await _itemRepo.UpdateAsync(item);
            tx.Complete();
        }
    }
}
