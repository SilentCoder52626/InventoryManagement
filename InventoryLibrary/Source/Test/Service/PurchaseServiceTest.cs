using InventoryLibrary.Dto.Purchase;
using InventoryLibrary.Dto.PurchaseDetail;
using InventoryLibrary.Entity;
using InventoryLibrary.Repository.Interface;
using InventoryLibrary.Source.Repository.Interface;
using InventoryLibrary.Source.Services.Implementation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace InventoryLibrary.Source.Test.Service
{
    public class PurchaseServiceTest
    {
        private readonly Mock<PurchaseRepositoryInterface> _purchaseRepo = new Mock<PurchaseRepositoryInterface>();
        private readonly Mock<SupplierRepositoryInterface> _supplierRepo = new Mock<SupplierRepositoryInterface>();
        private readonly Mock<ItemRepositoryInterface> _itemRepo = new Mock<ItemRepositoryInterface>();

        private Purchase _purchase;
        private PurchaseCreateDTO _purchaseCreate;
        private PurchaseDetailCreateDTO _purchaseDetailCreate;
        private PurchaseService _purchaseService;
       

        private Supplier _supplier;

        private readonly decimal total       = 110;
        private readonly decimal grand_total = 220;
        private readonly decimal discount    = 10;
        private readonly decimal vat         = 110 - 10 * (13 / 100);

        private readonly long qty = 1;
        private readonly decimal amt = 100;
        private readonly decimal rate = 100;

        private readonly string name = "Prism Koriala";
        private readonly string address = "birtamode";
        private readonly string email = "email@rmail.com";
        private readonly string phone = "9898989898";
        public PurchaseServiceTest()
        {
            _purchase        = new Purchase(_supplier, total, grand_total, discount, vat);
            _purchaseService = new PurchaseService( _purchaseRepo.Object,  _supplierRepo.Object, _itemRepo.Object);
            _purchaseCreate  = new PurchaseCreateDTO();
            _purchaseDetailCreate = new PurchaseDetailCreateDTO();
            _supplier = new Supplier(name, address, email, phone);
        }


        [Fact]
        public void Test_Purchase_Is_Created_With_Correct_Data()
        {


            _purchaseCreate.Discount   = 20;
            _purchaseCreate.GrandTotal = Convert.ToDecimal(90.4);
            _purchaseCreate.Total      = 100;
            _purchaseCreate.Vat        = Convert.ToDecimal(10.4);
           
           
            _supplierRepo.Setup(a => a.GetById(It.IsAny<long>())).ReturnsAsync(_supplier);

            var PurchaseDetail = new List<PurchaseDetailCreateDTO>();

            _purchaseCreate.PurchaseDetails = new List<PurchaseDetailCreateDTO>()
            {
                new PurchaseDetailCreateDTO()
                {
                    PchId = 1,
                    Qty = 10,
                    Amount = 100,
                    Rate = 10,
                    
                }
            };
            _purchaseCreate.PurchaseDetails = PurchaseDetail;

            _purchaseService.Create(_purchaseCreate);

            _purchaseRepo.Verify(a => a.InsertAsync(It.Is<Purchase>(b =>

            b.Discount.Equals(_purchaseCreate.Discount) /*&&*/
            //b.GrandTotal.Equals(_purchaseCreate.GrandTotal) &&
            //b.Total.Equals(_purchaseCreate.Total) &&
            //b.Vat.Equals(_purchaseCreate.Vat) &&
            //b.Suppliers.Equals(_supplier) &&
            //b.PurchaseDetails.First().Equals(_purchaseCreate.PurchaseDetails.First())


            )), Times.Once);

        }
    }
}
