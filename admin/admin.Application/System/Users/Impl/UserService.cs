using admin.Application.System.Dtos;
using admin.Core.Entity.Sys;
using admin.Core.Models.Sys;
using admin.Core.Repositorys.Sys;
using Furion.DataEncryption;
using Mapster;
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

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginInput"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<SysUser> Login(LoginInput loginInput)
    {
        // 获取加密后的密码
        var encryptPassword = MD5Encryption.Encrypt(loginInput.Password);
        var user = await _userRepository.GetFirstAsync(x =>
            x.Account == loginInput.Account && x.Password == encryptPassword);
        return user;
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="loginInput"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> AddUser(UserInput loginInput)
    {
        // 获取加密后的密码
        var encryptPassword = MD5Encryption.Encrypt(loginInput.Password);
        loginInput.Password = encryptPassword;
        var user = loginInput.Adapt<SysUser>();
        return await _userRepository.InsertReturnSnowflakeIdAsync(user) > 0;
    }
}