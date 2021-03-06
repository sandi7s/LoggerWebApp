using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Logger.Authorization.Roles;
using Logger.Authorization.Users;
using Logger.MultiTenancy;

namespace Logger.EntityFrameworkCore
{
    public class LoggerDbContext : AbpZeroDbContext<Tenant, Role, User, LoggerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Project.Project> Project { get; set; }
        public virtual DbSet<LogEntry.LogEntry> LogEntry { get; set; }

        public LoggerDbContext(DbContextOptions<LoggerDbContext> options)
            : base(options)
        {
        }
    }
}
