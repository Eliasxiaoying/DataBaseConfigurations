using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_DataBaseConfiguration.Config;

public class DatabaseConfigurationProvider : ConfigurationProvider
{
    private readonly Action<DbContextOptionsBuilder<DatabaseConfigurationContext>> builderAction;

    private IEnumerable<ConfigurationItem> configurations;

    public DatabaseConfigurationProvider(
        Action<DbContextOptionsBuilder<DatabaseConfigurationContext>> builder,
        IEnumerable<ConfigurationItem> defaultConfigurations)
    {
        this.builderAction = builder;
        this.configurations = defaultConfigurations;
    }

    public override void Load()
    {
        var builder = new DbContextOptionsBuilder<DatabaseConfigurationContext>();
        builderAction(builder);

        using (var context = new DatabaseConfigurationContext(builder.Options))
        {
            context.Database.EnsureCreated();
            if (context.Configurations.Any())
            {
                configurations = context.Configurations.Take(10).ToList();
            }
            else
            {
                context.Configurations.AddRangeAsync(configurations);
                context.SaveChanges();
            }
        }
    }

    public override bool TryGet(string key, out string value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            value = string.Empty;
            return false;
        }

        var config = configurations.Where(config => key.Equals(config.Key)).FirstOrDefault();
        if (config != null)
        {
            value = config.Value;
            return true;
        }

        var builder = new DbContextOptionsBuilder<DatabaseConfigurationContext>();
        builderAction(builder);

        using (var context = new DatabaseConfigurationContext(builder.Options))
        {
            config = context.Configurations.Where(config => key.Equals(config.Key)).FirstOrDefault();
            if (config != null)
            {
                value = config.Value;
                configurations.Append(config);
                return true;
            }
        };

        value = string.Empty;
        return false;
    }
}