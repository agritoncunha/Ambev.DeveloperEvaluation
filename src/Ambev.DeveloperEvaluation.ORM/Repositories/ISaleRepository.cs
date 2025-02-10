using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid saleId);
        Task<List<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(Guid saleId);
    }
}
