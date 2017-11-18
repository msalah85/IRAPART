using IRACMS.Configuration;
using IRACMS.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IRACMS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class IRACMSDbContextFactory : IDesignTimeDbContextFactory<IRACMSDbContext>
    {
        public IRACMSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<IRACMSDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            IRACMSDbContextConfigurer.Configure(builder, configuration.GetConnectionString(IRACMSConsts.ConnectionStringName));

            return new IRACMSDbContext(builder.Options);
        }
    }
}