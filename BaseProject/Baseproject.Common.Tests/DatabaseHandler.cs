// <copyright file="DatabaseHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Baseproject.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.DB;
    using BaseProject.Common.Model;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseHandler
    {
        private static BaseProjectContext context;

        public static BaseProjectContext Context => context ??= new BaseProjectContext(GetDbOptions());

        public static DbContextOptions GetDbOptions()
        {
            static DbConnection CreateInMemoryDatabase()
            {
                var connection = new SqliteConnection("Filename=:memory:");
                connection.Open();
                return connection;
            }

            var builder = new DbContextOptionsBuilder<BaseProjectContext>();

            builder.UseSqlite(CreateInMemoryDatabase());

            return builder.Options;
        }

        public static void Seed()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            if (!Context.ExampleEntities.Any())
            {
                Context.ExampleEntities.AddRange(new List<ExampleEntity>()
                {
                    new ()
                    {
                        Label = "label one"
                    },
                    new ()
                    {
                        Label = "label two"
                    },
                    new ()
                    {
                        Label = "label three"
                    },
                });
            }

            Context.SaveChanges();
        }
    }
}
