using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace IRACMS.EntityFrameworkCore
{
    public static class IRACMSDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<IRACMSDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<IRACMSDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}