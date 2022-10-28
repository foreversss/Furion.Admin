using admin.Application.System.Dtos;
using admin.Application.System.Users;
using admin.Core.Entity.Sys;
using admin.Core.Enum;
using admin.Core.Models.Sys;
using Furion.DataEncryption;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace admin.Web.Entry.Service.System;

/// <summary>
/// 用户控制器
/// </summary>
[ApiDescriptionSettings(Name = "Auth")]
[Route("api/[controller]")]
public class SysUserAppService: IDynamicApiController
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SysUserAppService(IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
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

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginInput"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<UserOutput> Login(LoginInput loginInput)
    {
        var user = await _userService.Login(loginInput);
        if (user == null) throw Oops.Oh(ErrorCode.D1000);

        //生产token
        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
        {
            { "UserId", user.Id }, // 存储Id
            { "Account", user.Account }, // 存储用户名
        });

        var userOutput = user.Adapt<UserOutput>();
        userOutput.Token = accessToken;


        // 获取刷新 token
        var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken); // 第二个参数是刷新 token 的有效期（分钟），默认三十天
        // 设置响应报文头
        if (_httpContextAccessor.HttpContext == null) return userOutput;
        _httpContextAccessor.HttpContext.Response.Headers["access-token"] = refreshToken;
        // 设置Swagger自动登录
        _httpContextAccessor.HttpContext.SigninToSwagger(accessToken);

        return userOutput;
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <returns></returns>
    [HttpPost("Add")]
    public async Task<bool> Add(UserInput userInput)
    {
        if (userInput == null) throw Oops.Oh(ErrorCode.D1700);
        return await _userService.AddUser(userInput);
    }

}