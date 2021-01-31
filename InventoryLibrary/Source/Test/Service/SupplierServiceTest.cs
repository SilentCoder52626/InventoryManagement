using InventoryLibrary.Dto.Supplier;
using InventoryLibrary.Entity;
using InventoryLibrary.Exceptions;
using InventoryLibrary.Services.Implementation;
using InventoryLibrary.Source.Dto.Supplier;
using InventoryLibrary.Source.Repository.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryLibrary.Source.Test.Service
{
    public class SupplierServiceTest
    {
        private readonly Mock<SupplierRepositoryInterface> _supplierRepo = new Mock<SupplierRepositoryInterface>();
        private SupplierCreateDTO _createDto;
        private SupplierUpdateDTO _updateDto;
        private readonly SupplierService _supplierService;
        private Supplier _supplier;
        private string full_name = "Prism Koirala";
        private string phone = "9818181818";
        private string address = "Birtamode-6";
        private string email = "prismco.biz@gmail.com";

        public SupplierServiceTest()
        {
            _supplierService = new SupplierService(_supplierRepo.Object);
            _createDto = new SupplierCreateDTO();
            _updateDto = new SupplierUpdateDTO();
            _supplier = new Supplier(full_name, address, email, phone);
        }

        [Fact]
        public async Task Test_OnCreate_SupplierService()
        {
            _createDto.Name = full_name;
            _createDto.Email = email;
            _createDto.Phone = phone;
            _createDto.Address = address;

            await _supplierService.Create(_createDto);

            _supplierRepo.Verify(a => a.InsertAsync(It.Is<Supplier>(b => 
               b.Name.Equals(_createDto.Name) &&
               b.Email.Equals(_createDto.Email) &&
               b.Address.Equals(_createDto.Address) &&
               b.Phone.Equals(_createDto.Phone)
            )), Times.Once);
        }

        [Fact]
        public async Task Test_OnUpdate_SupplierService()
        {
            _updateDto.Name = full_name;
            _updateDto.Email = email;
            _updateDto.Address = address;
            _updateDto.Phone = phone;

            _supplierRepo.Setup(a => a.GetById(It.IsAny<long>())).ReturnsAsync(_supplier);

            await _supplierService.Update(_updateDto);

            _supplierRepo.Verify(a => a.UpdateAsync(It.Is<Supplier>(b =>
                b.Address.Equals(_updateDto.Address) && 
                b.Email.Equals(_updateDto.Email) &&
                b.Name.Equals(_updateDto.Name) &&
                b.Phone.Equals(_updateDto.Phone)
            )), Times.Once );

        }

        [Fact]
        public async Task Test_OnDelete_SupplierService()
        {
            _supplierRepo.Setup(a => a.GetById(It.IsAny<long>())).ReturnsAsync(_supplier);

            await _supplierService.Delete(It.IsAny<long>());

            _supplierRepo.Verify(a => a.DeleteAsync(_supplier));

        }

        [Fact]
        public async Task Test_TriggerException_OnUpdateNull()
        {
            _supplierRepo.Setup(a => a.GetById(It.IsAny<long>())).Returns(Task.FromResult<Supplier>(null));

            await Assert.ThrowsAsync<CustomerNotFoundException>(() => _supplierService.Update(_updateDto));
        }
    }
}
