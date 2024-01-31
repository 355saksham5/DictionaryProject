using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.Controllers;
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
    public class SuggestionsControllerTests
    {
        public SuggestionController suggestionController;
        public Mock<ISuggestionService> suggestionService;
        public SuggestionsControllerTests()
        {
            suggestionService = new Mock<ISuggestionService>();
            suggestionController = new SuggestionController(suggestionService.Object);
        }
        [TestMethod]
        public async Task Suggestions_NoSuggestion_ReturnHttpNotFound()
        {
            suggestionService.Setup(x => x.GetSuggestionsAsync(It.IsAny<String>())).ReturnsAsync((List<String>)null);
            var actual = await suggestionController.Suggestions(It.IsAny<String>());
            Assert.IsNotNull(((actual as NotFoundResult)));

        }
        [TestMethod]
        public async Task Suggestions_GetSuggestion_ReturnOk()
        {
            suggestionService.Setup(x => x.GetSuggestionsAsync(It.IsAny<String>())).ReturnsAsync((new List<string>()));
            var actual = await suggestionController.Suggestions(It.IsAny<String>());
            Assert.IsNotNull(((actual as OkObjectResult)?.Value as List<String>));

        }
    }
}
