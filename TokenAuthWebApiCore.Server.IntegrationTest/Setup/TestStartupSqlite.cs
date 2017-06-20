﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TokenAuthWebApiCore.Server.IntegrationTest.Setup
{
	public class TestStartupSqlite : Startup
	{
		public TestStartupSqlite(IHostingEnvironment env) : base(env)
		{
		}

		public override void SetUpDataBase(IServiceCollection services)
		{
			var connectionStringBuilder = new SqliteConnectionStringBuilder
			{ DataSource = ":memory:" };
			var connectionString = connectionStringBuilder.ToString();
			var connection = new SqliteConnection(connectionString);
			services
			  .AddEntityFrameworkSqlite()
			  .AddDbContext<SecurityContext>(
				options => options.UseSqlite(connection)
			  );
		}

		public override void EnsureDatabaseCreated(SecurityContext dbContext)
		{
			dbContext.Database.OpenConnection();
			dbContext.Database.EnsureCreated();
		}
	}
}