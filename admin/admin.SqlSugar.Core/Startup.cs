using admin.Core.Entity.Sys;
using admin.SqlSugar.Core.DbContext;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace admin.SqlSugar.Core;

public class Startup : AppStartup
{

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddSqlSugarSetup(App.Configuration, dbName: "DefaultConnection", db =>
        {
            //初始化数据库
            db.DbMaintenance.CreateDatabase();


            var types = typeof(SysUser).Assembly.GetTypes()
                .Where(it => it.FullName != null && it.FullName.Contains("admin.Core.Entity.")).ToArray();

            db.CodeFirst.SetStringDefaultLength(200).InitTables(types);
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

    }
}