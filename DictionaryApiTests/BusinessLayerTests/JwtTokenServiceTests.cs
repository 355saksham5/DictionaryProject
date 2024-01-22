using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.Helpers;
using DictionaryApi.Models;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.BusinessLayerTests
{
    [TestClass]
    public class JwtTokenServiceTests
    {
        private readonly Mock<IOptions<JwtOptions>> options;
        private JwtTokenService jwtTokenService;
        public JwtTokenServiceTests()
        {
            options = new Mock<IOptions<JwtOptions>>();
            jwtTokenService = new JwtTokenService(options.Object);
        }

        [TestMethod]
        public async Task CreateTokenAsync_GetUserId_ReturnToken()
        {
           
        }

    }
}
