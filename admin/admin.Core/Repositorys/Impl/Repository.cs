using Furion;
using SqlSugar;

namespace admin.Core.Repositorys.Impl;


public class Repository<T> : SimpleClient<T>, IRepository<T> where T : class, new()
{
    public Repository(ISqlSugarClient context = null) : base(context) //默认值等于null不能少
    {
        base.Context = App.GetService<ISqlSugarClient>(); //用手动获取方式支持切换仓储
    }
}