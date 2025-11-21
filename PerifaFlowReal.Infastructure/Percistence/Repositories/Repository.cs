using Microsoft.EntityFrameworkCore;
using PerifaFlowReal.Application.Interfaces.Repositories;
using PerifaFlowReal.Infastructure.Percistence.Context;

namespace PerifaFlowReal.Infastructure.Percistence.Repositories;

public class Repository<T> : IRepository<T>  where T : class
{
    private readonly PerifaFlowContext _context;
    
    private readonly DbSet<T> _dbSet;

    public Repository(PerifaFlowContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    

    public async Task UpdateAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}