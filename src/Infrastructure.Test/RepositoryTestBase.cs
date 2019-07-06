using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingDong.CleanArchitect.Core;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Infrastructure.SqlServer;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Infrastructure;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    // The purpose of this unit test is a demo on how to using InMemory SQL Provider
    //   for unit testing, without SQLServer Express InMemory DB provider. 

    public class RepositoryTestBase
    {
        protected async void ExecuteTestCase(Func<IRepository<Guid, Place>, Task> action)
        {
            var options = new DbContextOptionsBuilder<DefaultDbContext>()
                                    // Random db name for parallel testing
                                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                    .Options;

            using (var context = new DefaultDbContext(options, new EmptyMediator()))
            {
                // It's VERY important.
                await context.Database.EnsureCreatedAsync();

                var repository = new GenericRepository<Guid, Place>(context, null);

                await action(repository);
            }
        }
    }
}
