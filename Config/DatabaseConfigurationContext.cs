using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _08_DataBaseConfiguration.Config
{
    public class DatabaseConfigurationContext : DbContext
    {
        public DatabaseConfigurationContext(DbContextOptions<DatabaseConfigurationContext> options) : base(options)
        {

        }

        public DbSet<ConfigurationItem> Configurations { get; set; }
    }
}