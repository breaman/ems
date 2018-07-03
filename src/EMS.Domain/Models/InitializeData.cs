using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EMS.Domain.Models
{
    public static class InitializeData
    {
        public static void Initialize(ApplicationDbContext dbContext, ILoggerFactory loggerFactory, string tournament)
        {
            ILogger logger = loggerFactory.CreateLogger("InitializeData");

            dbContext.Database.Migrate();
        }
    }
}