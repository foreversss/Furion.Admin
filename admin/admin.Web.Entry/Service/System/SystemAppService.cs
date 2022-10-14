using admin.Application.System.Dtos;
using admin.Application.System.Services;
using admin.Web.Core.Handlers;
using Furion;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace admin.Web.Entry.Service.System;

/// <summary>
/// 系统服务接口
/// </summary>
public class SystemAppService : IDynamicApiController
{
    private readonly ISystemService _systemService;
    private readonly IConfiguration _configuration;
    private readonly AppInfoOptions _appInfoOptions;
    public SystemAppService(ISystemService systemService,IConfiguration configuration,IOptionsSnapshot<AppInfoOptions> optionsSnapshot)
    {
        _systemService = systemService;
        _configuration = configuration;
        _appInfoOptions = optionsSnapshot.Value;
    }

    /// <summary>
    /// 获取系统描述
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
        //读取配置文件 方式一
        // var documentTitle1 = App.Configuration["SpecificationDocumentSettings:DocumentTitle"];
        var documentTitle1 = App.GetConfig<string>("SpecificationDocumentSettings:DocumentTitle");
        //读取配置文件方式二
        var documentTitle2 = _configuration.GetSection("SpecificationDocumentSettings:DocumentTitle").Get<string>();
        var documentTitle3 = _configuration["SpecificationDocumentSettings:DocumentTitle"];;

        /*
         * 在可依赖注入类中，依赖注入 IConfiguration
         * 在静态类/非依赖注入类中，选择 App.Configuration[path] 读取  
         */
        var documentTitle4 = _configuration.GetSection("SpecificationDocumentSettings");
        //配置更改通知
        ChangeToken.OnChange(() => App.Configuration.GetReloadToken(), () =>
        {
            var title = documentTitle4["GroupOpenApiInfos:DocumentTitle"];
        });
        
        
        return _systemService.GetDescription();
    }

    /// <summary>
    /// 获取app信息
    /// </summary>
    /// <returns></returns>
    [ServiceFilter(typeof(RequestAuditFilter))]
    public string GetAppInfo()
    {
        var httpContext = App.HttpContext;
        return $"{_appInfoOptions.Name}====={_appInfoOptions.Company}==={_appInfoOptions.Version}";
    }
}
