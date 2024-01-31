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
    public class DefinitionRepositoryTests
    {
        public IDefinitionsRepository definitionRepository { get; set; }
        public Mock<AppDbContext> context;
        public static List<DefinitionDto> myList = (new List<DefinitionDto>());
        Guid myId = Guid.NewGuid();

        public DefinitionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new Mock<AppDbContext>(options);
            var mockDefinitionDto = new Mock<DbSet<DefinitionDto>>();
            mockDefinitionDto.Setup(x => x.AddAsync(It.IsAny<DefinitionDto>(), default)).Callback< DefinitionDto, CancellationToken>
                ((word, token) => myList.Add(word)).ReturnsAsync(() => null);
            mockDefinitionDto.Setup(x => x.Remove(It.IsAny<DefinitionDto>())).Callback< DefinitionDto>
                ((word) => myList.RemoveAll(x=>x.Id==myId)).Returns(()=>null).Verifiable(Times.Once);

            var myQueryable = myList.AsQueryable();
            mockDefinitionDto.As<IQueryable<DefinitionDto>>().Setup(m => m.Provider).Returns(myQueryable.Provider);
            mockDefinitionDto.As<IQueryable<DefinitionDto>>().Setup(m => m.Expression).Returns(myQueryable.Expression);
            mockDefinitionDto.As<IQueryable<DefinitionDto>>().Setup(m => m.ElementType).Returns(myQueryable.ElementType);
            mockDefinitionDto.As<IQueryable<DefinitionDto>>().Setup(m => m.GetEnumerator()).Returns(() => myQueryable.GetEnumerator());

            context.Setup(c => c.Definitions).Returns(mockDefinitionDto.Object);
            myList.Add(new DefinitionDto
            {
                Id = Guid.NewGuid()

            });
            this.definitionRepository = new DefinitionRepository(context.Object);
        }


        [TestMethod]
        public async Task GetDefinitionByPosIdAsync_DetailsFound_ReturnDetails
           ()
        {
            await definitionRepository.AddDefinitionAsync(new DefinitionDto { Id = myId });
            await definitionRepository.GetDefinitionByPosIdAsync(myId);
            Assert.IsNotNull(myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task GetDefinitionByPosIdAsync_detailNotFound_ReturnNull
            ()
        {
            await definitionRepository.GetDefinitionByPosIdAsync(Guid.NewGuid());
            Assert.AreEqual(0, myList.Count(x => x.Id == myId));

        }
        [TestMethod]
        public async Task DeleteDefinitionByIdAsync_DetailsFound_Delete
           ()
        {
            var myId = Guid.NewGuid();
            await definitionRepository.AddDefinitionAsync(new DefinitionDto { Id = myId });
            await definitionRepository.DeleteDefinitionByIdAsync(myId);
           
        }

        [TestMethod]
        public async Task AddDefinitionAsync_GetPronounciation_AddSuccesfully
            ()
        {
            await definitionRepository.AddDefinitionAsync(new DefinitionDto { Id = myId });
            context.Verify(x => x.Definitions.AddAsync(It.IsAny<DefinitionDto>(), It.IsAny<CancellationToken>()), Times.Once);
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(myList.Count(x => x.Id == myId));

        }

    }
}

