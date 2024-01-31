using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Helpers;
using DictionaryApi.Models.DTOs;
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
    public class WordDetailsServiceTests
    {
        private WordDetailsService wordDetailsService;
        private readonly Mock<IBasicWordDetailsRepository> wordDetails;
        private readonly Mock<IDefinitionsRepository> definitions;
        private readonly Mock<IPhoneticAudioRepository> phoneticAudio;
        private readonly Mock<IAntonymsRepository> antonymsRepo;
        private readonly Mock<ISynonymsRepository> synonymsRepo;
        private readonly Mock<ICache> appCache;
        private readonly Mock<IUserCacheService> userCache;

        public WordDetailsServiceTests()
        {
            wordDetails  = new Mock<IBasicWordDetailsRepository>();
            definitions = new Mock<IDefinitionsRepository>();
            phoneticAudio = new Mock<IPhoneticAudioRepository>();
            antonymsRepo = new Mock<IAntonymsRepository>();
            synonymsRepo = new Mock<ISynonymsRepository>();
            appCache = new Mock<ICache>();
            userCache= new Mock<IUserCacheService>();
            wordDetailsService = new WordDetailsService(wordDetails.Object, definitions.Object,  phoneticAudio.Object, appCache.Object,
            antonymsRepo.Object, synonymsRepo.Object, userCache.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(AnyHttpException))]
        public async Task GetAntonymAsync_InvalidWordId_ThrowException()
        {
            IEnumerable<String> antonyms = null;
            antonymsRepo.Setup(x => x.GetAntonymsAsync(Guid.NewGuid())).ReturnsAsync(antonyms);
            wordDetails.Setup(x => x.GetDetailsByIdAsync(Guid.NewGuid())).ReturnsAsync(It.IsAny<BasicWordDetails>());
            var actual = await wordDetailsService.GetAntonymsAsync(It.IsAny<Guid>());
            antonymsRepo.Verify(x => x.GetAntonymsAsync(It.IsAny<Guid>()), Times.Never);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetAntonymAsync_ValidWordId_ReturnsAntonym()     
        {
            antonymsRepo.Setup(x => x.GetAntonymsAsync(Guid.NewGuid())).ReturnsAsync(new List<string>());
            wordDetails.Setup(x =>  x.GetDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails {Id = Guid.NewGuid() });
            var actual = await wordDetailsService.GetAntonymsAsync(It.IsAny<Guid>());
            antonymsRepo.Verify(x => x.GetAntonymsAsync(It.IsAny<Guid>()), Times.Once);
            Assert.IsNotNull(actual.ToList());
        }

        [TestMethod]
        public async Task GetBasicDetailsAsync_WordFound_ReturnsBasicDetails_AddInHistory()  
        {
            appCache.Setup(x => x.HandleCacheAsync(It.IsAny<String>())).ReturnsAsync(new BasicWordDetails());
            userCache.Setup(x => x.SetCacheAsync(It.IsAny<Guid>(), It.IsAny<String>()));
            var actual = await wordDetailsService.GetBasicDetailsAsync(It.IsAny<String>());
            userCache.Verify(x => x.SetCacheAsync(It.IsAny<Guid>(),It.IsAny<String>()), Times.Once);
            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public async Task GetBasicDetailsAsync_WordNotFound_ReturnsNull_NotAddInHistory()
        {
            appCache.Setup(x => x.HandleCacheAsync(It.IsAny<String>())).ReturnsAsync(It.IsAny<BasicWordDetails>());
            var actual = await wordDetailsService.GetBasicDetailsAsync(It.IsAny<String>());
            userCache.Verify(x => x.SetCacheAsync(It.IsAny<Guid>(), It.IsAny<String>()), Times.Never);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetBasicDetailsByIdAsync_GetWordIdIfPresent_ReturnsDetails()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails());
            var actual = await wordDetailsService.GetBasicDetailsByIdAsync(Guid.NewGuid());
            Assert.IsNotNull(actual);
        }
        [TestMethod]
        public async Task GetBasicDetailsByIdAsync_WordIdIfNotPresent_ReturnsNull()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(Guid.NewGuid())).ReturnsAsync(It.IsAny<BasicWordDetails>);
            var actual = await wordDetailsService.GetBasicDetailsByIdAsync(Guid.NewGuid());
            Assert.IsNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(BadHttpRequestException))]
        public async Task GetDefinitionAsync_IndexIsNull_ThrowException()
        {
            var actual = await wordDetailsService.GetDefinitionAsync(null,Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(AnyHttpException))]
        public async Task GetDefinitionAsync_InvalidWordId_ThrowException()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(Guid.NewGuid())).ReturnsAsync(It.IsAny<BasicWordDetails>);
            var actual = await wordDetailsService.GetDefinitionAsync(It.IsAny<int>(), Guid.NewGuid());
        }
        [TestMethod]
        public async Task GetDefinitionAsync_ValidWordId_ReturnsDefinitionDto()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails { Id = Guid.NewGuid() });
            definitions.Setup(x => x.GetAllDefinitionsByWordIdAsync(It.IsAny<Guid>())).ReturnsAsync(new List<DefinitionDto>() { new DefinitionDto()});
            var actual = await wordDetailsService.GetDefinitionAsync(0, Guid.NewGuid());
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(AnyHttpException))]
        public async Task GetDefinitionAsync_IndexOutOfBound_ThrowException()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(Guid.NewGuid())).ReturnsAsync(It.IsAny<BasicWordDetails>);
            definitions.Setup(x => x.GetAllDefinitionsByWordIdAsync(Guid.NewGuid())).ReturnsAsync(new List<DefinitionDto>() { new DefinitionDto() });
            var actual = await wordDetailsService.GetDefinitionAsync(1, Guid.NewGuid());
        }
        [TestMethod]
        [ExpectedException(typeof(AnyHttpException))]
        public async Task GetPronounciationAsync_InvalidWordId_ThrowException()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(Guid.NewGuid())).ReturnsAsync(It.IsAny<BasicWordDetails>());
            var actual = await wordDetailsService.GetPronounciationAsync(It.IsAny<Guid>());
            phoneticAudio.Verify(x => x.GetPronounciationByWordIdAsync(It.IsAny<Guid>()), Times.Never);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetPronounciationAsync_ValidWordId_ReturnsPronounciationLink()     
        {
            var fakePronounce = "Pronounciation Link";
            phoneticAudio.Setup(x => x.GetPronounciationByWordIdAsync(It.IsAny<Guid>())).ReturnsAsync(new PhoneticDto { PronounceLink=fakePronounce});
            wordDetails.Setup(x => x.GetDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails());
            var actual = await wordDetailsService.GetPronounciationAsync(Guid.NewGuid());
            phoneticAudio.Verify(x => x.GetPronounciationByWordIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.AreEqual(fakePronounce, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(AnyHttpException))]
        public async Task GetSynonymsAsync_InvalidWordId_ThrowException()
        {
            wordDetails.Setup(x => x.GetDetailsByIdAsync(Guid.NewGuid())).ReturnsAsync(It.IsAny<BasicWordDetails>());
            var actual = await wordDetailsService.GetSynonymsAsync(It.IsAny<Guid>());
            synonymsRepo.Verify(x => x.GetSynonymsAsync(It.IsAny<Guid>()), Times.Never);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task GetSynonymsAsync_ValidWordId_ReturnsSynonyms()
        {
            var fakeSynonymList = new List<String> { "1", "2", "3", "4", "5" };
            synonymsRepo.Setup(x => x.GetSynonymsAsync(Guid.NewGuid())).ReturnsAsync(fakeSynonymList);
            wordDetails.Setup(x => x.GetDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails());
            var actual = await wordDetailsService.GetSynonymsAsync(It.IsAny<Guid>());
            synonymsRepo.Verify(x => x.GetSynonymsAsync(It.IsAny<Guid>()), Times.Once);
            Assert.IsNotNull(actual?.ToList());
        }


    }
}
