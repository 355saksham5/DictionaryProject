//using DictionaryApi.Data;
//using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
//using DictionaryApi.Models.DTOs;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DictionaryApiTests.DataAccessTests
//{
//    [TestClass]
//        public class DefinitionRepositoryTests
//        {
//            public IDefinitionsRepository definitionRepository { get; set; }
//            public Mock<AppDbContext> context;
//            public static List<PhoneticDto> myList = (new List<PhoneticDto>());
//            Guid myId = Guid.NewGuid();

//            public DefinitionRepositoryTests()
//            {
//                var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
//                context = new Mock<AppDbContext>(options);
//                var mockDefinitionDto = new Mock<DbSet<PhoneticDto>>();
//                mockDefinitionDto.Setup(x => x.AddAsync(It.IsAny<PhoneticDto>(), default)).Callback<PhoneticDto, CancellationToken>
//                    ((word, token) => myList.Add(word)).ReturnsAsync(() => null);
//                mockDefinitionDto.Setup(x => x.Remove(It.IsAny<PhoneticDto>())).Callback<PhoneticDto>
//                    ((word) => myList.Remove(word)).Returns(() => null);

//                var myQueryable = myList.AsQueryable();
//                mockDefinitionDto.As<IQueryable<PhoneticDto>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
//                mockDefinitionDto.As<IQueryable<PhoneticDto>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
//                mockDefinitionDto.As<IQueryable<PhoneticDto>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
//                mockDefinitionDto.As<IQueryable<PhoneticDto>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

//                context.Setup(c => c.PhoneticAudios).Returns(mockDefinitionDto.Object);
//                myList.Add(new PhoneticDto
//                {
//                    Id = Guid.NewGuid()

//                });
//                this.phoneticAudioRepository = new PhoneticAudiosRepository(context.Object);
//            }


//            [TestMethod]
//            public async Task GetPronounciationByWordIdAsync_DetailsFound_ReturnDetails
//               ()
//            {
//                await phoneticAudioRepository.AddPronounciationAsync(new PhoneticDto { Id = myId });
//                await phoneticAudioRepository.GetPronounciationByWordIdAsync(myId);
//                Assert.IsNotNull(myList.Count(x => x.Id == myId));

//            }
//            [TestMethod]
//            public async Task GetPronounciationByWordIdAsync_detailNotFound_ReturnNull
//                ()
//            {
//                await phoneticAudioRepository.GetPronounciationByWordIdAsync(Guid.NewGuid());
//                Assert.AreEqual(0, myList.Count(x => x.Id == myId));

//            }
//            [TestMethod]
//            public async Task DeletePronounciationByIdAsync_DetailsFound_Delete
//               ()
//            {
//                var myId = Guid.NewGuid();
//                await phoneticAudioRepository.AddPronounciationAsync(new PhoneticDto { Id = myId });
//                await phoneticAudioRepository.DeletePronounciationByIdAsync(myId);
//                Assert.AreEqual(0, myList.Count(x => x.Id == myId));

//            }

//            [TestMethod]
//            public async Task AddPronounciationAsync_GetPronounciation_AddSuccesfully
//                ()
//            {
//                await phoneticAudioRepository.AddPronounciationAsync(new PhoneticDto { Id = myId });
//                context.Verify(x => x.PhoneticAudios.AddAsync(It.IsAny<PhoneticDto>(), It.IsAny<CancellationToken>()), Times.Once);
//                context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

//            }

//        }
//    }

//}
