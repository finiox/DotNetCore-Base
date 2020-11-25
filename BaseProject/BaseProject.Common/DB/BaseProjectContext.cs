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
        public BaseProjectContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ExampleEntity> ExampleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExampleEntity>()
                .HasKey(i => i.Id);
        }
    }
}
