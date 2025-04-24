using AllTheBeans.Core.Entities;
using AllTheBeans.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AllTheBeans.DataAccess.Repositories;

public class BeanRepository : IBeanRepository
{
    private readonly AppDbContext _context;

    public BeanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Bean bean)
    {
        _context.Beans.Add(bean);
        await _context.SaveChangesAsync();
    }

    public async Task<Bean?> GetByIdAsync(Guid id)
    {
        return await _context.Beans
            .Include(b => b.Country)
            .Include(b => b.Image)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Bean>> GetAllAsync()
    {
        return await _context.Beans
            .Include(b => b.Country)
            .Include(b => b.Image)
            .ToListAsync();
    }

    public async Task UpdateAsync(Bean bean)
    {
        _context.Beans.Update(bean);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var bean = await _context.Beans.FindAsync(id);
        if (bean != null)
        {
            _context.Beans.Remove(bean);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Bean?> GetBeanOfTheDayAsync()
    {
        return await _context.Beans
            .Include(b => b.Country)
            .Include(b => b.Image).FirstOrDefaultAsync(b => b.IsBeanOfTheDay);
    }
}
