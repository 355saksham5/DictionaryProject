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
        public async Task GetSuggestionsAsync_QueryStringNull_ReturnEmptySuggestionSuggestions()
        {
            var actual = suggestionService.GetSuggestionsAsync(null).Result;
            Assert.AreEqual(0, actual.Count());
        }
        [TestMethod]
        public async Task GetSuggestionsAsync_SuggestionLessThanLimit_ReturnSuggestionsExact()
        {
            var numOfSuggestions = 4;
            var fakeList = new Suggestion[numOfSuggestions];
            suggestionApi.Setup(x => x.GetSuggestionsAsync(It.IsAny<String>())).ReturnsAsync(fakeList.ToList);
            var actual = suggestionService.GetSuggestionsAsync(It.IsAny<String>()).Result;
            Assert.AreEqual(fakeList.Count(), actual.Count());
        }
        [TestMethod]
        public async Task GetSuggestionsAsync_SuggestionListGreaterThanLimit_ReturnSuggestionsLimited()
        {
            var numOfSuggestions = 10;
            var fakeList = new Suggestion[numOfSuggestions];
            suggestionApi.Setup(x => x.GetSuggestionsAsync(It.IsAny<String>())).ReturnsAsync(fakeList.ToList);
            var actual = suggestionService.GetSuggestionsAsync(It.IsAny<String>()).Result;
            Assert.AreEqual(5, actual.Count());
        }

    }
}
