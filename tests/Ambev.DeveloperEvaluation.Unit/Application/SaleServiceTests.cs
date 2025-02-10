using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<ILogger<SaleService>> _loggerMock;
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _loggerMock = new Mock<ILogger<SaleService>>();
            _saleService = new SaleService(_saleRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateSaleAsync_ShouldLogAndSaveSale()
        {
            var sale = new Sale { SaleId = Guid.NewGuid(), Items = new List<SaleItem> { new SaleItem { Quantity = 5, UnitPrice = 10 } } };

            await _saleService.CreateSaleAsync(sale);

            _saleRepositoryMock.Verify(repo => repo.AddAsync(sale), Times.Once);
            _loggerMock.Verify(log => log.LogInformation("SaleCreated: Sale {SaleId} created successfully.", sale.SaleId), Times.Once);
        }

        [Fact]
        public async Task UpdateSaleAsync_ShouldLogAndUpdateSale()
        {
            var sale = new Sale { SaleId = Guid.NewGuid(), Items = new List<SaleItem> { new SaleItem { Quantity = 5, UnitPrice = 10 } } };

            await _saleService.UpdateSaleAsync(sale);

            _saleRepositoryMock.Verify(repo => repo.UpdateAsync(sale), Times.Once);
            _loggerMock.Verify(log => log.LogInformation("SaleModified: Sale {SaleId} updated successfully.", sale.SaleId), Times.Once);
        }

        [Fact]
        public async Task DeleteSaleAsync_ShouldLogAndDeleteSale()
        {
            var saleId = Guid.NewGuid();

            await _saleService.DeleteSaleAsync(saleId);

            _saleRepositoryMock.Verify(repo => repo.DeleteAsync(saleId), Times.Once);
            _loggerMock.Verify(log => log.LogInformation("SaleCancelled: Sale {SaleId} deleted successfully.", saleId), Times.Once);
        }

        [Fact]
        public async Task CreateSaleAsync_ShouldApplyDiscountsCorrectly()
        {
            var sale = new Sale
            {
                SaleId = Guid.NewGuid(),
                Items = new List<SaleItem>
            {
                new SaleItem { Quantity = 5, UnitPrice = 20 },
                new SaleItem { Quantity = 15, UnitPrice = 50 }
            }
            };

            await _saleService.CreateSaleAsync(sale);

            Assert.Equal(200 * 0.9m, sale.Items[0].Total);
            Assert.Equal(750 * 0.8m, sale.Items[1].Total);
        }
    }
}
