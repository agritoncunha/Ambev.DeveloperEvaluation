using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Lidar com exceções relacionadas ao banco de dados, por exemplo, violação de chave única
            throw new Exception("An error occurred while adding the sale.", ex);
        }
    }

    public async Task UpdateAsync(Sale sale)
    {
        try
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Lidar com exceções relacionadas ao banco de dados
            throw new Exception("An error occurred while updating the sale.", ex);
        }
    }

    public async Task DeleteAsync(Guid saleId)
    {
        var sale = await _context.Sales.FindAsync(saleId);
        if (sale != null)
        {
            try
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Lidar com exceções de banco de dados, como falhas em relação a referências
                throw new Exception("An error occurred while deleting the sale.", ex);
            }
        }
    }
}
