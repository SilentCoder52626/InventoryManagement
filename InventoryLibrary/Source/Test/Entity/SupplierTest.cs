using InventoryLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InventoryLibrary.Source.Test.Entity
{
    public class SupplierTest
    {
        private readonly Supplier _supplier;
        private const string name = "Prism Koirala";
        private const string address = "Birtamode";
        private const string email = "prismkoirala@gmail.com";
        private const string phone = "9818195512";

        public SupplierTest()
        {
            _supplier = new Supplier(name, address, email, phone);
        }
        
        [Fact]
        public void Test_Supplier_isCreated_With_Correct_Data()
        {
            Assert.Equal(name, _supplier.Name);
            Assert.Equal(address, _supplier.Address);
            Assert.Equal(email, _supplier.Email);
            Assert.Equal(phone, _supplier.Phone);

        }

        [Fact]
        public void Test_Supplier_isUpdated_With_Correct_Data()
        {
            _supplier.Update(name, address, email, phone);
            Assert.Equal(name, _supplier.Name);
            Assert.Equal(address, _supplier.Address);
            Assert.Equal(email, _supplier.Email);
            Assert.Equal(phone, _supplier.Phone);
        }
    }
}
