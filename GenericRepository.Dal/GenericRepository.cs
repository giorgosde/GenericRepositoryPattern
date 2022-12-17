using GenericRepository.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Dal;

public interface IGenericRepository<T>
{
    Task<IEnumerable<T>> AllAsync();
    Task<T> GetByIdAsync(string id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<string> DeleteAsync(string id);
}

public class GenericRepository<T>: IGenericRepository<T> where T: BaseEntity
{

    protected readonly DatabaseContext _context;

    public GenericRepository(DatabaseContext context)
        => _context = context;

    public async Task<IEnumerable<T>> AllAsync()
        => await _context.Set<T>().ToListAsync();

    public async Task<T> CreateAsync(T entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        entity.DateUpdated = DateTime.Now;

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<string> DeleteAsync(string id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<T> GetByIdAsync(string id)
        => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<T> UpdateAsync(T entity)
    {
        entity.DateUpdated = DateTime.Now;

        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}