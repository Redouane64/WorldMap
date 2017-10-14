using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositoryBase;
using World.Data.Models;
using System.Xml.Linq;
using System.Linq;

namespace World.Data.Xml
{
	public class WorldRepository : IRepository<long, Country>
	{
		private readonly string _filename;

		/// <summary>
		/// Create instance of world countries repository from XML data source.
		/// </summary>
		/// <param name="filename">XML file.</param>
		public WorldRepository(string filename)
		{
			_filename = filename;
		}

		/// <summary>
		/// Method not implemented.
		/// </summary>
		public void Add(Country entity) => throw new NotImplementedException();

		/// <summary>
		/// Method not implemented.
		/// </summary>
		public void AddRange(IEnumerable<Country> entities) => throw new NotImplementedException();

		/// <summary>
		/// Method not implemented.
		/// </summary>
		public void Delete(Country entity) => throw new NotImplementedException();

		/// <summary>
		/// Method not implemented.
		/// </summary>
		public IEnumerable<Country> Find(Expression<Func<Country, bool>> predicate) => throw new NotImplementedException();

		/// <summary>
		/// Get country by key.
		/// </summary>
		/// <param name="key">Country key.</param>
		/// <returns>Country instance that corresponds to the provided key or Null if no country found.</returns>
		public Country GetByKey(string key)
		{
			XDocument xDocument = XDocument.Load(_filename);

			Country countries = xDocument.Element("Countries")
										.Elements("Country")
										.Where(x => x.Element("Key").Value == key)
										.Select(MapToCountry)
										.FirstOrDefault();

			return countries;
		}

		public Country Get(long id)
		{
			XDocument xDocument = XDocument.Load(_filename);

			Country country = xDocument.Element("Countries")
												.Elements("Country")
												.Where(e => Int64.Parse(e.Element("Id").Value) == id)
												.Select(MapToCountry)
												.SingleOrDefault();

			return country;
		}

		/// <summary>
		/// Method not implemented.
		/// </summary>
		public Country Get(int id) => throw new NotImplementedException();

		public IEnumerable<Country> GetAll()
		{
			XDocument xDocument = XDocument.Load(_filename);

			List<Country> countries = xDocument.Element("Countries")
									 .Elements("Country")
									 .Select(MapToCountry).ToList();

			return countries;
		}

		/// <summary>
		/// Method not implemented.
		/// </summary>
		public void Update(Country entity) => throw new NotImplementedException();

		/// <summary>
		/// Map an XML country element to an instance of Country.
		/// </summary>
		/// <param name="element">Element to map.</param>
		/// <returns>Country instance.</returns>
		private Country MapToCountry(XElement element) 
			=> new Country
					{
						Name = element.Element("Name").Value,
						Code = element.Element("Code").Value,
						Key = element.Element("Key").Value,
						Id = Int64.Parse(element.Element("Id").Value),
						Area = Int64.Parse(element.Element("Area").Value),
						Population = Int64.Parse(element.Element("Population").Value),
					};
	}
}
