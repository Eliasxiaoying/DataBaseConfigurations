using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace _08_DataBaseConfiguration.Config;

public class DatabaseConfigurationSource : IConfigurationSource
{
    private readonly Action<DbContextOptionsBuilder<DatabaseConfigurationContext>> builderAction;
    private readonly IEnumerable<ConfigurationItem> configurations;

    public DatabaseConfigurationSource(
        Action<DbContextOptionsBuilder<DatabaseConfigurationContext>> builderAction,
        IEnumerable<ConfigurationItem> configurations)
    {
        this.builderAction = builderAction;
        this.configurations = configurations;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DatabaseConfigurationProvider(builderAction, configurations);
    }
}