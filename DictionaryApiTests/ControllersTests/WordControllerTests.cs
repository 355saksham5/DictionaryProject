using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Controllers;
using DictionaryApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.ControllersTests
{
    [TestClass]
    public class WordControllerTests
    {
        private WordController wordController;
        private Mock<IWordDetailsService> wordDetailsService = new Mock<IWordDetailsService>();
        public WordControllerTests()
        {
            this.wordController = new WordController(wordDetailsService.Object);
        }

        [TestMethod]
        [DataRow("apple")]
        public async Task BasicDetails_WordNotNull_ReturnBasicWordDetails(string word)
        {
            wordDetailsService.Setup(x=>x.GetBasicDetailsAsync(It.IsAny<string>())).ReturnsAsync(new BasicWordDetails());
            var actual =  wordController.BasicDetails(word).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as BasicWordDetails));
        }

        [TestMethod]
        public async Task BasicDetailsById_ValidWordId_ReturnBasicWordDetails()
        {
            wordDetailsService.Setup(x => x.GetBasicDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails());
            var actual = wordController.BasicDetailsById(It.IsAny<Guid>()).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as BasicWordDetails));
        }


        [TestMethod]
        public async Task BasicDetailsById_WordIdNotPresent_ReturnHttpNotFound()
        {
            BasicWordDetails basicWordDetails = null;
            wordDetailsService.Setup(x => x.GetBasicDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(basicWordDetails);
            var actual = await wordController.BasicDetailsById(It.IsAny<Guid>());
            Assert.AreEqual(404, (actual as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public async Task Antonyms_ValidWordId_ReturnAntonym()
        {
            wordDetailsService.Setup(x => x.GetAntonymsAsync(It.IsAny<Guid>())).ReturnsAsync(new List<string>());
            var actual = wordController.Antonyms(It.IsAny<Guid>()).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as List<string>));
        }


        [TestMethod]
        public async Task Antonyms_WordIdNotPresent_ReturnHttpNotFound()
        {
            Antonyms antonyms = null;
            wordDetailsService.Setup(x => x.GetAntonymsAsync(It.IsAny<Guid>())).ReturnsAsync((List<string>)null);
            var actual = await wordController.Antonyms(It.IsAny<Guid>());
            Assert.AreEqual(404, (actual as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public async Task Synonyms_ValidWordId_ReturnSynonym()
        {
            wordDetailsService.Setup(x => x.GetSynonymsAsync(It.IsAny<Guid>())).ReturnsAsync(new List<string>());
            var actual = wordController.Synonyms(It.IsAny<Guid>()).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as List<string>));
        }


        [TestMethod]
        public async Task Synonyms_WordIdNotPresent_ReturnHttpNotFound()
        {
            wordDetailsService.Setup(x => x.GetSynonymsAsync(It.IsAny<Guid>())).ReturnsAsync((List<string>)null);
            var actual = await wordController.Synonyms(It.IsAny<Guid>());
            Assert.AreEqual(404, (actual as NotFoundResult).StatusCode);
        }


        [TestMethod]
        public async Task Pronounciation_ValidWordId_ReturnPronounciation()
        {
            wordDetailsService.Setup(x => x.GetPronounciationAsync(It.IsAny<Guid>())).ReturnsAsync(new string(""));
            var actual = wordController.Pronounciation(It.IsAny<Guid>()).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as string));
        }


        [TestMethod]
        public async Task Pronounciation_WordIdNotPresent_ReturnHttpNotFound()
        {
            wordDetailsService.Setup(x => x.GetPronounciationAsync(It.IsAny<Guid>())).ReturnsAsync((string)null);
            var actual = await wordController.Pronounciation(It.IsAny<Guid>());
            Assert.AreEqual(404, (actual as NotFoundResult).StatusCode);
        }
        [TestMethod]
        public async Task Definition_ValidWordId_ReturnBasicWordDetails()
        {
            wordDetailsService.Setup(x => x.GetDefinitionAsync(0,It.IsAny<Guid>())).ReturnsAsync(new DefinitionDto());
            var actual = wordController.Definition(0, It.IsAny<Guid>()).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as DefinitionDto));
        }


        [TestMethod]
        public async Task Definition_WordIdNotPresent_ReturnHttpNotFound()
        {
            wordDetailsService.Setup(x => x.GetDefinitionAsync(0,It.IsAny<Guid>())).ReturnsAsync((DefinitionDto)null);
            var actual = await wordController.Definition(0,It.IsAny<Guid>());
            Assert.AreEqual(404, (actual as NotFoundResult).StatusCode);
        }

    }
}
