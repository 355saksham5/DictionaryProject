using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApi.Models.MeaningApi;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.BusinessLayerTests
{
    [TestClass]
    public class CacheTests
    {
        private ICache _cache;
        private readonly Mock<IBasicWordDetailsRepository> appCache;
        private readonly Mock<IMeaningApiMapper> meaningApiMapper;
        private readonly Mock<IMeaningApi> meaningApi;
        
        public CacheTests()
        {
            appCache = new Mock<IBasicWordDetailsRepository>();
            meaningApiMapper = new Mock<IMeaningApiMapper>();
            meaningApi = new Mock<IMeaningApi>();
            _cache = new Cache(appCache.Object, meaningApiMapper.Object, meaningApi.Object);

        }
        [TestMethod]
        public async Task HandleCacheAsync_WordPresentInCache_ReturnDetailsFromCache()
        {
            appCache.Setup(x => x.GetDetailsAsync(It.IsAny<String>())).ReturnsAsync(new BasicWordDetails());
            var actual = _cache.HandleCacheAsync(It.IsAny<String>());
            Assert.IsNotNull(actual);
            meaningApi.Verify(x => x.GetWordDetailsAsync(It.IsAny<String>()), Times.Never);
            meaningApiMapper.Verify(x => x.MapBasicWordDetailsAsync(It.IsAny<List<WordDetails>>()), Times.Never);
        }

        [TestMethod]
        public async Task HandleCacheAsync_WordNotPresentInCacheButInExternalApi_ReturnDetailsFromExternalApi()
        {
            appCache.Setup(x => x.GetDetailsAsync(It.IsAny<String>())).ReturnsAsync(It.IsAny<BasicWordDetails>());
            meaningApi.Setup(x => x.GetWordDetailsAsync(It.IsAny<String>())).ReturnsAsync(new List<WordDetails>());
            meaningApiMapper.Setup(x => x.MapBasicWordDetailsAsync(new List<WordDetails>())).ReturnsAsync(new BasicWordDetails());
            var actual = _cache.HandleCacheAsync(It.IsAny<String>());
            Assert.IsNotNull(actual);
            meaningApi.Verify(x => x.GetWordDetailsAsync(It.IsAny<String>()), Times.Once);
            meaningApiMapper.Verify(x => x.MapBasicWordDetailsAsync(It.IsAny<List<WordDetails>>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteFromCache_AlreadyEmptyCache_DoNotCallDeleteFunctions()
        {
            appCache.Setup(x => x.GetLeastRecentlyUsed()).Returns(new List<Guid>());
            appCache.Setup(x => x.DeleteDetailsByIdAsync(It.IsAny<Guid>()));
            _cache.DeleteFromCache();
            appCache.Verify(x => x.DeleteDetailsByIdAsync(It.IsAny<Guid>()), Times.Never);
        }
        [TestMethod]
        public async Task DeleteFromCache_CacheNotEmpty_CallDeleteFunctions()
        {
            var fakeList = new List<Guid>();
            fakeList.Add(Guid.NewGuid());
            appCache.Setup(x => x.GetLeastRecentlyUsed()).Returns(fakeList);
            appCache.Setup(x => x.DeleteDetailsByIdAsync(It.IsAny<Guid>()));
            _cache.DeleteFromCache();
            appCache.Verify(x => x.DeleteDetailsByIdAsync(It.IsAny<Guid>()), Times.AtLeastOnce);

        }

    }
}
