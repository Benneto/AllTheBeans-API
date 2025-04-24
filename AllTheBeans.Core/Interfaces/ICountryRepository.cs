using AllTheBeans.Core.Entities;

public interface ICountryRepository
{
    Task<Country> GetByNameAsync(string name);
    Task<Country> CreateAsync(Country country);
}
