using Microsoft.EntityFrameworkCore;
using Satellite.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Satellite.Test.Config
{
    public static class ApplicationDbContextInMemory
    {
        public static ApplicationDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"Satellite.Db")
                .Options;

            return new ApplicationDbContext(options);

        }
    }
}
