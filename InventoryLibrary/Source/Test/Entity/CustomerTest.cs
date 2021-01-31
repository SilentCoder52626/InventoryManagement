using InventoryLibrary.Entity;

using Xunit;

namespace InventoryLibrary.Source.Test.Entity
{
    public class CustomerTest
    {
        private readonly Customer _customer;
        private const string fullname = "Test";
        private const string Address = "Address";
       
        public CustomerTest()
        {
            _customer = new Customer(fullname);
        }
        
        [Fact]
        public void Test_Customer_isCreated_with_Correct_data()
        {
            Assert.Equal(fullname, _customer.FullName);
        }
        
        [Fact]
        public void Test_Update_Method_Updates_With_Correct_Data()
        {

            _customer.Update(fullname);
            Assert.Equal(fullname, _customer.FullName);
        }

    }
}
