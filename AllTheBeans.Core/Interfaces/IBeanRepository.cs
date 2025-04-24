using AllTheBeans.Core.Entities;

namespace AllTheBeans.Core.Interfaces;

public interface IBeanRepository
{
    Task CreateAsync(Bean bean);
    Task<Bean?> GetByIdAsync(Guid id);
    Task<IEnumerable<Bean>> GetAllAsync();
    Task UpdateAsync(Bean bean);
    Task DeleteAsync(Guid id);
    Task<Bean?> GetBeanOfTheDayAsync();
}
