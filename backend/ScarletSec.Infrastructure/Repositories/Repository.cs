using ScarletSec.Application.Interfaces;
using ScarletSec.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace ScarletSec.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ScarletSecDbContext _context;

    public Repository(ScarletSecDbContext context)
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

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}