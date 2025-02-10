using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(Guid saleId)
        {
            return await _context.Sales.FindAsync(saleId);
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales.Include(s => s.Items).ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}
