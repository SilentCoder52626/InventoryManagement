using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.DTO.Item;
using InventoryLibrary.Source.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryLibrary.Source.Test.Service
{
    public class ItemServiceTest
    {
        private readonly Mock<ItemRepositoryInterface> _itemRepo = new Mock<ItemRepositoryInterface>();
        private string item_name = "Choco Fun";

        private ItemService _itemService;
        private ItemCreateDTO _createDto;
        private ItemUpdateDTO _updateDto;
        private Item _item;
        

        public ItemServiceTest()
        {
            _itemService = new ItemService(_itemRepo.Object);
            _createDto = new ItemCreateDTO();
            _updateDto = new ItemUpdateDTO();
            _item = new Item(item_name);
            
        }

        [Fact]
        public async Task Test_CreateMethod_CreateItem_With_Correct_Data()
        {
            _createDto.ItemName = item_name;

            await _itemService.Create(_createDto);

            _itemRepo.Verify(a => a.InsertAsync(It.Is<Item>(b => b.Name.Equals(_createDto.ItemName) )), Times.Once);
        }

        [Fact]
        public async Task Test_CreateMethod_UpdateItem_With_Correct_Data()
        {
            _updateDto.Name = item_name;

            _itemRepo.Setup(a => a.GetById(It.IsAny<long>())).ReturnsAsync(_item);

            await _itemService.Update(_updateDto);

            _itemRepo.Verify(a => a.UpdateAsync(It.Is<Item>(b => b.Name.Equals(_updateDto.Name))), Times.Once );
        }
        
        [Fact]
        public async Task Test_RaiseException_When_Update_Item_Is_Null()
        {
            _itemRepo.Setup(a => a.GetById(It.IsAny<long>())).Returns(Task.FromResult<Item>(null));

            await Assert.ThrowsAsync<ItemNotFoundException> (() => _itemService.Update(_updateDto));
        }

        [Fact]
        public async Task Test_CreateMethod_DeleteItem_Trigger()
        {
            _itemRepo.Setup(a => a.GetById(It.IsAny<long>())).ReturnsAsync(_item);

            await _itemService.Delete(It.IsAny<long>());

            _itemRepo.Verify(a => a.DeleteAsync(_item));
        }
     }
}
