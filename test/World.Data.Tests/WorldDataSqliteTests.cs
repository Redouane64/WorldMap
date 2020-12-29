using System.Data;

using Microsoft.Data.Sqlite;

using WorldMap.Common.Data;
using WorldMap.Common.Data.Repositories;
using WorldMap.Common.Models;

using Xunit;

namespace World.Data.Tests
{
	public class WorldDataSqliteTests : IClassFixture<DatabaseFixture>
    {
		public WorldDataSqliteTests(DatabaseFixture fixture)
		{
			Fixture = fixture;
		}

		public DatabaseFixture Fixture { get; }

		private CountriesRepository CreateRepository()
		{
			var connection = new CountriesRepository(Fixture.Db);
			Fixture.Db.Open();

			return connection;
		}

		[Theory]
		[InlineData("dz")]
		public void ShouldGetCountryByKey(string country_key)
		{
			ICountriesRepository repository = CreateRepository();

			string expected_country_name = "Algeria";
			string expected_country_code = "ALG";

			Country country = repository.GetByKey(country_key);

			Assert.NotNull(country);
			Assert.Equal(expected_country_name, country.Name);
			Assert.Equal(expected_country_code, country.Code);
		}

		[Theory]
		[InlineData(27)]
		public void ShouldGetCountryById(int country_id)
		{
			ICountriesRepository repository = CreateRepository();

			string expected_country_name = "Algeria";
			string expected_country_code = "ALG";

			Country country = repository.Get(country_id);

			Assert.NotNull(country);
			Assert.Equal(expected_country_name, country.Name);
			Assert.Equal(expected_country_code, country.Code);
		}

		[Fact]
		public void ShouldReturnAllCountries()
		{
			ICountriesRepository repository = CreateRepository();

			var countries = repository.GetAll();
			Assert.NotEmpty(countries);
		}
	}
}
