using InventoryLibrary.Dto.Sale;
using InventoryLibrary.Entity;
using InventoryLibrary.Services.Implementation;
using InventoryLibrary.Source.Dto.SaleDetail;
using InventoryLibrary.Source.Repository.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryLibrary.Source.Test.Service
{
    public class SaleServiceTest
    {
        private readonly Mock<SaleRepositoryInterface> _saleRepo = new Mock<SaleRepositoryInterface>();
        private readonly Mock<SaleDetailRepositoryInterface> _saleDetailRepo = new Mock<SaleDetailRepositoryInterface>();

        private SaleService _saleService;

        private SaleCreateDTO _saleCreateDto;
        private SaleDetailCreateDTO _saleDetailCreateDto;

        private Sale _sale;
        private SaleDetails _saleDetail;

        private decimal vat = 100;
        private long total = 1000;
        private long cus_id = 12;
        private long item_id = 2;
        private long net_total = 1000;
        private long discount = 50;
        private DateTime timestamp = DateTime.Now.Date;

        private int qty = 1;
        private long price = 50;
        private string customer_name = "Prism Koirala";
        private string item_name = "Choco Fun";
        private long sale_id = 1;

        public SaleServiceTest()
        {
            _saleService = new SaleService(_saleRepo.Object, _saleDetailRepo.Object);
            _saleCreateDto = new SaleCreateDTO();
            _saleDetailCreateDto = new SaleDetailCreateDTO();
            _sale = new Sale();
            _saleDetail = new SaleDetails();
        }

        [Fact]
        public async Task Test_OnCreateSale_Trigger()
        {
            _saleCreateDto.ItemId = item_id;
            _saleCreateDto.CusId = cus_id;
            _saleCreateDto.discount = discount;
            _saleCreateDto.total = total;
            _saleCreateDto.netTotal = net_total;
            _saleCreateDto.vat = vat;
            _saleCreateDto.timestamp = timestamp;
            _sale.SaleId = 1;

            _saleCreateDto.SaleDetails = new List<SaleDetailCreateDTO>()
            {
                new SaleDetailCreateDTO()
                {
                    SaleId = 1,
                    CustomerName = customer_name,
                    Total = total,
                    Qty = qty,
                    Price = price,
                    ItemName = item_name
                },
            };

            await _saleService.Create(_saleCreateDto);

            _saleRepo.Verify(a => a.InsertAsync(It.Is<Sale>(b =>
                b.CusId.Equals(_saleCreateDto.CusId) &&
                b.vat.Equals(_saleCreateDto.vat) &&
                b.total.Equals(_saleCreateDto.total) &&
                b.netTotal.Equals(_saleCreateDto.netTotal) &&
                b.discount.Equals(_saleCreateDto.discount) &&
                b.timestamp.Equals(_saleCreateDto.timestamp)

            )), Times.Once);

            _saleDetailRepo.Verify(a => a.InsertAsync(It.Is<SaleDetails>(b =>

              b.Price.Equals(_saleCreateDto.SaleDetails.First().Price) &&
              b.CustomerName.Equals(_saleCreateDto.SaleDetails.First().CustomerName) &&
              b.ItemName.Equals(_saleCreateDto.SaleDetails.First().ItemName) &&
              b.Qty.Equals(_saleCreateDto.SaleDetails.First().Qty)

           )), Times.Once);

        }
    }
}
