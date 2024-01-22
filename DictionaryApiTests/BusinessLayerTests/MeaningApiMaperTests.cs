using DictionaryApi.BusinessLayer.Services;
using DictionaryApi.BusinessLayer.Services.IServices;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.BusinessLayerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MeaningApiMaperTests
    {
        private IMeaningApiMapper meaningApiMapper;
        private Mock<IBasicWordDetailsRepository> basicWordDetailsRepo;
        private Mock<IDefinitionsRepository> definitionsRepo;
        private Mock<IPhoneticAudioRepository> phoneticAudiosRepo;
        private Mock<IAntonymsRepository> antonymsRepo;
        private Mock<ISynonymsRepository> synonymsRepo;
        private Mock<BasicWordDetails> basicWordDetails;

        public MeaningApiMaperTests()
        {
            basicWordDetailsRepo = new Mock<IBasicWordDetailsRepository> ();
            definitionsRepo = new Mock<IDefinitionsRepository> ();
            phoneticAudiosRepo = new Mock<IPhoneticAudioRepository> ();
            antonymsRepo = new Mock<IAntonymsRepository> ();
            synonymsRepo = new Mock<ISynonymsRepository>();
            basicWordDetails = new Mock<BasicWordDetails> ();
            meaningApiMapper = new MeaningApiMapper(basicWordDetailsRepo.Object, phoneticAudiosRepo.Object,
                 definitionsRepo.Object, antonymsRepo.Object, synonymsRepo.Object);
        }

        [TestMethod]
        public async Task MapBasicWordDetailsAsync_()
        {

        }
    }
}
