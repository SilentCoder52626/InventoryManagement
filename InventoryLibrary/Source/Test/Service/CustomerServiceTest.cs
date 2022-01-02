using InventoryLibrary.AppDbContext;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.DTO.Customer;
using InventoryLibrary.Source.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryLibrary.Source.Test.Service
{
    public class CustomerServiceTest
    {
        private readonly Mock<CustomerRepositoryInterface> _customerRepo = new Mock<CustomerRepositoryInterface>();
        private readonly Mock<Testdbcontext> _context = new Mock<Testdbcontext>();

        private CustomerService _customerService;
        private CustomerCreateDTO _createDto;
        private CustomerUpdateDTO _updateDto;
        private Customer _customer;
        private string full_name = "Prism Koirala";
        private string email = "prismco.biz@gmail.com";
        private string address = "Birtamode 6";
        private string gender = "Male";
        private string phone_number = "9898989898";

        public CustomerServiceTest()
        {
            _customerService = new CustomerService(_customerRepo.Object,_context.Object);
            _createDto = new CustomerCreateDTO();
            _updateDto = new CustomerUpdateDTO();
            _customer = new Customer(full_name);
        }

        [Fact]
        public async Task Test_CustomerOnCreate_Trigger()
        {
            _createDto.FullName = full_name;
            _createDto.Email = email;
            _createDto.Address = address;
            _createDto.PhoneNumber = phone_number;
            _createDto.Gender = gender;

            await _customerService.Create(_createDto);

            _customerRepo.Verify(a => a.InsertAsync(It.Is<Customer>( b =>
                b.FullName.Equals(_createDto.FullName) &&
                b.Email.Equals(_createDto.Email) &&
                b.PhoneNumber.Equals(_createDto.PhoneNumber) &&
                b.Gender.Equals(_createDto.Gender) &&
                b.Address.Equals(_createDto.Address)
            )), Times.Once);
        }
    }
}
