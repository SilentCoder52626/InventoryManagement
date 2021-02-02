using InventoryLibrary.Entity;
using InventoryLibrary.Source.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace InventoryLibrary.Source.Test.Entity
{
    public class PurchaseTest
    {
        private readonly Purchase _purchase;
        private const decimal amount = 105;
        private const decimal total = 105;
        private const decimal grand_total = 113;
        private const decimal discount = 10;
        private const decimal vat = 26;


        private readonly Supplier _supplier;
        private const string name    = "prismkoirala";
        private const string address = "birtamode";
        private const string email   = "prismkoirala@gmail.com";
        private const string phone   = "9818195512";


        private readonly Unit unit;
        private readonly Item _item;
        private const string item_name = "Choco Fun";

        private const long qty = 1;
        private const decimal rate = 100;

        public PurchaseTest()
        {
            unit = new Unit("as");
            _item     = new Item(unit,item_name,10);
            _supplier = new Supplier(name, address, email, phone);
            _purchase = new Purchase(_supplier ,total, grand_total, discount, vat);

        }

        [Fact]
        public void Test_Purchase_isCreated_With_Correct_Data()
        {
            Assert.Equal(_supplier, _purchase.Suppliers);
            Assert.Equal(total, _purchase.Total);
            Assert.Equal(grand_total, _purchase.GrandTotal);
            Assert.Equal(discount, _purchase.Discount);
            Assert.Equal(vat, _purchase.Vat);
        }



        [Fact]
        public void Test_Purchase_Calculate_With_Correct_Data()
        {
            var vat = 210 - discount * (13 / 100);
            var grand_total = vat + 210 - discount;
            _purchase.Calculate(amount);
            Assert.Equal(210, _purchase.Total);
            Assert.Equal(vat, _purchase.Vat);
            Assert.Equal(discount, _purchase.Discount);
            Assert.Equal(grand_total, _purchase.GrandTotal);
        }

        [Fact]
        public void Test_AddPurchaseDetail_Create_With_Correct_Data()
        {
            var PurchaseDetails = new List<PurchaseDetail>();

            PurchaseDetails.Add(new PurchaseDetail(_purchase, _item, qty, rate));

            _purchase.PurchaseDetails = new List<PurchaseDetail>() {
                new PurchaseDetail(_purchase, _item, qty, rate)
            };

            _purchase.AddPurchaseDetails(_item, qty, rate);

            Assert.Equal(PurchaseDetails.First().Amount, _purchase.PurchaseDetails.First().Amount);
            Assert.Equal(PurchaseDetails.First().Qty, _purchase.PurchaseDetails.First().Qty);
            Assert.Equal(PurchaseDetails.First().Rate, _purchase.PurchaseDetails.First().Rate);
        }
    }
}
