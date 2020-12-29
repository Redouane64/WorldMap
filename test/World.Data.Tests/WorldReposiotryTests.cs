using System;
using System.Collections.Generic;
using System.Linq;

using WorldMap.Common.Data;
using WorldMap.Common.Models;

using Xunit;

namespace World.Data.Xml.Tests
{
	using CountriesRepository = WorldMap.Common.Data.Repositories.XmlCountriesRepository;

	public class WorldReposiotryTests
    {
		readonly ICountriesRepository repository = new CountriesRepository("countries.xml");

		public WorldReposiotryTests()
		{}

        [Fact]
        public void InitializeRepository()
        {
			Assert.NotNull(repository);
        }


		[Theory]
		[InlineData("dz")]
		public void GetCountryByKey(string country_key)
		{
			string expected_code = "ALG";
			string expected_country_name = "Algeria";

			Country country = repository.GetByKey(country_key);

			Assert.NotNull(country);
			Assert.Equal(expected_code, country.Code);
			Assert.Equal(expected_country_name, country.Name);
		}

		[Theory]
		[InlineData(27)]
		public void GetCountryById(int country_id)
		{
			string expected_code = "ALG";
			string expected_country_name = "Algeria";

			var country = repository.Get(country_id);

			Assert.NotNull(country);
			Assert.Equal(expected_code, country.Code);
			Assert.Equal(expected_country_name, country.Name);
		}

		[Fact]
		public void GetCountries()
		{
			var countries = repository.GetAll();
			Assert.NotEmpty(countries);
		}
	}
}
