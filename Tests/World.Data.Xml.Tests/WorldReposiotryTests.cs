using System;
using System.Collections.Generic;
using System.Linq;
using World.Data.Models;
using Xunit;

namespace World.Data.Xml.Tests
{
    public class WorldReposiotryTests
    {
        [Fact]
        public void CRUD()
        {
			WorldRepository repository = new WorldRepository("Assets/countries.xml");

			Assert.NotNull(repository);
			RetrieveByKey(repository);
        }

		private void RetrieveByKey(WorldRepository repository)
		{
			string key = "dz";
			string expected_code = "ALG";
			string expected_country_name = "Algeria";

			Country country = repository.GetByKey(key);

			Assert.NotNull(country);
			Assert.Equal(expected_code, country.Code);
			Assert.Equal(expected_country_name, country.Name);
		}
	}
}
