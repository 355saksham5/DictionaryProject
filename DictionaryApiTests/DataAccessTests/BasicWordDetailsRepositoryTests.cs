using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApiTests.DataAccessTests
{
    [TestClass]
    public class BasicWordDetailsRepositoryTests
    {
        public IBasicWordDetailsRepository basicWordDetailsRepository { get; set; }
        public Mock<AppDbContext> context;
        public static List<BasicWordDetails> myList = (new List<BasicWordDetails>());
        Guid myId = Guid.NewGuid();

        public BasicWordDetailsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new Mock<AppDbContext>(options);
            var mockBasicWordDetails = new Mock<DbSet<BasicWordDetails>>();
            mockBasicWordDetails.Setup(x => x.AddAsync(It.IsAny<BasicWordDetails>(), default)).Callback<BasicWordDetails, CancellationToken>
                ((word, token) => myList.Add(word)).ReturnsAsync(() => null);

            var myQueryable = myList.AsQueryable();
            mockBasicWordDetails.As<IQueryable<BasicWordDetails>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
            mockBasicWordDetails.As<IQueryable<BasicWordDetails>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
            mockBasicWordDetails.As<IQueryable<BasicWordDetails>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
            mockBasicWordDetails.As<IQueryable<BasicWordDetails>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

            context.Setup(c => c.BasicWordDetails).Returns(mockBasicWordDetails.Object);
            myList.Add(new BasicWordDetails
            {
                Id = Guid.NewGuid(),
                Origin = "",
                DefaultPhoneticsText = "",
                IsPronounceLnkPresent = false,
                NumberOfDefinitions = 0

            });
            this.basicWordDetailsRepository = new BasicWordDetailRepository(context.Object);
        }

        [TestMethod]
        public async Task GetBasicWordDetailsAsync_DetailsFound_ReturnDetails
            ()
        {
            await basicWordDetailsRepository.AddDetailsAsync(new BasicWordDetails { Id = myId, Word = "myword" });
            await basicWordDetailsRepository.GetDetailsAsync("myword");
            Assert.IsNotNull(myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task GetBasicWordDetailsAsync_detailNotFound_ReturnNull
            ()
        {
            await basicWordDetailsRepository.GetDetailsAsync("word_unavailable");
            Assert.AreEqual(0, myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task GetBasicWordDetailsByIdAsync_DetailsFound_ReturnDetails
           ()
        {
            await basicWordDetailsRepository.AddDetailsAsync(new BasicWordDetails { Id = myId, Word = "myword" });
            await basicWordDetailsRepository.GetDetailsByIdAsync(myId);
            Assert.IsNotNull(myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task GetBasicWordDetailsByIdAsync_detailNotFound_ReturnNull
            ()
        {
            await basicWordDetailsRepository.GetDetailsByIdAsync(Guid.NewGuid());
            Assert.AreEqual(0, myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task DeleteDetailsByIdAsync_DetailsFound_Delete
           ()
        {
            await basicWordDetailsRepository.AddDetailsAsync(new BasicWordDetails { Id = myId, Word = "myword" });
            await basicWordDetailsRepository.DeleteDetailsByIdAsync(myId);
            Assert.AreEqual(0, myList.Count(x => x.Id == myId));

        }

        [TestMethod]
        public async Task AddDetailsAsync_GetWord_AddSuccesfully
            ()
        {
            await basicWordDetailsRepository.AddDetailsAsync(new BasicWordDetails { Id = myId, Word ="wordadded" });
            context.Verify(x => x.BasicWordDetails.AddAsync(It.IsAny<BasicWordDetails>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            //Assert.IsNotNull(myList.Find(x => x.Id == myId));

        }

    }
}
