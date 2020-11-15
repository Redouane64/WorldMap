using World.Data.Models;

namespace WorldMap.Common.Data
{
	public interface ICountriesRepository : IRepository<long, Country>
    {
        /// <summary>
        /// Get country by key.
        /// </summary>
        /// <param name="key">Country key.</param>
        /// <returns>Country instance that corresponds to the provided key or Null if no country found.</returns>
        Country GetByKey(string key);

    }
}
