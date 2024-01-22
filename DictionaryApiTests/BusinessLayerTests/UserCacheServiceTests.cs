using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.UserCache;
using DictionaryApiTests.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.BusinessLayerTests
{
    [TestClass]
    public class UserCacheServiceTests
    {

        private IUserCacheService userCacheService;
        private Mock<IUserCacheRepository> userCacheRepo;
        private Mock<FakeIHttpAccessor> contextAccessor;
        public UserCacheServiceTests()
        {
            this.userCacheRepo = new Mock<IUserCacheRepository>();
            contextAccessor = new Mock<FakeIHttpAccessor>();
            userCacheService = new UserCacheService(userCacheRepo.Object, contextAccessor.Object);
        }

        [TestMethod]
        public async Task ClearCacheAsync_OnCall_CalledCacheRepo()
        {
            await userCacheService.ClearCacheAsync();
            userCacheRepo.Verify(x=>x.ClearUserCacheAsync(It.IsAny<Guid>()), Times.Once());
        }

        [TestMethod]
        public async Task GetCacheAsync_UserIdPresent_ReturnWordsInHistory()
        {
            userCacheRepo.Setup(x => x.GetCacheByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<UserCache> {new UserCache { Cache = new CachedWord()} });
            var actual = userCacheService.GetCacheAsync().Result;
            Assert.AreEqual(1,actual.Count());

        }
        [TestMethod]
        public async Task GetCacheAsync_UserIdNotPresent_ReturnEmptyHistory()
        {
            userCacheRepo.Setup(x => x.GetCacheByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<UserCache>());
            var actual = userCacheService.GetCacheAsync().Result;
            Assert.AreEqual(0, actual.Count());

        }
        [TestMethod]
        public async Task SetCacheAsync_WordAlreadyInHistory_NotAddWordsInHistory()
        {
            userCacheRepo.Setup(x => x.GetCacheByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<UserCache> 
            { new UserCache { Cache = new CachedWord { Word = "word" } } });
            await userCacheService.SetCacheAsync(Guid.NewGuid(), "word");
            userCacheRepo.Verify(x => x.AddWordToCacheAsync(It.IsAny<Guid>(), It.IsAny<UserCache>()), Times.Never);

        }
        [TestMethod]
        public async Task SetCacheAsync_WordNotInHistory_AddWordsInHistory()
        {
            userCacheRepo.Setup(x => x.GetCacheByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<UserCache>());
            await userCacheService.SetCacheAsync(Guid.NewGuid(),"");
            userCacheRepo.Verify(x => x.AddWordToCacheAsync(It.IsAny<Guid>(), It.IsAny<UserCache>()),Times.Once);

        }

    }
}
