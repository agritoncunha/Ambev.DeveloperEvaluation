using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            ValidateSale(sale);
            await _saleRepository.AddAsync(sale);
            return sale;
        }

        public async Task<Sale?> GetSaleByIdAsync(Guid saleId)
        {
            return await _saleRepository.GetByIdAsync(saleId);
        }

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            return await _saleRepository.GetAllAsync();
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            ValidateSale(sale);
            await _saleRepository.UpdateAsync(sale);
        }

        public async Task DeleteSaleAsync(Guid saleId)
        {
            await _saleRepository.DeleteAsync(saleId);
        }

        private void ValidateSale(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity >= 4 && item.Quantity < 10)
                    item.Discount = item.UnitPrice * item.Quantity * 0.10m;
                else if (item.Quantity >= 10 && item.Quantity <= 20)
                    item.Discount = item.UnitPrice * item.Quantity * 0.20m;
                else if (item.Quantity > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 identical items.");
                else
                    item.Discount = 0;
            }

            sale.TotalAmount = sale.Items.Sum(i => i.Total);
        }
    }
}
