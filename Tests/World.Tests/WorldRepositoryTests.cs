﻿namespace World.Tests
{
	using System;
	using System.Data;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using World.Data.Models;
	using World.Data.Repositories;

	[TestClass]
	public class WorldRepositoryTests
	{
		[TestMethod]
		public void ShouldGetCountryByKey()
		{
			IDbConnection connection = null; // TO DO: use SqliteDbConnection from package Microsoft.Data.Sqlite
			var repository = new WorldRepository(connection);
			connection.Open();

			string expected_country_name = "Algeria";
			string expected_country_code = "ALG";

			string country_key = "dz";

			Country country = repository.GetByKey(country_key);

			Assert.IsNotNull(country);
			Assert.AreEqual(expected_country_name, country.Name);
			Assert.AreEqual(expected_country_code, country.Code);

			connection.Close();
		}
	}
}
