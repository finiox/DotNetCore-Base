// <copyright file="BaseProjectContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.DB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Infrastructure.Configuration;
    using BaseProject.Common.Model;
    using Microsoft.EntityFrameworkCore;

    public class BaseProjectContext : DbContext
    {
        private readonly DBConfiguration _dbConfig;

        public BaseProjectContext()
        {
            _dbConfig = new DBConfiguration()
            {
                ConnectionString = "Data Source=localhost;Initial Catalog=Database; Integrated Security=true; Connect Timeout=120"
            };
        }

        public BaseProjectContext(AppConfiguration appConfig)
        {
            _dbConfig = appConfig.DB;
        }

        public DbSet<ExampleEntity> ExampleEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_dbConfig.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExampleEntity>()
                .HasKey(i => i.Id);
        }
    }
}
