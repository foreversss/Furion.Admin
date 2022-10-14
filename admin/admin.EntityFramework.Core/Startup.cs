using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace admin.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "admin.Database.Migrations");
    }
}
