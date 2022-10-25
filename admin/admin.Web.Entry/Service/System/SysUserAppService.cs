using admin.Application.System.Users;
using admin.Core.Entity.Sys;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace admin.Web.Entry.Service.System;

/// <summary>
/// 用户控制器
/// </summary>
[ApiDescriptionSettings("系统管理")]
[Route("api/[controller]")]
public class SysUserAppService: IDynamicApiController
{
    private readonly IUserService _userService;

    public SysUserAppService(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 查询用户
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<SysUser>> Get()
    {
        return await _userService.GetUser();
    }
}