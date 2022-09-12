using _08_DataBaseConfiguration.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace _08_DataBaseConfiguration;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appSettings.json");
        var configuration = builder.Build();

        var configBuilder = new ConfigurationBuilder();

        configBuilder.AddDatabaseConfig(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        }, null);

        var config = configBuilder.Build();
        var value = config["002"];
        Console.WriteLine($"002-{value}");

        Console.WriteLine("Hello World!");
    }
}