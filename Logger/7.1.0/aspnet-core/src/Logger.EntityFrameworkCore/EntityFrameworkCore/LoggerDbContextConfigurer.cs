using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Logger.EntityFrameworkCore
{
    public static class LoggerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LoggerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LoggerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
