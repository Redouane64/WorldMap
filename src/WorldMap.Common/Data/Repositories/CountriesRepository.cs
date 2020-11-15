using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using WorldMap.Common.Data.Repositories;
using WorldMap.Common.Models;

namespace WorldMap.Common.Data.Repositories
{

	public partial class CountriesRepository : Repository<long, Country>
	{

		public CountriesRepository(IDbConnection connection) 
			: base(connection)
		{
		}

		protected override void Map(IDataReader reader, Country entity)
		{
			entity.Id = (long)reader[nameof(Country.Id)];
			entity.Name = (string)reader[nameof(Country.Name)];
			entity.Code = (string)reader[nameof(Country.Code)];
			entity.Key = (string)reader[nameof(Country.Key)];
			entity.Population = (long)reader[nameof(Country.Population)];
			entity.Area = (long)reader[nameof(Country.Area)];
		}

		public override void Add(Country entity)
		{
			using (var command = Connection.CreateCommand())
			{
				internalAdd(new [] { entity }, command);
			}
		}

		internal void internalAdd(IEnumerable<Country> countries, IDbCommand command)
		{
			foreach (var country in countries)
			{
				command.CommandText = @"INSERT INTO 
											countries(title, key, code, pop, area)
										VALUES (@title, @key, @code, @pop, @area)";

				var titleParameter = command.CreateParameter();
				titleParameter.ParameterName = "@title";
				titleParameter.Value = country.Name;
				command.Parameters.Add(titleParameter);

				var keyParameter = command.CreateParameter();
				keyParameter.ParameterName = "@key";
				keyParameter.Value = country.Key;
				command.Parameters.Add(keyParameter);

				var codeParameter = command.CreateParameter();
				codeParameter.ParameterName = "@code";
				codeParameter.Value = country.Code;
				command.Parameters.Add(codeParameter);

				var popParameter = command.CreateParameter();
				popParameter.ParameterName = "@pop";
				popParameter.Value = country.Population;
				command.Parameters.Add(popParameter);

				var areaParameter = command.CreateParameter();
				areaParameter.ParameterName = "@area";
				areaParameter.Value = country.Area;
				command.Parameters.Add(areaParameter);

				command.ExecuteNonQuery();

			}
		}

		public override void AddRange(IEnumerable<Country> entities)
		{
			using (var transaction = Connection.BeginTransaction())
			using (var command = Connection.CreateCommand())
			{
				command.Transaction = transaction;
				try
				{
					internalAdd(entities, command);

					transaction.Commit();
				}
				catch (InvalidOperationException ioe)
				{
					transaction.Rollback();
					throw ioe;
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw e;
				}
			}
		}

		public override void Delete(Country entity)
		{
			using (var command = Connection.CreateCommand())
			{
				command.CommandText = "DELETE FROM countries WHERE id = @id";
				var idParameter = command.CreateParameter();
				idParameter.ParameterName = "@id";
				idParameter.Value = entity.Id;
				command.Parameters.Add(idParameter);

				command.ExecuteNonQuery();
			}
		}

		public override IEnumerable<Country> GetAll()
		{
			using (var command = Connection.CreateCommand())
			{
				command.CommandText = @"SELECT id AS Id,
												title AS Name,
												code AS Code,
												key AS Key,
												pop AS Population,
												area AS Area 
										FROM countries";

				return GetAll(command);
			}
		}

		public override Country Get(long id)
		{
			using (var command = Connection.CreateCommand())
			{
				command.CommandText = @"SELECT id AS Id,
												title AS Name,
												code AS Code,
												key AS Key,
												pop AS Population,
												area AS Area 
										FROM countries WHERE id = @id";

				var idParameter = command.CreateParameter();
				idParameter.ParameterName = "@id";
				idParameter.Value = id;
				command.Parameters.Add(idParameter);

				return GetAll(command).FirstOrDefault();
			}
		}

		public override void Update(Country entity)
		{
			using (var command = Connection.CreateCommand())
			{
				command.CommandText = @"UPDATE countries
										SET key=@key, code=@code, title=@title, pop=@pop, area=@area
										WHERE id = @id";

				var idParameter = command.CreateParameter();
				idParameter.ParameterName = "@id";
				idParameter.Value = entity.Id;
				command.Parameters.Add(idParameter);

				var keyParameter = command.CreateParameter();
				keyParameter.ParameterName = "@key";
				keyParameter.Value = entity.Key;
				command.Parameters.Add(keyParameter);

				var codeParameter = command.CreateParameter();
				codeParameter.ParameterName = "@code";
				codeParameter.Value = entity.Code;
				command.Parameters.Add(codeParameter);

				var titleParameter = command.CreateParameter();
				titleParameter.ParameterName = "@title";
				titleParameter.Value = entity.Name;
				command.Parameters.Add(titleParameter);


				var popParameter = command.CreateParameter();
				popParameter.ParameterName = "@pop";
				popParameter.Value = entity.Population;
				command.Parameters.Add(popParameter);

				var areaParameter = command.CreateParameter();
				areaParameter.ParameterName = "@area";
				areaParameter.Value = entity.Area;
				command.Parameters.Add(areaParameter);

				command.ExecuteNonQuery();
			}
		}

	}
}