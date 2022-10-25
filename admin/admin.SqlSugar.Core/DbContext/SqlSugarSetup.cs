using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace admin.SqlSugar.Core.DbContext;


/// <summary>
/// sqlsugar
/// </summary>
public static class SqlSugarSetup
{
    public static void AddSqlSugarSetup(this IServiceCollection services, IConfiguration configuration,
        string dbName = "DefaultConnection", Action<SqlSugarScope>? initDataBaseHandler = null)
    {
        var configConnection = new ConnectionConfig()
        {
            //连接字符串
            ConnectionString = configuration?.GetSection("ConnectionStrings")?[dbName],
            //数据库类型
            DbType = DbType.MySql,
            //是否自动关闭数据
            IsAutoCloseConnection = true,
            ConfigureExternalServices = new ConfigureExternalServices()
            {
                //高版C#写法 支持string?和string 
                EntityService = (c, p) =>
                {
                    if (new NullabilityInfoContext().Create(c).WriteState is NullabilityState.Nullable)
                    {
                        p.IsNullable = true;
                    }

                    p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName); //ToUnderLine驼峰转下划线方法
                    p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName); //ToUnderLine驼峰转下划线方法
                }
            }
        };
        var sqlSugar = new SqlSugarScope(configConnection, db =>
        {
            //单例参数配置，所有上下文生效
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //输出sql
                Console.WriteLine(sql);
            };
        });
        initDataBaseHandler?.Invoke(sqlSugar);
        services.AddSingleton<ISqlSugarClient>(sqlSugar);
    }
}