using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
using DictionaryApi.Models.DTOs;
using DictionaryApiTests.Helper;
using DictionaryApiTests.Models;
using Hangfire.MemoryStorage.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DictionaryApiTests.DataAccessTests
{
    [TestClass]
    public class AntonymsRepositoryTests
    {
        public IAntonymsRepository antonymsRepository { get; set; }
        public Mock<AppDbContext> context;
        public static List<Antonyms> myList = (new List<Antonyms>());
        Guid myId = Guid.NewGuid();

        public AntonymsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new Mock<AppDbContext>(options);
            var mockAntonyms = new Mock<DbSet<Antonyms>>();
            mockAntonyms.Setup(x => x.AddAsync(It.IsAny<Antonyms>(), default)).Callback<Antonyms, CancellationToken>
                ((antonym,token) => myList.Add(antonym)).ReturnsAsync(()=>null).Verifiable(Times.Once);

            var myQueryable = myList.AsQueryable();
            mockAntonyms.As<IQueryable<Antonyms>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
            mockAntonyms.As<IQueryable<Antonyms>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
            mockAntonyms.As<IQueryable<Antonyms>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
            mockAntonyms.As<IQueryable<Antonyms>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

            context.Setup(c => c.Antonyms).Returns(mockAntonyms.Object);
            myList.Add(new Antonyms
            {
                Id = Guid.NewGuid(),
                BasicWordDetailsId = Guid.NewGuid(),
                Antonym = new List<Words>
                {
                    new Words{Id = Guid.NewGuid(),Word = "ant1"},
                    new Words{Id = Guid.NewGuid(),Word = "ant2"}
                }
            });
            this.antonymsRepository = new AntonymsRepository(context.Object);
        }

        [TestMethod]
        public async Task GetAntonymsAsync_AntonymsFound_ReturnList
            ()
        {
            await antonymsRepository.AddAntonymsAsync(new Antonyms { Id = myId });
            var actual = await antonymsRepository.GetAntonymsAsync(myId);
            Assert.AreNotEqual(0,myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task GetAntonymsAsync_AntonymsNotFound_ReturnNull
            ()
        { 
            var actual = await antonymsRepository.GetAntonymsAsync(Guid.NewGuid());
            Assert.AreEqual(0, myList.Count(x => x.Id == myId));

        }

        [TestMethod]
        public async Task AddAntonymsAsync_WithoutAnyFormatErrorInAntonym_AddSuccesfully
            ()
        {
            await antonymsRepository.AddAntonymsAsync(new Antonyms { Id = myId });
            context.Verify(x => x.Antonyms.AddAsync(It.IsAny<Antonyms>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(myList.Find(x => x.Id == myId));

        }

    }
}
