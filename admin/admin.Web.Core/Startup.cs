using admin.Application.System.Dtos;
using admin.Web.Core.Handlers;
using admin.Web.Core.ServiceExtension;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace admin.Web.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //添加jwt，并全局引用
        services.AddJwt<JwtHandler>(enableGlobalAuthorize:true);
        services.AddConfigurableOptions<AppInfoOptions>();
        services.AddScoped<RequestAuditFilter>();

        //启用跨域 Cors
        services.AddCorsAccessor();

        services.AddControllers()
                .AddInjectWithUnifyResult();
        
        services.AddSnowflakeId();// 雪花Id
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCorsAccessor();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseInject(string.Empty);
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
