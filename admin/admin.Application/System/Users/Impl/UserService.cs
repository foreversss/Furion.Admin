using admin.Core.Entity.Sys;
using admin.Core.Repositorys.Sys;
using Microsoft.AspNetCore.Mvc;

namespace admin.Application.System.Users.Impl;

/// <summary>
/// 用户
/// </summary>
public class UserService : IUserService,ITransient
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    public async Task<List<SysUser>> GetUser()
    {
        return await _userRepository.GetListAsync();
    }
}