using AllTheBeans.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AllTheBeans.DataAccess.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Country?> GetByNameAsync(string name)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<Country> CreateAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }
    }
}
