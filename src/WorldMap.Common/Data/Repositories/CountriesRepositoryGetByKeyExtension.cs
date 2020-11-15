using System.Linq;

using World.Data.Models;

namespace WorldMap.Common.Data.Repositories
{
	public partial class CountriesRepository : ICountriesRepository
	{
		public Country GetByKey(string key)
		{
			using (var command = Connection.CreateCommand())
			{
				command.CommandText = @"SELECT id AS Id,
												title AS Name,
												code AS Code,
												key AS Key,
												pop AS Population,
												area AS Area 
										FROM countries WHERE Key = @key";

				var idParameter = command.CreateParameter();
				idParameter.ParameterName = "@key";
				idParameter.Value = key;
				command.Parameters.Add(idParameter);

				return GetAll(command).FirstOrDefault();
			}
		}
	}
}
