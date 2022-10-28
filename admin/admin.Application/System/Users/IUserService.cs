using admin.Application.System.Dtos;
using admin.Core.Entity.Sys;
using admin.Core.Models.Sys;

namespace admin.Application.System.Users;

/// <summary>
/// 用户业务逻辑
/// </summary>
public interface IUserService
{
    Task<List<SysUser>> GetUser();

    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    Task<SysUser> Login(LoginInput loginInput);
    
    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    Task<bool> AddUser(UserInput loginInput);
}