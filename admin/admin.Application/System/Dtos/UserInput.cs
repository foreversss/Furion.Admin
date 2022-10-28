using System.ComponentModel.DataAnnotations;

namespace admin.Application.System.Dtos;

/// <summary>
/// 添加用户
/// </summary>
public class UserInput
{
    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号名称不能为空")]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }

    /// <summary>
    /// 确认密码
    /// </summary>
    [Required(ErrorMessage = "确认密码不能为空"), Compare(nameof(Password), ErrorMessage = "两次密码不一致")]
    public string Confirm { get; set; }
}