using Microsoft.EntityFrameworkCore;
using TeamSearcher.Application.Contracts.Persistence;

namespace TeamSearcher.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T :class
{
    private readonly AppDbContext _db;

    public GenericRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<T> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Set<T>().ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);
        
        return entity != null;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _db.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }
}