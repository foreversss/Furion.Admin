using admin.Core.Entity.Sys;
using admin.Core.Repositorys.Impl;
using Furion.DependencyInjection;

namespace admin.Core.Repositorys.Sys.Impl;

public class UserRepository : Repository<SysUser>, IUserRepository, ITransient
{

}