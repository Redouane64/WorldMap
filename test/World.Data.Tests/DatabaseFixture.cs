using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;

using Xunit;

namespace World.Data.Tests
{
	public class DatabaseFixture : IDisposable
	{
		public DatabaseFixture()
		{
			this.Db = new SqliteConnection("Data Source=world.db");

		}

		public void Dispose() => this.Db.Dispose();

		public IDbConnection Db { get; private set; }
	}
}
