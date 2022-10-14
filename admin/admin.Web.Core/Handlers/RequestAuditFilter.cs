using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace admin.Web.Core.Handlers;

public class RequestAuditFilter:IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器、路由信息
        var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        
        // 获取请求的方法
        var method = actionDescriptor!.MethodInfo;
        
        
        
        //============== 这里是执行方法之后获取数据 ====================
        var actionContext = await next();
        // 获取返回的结果
        var returnResult = actionContext.Result;
        // 判断是否请求成功，没有异常就是请求成功
        var isRequestSucceed = actionContext.Exception == null;

        
        throw new System.NotImplementedException();
    }
}