using admin.Core.Entity.Sys;

namespace admin.Application.System.Users;

/// <summary>
/// 用户业务逻辑
/// </summary>
public interface IUserService
{
    Task<List<SysUser>> GetUser();
}