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
            //Assert.AreEqual(200,((actual as OkObjectResult)?.StatusCode));
        }

        [TestMethod]
        public async Task BasicDetailsById_ValidWordId_ReturnBasicWordDetails()
        {
            wordDetailsService.Setup(x => x.GetBasicDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails());
            var actual = wordController.BasicDetailsById(It.IsAny<Guid>()).Result;
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as BasicWordDetails));
            //Assert.AreEqual(200, ((actual as OkObjectResult)?.StatusCode));
        }

        [TestMethod]
        public async Task BasicDetailsById_InValidWordId_ReturnHttpBadRequest()
        {
            Guid invalidGuid = new Guid("123");
            wordDetailsService.Setup(x => x.GetBasicDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new BasicWordDetails());
            var actual = wordController.BasicDetailsById(invalidGuid).Result;
            Assert.IsNotNull(((actual as BadRequestObjectResult)?.Value as ProblemDetails));
        }

        [TestMethod]
        public async Task BasicDetailsById_WordIdNotPresent_ReturnHttpNotFound()
        {
            BasicWordDetails basicWordDetails = null;
            wordDetailsService.Setup(x => x.GetBasicDetailsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(basicWordDetails);
            var actual = wordController.BasicDetailsById(It.IsAny<Guid>()).Result;
            Assert.IsNotNull(actual as NotFoundObjectResult);
        }

    }
}
