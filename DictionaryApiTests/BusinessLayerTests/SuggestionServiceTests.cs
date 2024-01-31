using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.ExternalApiHandlers.IExternalApiHandlers;
using DictionaryApi.Models.SuggestionApi;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.BusinessLayerTests
{
    [TestClass]
    public class SuggestionServiceTests
    {
        private readonly SuggestionService suggestionService;
        private readonly Mock<ISuggestionApi> suggestionApi;

        public SuggestionServiceTests()
        {
            suggestionApi = new Mock<ISuggestionApi>();
            suggestionApi.Setup(x => x.GetSuggestionsAsync(It.IsAny<String>())).ReturnsAsync(new List<Suggestion>());
            suggestionService = new SuggestionService(suggestionApi.Object);
        }

        [TestMethod]
        public async Task GetSuggestionsAsync_QueryStringNull_ReturnEmptySuggestions()
        {
            var actual = suggestionService.GetSuggestionsAsync(null).Result;
            Assert.AreEqual(0, actual.Count());
        }
        [TestMethod]
        public async Task GetSuggestionsAsync_QueryString_ReturnSuggestions()
        {
            suggestionApi.Setup(x => x.GetSuggestionsAsync(It.IsAny<String>())).ReturnsAsync(new List<Suggestion> { new Suggestion { } });
            var actual = suggestionService.GetSuggestionsAsync("").Result;
            Assert.AreNotEqual(0, actual.Count());
        }

    }
}
