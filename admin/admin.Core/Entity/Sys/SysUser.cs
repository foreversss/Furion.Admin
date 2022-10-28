using SqlSugar;

namespace admin.Core.Entity.Sys;

/// <summary>
/// 系统用户
/// </summary>
[SugarTable("sys_user", TableDescription = "用户表")] //表添加备注
public class SysUser
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnDataType = "varchar", ColumnDescription = "用户名称", Length = 64)]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnDataType = "varchar", ColumnDescription = "用户密码", Length = 128)]
    public string Password { get; set; }
}