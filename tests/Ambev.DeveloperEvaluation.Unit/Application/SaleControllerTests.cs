using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class SalesControllerTests
    {
        private readonly Mock<SaleService> _saleServiceMock;
        private readonly SalesController _salesController;

        public SalesControllerTests()
        {
            _saleServiceMock = new Mock<SaleService>();
            _salesController = new SalesController(_saleServiceMock.Object);
        }

        [Fact]
        public async Task CreateSale_ShouldReturnCreatedResponse()
        {
            var sale = new Sale { SaleId = Guid.NewGuid(), Items = new List<SaleItem> { new SaleItem { Quantity = 5, UnitPrice = 10 } } };
            _saleServiceMock.Setup(service => service.CreateSaleAsync(sale)).ReturnsAsync(sale);

            var result = await _salesController.CreateSale(sale);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(sale.SaleId, ((Sale)createdResult.Value).SaleId);
        }

        [Fact]
        public async Task GetSaleById_ShouldReturnOk_WhenSaleExists()
        {
            var sale = new Sale { SaleId = Guid.NewGuid() };
            _saleServiceMock.Setup(service => service.GetSaleByIdAsync(sale.SaleId)).ReturnsAsync(sale);

            var result = await _salesController.GetSaleById(sale.SaleId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sale, okResult.Value);
        }

        [Fact]
        public async Task GetSaleById_ShouldReturnNotFound_WhenSaleDoesNotExist()
        {
            _saleServiceMock.Setup(service => service.GetSaleByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null);

            var result = await _salesController.GetSaleById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllSales_ShouldReturnOk_WithListOfSales()
        {
            var sales = new List<Sale> { new Sale { SaleId = Guid.NewGuid() } };
            _saleServiceMock.Setup(service => service.GetAllSalesAsync()).ReturnsAsync(sales);

            var result = await _salesController.GetAllSales();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sales, okResult.Value);
        }

        [Fact]
        public async Task UpdateSale_ShouldReturnNoContent()
        {
            var sale = new Sale { SaleId = Guid.NewGuid() };

            var result = await _salesController.UpdateSale(sale.SaleId, sale);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSale_ShouldReturnNoContent()
        {
            var saleId = Guid.NewGuid();

            var result = await _salesController.DeleteSale(saleId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
