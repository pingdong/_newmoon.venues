using System;
using PingDong.Newmoon.Places.Core;
using Xunit;

namespace PingDong.Newmoon.Events.Infrastructure.Repositories
{
    // The purpose of this unit test is a demo on how to using InMemory SQL Provider
    //   for unit testing, without SQLServer Express InMemory DB provider. 

    public class PlaceRepositoryTest : RepositoryTestBase
    {
        private readonly Address _defaultAddress = new Address("1", "st.", "akl", "ak", "nz","0920");
        private readonly string _defaultName = "PLACE";
        
        [Fact]
        public void Add_Then_Get()
        {
            ExecuteTestCase(async repository =>
            {
                // Arrange
                // Surprise, a variable can be named in Simplified Chinese!!
                // Just for fun
                var 某地点 = new Place(_defaultName, _defaultAddress);
                某地点.Occupy();

                // Ack
                await repository.AddAsync(某地点);
                await repository.UnitOfWork.SaveChangesAsync();
                
                // Assert
                var list = await repository.ListAsync();

                Assert.Single(list);

                var place = list[0];
                Assert.Equal(_defaultName, place.Name);
                Assert.Equal(_defaultAddress, place.Address);
                Assert.Equal(PlaceState.Occupied, place.State);
            });
        }
    }
}
