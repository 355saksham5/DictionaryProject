using DictionaryApi.Data;
using DictionaryApi.DataAccess.DbHandlers;
using DictionaryApi.DataAccess.DbHandlers.IDbHandlers;
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
        public class SynonymsRepositoryTests
        {
            public ISynonymsRepository synonymsRepository { get; set; }
            public Mock<AppDbContext> context;
            public static List<Synonyms> myList = (new List<Synonyms>());
            Guid myId = Guid.NewGuid();

            public SynonymsRepositoryTests()
            {
                var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
                context = new Mock<AppDbContext>(options);
                var mockSynonyms = new Mock<DbSet<Synonyms>>();
                mockSynonyms.Setup(x => x.AddAsync(It.IsAny<Synonyms>(), default)).Callback<Synonyms, CancellationToken>
                    ((synonym, token) => myList.Add(synonym)).ReturnsAsync(() => null).Verifiable(Times.Once);

                var myQueryable = myList.AsQueryable();
                mockSynonyms.As<IQueryable<Synonyms>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
                mockSynonyms.As<IQueryable<Synonyms>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
                mockSynonyms.As<IQueryable<Synonyms>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
                mockSynonyms.As<IQueryable<Synonyms>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

                context.Setup(c => c.Synonyms).Returns(mockSynonyms.Object);
                myList.Add(new Synonyms
                {
                    Id = Guid.NewGuid(),
                    BasicWordDetailsId = Guid.NewGuid(),
                    Synonym = new List<Words>
                {
                    new Words{Id = Guid.NewGuid(),Word = "ant1"},
                    new Words{Id = Guid.NewGuid(),Word = "ant2"}
                }
                });
                this.synonymsRepository = new SynonymsRepository(context.Object);
            }

            [TestMethod]
            public async Task GetSynonymsAsync_SynonymsFound_ReturnList
                ()
            {
                await synonymsRepository.AddSynonymsAsync(new Synonyms { Id = myId });
                var actual = await synonymsRepository.GetSynonymsAsync(myId);
                Assert.AreNotEqual(0, myList.Count(x => x.Id == myId));

            }
            [TestMethod]
            public async Task GetSynonymsAsync_SynonymsNotFound_ReturnNull
                ()
            {
                var actual = await synonymsRepository.GetSynonymsAsync(Guid.NewGuid());
                Assert.AreEqual(0, myList.Count(x => x.Id == myId));

            }

            [TestMethod]
            public async Task AddSynonymsAsync_WithoutAnyFormatErrorInAntonym_AddSuccesfully
                ()
            {
                await synonymsRepository.AddSynonymsAsync(new Synonyms { Id = myId });
                context.Verify(x => x.Synonyms.AddAsync(It.IsAny<Synonyms>(), It.IsAny<CancellationToken>()), Times.Once);
                context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
                Assert.IsNotNull(myList.Find(x => x.Id == myId));

            }

        }
    }
