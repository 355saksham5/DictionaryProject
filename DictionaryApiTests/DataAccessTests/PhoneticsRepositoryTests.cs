using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.DataAccess.DbHandlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryApi.Models.DTOs;

namespace DictionaryApiTests.DataAccessTests
{
    [TestClass]
    public class PhoneticAudioRepositoryTests
    {
        public IPhoneticAudioRepository phoneticAudioRepository { get; set; }
        public Mock<AppDbContext> context;
        public static List<PhoneticDto> myList = (new List<PhoneticDto>());
        Guid myId = Guid.NewGuid();

        public PhoneticAudioRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new Mock<AppDbContext>(options);
            var mockPhoneticDto = new Mock<DbSet<PhoneticDto>>();
            mockPhoneticDto.Setup(x => x.AddAsync(It.IsAny<PhoneticDto>(), default)).Callback<PhoneticDto, CancellationToken>
                ((word, token) => myList.Add(word)).ReturnsAsync(() => null);
            mockPhoneticDto.Setup(x => x.Remove(It.IsAny<PhoneticDto>())).Callback<PhoneticDto>
                ((word) => myList.Remove(word)).Returns(() => null).Verifiable(Times.Once);

            var myQueryable = myList.AsQueryable();
            mockPhoneticDto.As<IQueryable<PhoneticDto>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
            mockPhoneticDto.As<IQueryable<PhoneticDto>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
            mockPhoneticDto.As<IQueryable<PhoneticDto>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
            mockPhoneticDto.As<IQueryable<PhoneticDto>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

            context.Setup(c => c.PhoneticAudios).Returns(mockPhoneticDto.Object);
            myList.Add(new PhoneticDto
            {
                Id = Guid.NewGuid()

            });
            this.phoneticAudioRepository = new PhoneticAudiosRepository(context.Object);
        }

        
        [TestMethod]
        public async Task GetPronounciationByWordIdAsync_DetailsFound_ReturnDetails
           ()
        {
            await phoneticAudioRepository.AddPronounciationAsync(new PhoneticDto { Id = myId });
            await phoneticAudioRepository.GetPronounciationByWordIdAsync(myId);
            Assert.IsNotNull(myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task GetPronounciationByWordIdAsync_detailNotFound_ReturnNull
            ()
        {
            await phoneticAudioRepository.GetPronounciationByWordIdAsync(Guid.NewGuid());
            Assert.AreEqual(0, myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task DeletePronounciationByIdAsync_DetailsFound_Delete
           ()
        {
            var myId = Guid.NewGuid();
            await phoneticAudioRepository.AddPronounciationAsync(new PhoneticDto { Id = myId });
            await phoneticAudioRepository.DeletePronounciationByIdAsync(myId);
        }

        [TestMethod]
        public async Task AddPronounciationAsync_GetPronounciation_AddSuccesfully
            ()
        {
            await phoneticAudioRepository.AddPronounciationAsync(new PhoneticDto { Id = myId });
            context.Verify(x => x.PhoneticAudios.AddAsync(It.IsAny<PhoneticDto>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(myList.Count(x => x.Id == myId));
        }

    }
}
