using System.Linq;
using System.Data;
using World.Data.Models;

namespace World.Data.Repositories
{
	public class WorldRepository : CountriesRepository
	{
		public WorldRepository(IDbConnection connection)
			: base(connection)
		{
		}

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
