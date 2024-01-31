using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.SuggestionApi;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryApi.Models.UserCache;

namespace DictionaryApiTests.DataAccessTests
{
    [TestClass]
    public class UserCacheRepositoryTests
    {
        public IUserCacheRepository userCacheRepository { get; set; }
        public Mock<AppDbContext> context;
        public static List<UserCache> myList = (new List<UserCache>());
        Guid myId = Guid.NewGuid();

        public UserCacheRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new Mock<AppDbContext>(options);
            var mockUserCache = new Mock<DbSet<UserCache>>();
            mockUserCache.Setup(x => x.AddAsync(It.IsAny<UserCache>(), default)).Callback<UserCache, CancellationToken>
                ((usercache, token) => myList.Add(usercache)).ReturnsAsync(() => null).Verifiable(Times.AtLeastOnce);
            mockUserCache.Setup(x => x.Remove(It.IsAny<UserCache>())).Callback<UserCache>
                ((usercache) => myList.Remove(usercache)).Returns(() => null);

            var myQueryable = myList.AsQueryable();
            mockUserCache.As<IQueryable<UserCache>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
            mockUserCache.As<IQueryable<UserCache>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
            mockUserCache.As<IQueryable<UserCache>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
            mockUserCache.As<IQueryable<UserCache>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

            context.Setup(c => c.UserCache).Returns(mockUserCache.Object);
            myList.Add(new UserCache
            {
                Id = Guid.NewGuid()
            });
            this.userCacheRepository = new UserCacheRepository(context.Object);
        }

        [TestMethod]
        public async Task GetCacheByUserIdAsync_CacheFound_ReturnList
            ()
        {
            var id = Guid.NewGuid();
            await userCacheRepository.AddWordToCacheAsync(id,new UserCache());
            var actual = await userCacheRepository.GetCacheByUserIdAsync(id);
            Assert.AreNotEqual(0, myList.Count());

        }
        [TestMethod]
        public async Task GetCacheByUserIdAsync_CacheNotFound_Returnnull
            ()
        {
            var id = Guid.NewGuid();
            var actual = await userCacheRepository.GetCacheByUserIdAsync(id);
            Assert.AreEqual(0, actual.Count());


        }

        [TestMethod]
        public async Task AddWordToCacheAsync_OldestEntryNull_AddSuccesfully
            ()
        {

            await userCacheRepository.AddWordToCacheAsync(myId,new UserCache { Id = myId });
            context.Verify(x => x.UserCache.AddAsync(It.IsAny<UserCache>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(myList.Find(x => x.Id == myId));

        }
        [TestMethod]
        public async Task AddWordToCacheAsync_OldestEntryNotNull_AddSuccesfully
            ()
        {
            for(int i =0;i<6;i++)
            {
                await userCacheRepository.AddWordToCacheAsync(myId, new UserCache { Id = myId });

            }
            await userCacheRepository.AddWordToCacheAsync(myId, new UserCache { Id = myId });
            context.Verify(x => x.UserCache.AddAsync(It.IsAny<UserCache>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
            Assert.IsNotNull(myList.Find(x => x.Id == myId));

        }
        [TestMethod]
        public async Task ClearUserCacheAsync_CacheItemNull_ClearSuccesfully
            ()
        {
            var myId = Guid.NewGuid();
            await userCacheRepository.ClearUserCacheAsync(myId);
            context.Verify(x => x.UserCache.Remove(It.IsAny<UserCache>()), Times.Never);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

        }
        [TestMethod]
        public async Task ClearUserCacheAsync_CacheItemNotNull_ClearSuccesfully
            ()
        {
            var id = Guid.NewGuid();
            await userCacheRepository.AddWordToCacheAsync(id, new UserCache { Id = id });
            await userCacheRepository.ClearUserCacheAsync(myId);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);

        }

    }
}
