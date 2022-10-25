using SqlSugar;

namespace admin.Core.Repositorys;

/// <summary>
/// 仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> : ISimpleClient<T> where T : class, new()
{
    
}