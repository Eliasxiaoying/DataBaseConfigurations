using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace _08_DataBaseConfiguration.Config;

public static class DatabaseConfigurationExtensions
{
    public static IConfigurationBuilder AddDatabaseConfig(
        this IConfigurationBuilder builder,
        Action<DbContextOptionsBuilder<DatabaseConfigurationContext>> options,
        IEnumerable<ConfigurationItem> defaultConfigurations)
    {
        return builder.Add(new DatabaseConfigurationSource(options, defaultConfigurations));
    }
}