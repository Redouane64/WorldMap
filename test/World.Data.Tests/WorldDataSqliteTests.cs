using System.Data;

using Microsoft.Data.Sqlite;

using WorldMap.Common.Data;
using WorldMap.Common.Data.Repositories;
using WorldMap.Common.Models;

using Xunit;

namespace World.Data.Tests
{
	public class WorldDataSqliteTests
    {
		[Fact]
		public void ShouldGetCountryByKey()
		{
			IDbConnection connection = new SqliteConnection("Data Source=Assets/world.db");
			ICountriesRepository repository = new CountriesRepository(connection);
			connection.Open();

			string expected_country_name = "Algeria";
			string expected_country_code = "ALG";

			string country_key = "dz";

			Country country = repository.GetByKey(country_key);

			Assert.NotNull(country);
			Assert.Equal(expected_country_name, country.Name);
			Assert.Equal(expected_country_code, country.Code);

			connection.Close();
		}
	}
}
