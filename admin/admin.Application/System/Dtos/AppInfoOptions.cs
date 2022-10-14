using Furion.ConfigurableOptions;

namespace admin.Application.System.Dtos;

public class AppInfoOptions : IConfigurableOptions
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string Company { get; set; }
}