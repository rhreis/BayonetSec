using ScarletSec.Domain.Entities;
using ScarletSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ScarletSec.Infrastructure.Data.Repositories;

public class BaseRepository<T> where T : class
{
    protected readonly ScarletSecDbContext _context;

    public BaseRepository(ScarletSecDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}