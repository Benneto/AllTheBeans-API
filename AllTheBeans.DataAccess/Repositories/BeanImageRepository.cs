using AllTheBeans.Core.Entities;

namespace AllTheBeans.DataAccess.Repositories
{
    public class BeanImageRepository : IBeanImageRepository
    {
        private readonly AppDbContext _context;

        public BeanImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BeanImage> CreateAsync(BeanImage beanImage)
        {
            _context.BeanImages.Add(beanImage);
            await _context.SaveChangesAsync();
            return beanImage;
        }

        public async Task<BeanImage?> GetByIdAsync(Guid id)
        {
            return await _context.BeanImages.FindAsync(id);
        }

        public async Task UpdateAsync(BeanImage image)
        {
            _context.BeanImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var beanImage = await _context.BeanImages.FindAsync(id);
            if (beanImage != null)
            {
                _context.BeanImages.Remove(beanImage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
