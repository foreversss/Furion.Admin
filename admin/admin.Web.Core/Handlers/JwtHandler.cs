using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using admin.Core.Options;
using Furion;
using Furion.DataEncryption;

namespace admin.Web.Core;

public class JwtHandler : AppAuthorizeHandler
{

    public override async Task HandleAsync(AuthorizationHandlerContext context)
    {
        // 自动刷新 token
        if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext(),
                App.GetOptions<JWTSettingsOptions>().ExpiredTime,
                App.GetOptions<RefreshTokenSettingOptions>().ExpiredTime))
        {
            await AuthorizeHandleAsync(context);
        }
        else context.Fail(); 
    }


    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 这里写您的授权判断逻辑，授权通过返回 true，否则返回 false

        return Task.FromResult(true);
    }
}
