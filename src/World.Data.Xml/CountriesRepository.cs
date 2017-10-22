using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using World.Data.Models;
using System.Xml.Linq;
using System.Linq;
using World.Data.Common;
using System.IO;

namespace World.Data.Xml
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly XDocument xDocument;

        /// <summary>
        /// Create instance of world countries repository from XML data source.
        /// </summary>
        /// <param name="filename">XML file.</param>
        public CountriesRepository(string filename)
        {
            xDocument = XDocument.Load(File.OpenRead(filename));
        }

        public CountriesRepository(Stream stream)
        {
            xDocument = XDocument.Load(stream);
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

        public Country GetByKey(string key)
        {
            Country countries = xDocument.Element("Countries")
                                        .Elements("Country")
                                        .Where(x => x.Element("Key").Value == key)
                                        .Select(MapToCountry)
                                        .FirstOrDefault();
            return countries;
        }

        public Country Get(long id)
        {
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
