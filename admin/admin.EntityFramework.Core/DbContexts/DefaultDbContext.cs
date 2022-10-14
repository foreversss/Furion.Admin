﻿using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace admin.EntityFramework.Core;

[AppDbContext("admin", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}
