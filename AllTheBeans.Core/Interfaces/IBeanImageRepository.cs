using AllTheBeans.Core.Entities;

public interface IBeanImageRepository
{
    Task<BeanImage> CreateAsync(BeanImage image);

    Task<BeanImage?> GetByIdAsync(Guid id);

    Task UpdateAsync(BeanImage image);

    Task DeleteAsync(Guid id);
}
