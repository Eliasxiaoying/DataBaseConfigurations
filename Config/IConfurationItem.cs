using System;
using System.Collections.Generic;
using System.Text;

namespace _08_DataBaseConfiguration.Config;

public class ConfigurationItem
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }
}